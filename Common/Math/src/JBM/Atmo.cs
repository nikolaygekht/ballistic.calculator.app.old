using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.JBM
{
    public class Atmo
    {
        private double mTemperature;   /* temperature in °F */
        private double mPressure;      /* pressure in in Hg    */
        private double mHumidity;      /* relative humdity     */
        private double mAltitude;      /* altitude in feet     */
        private double mMach;          /* mach 1.0 in feet/sec */
        private double mDensity;       /* atmospheric density  */

        private const double ATMOS_TEMPSTD = 59.0;
        private const double ATMOS_PRESSSTD = 29.92;
        private const double ATMOS_HUMSTD = 0.0;
        private const double ATMOS_ALTSTD = 0.0;
        private const double ATMOS_MACHSTD = 1116.4499;
        internal const double ATMOS_DENSSTD = 0.076474;

        public double Pressure
        {
            get
            {
                return mPressure;
            }
        }

        public double Humidity
        {
            get
            {
                return mHumidity;
            }
        }

        public double Altitude
        {
            get
            {
                return mAltitude;
            }
        }

        public double Mach
        {
            get
            {
                return mMach;
            }
        }

        public double Density
        {
            get
            {
                return mDensity;
            }
        }

        public Atmo(double temperature, double pressure, double humidity, double altitude)
        {
            mTemperature = temperature;
            mPressure = pressure;
            mHumidity = humidity;
            mAltitude = altitude;
            calculate();
        }

        /** Default atmosphere at 0 ft above the Sea level */
        public Atmo()
        {
            mTemperature = ATMOS_TEMPSTD;
            mPressure = ATMOS_PRESSSTD;
            mHumidity = ATMOS_HUMSTD;
            mAltitude = ATMOS_ALTSTD;
            calculate();
        }

        /** Standard ICAO atmosphere at specified altitude */
        public Atmo(double altitude)
        {
            mAltitude = altitude;
            mTemperature = ATMOS_TSTDABS + altitude * ATMOS_TEMPGRAD;
            mPressure = ATMOS_PRESSSTD * System.Math.Pow(ATMOS_TSTDABS / mTemperature, ATMOS_PRESSEXPNT);
            mTemperature = mTemperature - ATMOS_T0; /* line above need absolute! */
            mHumidity = ATMOS_HUMSTD;
            calculate();
        }

        private const double ATMOS_TSTDABS = 518.67;        /* Standard Temperature - °R */
        private const double ATMOS_T0 = 459.67;             /* Freezing Point       - °R */
        private const double ATMOS_TEMPGRAD = -3.56616e-03; /* Temperature Gradient - °F/ft */
        private const double ATMOS_PRESSEXPNT = -5.255876;  /* Pressure Exponent    - none */
        private const double ATMOS_VV1 = 49.0223;           /* Sound Speed coefficient */
        private const double ATMOS_A0 = 1.24871;            /* Vapor Pressure coefficients */
        private const double ATMOS_A1 = 0.0988438;
        private const double ATMOS_A2 = 0.00152907;
        private const double ATMOS_A3 = -3.07031e-06;
        private const double ATMOS_A4 = 4.21329e-07;
        private const double ATMOS_ETCONV = 3.342e-04;

        private void calculate()
        {
            double t, p, hc, et, et0;
            t = mTemperature;
            p = mPressure;
            if (t > 0.0)
            {
                et0 = ATMOS_A0 + t * (ATMOS_A1 + t * (ATMOS_A2 + t * (ATMOS_A3 + t * ATMOS_A4)));
                et = ATMOS_ETCONV * mHumidity * et0;
                hc = (p - 0.3783 * et) / ATMOS_PRESSSTD;
            }
            else
                hc = 1.0;
            mDensity = ATMOS_DENSSTD * (ATMOS_TSTDABS / (t + ATMOS_T0)) * hc;
            mMach = System.Math.Sqrt(t + ATMOS_T0) * ATMOS_VV1;
        }
    }

}
