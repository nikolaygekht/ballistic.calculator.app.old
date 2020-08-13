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
using Android.Locations;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;
using Android.Hardware;
using System.Globalization;
using Gehtsoft.BallisticCalculator.Views;
using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Utils;
using Gehtsoft.BallisticCalculator.Model;


namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Set Atmosphere")]
    public class EditAtmosphereActivity : EditActivityBase, ILocationListener, ISensorEventListener
    {
        protected override bool IsImperial
        {
            get
            {
                return _dataProvider.MeasurementSystem
                    == MeasurementSystem.Imperial;
            }
        }

        private const string PERCENT_SIGN = "%";

        private static AtmosphereInfo _atmosphereInfo;

        private EditTextEx _editTemperature;
        private EditTextEx _editAltitude;
        private EditTextEx _editPressure;
        private EditTextEx _editHumidity;

        private Button _buttonHumidityUnits;
        private Button _buttonTemperatureUnits;
        private Button _buttonAltitudeUnits;
        private Button _buttonPressureUnits;
        private Button _buttonUseSensors;

        private bool _usingSensors;

        private SensorManager _sensorManager;
        private LocationManager _locationManager;

        private Sensor _pressureSensor;
        private string _locationProvider;

        private TemperatureProvider _temperatureProvider;
        private BallisticDataProvider _dataProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SetAtmo);

            _dataProvider = BallisticDataProvider.Instance;
            if (_atmosphereInfo == null)
            {
                _atmosphereInfo = _dataProvider.AtmosphereData.AtmosphereInfo;
            }

            CreateControls();
            fillControlsFromAtmosphereInfo(_atmosphereInfo);
            InitSensors();
            Init();
        }

        private void CreateControls()
        {
            _editAltitude = FindViewById<EditTextEx>(Resource.Id.editTextAltitude);
            _buttonAltitudeUnits = FindViewById<Button>(Resource.Id.buttonAltitudeUnits);
            _editPressure = FindViewById<EditTextEx>(Resource.Id.editTextPressure);
            _buttonPressureUnits = FindViewById<Button>(Resource.Id.buttonPressureUnits);
            _editTemperature = FindViewById<EditTextEx>(Resource.Id.editTextTemperature);
            _buttonTemperatureUnits = FindViewById<Button>(Resource.Id.buttonTemperatureUnits);
            _editHumidity = FindViewById<EditTextEx>(Resource.Id.editTextHumidity);
            _buttonHumidityUnits = FindViewById<Button>(Resource.Id.buttonHumidityUnits);
            _buttonUseSensors = FindViewById<Button>(Resource.Id.buttonUseGPS);

            BindButton(_buttonAltitudeUnits, _editAltitude);
            BindButton(_buttonPressureUnits, _editPressure);
            BindButton(_buttonTemperatureUnits, _editTemperature);
            BindButton(_buttonHumidityUnits, _editHumidity);

            _buttonHumidityUnits.Text = PERCENT_SIGN;
        }

        private void fillControlsFromAtmosphereInfo(AtmosphereInfo atmoInfo)
        {
            if (!IsImperial &&
               (
                   atmoInfo.Temperature.SetUnit == Temperature.Unit.Fahrenheit
               ) ||
               IsImperial &&
               (
                   atmoInfo.Temperature.SetUnit == Temperature.Unit.Celsius 
               ))
            {
                _buttonTemperatureUnits.Text = Temperature.UnitToName(DefaultUnits.Atmosphere.Temperature);
            }
            else
            {
                _buttonTemperatureUnits.Text = Temperature.UnitToName(atmoInfo.Temperature.SetUnit);
            }

            var adapterTemper = new TemperatureAdapter(atmoInfo.Temperature);
            adapterTemper.ChangeUnit(_buttonTemperatureUnits.Text);
            _editTemperature.UnitsAdapter = adapterTemper;


            if (!IsImperial &&
               (
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Inch ||
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Foot ||
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Yard ||
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Mile
               ) ||
               IsImperial &&
               (
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Millimeter ||
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Centimeter ||
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Meter ||
                   atmoInfo.Altitude.SetUnit == Distance.Unit.Kilometer
               ))
            {
                _buttonAltitudeUnits.Text = Distance.UnitToName(DefaultUnits.Altitude);
            }
            else
            {
                _buttonAltitudeUnits.Text = Distance.UnitToName(atmoInfo.Altitude.SetUnit);
            }

            var adapterAltit = new DistanceAdapter(atmoInfo.Altitude);
            adapterAltit.ChangeUnit(_buttonAltitudeUnits.Text);
            _editAltitude.UnitsAdapter = adapterAltit;


            if (!IsImperial &&
              (
                  atmoInfo.Pressure.SetUnit == Pressure.Unit.InchHg
              ) ||
              IsImperial &&
              (
                  atmoInfo.Pressure.SetUnit == Pressure.Unit.MmHg ||
                  atmoInfo.Pressure.SetUnit == Pressure.Unit.Bar ||
                  atmoInfo.Pressure.SetUnit == Pressure.Unit.hPa
              ))
            {
                _buttonPressureUnits.Text = Pressure.UnitToName(DefaultUnits.Atmosphere.Pressure);
            }
            else
            {
                _buttonPressureUnits.Text = Pressure.UnitToName(atmoInfo.Pressure.SetUnit);
            }

            var adapterPress = new PressureAdapter(atmoInfo.Pressure);
            adapterPress.ChangeUnit(_buttonPressureUnits.Text);
            _editPressure.UnitsAdapter = adapterPress;

            _editHumidity.Text = (atmoInfo.Humidity * 100).ToString("F2", CultureInfo.InvariantCulture);
        }

        void fillAtmosphereInfoFromControls(AtmosphereInfo atmoInfo)
        {
            if (atmoInfo == null)
                return;

            var temperaturevalue = _editTemperature.UnitsAdapter.CurrentValue();
            var altitudeValue = _editAltitude.UnitsAdapter.CurrentValue();
            var pressureValue = _editPressure.UnitsAdapter.CurrentValue();

            var temperatureUnits = Temperature.NameToUnit(_editTemperature.UnitsAdapter.CurrentUnit());
            var altitudeUnits = Distance.NameToUnit(_editAltitude.UnitsAdapter.CurrentUnit());
            var pressureUnits = Pressure.NameToUnit(_editPressure.UnitsAdapter.CurrentUnit());

            atmoInfo.Temperature = new Temperature(temperaturevalue, temperatureUnits);
            atmoInfo.Altitude = new Distance(altitudeValue, altitudeUnits);
            atmoInfo.Pressure = new Pressure(pressureValue, pressureUnits);

            double humidity;
            bool isOK = Utilities.TryParseDouble(_editHumidity.Text, out humidity);
            if (isOK)
                atmoInfo.Humidity = humidity / 100;
            else
                atmoInfo.Humidity = double.PositiveInfinity;
        }

        void InitSensors()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            var criteria = new Criteria();
            criteria.Accuracy = Accuracy.Coarse;

            _locationProvider = _locationManager.GetBestProvider(criteria, true);

            _sensorManager = (SensorManager)GetSystemService(SensorService);
            _pressureSensor = _sensorManager.GetDefaultSensor(SensorType.Pressure);

            _temperatureProvider = new TemperatureProvider(this);
            _temperatureProvider.TemperatureChanged += temperatureProvider_TemperatureChanged;

            if (_locationProvider.Length > 0 || _pressureSensor != null)
                _buttonUseSensors.Click += buttonUseSensors_Click;
            else
                _buttonUseSensors.Enabled = false;
        }

        private void temperatureProvider_TemperatureChanged(object sender, TemperatureProvider.TemperatureEventArgs e)
        {
            _dataProvider.AtmosphereData.AtmosphereInfo.Temperature = e.Temperature;
            _editTemperature.UnitsAdapter.Set(
                e.Temperature.Get(Temperature.NameToUnit(_buttonTemperatureUnits.Text)), 
                _buttonTemperatureUnits.Text);
        }

        public void OnLocationChanged(Location location)
        {
            if (location.HasAltitude)
            {
                Distance distance = new Distance(location.Altitude, Distance.Unit.Meter);
                _editAltitude.UnitsAdapter.Set(distance.Get(Distance.NameToUnit(_buttonAltitudeUnits.Text)), _buttonAltitudeUnits.Text);
            }
        }

        public void OnProviderDisabled(string provider)
        {
            _locationManager.RemoveUpdates(this);
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Pressure)
            {
                Pressure pressure = new Pressure(e.Values[0], Pressure.Unit.hPa);
                _editPressure.UnitsAdapter.Set(pressure.Get(Pressure.NameToUnit(_buttonPressureUnits.Text)), _buttonPressureUnits.Text);
            }
        }

        override protected void OnSaveButtonClick(object sender, EventArgs e)
        {
            fillAtmosphereInfoFromControls(_atmosphereInfo);
            var atmoData = _dataProvider.AtmosphereData;

            if (double.IsPositiveInfinity(_atmosphereInfo.Humidity))
                _atmosphereInfo.Humidity = atmoData.AtmosphereInfo.Humidity;

            atmoData.AtmosphereInfo = _atmosphereInfo;
            _atmosphereInfo = null;

            Finish();
        }

        override protected void OnCancelButtonClick(object sender, EventArgs e)
        {
            _atmosphereInfo = null;
            Finish();
        }

        public override void OnBackPressed()
        {
            OnSaveButtonClick(this, new EventArgs());
        }

        override protected IEnumerable<Button> GetAngleButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetDistanceButtons()
        {
            var distanceButtons = new List<Button>();
            distanceButtons.Add(_buttonAltitudeUnits);
            return distanceButtons;
        }

        override protected IEnumerable<Button> GetEnergyButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetPressureButtons()
        {
            var pressureButtons = new List<Button>();
            pressureButtons.Add(_buttonPressureUnits);
            return pressureButtons;
        }

        override protected IEnumerable<Button> GetTemperatureButtons()
        {
            var temeratureButtons = new List<Button>();
            temeratureButtons.Add(_buttonTemperatureUnits);
            return temeratureButtons;
        }

        override protected IEnumerable<Button> GetVelocityButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetWeightButtons()
        {
            return new List<Button>();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (_atmosphereInfo != null)
                fillAtmosphereInfoFromControls(_atmosphereInfo);

            if (_usingSensors)
            {
                _locationManager.RemoveUpdates(this);
                _sensorManager.UnregisterListener(this);
                _temperatureProvider.Pause();
            } 
        }

        protected override void OnResume()
        {
            base.OnResume();

            fillControlsFromAtmosphereInfo(_atmosphereInfo);

            if (_usingSensors)
            {
                if (_locationProvider.Length > 0)
                    _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
                if (_pressureSensor != null)
                    _sensorManager.RegisterListener(this, _pressureSensor, SensorDelay.Normal);
                _temperatureProvider.Resume();

                _buttonUseSensors.Text = Resources.GetString(Resource.String.btn_lbl_StopUsingSensors);
            }
        }

        private void buttonUseSensors_Click(object sender, EventArgs e)
        {
            if (_usingSensors == false)
            {
                _buttonUseSensors.Enabled = false;

                if (_locationProvider.Length > 0)
                    _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
                if (_pressureSensor != null)
                    _sensorManager.RegisterListener(this, _pressureSensor, SensorDelay.Game);
                _temperatureProvider.Resume();

                _usingSensors = true;

                _buttonUseSensors.Text = Resources.GetString(Resource.String.btn_lbl_StopUsingSensors);
                _buttonUseSensors.Enabled = true;
            }
            else
            {
                _buttonUseSensors.Enabled = false;

                _locationManager.RemoveUpdates(this);
                _sensorManager.UnregisterListener(this);
                _temperatureProvider.Pause();

                _usingSensors = false;

                _buttonUseSensors.Text = Resources.GetString(Resource.String.btn_lbl_UseSensors);
                _buttonUseSensors.Enabled = true;
            }
        }
    }
}