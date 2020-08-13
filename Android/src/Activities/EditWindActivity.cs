using System;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Gehtsoft.BallisticCalculator.Utils;
using MathEx.ExternalBallistic.Units;
using System.Collections.Generic;
using Gehtsoft.BallisticCalculator.Views;
using Gehtsoft.BallisticCalculator.DataProviders;
using System.Linq;
using MathEx.ExternalBallistic;
using Android.Text;
using Java.Lang;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Set Wind")]
    public class EditWindActivity : EditActivityBase, ISensorEventListener
    {
        protected override bool IsImperial
        {
            get
            {
                return _dataProvider.MeasurementSystem ==
                    Model.MeasurementSystem.Imperial;
            }
        }

        private BallisticDataProvider _dataProvider;
        private static WindInfo _windInfo;

        private double _azimuthAngle;
        private bool _magnitometerDataUpdated;
        private bool _accelerometerDataUpdated;
        private Button _buttonBeaufortWindScale;
        private Button _buttonWindAngleUnits;
        private Button _buttonWindSpeedUnits;
        private Button _buttonUseSensors;
        private EditTextEx _editWindSpeed;
        private EditTextEx _editWindAngle;
        private AnglePicker _anglePickerWindDirection;

        private SensorManager _sensorManager;
        private Sensor _magnitometerSensor;
        private Sensor _accelerometerSensor;
        private IList<float> _magnitometerData;
        private IList<float> _accelerometerData;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SetWind);
            _dataProvider = BallisticDataProvider.Instance;

            if (_windInfo == null)
                _windInfo = _dataProvider.AtmosphereData.WindInfo;

            CreateControls();
            fillControlsFromWindInfo(_windInfo);
            InitSensors();

            Init();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 1 && resultCode == Result.Ok)
            {
                double windSpeed = data.GetDoubleExtra("WindSpeedValue", 0);
                string selectedUnits = data.GetStringExtra("SelectedUnits");
                if (string.IsNullOrEmpty(selectedUnits))
                    selectedUnits = _buttonWindSpeedUnits.Text;
                _editWindSpeed.UnitsAdapter.Set(windSpeed, selectedUnits);
                if (_windInfo != null)
                    _windInfo.Speed = new Velocity(windSpeed, Velocity.NameToUnit(selectedUnits));
            }
        }

        private void CreateControls()
        {
            _editWindSpeed = FindViewById<EditTextEx>(Resource.Id.editTextWindSpeed);
            _buttonWindSpeedUnits = FindViewById<Button>(Resource.Id.buttonWindSpeedUnits);
            _editWindAngle = FindViewById<EditTextEx>(Resource.Id.editTextWindDirection);
            _buttonWindAngleUnits = FindViewById<Button>(Resource.Id.buttonWindDirectionUnits);

            _buttonBeaufortWindScale = FindViewById<Button>(Resource.Id.buttonBeaufortChart);
            _buttonUseSensors = FindViewById<Button>(Resource.Id.buttonUseGPS);
            _anglePickerWindDirection = FindViewById<AnglePicker>(Resource.Id.anglePickerWindDirection);

            BindButton(_buttonWindAngleUnits, _editWindAngle);
            BindButton(_buttonWindSpeedUnits, _editWindSpeed);

            _buttonBeaufortWindScale.Click += buttonBeaufortWindScale_Click; ;
            _buttonUseSensors.Click += buttonUseSensors_Click; ;
            _anglePickerWindDirection.AngleChanged += anglePickerWindDirection_AngleChanged;
            _editWindAngle.TextChanged += editWindAngle_TextChanged;

            /*IInputFilter[] inputFilters = { new AngleInputFilter() };
            _editWindAngle.SetFilters(inputFilters);*/
        }

        private void editWindAngle_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            double angle = 0;
            _anglePickerWindDirection.AngleChanged -= anglePickerWindDirection_AngleChanged;
            Utilities.TryParseDouble(e.Text.ToString(), out angle);
            Angle.Unit angleUnit = Angle.NameToUnit(_buttonWindAngleUnits.Text);
            _anglePickerWindDirection.Angle = (int)(new Angle(angle, angleUnit).Get(Angle.Unit.Degree));
            _anglePickerWindDirection.AngleChanged += anglePickerWindDirection_AngleChanged;            
        }

        private void anglePickerWindDirection_AngleChanged(object sender, AnglePicker.AngleEventArgs e)
        {
            _editWindAngle.UnitsAdapter.Set(new Angle(e.Angle, Angle.Unit.Degree).Get(Angle.NameToUnit(_buttonWindAngleUnits.Text)), _buttonWindAngleUnits.Text);
            _editWindAngle.Text = Utilities.RoundDouble(_editWindAngle.UnitsAdapter.CurrentValue(), _editWindAngle.UnitsAdapter.DefaultDisplayPrecision()).ToString();
        }

        private void buttonUseSensors_Click(object sender, EventArgs e)
        {
            double windDirection = 0;
            // Get target direction angle
            AlertDialog.Builder targetAlertDialogBuilder = new AlertDialog.Builder(this);
            targetAlertDialogBuilder.SetTitle(Resource.String.edit_hint_WindDirection);
            targetAlertDialogBuilder.SetMessage(Resource.String.msg_TowardTarget);
            targetAlertDialogBuilder.SetPositiveButton(Resource.String.btn_lbl_Continue, (EventHandler<DialogClickEventArgs>)((s, ea) =>
            {
                windDirection -= _azimuthAngle;
                // Get wind direction angle
                AlertDialog.Builder windAlertDialogBuilder = new AlertDialog.Builder(this);
                windAlertDialogBuilder.SetTitle(Resource.String.edit_hint_WindDirection);
                windAlertDialogBuilder.SetMessage(Resource.String.msg_AgainstWind);
                windAlertDialogBuilder.SetPositiveButton(Resource.String.btn_lbl_Continue, (EventHandler<DialogClickEventArgs>)((ds, dea) =>
                {
                    windDirection += _azimuthAngle;
                    if (windDirection > 180)
                        windDirection -= 360;
                    else if (windDirection < -180)
                        windDirection += 360;
                    _editWindAngle.UnitsAdapter.Set(new Angle(windDirection, Angle.Unit.Degree).Get(Angle.NameToUnit(_buttonWindAngleUnits.Text)), 
                        _buttonWindAngleUnits.Text);
                    _anglePickerWindDirection.Angle = (int)windDirection;
                }));
                windAlertDialogBuilder.SetNegativeButton(Resource.String.btn_lbl_Cancel, (EventHandler<DialogClickEventArgs>)((ds, dea) => { return; }));
                windAlertDialogBuilder.Show();
            }));
            targetAlertDialogBuilder.SetNegativeButton(Resource.String.btn_lbl_Cancel, (EventHandler<DialogClickEventArgs>)((s, ea) => { return; }));
            targetAlertDialogBuilder.Show();
        }

        private void buttonBeaufortWindScale_Click(object sender, EventArgs e)
        {
            var data = new Intent(this, typeof(WindSpeedListActivity));
            data.PutExtra("SelectedUnits", _buttonWindSpeedUnits.Text);
            StartActivityForResult(data, 1);
        }

        private void fillControlsFromWindInfo(WindInfo windInfo)
        {
            if (!IsImperial &&
               (
                   windInfo.Speed.SetUnit == Velocity.Unit.FeetPerSecond ||
                   windInfo.Speed.SetUnit == Velocity.Unit.MilesPerHour
               ) ||
               IsImperial &&
               (
                   windInfo.Speed.SetUnit == Velocity.Unit.KilometersPerHour ||
                   windInfo.Speed.SetUnit == Velocity.Unit.MeterPerSecond
               ))
            {
                _buttonWindSpeedUnits.Text = Velocity.UnitToName(DefaultUnits.Wind.Velocity);
            }
            else
            {
                _buttonWindSpeedUnits.Text = Velocity.UnitToName(windInfo.Speed.SetUnit);
            }

            IUnitsAdapter velocityAdapter = new VelocityAdapter(windInfo.Speed);
            velocityAdapter.ChangeUnit(_buttonWindSpeedUnits.Text);
            _editWindSpeed.UnitsAdapter = velocityAdapter;

    
            if (!IsImperial &&
               (
                   windInfo.Direction.SetUnit == Angle.Unit.InPer100Yards
               ) ||
               IsImperial &&
               (
                   windInfo.Direction.SetUnit == Angle.Unit.CmPer100Meters
               ))
            {
                _buttonWindAngleUnits.Text = Angle.UnitToName(DefaultUnits.Wind.Angle);
            }
            else
            {
                _buttonWindAngleUnits.Text = Angle.UnitToName(windInfo.Direction.SetUnit);
            }
        
            IUnitsAdapter directionAdapter = new AngleAdapter(windInfo.Direction);
            directionAdapter.ChangeUnit(_buttonWindAngleUnits.Text);
            _editWindAngle.UnitsAdapter = directionAdapter;

            _anglePickerWindDirection.Angle = (int)windInfo.Direction.Get(Angle.Unit.Degree);
        }

        private void fillWindInfoFromControls(WindInfo windInfo)
        {
            if (windInfo == null)
                return;

            var atmoData = _dataProvider.AtmosphereData;

            double windSpeedValue = _editWindSpeed.UnitsAdapter.CurrentValue();
            double windAngleValue = _editWindAngle.UnitsAdapter.CurrentValue();
            Velocity.Unit windSpeetUnit = Velocity.NameToUnit(_editWindSpeed.UnitsAdapter.CurrentUnit());
            Angle.Unit windAngleUnit = Angle.NameToUnit(_editWindAngle.UnitsAdapter.CurrentUnit());

            windInfo.Speed = new Velocity(windSpeedValue, windSpeetUnit);
            windInfo.Direction = new Angle(windAngleValue, windAngleUnit);
        }

        private void InitSensors()
        {
            _sensorManager = (SensorManager)GetSystemService(SensorService);
            _magnitometerSensor = _sensorManager.GetDefaultSensor(SensorType.MagneticField);
            _accelerometerSensor = _sensorManager.GetDefaultSensor(SensorType.Accelerometer);
        }


        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.MagneticField)
            {
                _magnitometerData = new List<float>(e.Values);
                _magnitometerDataUpdated = true;
            }
            else if (e.Sensor.Type == SensorType.Accelerometer)
            {
                _accelerometerData = new List<float>(e.Values);
                _accelerometerDataUpdated = true;
            }

            if (_accelerometerDataUpdated && _magnitometerDataUpdated)
            {
                float[] rotationMatrix = new float[9];
                float[] orientationRad = new float[3];

                SensorManager.GetRotationMatrix(rotationMatrix, null, _accelerometerData.ToArray(), _magnitometerData.ToArray());
                SensorManager.GetOrientation(rotationMatrix, orientationRad);

                _azimuthAngle = Angle.Convert(orientationRad[0], Angle.Unit.Radian, Angle.Unit.Degree);

                _magnitometerDataUpdated = false;
                _accelerometerDataUpdated = false;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            fillWindInfoFromControls(_windInfo);

            if (_sensorManager != null)
            {
                _sensorManager.UnregisterListener(this, _magnitometerSensor);
                _sensorManager.UnregisterListener(this, _accelerometerSensor);
            }
        }

        protected override void OnResume()
        {
            if (_windInfo != null)
                fillControlsFromWindInfo(_windInfo);

            base.OnResume();
            if (_sensorManager != null)
            {
                _sensorManager.RegisterListener(this, _magnitometerSensor, SensorDelay.Game);
                _sensorManager.RegisterListener(this, _accelerometerSensor, SensorDelay.Game);
            }
        }

        override protected void OnSaveButtonClick(object sender, EventArgs e)
        {
            fillWindInfoFromControls(_windInfo);
            _dataProvider.AtmosphereData.WindInfo = _windInfo;
            _windInfo = null;

            Finish();
        }

        override protected void OnCancelButtonClick(object sender, EventArgs e)
        {
            _windInfo = null;
            Finish();
        }

        public override void OnBackPressed()
        {
            OnSaveButtonClick(this, new EventArgs());
        }

        override protected IEnumerable<Button> GetAngleButtons()
        {
            List<Button> angleButtons = new List<Button>();
            angleButtons.Add(_buttonWindAngleUnits);
            return angleButtons;
        }

        override protected IEnumerable<Button> GetVelocityButtons()
        {
            List<Button> velocityButtons = new List<Button>();
            velocityButtons.Add(_buttonWindSpeedUnits);
            return velocityButtons;
        }

        override protected IEnumerable<Button> GetDistanceButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetEnergyButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetPressureButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetTemperatureButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetWeightButtons()
        {
            return new List<Button>();
        }
        /*
        class AngleInputFilter : Java.Lang.Object, IInputFilter
        {
            public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
            {
                if (dest.Length() == 0 && source.ToString() == "-")
                    return source;

                double number = 0;
                string numStr = dest.ToString();
                var resStr = numStr.Insert(dstart, source.ToString());

                bool isOk = Utilities.TryParseDouble(resStr, out number);

                if (!isOk)
                    return new Java.Lang.String("");

                if (number >= -180 && number <= 180)
                    return source;
                else
                    return new Java.Lang.String("");
            }
        }*/
    }
}
 