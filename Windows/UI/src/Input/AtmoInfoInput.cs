using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;


namespace Gehtsoft.BallisticCalculator.UI.src.Input
{
    public partial class AtmoInfoInput : UserControl, IMeasurementSystemListener
    {
        private bool mShowWindInfo;

        public bool ShowWindInfo
        {
            get
            {
                return mShowWindInfo;
            }
            set
            {
                mShowWindInfo = value;
                UpdateShowWindInfo();
            }
        }
       

        public AtmoInfoInput()
        {
            InitializeComponent();
            SetDefaultAtmosphere();
        }

        private void UpdateShowWindInfo()
        {
            labelWind1.Visible = mShowWindInfo;
            labelWind2.Visible = mShowWindInfo;
            velocityWind.Visible = mShowWindInfo;
            angleWind.Visible = mShowWindInfo;
            angleSelector1.Visible = mShowWindInfo;

            if (!mShowWindInfo)
            {
                this.Width = labelCompactShape.Left + labelCompactShape.Width;
                this.Height = labelCompactShape.Top + labelCompactShape.Height;
            }
            else
            {
                this.Width = labelFullShape.Left + labelFullShape.Width;
                this.Height = labelFullShape.Top + labelFullShape.Height;

            }
        }

        private MeasurementSystem mMeasurementSystem;

        public MeasurementSystem MeasurementSystem
        {
            get
            {
                return mMeasurementSystem;
            }
            set 
            {
                mMeasurementSystem = value;
                if (mMeasurementSystem == UI.MeasurementSystem.Imperial)
                {
                    distanceAltitude.Unit = Distance.Unit.Foot;
                    pressureAtm.Unit = Pressure.Unit.InchHg;
                    temperatureAtm.Unit = Temperature.Unit.Fahrenheit;
                    velocityWind.Unit = Velocity.Unit.MilesPerHour;
                }
                else
                {
                    distanceAltitude.Unit = Distance.Unit.Meter;
                    pressureAtm.Unit = Pressure.Unit.hPa;
                    temperatureAtm.Unit = Temperature.Unit.Celsius;
                    velocityWind.Unit = Velocity.Unit.MeterPerSecond;
                }
            }
        }

        private void SetDefaultAtmosphere()
        {
            AtmosphereInfo info = new AtmosphereInfo();

            distanceAltitude.Value = info.Altitude.ToUnit(distanceAltitude.Value.SetUnit);
            pressureAtm.Value = info.Pressure.ToUnit(pressureAtm.Value.SetUnit);
            temperatureAtm.Value = info.Temperature.ToUnit(temperatureAtm.Value.SetUnit);
            numericHumidity.Value = (decimal)(info.Humidity * 100);

        }

        private void buttonResetTemperature_Click(object sender, EventArgs e)
        {
            SetDefaultAtmosphere();
        }

        public AtmosphereInfo GetAtmosphere()
        {
            return new AtmosphereInfo(distanceAltitude.Value, pressureAtm.Value, temperatureAtm.Value, ((double)numericHumidity.Value) / 100);
        }

        public WindInfo GetWind()
        {
            if (!mShowWindInfo)
                return null;

            return new WindInfo(angleWind.Value, velocityWind.Value);
        }

        public void Initialize(MeasurementSystem msi)
        {
            MeasurementSystem = msi;
            SetDefaultAtmosphere();


            //prevent resizing under wine
            distanceAltitude.Width = labelT.Width;
            distanceAltitude.LockSize(true);
            pressureAtm.Width = labelT.Width;
            pressureAtm.LockSize(true);
            temperatureAtm.Width = labelT.Width;
            temperatureAtm.LockSize(true);
            velocityWind.Width = labelT.Width;
            velocityWind.LockSize(true);
            angleWind.Width = labelT.Width;
            angleWind.LockSize(true);
        }

        private void rectangleShapeFull_EnabledChanged(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
                c.Enabled = Enabled;
        }

        bool selfWindAngleChange = false;

        private void angleSelector1_AngleChanged()
        {
            if (!selfWindAngleChange)
            {
                try
                {
                    selfWindAngleChange = true;
                    Angle.Unit u = angleWind.Unit;
                    Angle t = new Angle(angleSelector1.Angle, Angle.Unit.Degree);
                    angleWind.Value = t.ToUnit(u);
                }
                finally
                {
                    selfWindAngleChange = false;
                }
            }
        }

        private void angleWind_ValueChanged(object sender, EventArgs e)
        {
            if (!selfWindAngleChange)
            {
                try
                {
                    selfWindAngleChange = true;
                    angleSelector1.Angle = (int)(Math.Round(angleWind.Value.Get(Angle.Unit.Degree)));
                }
                finally
                {
                    selfWindAngleChange = false;
                }
            }
        }

        public void AddControlsToFormState(FormState state)
        {
            AddControlsToFormState(state, "");
        }

        public void AddControlsToFormState(FormState state, string prefix)
        {
            state.AddControl(prefix + "atm-altitude", distanceAltitude);
            state.AddControl(prefix + "atm-pressure", pressureAtm);
            state.AddControl(prefix + "atm-temperature", temperatureAtm);
            state.AddControl(prefix + "atm-humidity", numericHumidity);
            state.AddControl(prefix + "atm-wind-speed", velocityWind);
            state.AddControl(prefix + "atm-wind-direction-1", angleWind);
            state.AddControl(prefix + "atm-wind-direction-2", angleSelector1);
        }
    }
}
