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

namespace BallisticCalculator.Utils
{
    class TemperatureProvider : BroadcastReceiver, ISensorEventListener
    {
        public class TemperatureEventArgs : EventArgs
        {
            public TemperatureEventArgs(float temperature)
            {
                Temperature = new Temperature(temperature, MathEx.ExternalBallistic.Units.Temperature.Unit.Celsius);
            }

            public Temperature Temperature { get; private set; }
        }

        SensorManager sensorManager;
        Sensor temperatureSensor;

        bool enabled = false;
        float cachedTemperatureValue = float.NaN;

        public TemperatureProvider(Context context)
        {
            sensorManager = (SensorManager)context.GetSystemService(Context.SensorService);
            temperatureSensor = sensorManager.GetDefaultSensor(SensorType.AmbientTemperature);
            if (temperatureSensor == null)
                temperatureSensor = sensorManager.GetDefaultSensor(SensorType.Temperature);

            if (temperatureSensor == null)
            {
                IntentFilter localIntentFilter = new IntentFilter();
                localIntentFilter.AddAction(Intent.ActionBatteryChanged);
                context.RegisterReceiver(this, localIntentFilter);
            }
        }

        private event EventHandler<TemperatureEventArgs> _temperatureChanged;

        public event EventHandler<TemperatureEventArgs> TemperatureChanged
        {
            add
            {
                _temperatureChanged += value;
                if (float.IsNaN(cachedTemperatureValue) == false)
                    value(this, new TemperatureEventArgs(cachedTemperatureValue));
            }

            remove
            {
                _temperatureChanged -= value;
            }
        }

        public void Pause()
        {
            if (sensorManager != null && temperatureSensor != null)
                sensorManager.UnregisterListener(this, temperatureSensor);
            enabled = false;
        }

        public void Resume()
        {
            if (sensorManager != null && temperatureSensor != null)
                sensorManager.RegisterListener(this, temperatureSensor, SensorDelay.Normal);
            enabled = true;

            if (float.IsNaN(cachedTemperatureValue) == false && _temperatureChanged != null)
                _temperatureChanged(this, new TemperatureEventArgs(cachedTemperatureValue));
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            cachedTemperatureValue = e.Values[0];
            if (e.Sensor.Type == SensorType.AmbientTemperature || e.Sensor.Type == SensorType.Temperature)
                if (enabled && _temperatureChanged != null)
                    _temperatureChanged(this, new TemperatureEventArgs(cachedTemperatureValue));
        }

        public override void OnReceive(Context context, Intent intent)
        {
            cachedTemperatureValue = intent.GetIntExtra(BatteryManager.ExtraTemperature, 0) / 10.0f;
            if (enabled && _temperatureChanged != null)
                _temperatureChanged(this, new TemperatureEventArgs(cachedTemperatureValue));
        }
    }
}