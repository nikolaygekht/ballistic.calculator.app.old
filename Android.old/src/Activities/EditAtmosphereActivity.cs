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
using BallisticCalculator.Utils;
using MathEx.ExternalBallistic.Units;
using Android.Hardware;
using System.Globalization;
using BallisticCalculator.Views;


namespace BallisticCalculator.Activities
{
    [Activity(Label = "Edit Atmosphere")]
    public class EditAtmosphereActivity : EditActivityBase, ILocationListener, ISensorEventListener
    {
        EditTextEx editAltitude;
        Button buttonAltitudeUnits;

        EditTextEx editPressure;
        Button buttonPressureUnits;

        EditTextEx editTemperature;
        Button buttonTemperatureUnits;

        EditText editHumidity;
        Button buttonHumidityUnits;

        Button buttonUseSensors;
        bool usingSensors = false;

        SensorManager sensorManager;
        LocationManager locationManager;

        Sensor pressureSensor;
        String locationProvider;

        TemperatureProvider temperatureProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EditAtmosphere);

            InitControls();
            InitUnits();

            fillDataFromAtmosphereInfo(ApplicationData.Instance.AtmosphereInfo);

            Init();
            InitDeviceSensors();

            if (locationProvider.Length == 0 && pressureSensor == null)
            {
                Utilities.SetButtonState(buttonUseSensors, false);
            }
            else
                buttonUseSensors.Click += buttonUseSensors_Click;
        }

        private void buttonUseSensors_Click(object sender, EventArgs e)
        {
            if (usingSensors == false)
            {
                Utilities.SetButtonState(buttonUseSensors, false);

                if (locationProvider.Length > 0)
                    locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
                if (pressureSensor != null)
                    sensorManager.RegisterListener(this, pressureSensor, SensorDelay.Normal);
                temperatureProvider.Resume();

                usingSensors = true;

                buttonUseSensors.Text = Resources.GetString(Resource.String.btn_lbl_StopUsingSensors);
                Utilities.SetButtonState(buttonUseSensors, true);
            }
            else
            {
                Utilities.SetButtonState(buttonUseSensors, false);

                locationManager.RemoveUpdates(this);
                sensorManager.UnregisterListener(this);
                temperatureProvider.Pause();

                usingSensors = false;

                buttonUseSensors.Text = Resources.GetString(Resource.String.btn_lbl_UseSensors);
                Utilities.SetButtonState(buttonUseSensors, true);
            }
        }

        private void InitDeviceSensors()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria { Accuracy = Accuracy.Coarse };
            locationProvider = locationManager.GetBestProvider(criteriaForLocationService, true);

            sensorManager = (SensorManager)GetSystemService(SensorService);
            pressureSensor = sensorManager.GetDefaultSensor(SensorType.Pressure);

            temperatureProvider = new TemperatureProvider(this);
            temperatureProvider.TemperatureChanged += temperatureProvider_TemperatureChanged;
        }

        private void temperatureProvider_TemperatureChanged(object sender, TemperatureProvider.TemperatureEventArgs e)
        {
            editTemperature.Text = Utilities.RoundDouble(e.Temperature.Get(Temperature.NameToUnit(buttonTemperatureUnits.Text)), Temperature.DefaultDisplayPrecision(Temperature.NameToUnit(buttonTemperatureUnits.Text))).ToString();
        }

        private void fillDataFromAtmosphereInfo(AtmosphereInfo atmosphereInfo)
        {
            if (atmosphereInfo != null)
            {
                if (atmosphereInfo.Altitude != null)
                {
                    if (ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Metric &&
                        (
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Inch ||
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Foot ||
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Yard ||
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Mile
                        ) ||
                        ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial &&
                        (
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Millimeter ||
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Centimeter ||
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Meter ||
                            atmosphereInfo.Altitude.SetUnit == Distance.Unit.Kilometer
                        ))
                        buttonAltitudeUnits.Text = Distance.UnitToName(DefaultUnits.Altitude);
                    else
                        buttonAltitudeUnits.Text = Distance.UnitToName(atmosphereInfo.Altitude.SetUnit);

                    IUnitsAdapter adapter = new DistanceAdapter(atmosphereInfo.Altitude);
                    adapter.ChangeUnit(buttonAltitudeUnits.Text);
                    editAltitude.UnitsAdapter = adapter;
                }

                editHumidity.Text = (atmosphereInfo.Humidity * 100).ToString("F2", CultureInfo.InvariantCulture);

                if (atmosphereInfo.Pressure != null)
                {
                    if (ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Metric &&
                        (
                            atmosphereInfo.Pressure.SetUnit == Pressure.Unit.InchHg
                        ) ||
                        ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial &&
                        (
                            atmosphereInfo.Pressure.SetUnit == Pressure.Unit.MmHg
                        ))
                        buttonPressureUnits.Text = Pressure.UnitToName(DefaultUnits.Atmosphere.Pressure);
                    else
                        buttonPressureUnits.Text = Pressure.UnitToName(atmosphereInfo.Pressure.SetUnit);

                    IUnitsAdapter adapter = new PressureAdapter(atmosphereInfo.Pressure);
                    adapter.ChangeUnit(buttonPressureUnits.Text);
                    editPressure.UnitsAdapter = adapter;
                }

                if (atmosphereInfo.Temperature != null)
                {
                    if (ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Metric &&
                        (
                            atmosphereInfo.Temperature.SetUnit == Temperature.Unit.Fahrenheit
                        ) ||
                        ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial &&
                        (
                            atmosphereInfo.Temperature.SetUnit == Temperature.Unit.Celsius
                        ))
                        buttonTemperatureUnits.Text = Temperature.UnitToName(DefaultUnits.Atmosphere.Temperature);
                    else
                        buttonTemperatureUnits.Text = Temperature.UnitToName(atmosphereInfo.Temperature.SetUnit);

                    IUnitsAdapter adapter = new TemperatureAdapter(atmosphereInfo.Temperature);
                    adapter.ChangeUnit(buttonTemperatureUnits.Text);
                    editTemperature.UnitsAdapter = adapter;
                }
            }
        }

        private void InitControls()
        {
            editAltitude = FindViewById<EditTextEx>(Resource.Id.editTextAltitude);
            buttonAltitudeUnits = FindViewById<Button>(Resource.Id.buttonAltitudeUnits);
            BindButton(buttonAltitudeUnits, editAltitude);

            editPressure = FindViewById<EditTextEx>(Resource.Id.editTextPressure);
            buttonPressureUnits = FindViewById<Button>(Resource.Id.buttonPressureUnits);
            BindButton(buttonPressureUnits, editPressure);

            editTemperature = FindViewById<EditTextEx>(Resource.Id.editTextTemperature);
            buttonTemperatureUnits = FindViewById<Button>(Resource.Id.buttonTemperatureUnits);
            BindButton(buttonTemperatureUnits, editTemperature);

            editHumidity = FindViewById<EditText>(Resource.Id.editTextHumidity);
            buttonHumidityUnits = FindViewById<Button>(Resource.Id.buttonHumidityUnits);

            buttonUseSensors = FindViewById<Button>(Resource.Id.buttonUseGPS);
        }

        private void InitUnits()
        {
            buttonAltitudeUnits.Text = Distance.UnitToName(DefaultUnits.Altitude);
            buttonPressureUnits.Text = Pressure.UnitToName(DefaultUnits.Atmosphere.Pressure);
            buttonTemperatureUnits.Text = Temperature.UnitToName(DefaultUnits.Atmosphere.Temperature);
            buttonHumidityUnits.Text = "%";
        }

        protected override IEnumerable<Button> GetAngleButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetDistanceButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(buttonAltitudeUnits);
            return buttons;
        }

        protected override IEnumerable<Button> GetEnergyButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetPressureButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(buttonPressureUnits);
            return buttons;
        }

        protected override IEnumerable<Button> GetTemperatureButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(buttonTemperatureUnits);
            return buttons;
        }

        protected override IEnumerable<Button> GetVelocityButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetWeightButtons()
        {
            return new List<Button>();
        }

        protected override void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (ApplicationData.Instance.AtmosphereInfo == null)
                ApplicationData.Instance.AtmosphereInfo = new AtmosphereInfo(null, null, null, 0.0);

            ApplicationData.Instance.AtmosphereInfo.Altitude = new Distance(editAltitude.UnitsAdapter.CurrentValue(), Distance.NameToUnit(editAltitude.UnitsAdapter.CurrentUnit()));
            ApplicationData.Instance.AtmosphereInfo.Pressure = new Pressure(editPressure.UnitsAdapter.CurrentValue(), Pressure.NameToUnit(editPressure.UnitsAdapter.CurrentUnit()));
            ApplicationData.Instance.AtmosphereInfo.Temperature = new Temperature(editTemperature.UnitsAdapter.CurrentValue(), Temperature.NameToUnit(editTemperature.UnitsAdapter.CurrentUnit()));

            double humidity;
            if (Utilities.TryParseDouble(editHumidity.Text, out humidity))
                ApplicationData.Instance.AtmosphereInfo.Humidity = humidity / 100;

            Finish();
        }

        protected override void OnCancelButtonClick(object sender, EventArgs e)
        {
            Finish();
        }

        public void OnLocationChanged(Location location)
        {
            if (location.HasAltitude)
            {
                Distance newValue = new Distance(location.Altitude, Distance.Unit.Meter);
                editAltitude.UnitsAdapter.Set(newValue.Get(Distance.NameToUnit(buttonAltitudeUnits.Text)), buttonAltitudeUnits.Text);
            }
        }

        public void OnProviderDisabled(string provider)
        {
            locationManager.RemoveUpdates(this);
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (usingSensors)
            {
                locationManager.RemoveUpdates(this);
                sensorManager.UnregisterListener(this);
                temperatureProvider.Pause();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (usingSensors)
            {
                if (locationProvider.Length > 0)
                    locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
                if (pressureSensor != null)
                    sensorManager.RegisterListener(this, pressureSensor, SensorDelay.Normal);
                temperatureProvider.Resume();

                buttonUseSensors.Text = Resources.GetString(Resource.String.btn_lbl_StopUsingSensors);
            }
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Pressure)
            {
                Pressure newValue = new Pressure(e.Values[0], Pressure.Unit.hPa);
                editPressure.UnitsAdapter.Set(newValue.Get(Pressure.NameToUnit(buttonPressureUnits.Text)), buttonPressureUnits.Text);
            }
        }
    }
}