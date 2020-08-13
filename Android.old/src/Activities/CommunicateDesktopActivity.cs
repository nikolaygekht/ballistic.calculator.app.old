using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Gehtsoft.BallisticCalculator.Connectivity;
using BallisticCalculator.Utils;
using System.Threading.Tasks;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Communicate Desktop")]
    public class CommunicateDesktopActivity : Activity
    {
        EditText editTextAddress;
        EditText editTextPort;

        RadioButton radioButtonJoinTraces;
        RadioButton radioButtonReplaceTraces;

        Button buttonCommunicate;
        Button buttonBack;

        bool replaceTraces = true;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CommunicateDesktop);

            editTextAddress = FindViewById<EditText>(Resource.Id.editTextDesktopAddress);
            editTextPort = FindViewById<EditText>(Resource.Id.editTextDesktopPort);

            radioButtonJoinTraces = FindViewById<RadioButton>(Resource.Id.radioButtonJoinTraces);
            radioButtonReplaceTraces = FindViewById<RadioButton>(Resource.Id.radioButtonReplaceTraces);

            buttonCommunicate = FindViewById<Button>(Resource.Id.buttonCommunicate);
            buttonBack = FindViewById<Button>(Resource.Id.buttonBack);

            buttonCommunicate.Click += buttonCommunicate_Click;
            buttonBack.Click += buttonBack_Click;
            radioButtonJoinTraces.Click += radioButtonJoinTraces_Click;
            radioButtonReplaceTraces.Click += radioButtonReplaceTraces_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();

            ISharedPreferencesEditor editor = GetPreferences(FileCreationMode.Private).Edit();
            editor.PutString("edittext_ip", editTextAddress.Text);
            editor.PutString("edittext_port", editTextPort.Text);
            editor.PutBoolean("replace_traces", replaceTraces);
            editor.Commit();
        }

        protected override void OnResume()
        {
            base.OnResume();

            ISharedPreferences preferences = GetPreferences(FileCreationMode.Private);
            editTextAddress.Text = preferences.GetString("edittext_ip", String.Empty);
            editTextPort.Text = preferences.GetString("edittext_port", String.Empty);
            replaceTraces = preferences.GetBoolean("replace_traces", true);

            if (replaceTraces)
                radioButtonReplaceTraces.PerformClick();
            else
                radioButtonJoinTraces.PerformClick();
        }

        void radioButtonReplaceTraces_Click(object sender, EventArgs e)
        {
            replaceTraces = true;
        }

        void radioButtonJoinTraces_Click(object sender, EventArgs e)
        {
            replaceTraces = false;
        }

        void buttonBack_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            Finish();
        }

        void buttonCommunicate_Click(object sender, EventArgs e)
        {
            Uri hostUri;
            string hostStringUri = string.Format("http://{0}:{1}/traces", editTextAddress.Text, editTextPort.Text);
            if (editTextPort.Text.Length > 0 && editTextAddress.Text.Length > 0 && Uri.TryCreate(hostStringUri, UriKind.Absolute, out hostUri))
            {
                Utilities.SetButtonState(buttonCommunicate, false);
                requestTracesFromHost(hostUri).ContinueWith(OnRequestTracesFromHostFailed, TaskContinuationOptions.OnlyOnFaulted);
            }
            else
            {
                Toast.MakeText(this, Resource.String.msg_InvalidUri, ToastLength.Short).Show();
            }
        }

        private void onTracesLoaded(TraceInfo[] traces)
        {
            if (traces != null)
            {
                if (replaceTraces)
                    ApplicationData.Instance.TraceInfoCollection.Clear();
                foreach (TraceInfo info in traces)
                    ApplicationData.Instance.TraceInfoCollection.Add(info);

                ApplicationData.Instance.SaveTracesToFile();

                if (ApplicationData.Instance.SelectedTraceInfo != null)
                    if (ApplicationData.Instance.TraceInfoCollection.Find(ApplicationData.Instance.SelectedTraceInfo.TraceName) == -1)
                        ApplicationData.Instance.SelectedTraceInfo = null;

                RunOnUiThread(() =>
                {
                    Utilities.SetButtonState(buttonCommunicate, true);
                    Toast.MakeText(this, string.Format(Resources.GetString(Resource.String.msg_LoadedNTraces), traces.Length), ToastLength.Long).Show();
                });
            }
            else
            {
                RunOnUiThread(() =>
                {
                    Utilities.SetButtonState(buttonCommunicate, true);
                    Toast.MakeText(this, Resource.String.msg_LoadTracesFail1, ToastLength.Long).Show();
                });
            }
        }

        private async Task requestTracesFromHost(Uri hostUri)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                HttpResponseMessage hostResponse = await httpClient.GetAsync(hostUri, HttpCompletionOption.ResponseContentRead, cancellationTokenSource.Token);
                string respXml = await hostResponse.Content.ReadAsStringAsync();
                TraceInfo[] traces = TraceInfoController.Serialization.XmlToArray(respXml);
                onTracesLoaded(traces);
            }
            catch (Exception ex)
            {
                if (ex is System.OperationCanceledException || ex is System.Xml.XmlException)
                {
                    onTracesLoaded(null);
                    return;
                }
                else
                    throw ex;
            }
        }

        private void OnRequestTracesFromHostFailed(Task task)
        {
            Exception ex = task.Exception;
            RunOnUiThread(() =>
            {
                Utilities.SetButtonState(buttonCommunicate, true);
                Toast.MakeText(this, string.Format("{0}\n{1}", Resources.GetString(Resource.String.msg_LoadTracesFail2), /*ex.Message*/ex.ToString()), ToastLength.Long).Show();
            });
        }
    }
}