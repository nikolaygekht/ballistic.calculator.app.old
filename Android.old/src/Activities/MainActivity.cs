using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using MathEx.ExternalBallistic.Serialization;
using MathEx.ExternalBallistic;
using BallisticCalculator.Serialization;
using BallisticCalculator.Utils;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Ballistic Calculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            SerializerInstance.Init();

            Button selectTraceButton = FindViewById<Button>(Resource.Id.buttonSelectTrace);
            selectTraceButton.Click += selectTraceButton_Click;
            Button ballisticTableButton = FindViewById<Button>(Resource.Id.buttonBallisticTable);
            ballisticTableButton.Click += ballisticTableButton_Click;
            Button settingsButton = FindViewById<Button>(Resource.Id.buttonSettings);
            settingsButton.Click += settingsButton_Click;
            Button editWindButton = FindViewById<Button>(Resource.Id.buttonEditWind);
            editWindButton.Click += editWindButton_Click;
            Button editAtmosphereButton = FindViewById<Button>(Resource.Id.buttonEditAtmosphere);
            editAtmosphereButton.Click += editAtmosphereButton_Click;
            Button singleShotButton = FindViewById<Button>(Resource.Id.buttonSingleShot);
            singleShotButton.Click += singleShotButton_Click;
            Button communicateDesktopButton = FindViewById<Button>(Resource.Id.buttonCommunicateDesktop);
            communicateDesktopButton.Click += communicateDesktopButton_Click;
        }

        private bool exit = false;

        public override void OnBackPressed()
        {
            if (exit)
            {
                Finish();
            }
            else
            {
                Toast.MakeText(this, Resource.String.msg_PressBackToExit, ToastLength.Short).Show();
                exit = true;
                new System.Threading.Timer((object state) => { exit = false; }, null, TimeSpan.FromSeconds(3), TimeSpan.FromMilliseconds(-1));
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            ISharedPreferencesEditor editor = GetPreferences(FileCreationMode.Private).Edit();
            if (ApplicationData.Instance.SelectedTraceInfo != null)
                editor.PutString("selected_trace_name", ApplicationData.Instance.SelectedTraceInfo.TraceName);
            editor.Commit();
        }

        protected override void OnResume()
        {
            base.OnResume();

            ISharedPreferences preferences = GetPreferences(FileCreationMode.Private);
            if (ApplicationData.Instance.TraceInfoCollection != null && ApplicationData.Instance.SelectedTraceInfo == null)
            {

                string traceName = preferences.GetString("selected_trace_name", String.Empty);
                if (traceName.Length > 0)
                {
                    int traceIndex = ApplicationData.Instance.TraceInfoCollection.Find(traceName);
                    if (traceIndex != -1)
                    {
                        ApplicationData.Instance.SelectedTraceInfo = ApplicationData.Instance.TraceInfoCollection[traceIndex];
                    }
                }
            }
            if (ApplicationData.Instance.SelectedTraceInfo != null)
                Title = String.Format("{0} ({1})", Resources.GetString(Resource.String.lbl_MainActivity), ApplicationData.Instance.SelectedTraceInfo.TraceName);
            else
                Title = Resources.GetString(Resource.String.lbl_MainActivity);
        }

        protected override void OnStop()
        {
            base.OnStop();

            ApplicationData.Instance.SaveTracesToFile();
        }

        protected override void OnStart()
        {
            base.OnStart();

            ApplicationData.Instance.LoadTracesFromFile();

            if (ApplicationData.Instance.SelectedTraceInfo != null)
                Title = String.Format("{0} ({1})", Resources.GetString(Resource.String.lbl_MainActivity), ApplicationData.Instance.SelectedTraceInfo.TraceName);
            else
                Title = Resources.GetString(Resource.String.lbl_MainActivity);

            ISharedPreferences preferences = GetPreferences(FileCreationMode.Private);
            ApplicationData.Instance.PrefferedUnits = (ApplicationData.EPrefferedUnits)preferences.GetInt("preffered_units", (int)ApplicationData.Instance.PrefferedUnits);
        }

        void singleShotButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SingleShotActivity));
        }

        void editAtmosphereButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditAtmosphereActivity));
        }

        void editWindButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditWindActivity));
        }

        void settingsButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SettingsActivity));
        }

        void selectTraceButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(TracesActivity));
        }

        void ballisticTableButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(BallisticTableActivity));
        }

        void communicateDesktopButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CommunicateDesktopActivity));
        }
    }
}


