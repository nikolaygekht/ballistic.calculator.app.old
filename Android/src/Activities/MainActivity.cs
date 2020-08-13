using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gehtsoft.BallisticCalculator.Model;
using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Utils;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Ballistic Calculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string TRACE_NAME_PROPERTY = "TraceName";
        private string _applicationName;
        private BallisticDataProvider _dataProvider;
        private string _updateServerAddress;
        private int _updateServerPort;
        private bool _isUpdateSupported;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            _dataProvider = BallisticDataProvider.Instance;
            _dataProvider.Init();
            _applicationName = Resources.GetString(Resource.String.ApplicationName);
            createConstrols();
        }

        private void createConstrols()
        {
            Button buttonSelectTrace = FindViewById<Button>(Resource.Id.btnSelectTrace);
            Button buttonSetAtmo = FindViewById<Button>(Resource.Id.btnSetAtmo);
            Button buttonSetWind = FindViewById<Button>(Resource.Id.btnSetWind);
            Button buttonBallisticTable = FindViewById<Button>(Resource.Id.btnBallisticTable);
            Button buttonSingleShot = FindViewById<Button>(Resource.Id.btnSingleShot);
            Button buttonCommunicateDesktop = FindViewById<Button>(Resource.Id.btnCommunicateDesktop);
            Button buttonSettings = FindViewById<Button>(Resource.Id.btnSettings);

            buttonSelectTrace.Click += OnSelectTraceBtnTouched;
            buttonSetAtmo.Click += OnSetAtmoBtnTouched;
            buttonSetWind.Click += OnSetWindBtnTouched;
            buttonBallisticTable.Click += OnBallisticTableBtnTouched;
            buttonSingleShot.Click += OnSingleShotBtnTouched;
            buttonCommunicateDesktop.Click += OnCommunicateDesktopBtnTouched;
            buttonSettings.Click += OnSettingsBtnTouched;
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            if (_isUpdateSupported)
                menu.Add(Menu.None, 0, Menu.None, "Update");
            menu.Add(Menu.None, 1, Menu.None, "About");

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            PackageManager manager = this.PackageManager;
            PackageInfo info = manager.GetPackageInfo(this.PackageName, 0);

            switch (item.ItemId)
            {
                case 0:
                    new ApkUpdater(info.VersionCode, this, _updateServerAddress, _updateServerPort).Execute();
                    return true;
                case 1:
                    string version = "Ballistic Calculator\n" +
                                     "Version " + info.VersionName + " (beta)\n" + 
                                     "Build " + info.VersionCode + "\n";  

                    new AlertDialog.Builder(this)
                   .SetPositiveButton("Yes", (sender, args) =>
                    {
                    // User pressed yes
                    })
                   .SetMessage(version)
                   .SetTitle("About")
                   .Show();

                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            ISharedPreferences sharedPrefs = GetPreferences(FileCreationMode.Private);

            string measurmentSystem = sharedPrefs.GetString("MeasurmentSystem", null);
            if (!string.IsNullOrEmpty(measurmentSystem))
            {
                _dataProvider.MeasurementSystem = (measurmentSystem == "Imperial") ?
                    MeasurementSystem.Imperial : MeasurementSystem.Metric;
            }

            float shotStep = sharedPrefs.GetFloat("ShotStep", float.PositiveInfinity);
            string shotStepUnit = sharedPrefs.GetString("ShotStepUnit", null);

            if (!float.IsPositiveInfinity(shotStep) && !string.IsNullOrEmpty(shotStepUnit))
            {
                _dataProvider.ShotData.StepForBallisticTable =
                    new Distance(shotStep,
                    Distance.NameToUnit(shotStepUnit)
                );
            }

            float shotSingleShotStep = sharedPrefs.GetFloat("ShotStepSingleShot", float.PositiveInfinity);
            string shotStepSingleShotUnit = sharedPrefs.GetString("ShotStepSingleShotUnit", null);

            if (!float.IsPositiveInfinity(shotSingleShotStep) && !string.IsNullOrEmpty(shotStepSingleShotUnit))
            {
                _dataProvider.ShotData.StepForSingleShot =
                    new Distance(shotSingleShotStep,
                    Distance.NameToUnit(shotStepSingleShotUnit)
                );
            }

            if (_dataProvider.GetSelectedTraceName() == "")
            {
                string name = sharedPrefs.GetString(TRACE_NAME_PROPERTY, null);
                _dataProvider.SetSelectedTraceByName(name);
            }
            var selTraceName = _dataProvider.GetSelectedTraceNameOrDefault("");
            if (selTraceName != "")
                Title = _applicationName + " - " + selTraceName;
            else
                Title = _applicationName;
        }

        protected override void OnStart()
        {
            base.OnStart();
            Bootstrap.Instance.Init();
            var selTraceName = _dataProvider.GetSelectedTraceNameOrDefault("");
            if (selTraceName != "")
                Title = _applicationName + " - " + selTraceName;
            else
                Title = _applicationName;
        }

        protected override void OnStop()
        {
            base.OnStop();
            Bootstrap.Instance.Shutdown();
        }

        void OnSelectTraceBtnTouched(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(TracesActivity));
            StartActivityForResult(intent, 2);
        }

        void OnSetAtmoBtnTouched(object sender, EventArgs e)
        {
            StartActivity(typeof(EditAtmosphereActivity));
        }

        void OnSetWindBtnTouched(object sender, EventArgs e)
        {
            StartActivity(typeof(EditWindActivity));
        }

        void OnBallisticTableBtnTouched(object sender, EventArgs e)
        {
            StartActivity(typeof(BallisticTableActivity));
        }

        void OnSingleShotBtnTouched(object sender, EventArgs e)
        {
            StartActivity(typeof(SingleShotActivity));
        }

        void OnCommunicateDesktopBtnTouched(object sender, EventArgs e)
        {
            StartActivity(typeof(CommunicateDesktopActivity));
        }

        void OnSettingsBtnTouched(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivityForResult(intent, 1);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1 && resultCode == Result.Ok)
            {
                ISharedPreferences sharedPrefs = GetPreferences(FileCreationMode.Private);
                ISharedPreferencesEditor prefsEditor = sharedPrefs.Edit();
                prefsEditor.PutString("MeasurmentSystem", _dataProvider.MeasurementSystem.ToString());
                prefsEditor.PutFloat("ShotStep", (float)_dataProvider.ShotData.StepForBallisticTable.Get(_dataProvider.ShotData.StepForBallisticTable.SetUnit));
                prefsEditor.PutString("ShotStepUnit", Distance.UnitToName(_dataProvider.ShotData.StepForBallisticTable.SetUnit));

                prefsEditor.PutFloat("ShotStepSingleShot", (float)_dataProvider.ShotData.StepForSingleShot.Get(_dataProvider.ShotData.StepForSingleShot.SetUnit));
                prefsEditor.PutString("ShotStepSingleShotUnit", Distance.UnitToName(_dataProvider.ShotData.StepForSingleShot.SetUnit));

                prefsEditor.Apply();
            }

            if (requestCode == 2 && resultCode == Result.Ok)
            {
                ISharedPreferences sharedPrefs = GetPreferences(FileCreationMode.Private);
                ISharedPreferencesEditor prefsEditor = sharedPrefs.Edit();
                prefsEditor.PutString(TRACE_NAME_PROPERTY, _dataProvider.GetSelectedTraceName());
                prefsEditor.Apply();
            }
        }
    }
}


