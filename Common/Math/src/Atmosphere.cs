using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public class AtmosphereInfo
    {
        private Distance mAltitude = new Distance(0, Distance.Unit.Foot);
        /** Altitude over the sea level. */
        public Distance Altitude
        {
            get
            {
                return mAltitude;
            }
            set
            {
                mAltitude = value;
            }
        }


        private Pressure mPressure = new Pressure(29.53, Pressure.Unit.InchHg);

        public Pressure Pressure
        {
            get
            {
                return mPressure;
            }
            set
            {
                mPressure = value;
            }
        }

        private Temperature mTemperature = new Temperature(59, Temperature.Unit.Fahrenheit);

        public Temperature Temperature
        {
            get
            {
                return mTemperature;
            }
            set
            {
                mTemperature = value;
            }
        }


        private double mHumidity = 0.78;

        public double Humidity
        {
            get
            {
                return mHumidity;
            }
            set
            {
                mHumidity = value;
            }
        }

        public AtmosphereInfo()
        {
        }

        public AtmosphereInfo(Distance altitude, Pressure pressure, Temperature temperature, double humidity)
        {
            mAltitude = altitude;
            mPressure = pressure;
            mTemperature = temperature;
            mHumidity = humidity;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            if (mAltitude != null)
            {
                b.Append("Altitude=");
                b.Append(mAltitude.ToString(mAltitude.SetUnit));
                b.AppendLine();
            }
            if (mTemperature != null)
            {
                b.Append("Temperature=");
                b.Append(mTemperature.ToString(mTemperature.SetUnit));
                b.AppendLine();
            }
            if (mPressure != null)
            {
                b.Append("Pressure=");
                b.Append(mPressure.ToString(mPressure.SetUnit));
                b.AppendLine();
            }
            b.Append("Humidity=");
            b.Append((mHumidity * 100).ToString("0"));
            b.Append('%', 1);
            b.AppendLine();
            return b.ToString();
        }
    }
}