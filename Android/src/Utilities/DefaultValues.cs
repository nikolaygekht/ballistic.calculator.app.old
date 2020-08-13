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
using Gehtsoft.BallisticCalculator.Connectivity;
using Gehtsoft.BallisticCalculator.Model;
using MathEx.ExternalBallistic.Units;
using Gehtsoft.BallisticCalculator.Utils;
using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.DataProviders;

namespace Gehtsoft.BallisticCalculator.Utils
{
    static class DefaultValues
    {
        private static bool IsImperial
        {
            get
            {
                return BallisticDataProvider.Instance.MeasurementSystem
                    == MeasurementSystem.Imperial;
            }
        }

        public static TraceInfo CreateTraceInfo()
        {
                var traceInfo = new TraceInfo();
                traceInfo.Metric = (IsImperial == false);

                traceInfo.BallisticCoefficient = 0.5;
                traceInfo.BulletWeight = new Weight(0, DefaultUnits.Bullet.Weight);
                traceInfo.MuzzleVelocity = new Velocity(0, DefaultUnits.Bullet.Velocity);

                traceInfo.SightHeight = 
                traceInfo.Metric ? new Distance(5, Distance.Unit.Centimeter) : new Distance(1.5, Distance.Unit.Inch);
                traceInfo.ZeroDistance = 
                traceInfo.Metric ? new Distance(91.44, Distance.Unit.Meter) : new Distance(100, Distance.Unit.Yard);

                traceInfo.ZeroElevationAngle = new Angle(0, Angle.DefaultUnit);

                traceInfo.DriftInfo = false;
                traceInfo.BulletLength = new Distance(0, DefaultUnits.Bullet.Size);  
                traceInfo.BulletDiameter = new Distance(0, DefaultUnits.Bullet.Size);
                traceInfo.RiflingTwist = new Distance(0, DefaultUnits.Bullet.Size);
                traceInfo.RiflingRightHandTwist = true;

                traceInfo.VerticalClick = new Angle(0.25, Angle.Unit.Moa);
                traceInfo.HorizonalClick = new Angle(0.25, Angle.Unit.Moa);

                return traceInfo;
           
        }

        public static AtmosphereInfo CreateAtmosphereInfo()
        {
            if (IsImperial)
            {
                var altitude = new Distance(0, DefaultUnits.Altitude);
                var pressure = new Pressure(29.53, Pressure.Unit.InchHg);
                var temperature = new Temperature(59, Temperature.Unit.Fahrenheit);
                return new AtmosphereInfo(altitude, pressure, temperature, 0.78);
            }
            else
            {
                var altitude = new Distance(0, DefaultUnits.Altitude);
                var pressure = new Pressure(1000, Pressure.Unit.hPa);
                var temperature = new Temperature(15, Temperature.Unit.Celsius);
                return new AtmosphereInfo(altitude, pressure, temperature, 0.78);
            }
        }

        public static Distance CreateShotStep()
        {
            Distance step;
            if (IsImperial)
            {
                step = new Distance(25, Distance.Unit.Yard);
            }
            else
            {
                step = new Distance(22.86, Distance.Unit.Meter);
            }

            return step;
        }

        public static WindInfo CreateWindInfo()
        {
            var angle = new Angle(0, DefaultUnits.Wind.Angle);
            var velocity = new Velocity(0, DefaultUnits.Wind.Velocity);
            return new WindInfo(angle, velocity);
        }

        public static Angle CreateAngleWithDefaultValue()
        {
            return new Angle(0, Angle.Unit.Degree);
        }

        public static Distance CreateDistanceeWithDefaultValue(int defaultValue = 0)
        {
            return new Distance(defaultValue, Distance.Unit.Yard);
        }
    }
}