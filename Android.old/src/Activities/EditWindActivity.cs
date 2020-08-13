using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;

using MathEx.ExternalBallistic.Units;
using BallisticCalculator.Utils;
using BallisticCalculator.Views;
using MathEx.ExternalBallistic;
using System.Globalization;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Edit Wind")]
    public class EditWindActivity : EditActivityBase, ISensorEventListener
    {
        EditTextEx editWindSpeed;
        Button buttonWindSpeedUnits;

        EditTextEx editWindAngle;
        Button buttonWindAngleUnits;

        Button buttonBeaufortWindScale;
        Button buttonUseSensors;

        AnglePicker anglePickerWindDirection;

        SensorManager sensorManager;
        Sensor magnitometerSensor;
        Sensor accelerometerSensor;

        double azimuthAngle = 0;
        IList<float> magnitometerData = null;
        IList<float> accelerometerData = null;

        bool magnitometerDataUpdated = false;
        bool accelerometerDataUpdated = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EditWind);

            InitControls();
            InitUnits();
            InitSensorManager();

            fillDataFromWindInfo(ApplicationData.Instance.WindInfo);
            buttonBeaufortWindScale.Click += buttonBeaufortWindScale_Click;
            buttonUseSensors.Click += buttonUseSensors_Click;
            anglePickerWindDirection.AngleChanged += anglePickerWindDirection_AngleChanged;
            editWindAngle.TextChanged += editWindAngle_TextChanged;

            Init();
        }

        void editWindAngle_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            double angle;
            Utilities.TryParseDouble(e.Text.ToString(), out angle);
            anglePickerWindDirection.AngleChanged -= anglePickerWindDirection_AngleChanged;
            anglePickerWindDirection.Angle = (int)(new Angle(angle, Angle.NameToUnit(buttonWindAngleUnits.Text)).Get(Angle.Unit.Degree));
            anglePickerWindDirection.AngleChanged += anglePickerWindDirection_AngleChanged;
        }

        void anglePickerWindDirection_AngleChanged(object sender, AnglePicker.AngleEventArgs e)
        {
            editWindAngle.UnitsAdapter.Set(new Angle(e.Angle, Angle.Unit.Degree).Get(Angle.NameToUnit(buttonWindAngleUnits.Text)), buttonWindAngleUnits.Text);
            editWindAngle.Text = Utilities.RoundDouble(editWindAngle.UnitsAdapter.CurrentValue(), editWindAngle.UnitsAdapter.DefaultDisplayPrecision()).ToString();
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (sensorManager != null)
            {
                sensorManager.UnregisterListener(this, magnitometerSensor);
                sensorManager.UnregisterListener(this, accelerometerSensor);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (sensorManager != null)
            {
                sensorManager.RegisterListener(this, magnitometerSensor, SensorDelay.Game);
                sensorManager.RegisterListener(this, accelerometerSensor, SensorDelay.Game);
            }
        }

        void buttonUseSensors_Click(object sender, EventArgs e)
        {
            double windDirection = 0;
            // Get target direction angle
            AlertDialog.Builder targetAlertDialogBuilder = new AlertDialog.Builder(this);
            targetAlertDialogBuilder.SetTitle(Resource.String.edit_hint_WindDirection);
            targetAlertDialogBuilder.SetMessage(Resource.String.msg_TowardTarget);
            targetAlertDialogBuilder.SetPositiveButton(Resource.String.btn_lbl_Continue, (EventHandler<DialogClickEventArgs>)((s, ea) =>
            {
                windDirection -= azimuthAngle;
                // Get wind direction angle
                AlertDialog.Builder windAlertDialogBuilder = new AlertDialog.Builder(this);
                windAlertDialogBuilder.SetTitle(Resource.String.edit_hint_WindDirection);
                windAlertDialogBuilder.SetMessage(Resource.String.msg_AgainstWind);
                windAlertDialogBuilder.SetPositiveButton(Resource.String.btn_lbl_Continue, (EventHandler<DialogClickEventArgs>)((ds, dea) =>
                {
                    windDirection += azimuthAngle;
                    if (windDirection > 180)
                        windDirection -= 360;
                    else if (windDirection < -180)
                        windDirection += 360;
                    editWindAngle.UnitsAdapter.Set(new Angle(windDirection, Angle.Unit.Degree).Get(Angle.NameToUnit(buttonWindAngleUnits.Text)), buttonWindAngleUnits.Text);
                    anglePickerWindDirection.Angle = (int)windDirection;
                }));
                windAlertDialogBuilder.SetNegativeButton(Resource.String.btn_lbl_Cancel, (EventHandler<DialogClickEventArgs>)((ds, dea) => { return; }));
                windAlertDialogBuilder.Show();
            }));
            targetAlertDialogBuilder.SetNegativeButton(Resource.String.btn_lbl_Cancel, (EventHandler<DialogClickEventArgs>)((s, ea) => { return; }));
            targetAlertDialogBuilder.Show();
        }

        private void buttonBeaufortWindScale_Click(object sender, EventArgs e)
        {
            Intent beaufortWindScaleActivityIntent = new Intent(this, typeof(BeaufortChartActivity));
            beaufortWindScaleActivityIntent.PutExtra("SelectedUnits", buttonWindSpeedUnits.Text);
            StartActivityForResult(beaufortWindScaleActivityIntent, 1);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                if (resultCode == Result.Ok)
                {
                    double speedValue = data.Extras.GetDouble("speed_value", 0);
                    string speedUnits = data.Extras.GetString("speed_units", buttonWindSpeedUnits.Text);

                    editWindSpeed.UnitsAdapter.Set(speedValue, speedUnits);
                }
            }
        }

        private void fillDataFromWindInfo(WindInfo windInfo)
        {
            if (windInfo != null)
            {
                if (windInfo.Speed != null)
                {
                    if (ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Metric &&
                        (
                            windInfo.Speed.SetUnit == Velocity.Unit.FeetPerSecond ||
                            windInfo.Speed.SetUnit == Velocity.Unit.MilesPerHour
                        ) ||
                        ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial &&
                        (
                            windInfo.Speed.SetUnit == Velocity.Unit.KilometersPerHour ||
                            windInfo.Speed.SetUnit == Velocity.Unit.MeterPerSecond
                        ))
                        buttonWindSpeedUnits.Text = Velocity.UnitToName(DefaultUnits.Wind.Velocity);
                    else
                        buttonWindSpeedUnits.Text = Velocity.UnitToName(windInfo.Speed.SetUnit);

                    IUnitsAdapter adapter = new VelocityAdapter(windInfo.Speed);
                    adapter.ChangeUnit(buttonWindSpeedUnits.Text);
                    editWindSpeed.UnitsAdapter = adapter;
                }

                if (windInfo.Direction != null)
                {
                    if (ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Metric &&
                        (
                            windInfo.Direction.SetUnit == Angle.Unit.InPer100Yards
                        ) ||
                        ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial &&
                        (
                            windInfo.Direction.SetUnit == Angle.Unit.CmPer100Meters
                        ))
                        buttonWindAngleUnits.Text = Angle.UnitToName(DefaultUnits.Wind.Angle);
                    else
                        buttonWindAngleUnits.Text = Angle.UnitToName(windInfo.Direction.SetUnit);

                    IUnitsAdapter adapter = new AngleAdapter(windInfo.Direction);
                    adapter.ChangeUnit(buttonWindAngleUnits.Text);
                    editWindAngle.UnitsAdapter = adapter;

                    anglePickerWindDirection.Angle = (int)windInfo.Direction.Get(Angle.Unit.Degree);
                }
            }
        }

        private void InitSensorManager()
        {
            sensorManager = (SensorManager)GetSystemService(SensorService);
            magnitometerSensor = sensorManager.GetDefaultSensor(SensorType.MagneticField);
            accelerometerSensor = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
        }

        private void InitUnits()
        {
            buttonWindSpeedUnits.Text = Velocity.UnitToName(DefaultUnits.Wind.Velocity);
            buttonWindAngleUnits.Text = Angle.UnitToName(DefaultUnits.Wind.Angle);
        }

        private void InitControls()
        {
            editWindSpeed = FindViewById<EditTextEx>(Resource.Id.editTextWindSpeed);
            buttonWindSpeedUnits = FindViewById<Button>(Resource.Id.buttonWindSpeedUnits);
            BindButton(buttonWindSpeedUnits, editWindSpeed);

            editWindAngle = FindViewById<EditTextEx>(Resource.Id.editTextWindDirection);
            buttonWindAngleUnits = FindViewById<Button>(Resource.Id.buttonWindDirectionUnits);
            BindButton(buttonWindAngleUnits, editWindAngle);

            buttonBeaufortWindScale = FindViewById<Button>(Resource.Id.buttonBeaufortChart);
            buttonUseSensors = FindViewById<Button>(Resource.Id.buttonUseGPS);

            anglePickerWindDirection = FindViewById<AnglePicker>(Resource.Id.anglePickerWindDirection);
        }

        protected override void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (ApplicationData.Instance.WindInfo == null)
                ApplicationData.Instance.WindInfo = new WindInfo(null, null);

            ApplicationData.Instance.WindInfo.Speed = new Velocity(editWindSpeed.UnitsAdapter.CurrentValue(), Velocity.NameToUnit(editWindSpeed.UnitsAdapter.CurrentUnit()));
            ApplicationData.Instance.WindInfo.Direction = new Angle(editWindAngle.UnitsAdapter.CurrentValue(), Angle.NameToUnit(editWindAngle.UnitsAdapter.CurrentUnit()));

            Finish();
        }

        protected override void OnCancelButtonClick(object sender, EventArgs e)
        {
            Finish();
        }

        protected override IEnumerable<Button> GetAngleButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(buttonWindAngleUnits);
            return buttons;
        }

        protected override IEnumerable<Button> GetDistanceButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetEnergyButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetPressureButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetTemperatureButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetVelocityButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(buttonWindSpeedUnits);
            return buttons;
        }

        protected override IEnumerable<Button> GetWeightButtons()
        {
            return new List<Button>();
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.MagneticField)
            {
                magnitometerData = new List<float>(e.Values);
                magnitometerDataUpdated = true;
            }
            else if (e.Sensor.Type == SensorType.Accelerometer)
            {
                accelerometerData = new List<float>(e.Values);
                accelerometerDataUpdated = true;
            }

            if (accelerometerDataUpdated && magnitometerDataUpdated)
            {
                float[] rotationMatrix = new float[9];
                float[] orientationRad = new float[3];

                SensorManager.GetRotationMatrix(rotationMatrix, null, accelerometerData.ToArray(), magnitometerData.ToArray());
                SensorManager.GetOrientation(rotationMatrix, orientationRad);

                azimuthAngle = Angle.Convert(orientationRad[0], Angle.Unit.Radian, Angle.Unit.Degree);

                magnitometerDataUpdated = false;
                accelerometerDataUpdated = false;
            }
        }
    }
}