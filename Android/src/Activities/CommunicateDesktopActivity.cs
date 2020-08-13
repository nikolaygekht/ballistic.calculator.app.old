using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using Gehtsoft.BallisticCalculator.Connectivity;
using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Model;
using Gehtsoft.BallisticCalculator.Utils;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.Views;
using Android.Views.InputMethods;
using Gehtsoft.BallisticCalculator.View;
using KeyboardType = Android.Content.Res.KeyboardType;


namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Communicate Desktop")]
    public class CommunicateDesktopActivity : Activity, IServiceFinderDelegate
    {
        private const string COMDESK_ADDRESS = "CommunicateDesctopAddress";
        private const string COMDESK_POSRT = "CommunicateDesctopPort";
        private const string COMDESK_IS_REPLACE_TRACES = "CommunicateDesctopIsReplaceTraces";
        private const string SERVICE_NAME = "BallisticCalculatorService";

        private InetAddresEditText _editDesktopAddress;
        private EditText _editDesktopPort;
        private RadioButton _radioButtonReplaceTraces;
        private RadioButton _radioButtonJoinTraces;
        private Button _buttonCommunicate;
        private Button _buttonCancel;
        private CancellationTokenSource _cancelTaskToken;
        private bool isSaveAllData = true;
        private ServiceFinder _serviceFinder;
        

        private BallisticDataProvider _dataProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CommunicateDesktop);

            CreateConstrols();

            _dataProvider = BallisticDataProvider.Instance;
            _cancelTaskToken = new CancellationTokenSource();

            LoadData();

            ShowSoftKeyboard(_editDesktopAddress, false);
            
            _serviceFinder = new ServiceFinder(SERVICE_NAME);
            _serviceFinder.Delegate = this;
            _serviceFinder.start();
        }

        private void ShowSoftKeyboard(Android.Views.View input, bool selectText)
        {
            if (selectText) ((EditText)input).SelectAll();
            ThreadPool.QueueUserWorkItem(s =>
            {
                Thread.Sleep(100); // For some reason, a short delay is required here.
                RunOnUiThread(() => ((InputMethodManager)GetSystemService(InputMethodService)).ShowSoftInput(input, ShowFlags.Implicit));
            });
        }

        private void CreateConstrols()
        {
            _editDesktopAddress = FindViewById<InetAddresEditText>(Resource.Id.editTextDesktopAddress);
            _editDesktopPort = FindViewById<EditText>(Resource.Id.editTextDesktopPort);
            _radioButtonJoinTraces = FindViewById<RadioButton>(Resource.Id.radioButtonJoinTraces);
            _radioButtonReplaceTraces = FindViewById<RadioButton>(Resource.Id.radioButtonReplaceTraces);
            _buttonCommunicate = FindViewById<Button>(Resource.Id.buttonCommunicate);
            _buttonCancel = FindViewById<Button>(Resource.Id.buttonCancel);

            _radioButtonReplaceTraces.Checked = true;

            _radioButtonJoinTraces.Click += radioButtonJoinTraces_Click;
            _radioButtonReplaceTraces.Click += radioButtonReplaceTraces_Click;
            _buttonCommunicate.Click += buttonCommunicate_Click;
            _buttonCancel.Click += buttonCancel_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _serviceFinder?.stop();
            SaveData();
        }

        protected override void OnResume()
        {
            base.OnResume();
            LoadData();
        }

        private void radioButtonJoinTraces_Click(object sender, EventArgs e)
        {
        }

        private void radioButtonReplaceTraces_Click(object sender, EventArgs e)
        {
        }

        private async void buttonCommunicate_Click(object sender, EventArgs e)
        {
            Uri desktopUri;
            string address = _editDesktopAddress.Text;
            string port = _editDesktopPort.Text;
            bool isAddrValid = Utilities.ValidateInetAddress(address);
            bool isPortValid = Utilities.ValidateInetPort(port);
            _cancelTaskToken = new CancellationTokenSource();
            if (!isAddrValid || !isPortValid)
            {
                ShowMessage(Resource.String.msg_InvalidUri);
                return;
            }

            address = Utilities.RemoveLeadingZerosInOctets(address);

            string desktopUriStr = string.Format("http://{0}:{1}/traces", address, port);
            bool isCreated = Uri.TryCreate(desktopUriStr, UriKind.Absolute, out desktopUri);
            if (!isCreated)
            {
                ShowMessage(Resource.String.msg_InvalidUri);
                return;
            }
            _buttonCommunicate.Enabled = false;
            CreateTask(desktopUri).ContinueWith(OnRequestFailed, TaskContinuationOptions.OnlyOnFaulted);
        }

        public override void OnBackPressed()
        {
            _cancelTaskToken?.Cancel();
            SaveData();
            _serviceFinder?.stop();
            Finish();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _cancelTaskToken.Cancel();
            isSaveAllData = false;
            _serviceFinder?.stop();
            Finish();
        }

        private async Task CreateTask(Uri hostUri)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                HttpResponseMessage hostResponse = await httpClient.GetAsync(
                                                            hostUri, 
                                                            HttpCompletionOption.ResponseContentRead, 
                                                            _cancelTaskToken.Token
                                                            );
                string respXml = await hostResponse.Content.ReadAsStringAsync();
                TraceInfo[] traces = TraceInfoController.Serialization.XmlToArray(respXml);
                OnTracesLoadFinished(traces);
            }
            catch (Exception e)
            {
                if (e is System.OperationCanceledException || e is System.Xml.XmlException)
                {
                    OnTracesLoadFinished(null);
                    return;
                }
                else
                    throw e;
            }
        }

        private void OnRequestFailed(Task task)
        {
            Utilities.RunOnMainThread(() =>
            {
                ShowMessage(
                    Resources.GetString(Resource.String.msg_ServerIsNotFound)
                    );

                _buttonCommunicate.Enabled = true;
            });
        }

        private void OnTracesLoadFinished(TraceInfo[] traces)
        {
            SaveData();

            _cancelTaskToken.Cancel();
            if (traces == null)
            {
                Utilities.RunOnMainThread(() =>
                {
                    ShowMessage(Resource.String.msg_ServerIsNotFound);
                    _buttonCommunicate.Enabled = true;
                });
                return;
            }

            var traceData = _dataProvider.TraceData;

            if (traceData.TraceInfoCollection == null)
                traceData.TraceInfoCollection = new TraceInfoCollection();

            if (_radioButtonReplaceTraces.Checked == true)
            {
                traceData.TraceInfoCollection.Clear();
                traceData.SelectedTraceInfo = null;
                _dataProvider.SaveTraces();
            }

            foreach (TraceInfo info in traces)
            {
                int pos = traceData.TraceInfoCollection.Find(info.TraceName);
                if (pos != -1)
                    traceData.TraceInfoCollection.RemoveAt(pos);
                traceData.TraceInfoCollection.Add(info);
            }

            _dataProvider.SaveTraces();

            Utilities.RunOnMainThread(() =>
            {
                ShowMessage(string.Format(
                    Resources.GetString(Resource.String.msg_LoadedNTraces),
                    traces.Length
                    ));
                _buttonCommunicate.Enabled = true;
            });

            _cancelTaskToken = null;

            OnBackPressed();
        }

        private void ShowMessage(int msgid)
        {
            Toast.MakeText(this, msgid, ToastLength.Long).Show();
        }

        private void ShowMessage(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Long).Show();
        }

        private void SaveData()
        {
            if (!isSaveAllData) return;

            ISharedPreferences sharedPrefs = GetPreferences(FileCreationMode.Private);
            ISharedPreferencesEditor prefsEditor = sharedPrefs.Edit();

            bool isAddrOk = Utilities.ValidateInetAddress(_editDesktopAddress.Text);
            bool isPortOk = Utilities.ValidateInetPort(_editDesktopPort.Text);

            prefsEditor.PutString(COMDESK_ADDRESS, _editDesktopAddress.Text);
            prefsEditor.PutString(COMDESK_POSRT, _editDesktopPort.Text);
          
            prefsEditor.PutBoolean(COMDESK_IS_REPLACE_TRACES, _radioButtonReplaceTraces.Checked);
            prefsEditor.Commit();
        }

        private void LoadData()
        {
            ISharedPreferences sharedPrefs = GetPreferences(FileCreationMode.Private);
            string name = sharedPrefs.GetString(_dataProvider.GetSelectedTraceName(), "");

            string addr = null;

            if (Resources.Configuration.Keyboard != KeyboardType.Nokeys)
            {

                addr = sharedPrefs.GetString(COMDESK_ADDRESS, "192.168.000.000");
                /*if (!Utilities.ValidateInetAddress(addr))
                    addr = "192.168.000.000";*/
           }
            else
            {
                addr = sharedPrefs.GetString(COMDESK_ADDRESS, "192.168.");
                /*if (!Utilities.ValidateInetAddress(addr))
                    addr = "192.168.";*/
            }

            if (string.IsNullOrEmpty(addr))
                addr = "192.168.";

            _editDesktopAddress.Text = addr;
            if (_editDesktopAddress.Text.EndsWith("192.168."))
                _editDesktopAddress.SetSelection(8);
            
            string port = sharedPrefs.GetString(COMDESK_POSRT, "9000");
            if (!Utilities.ValidateInetPort(port))
                port = "9000";

            _editDesktopPort.Text = port;

            bool isReplaceTraces = sharedPrefs.GetBoolean(COMDESK_IS_REPLACE_TRACES, true);

            if (isReplaceTraces)
                _radioButtonReplaceTraces.Checked = true;
            else
                _radioButtonJoinTraces.Checked = true;
        }

        public void serviceFound(string serviceName, string serviceAddress, string servicePort)
        {
            _serviceFinder?.stop();

            Utilities.RunOnMainThread(() =>
            {
                if (_editDesktopAddress.Text == serviceAddress && _editDesktopPort.Text == servicePort)
                {
                    return;
                }

                new AlertDialog.Builder(this)
                    .SetPositiveButton("YES", (sender, e) =>
                    {
                        _editDesktopAddress.Text = serviceAddress;
                        _editDesktopPort.Text = servicePort;
                    })
                    .SetNegativeButton("NO", (sender, e) => { })
                    .SetMessage("IP address " + serviceAddress + ":" + servicePort + " for desktop server of Ballistic Calculator is found.\n" +
                                "Should it be used for communication?")
                    .Show();

            });
        }
    }
}