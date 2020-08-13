using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Model;
using System;

namespace Gehtsoft.BallisticCalculator.Utils
{

    class UnitsBase
    {
        protected static bool IsImperial
        {
            get
            {
                return BallisticDataProvider.Instance.MeasurementSystem 
                    == MeasurementSystem.Imperial;
            }
        }
    }

    class DefaultUnits : UnitsBase
    {
        public class Bullet : UnitsBase
        {
            public static MathEx.ExternalBallistic.Units.Distance.Unit Size
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
                    else
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Millimeter;
                }
            }

            public static MathEx.ExternalBallistic.Units.Velocity.Unit Velocity
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Velocity.Unit.FeetPerSecond;
                    else
                        return MathEx.ExternalBallistic.Units.Velocity.Unit.MeterPerSecond;
                }
            }

            public static MathEx.ExternalBallistic.Units.Weight.Unit Weight
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Weight.Unit.Grain;
                    else
                        return MathEx.ExternalBallistic.Units.Weight.Unit.Gram;
                }
            }

            public static MathEx.ExternalBallistic.Units.Energy.Unit Energy
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Energy.Unit.FootPounds;
                    else
                        return MathEx.ExternalBallistic.Units.Energy.Unit.Joule;
                }
            }
        }

        public class Reticle : UnitsBase
        {
            public static MathEx.ExternalBallistic.Units.Angle.Unit Adjustment
            {
                get
                {
                    return MathEx.ExternalBallistic.Units.Angle.Unit.MilDot;
                }
            }

            public static MathEx.ExternalBallistic.Units.Angle.Unit Elevation
            {
                get
                {
                    return MathEx.ExternalBallistic.Units.Angle.Unit.Radian;
                }
            }
        }

        public class Atmosphere : UnitsBase
        {
            public static MathEx.ExternalBallistic.Units.Pressure.Unit Pressure
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Pressure.Unit.InchHg;
                    else
                        return MathEx.ExternalBallistic.Units.Pressure.Unit.hPa;
                }
            }

            public static MathEx.ExternalBallistic.Units.Temperature.Unit Temperature
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Temperature.Unit.Fahrenheit;
                    else
                        return MathEx.ExternalBallistic.Units.Temperature.Unit.Celsius;
                }
            }
        }

        public class Wind : UnitsBase
        {
            public static MathEx.ExternalBallistic.Units.Velocity.Unit Velocity
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Velocity.Unit.MilesPerHour;
                    else
                        return MathEx.ExternalBallistic.Units.Velocity.Unit.MeterPerSecond;
                }
            }

            public static MathEx.ExternalBallistic.Units.Angle.Unit Angle
            {
                get
                {
                    return MathEx.ExternalBallistic.Units.Angle.Unit.Degree;
                }
            }
        }

        public class Target : UnitsBase
        {
            public static MathEx.ExternalBallistic.Units.Weight.Unit Weight
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Weight.Unit.Pound;
                    else
                        return MathEx.ExternalBallistic.Units.Weight.Unit.Kilogram;
                }
            }

            public static MathEx.ExternalBallistic.Units.Distance.Unit Size
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
                    else
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Centimeter;
                }
            }
        }

        public class Zero : UnitsBase
        {
            public static MathEx.ExternalBallistic.Units.Distance.Unit Sight
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
                    else
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Centimeter;
                }
            }

            public static MathEx.ExternalBallistic.Units.Distance.Unit Distance
            {
                get
                {
                    if (IsImperial)
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Yard;
                    else
                        return MathEx.ExternalBallistic.Units.Distance.Unit.Meter;
                }
            }
        }

        public static MathEx.ExternalBallistic.Units.Distance.Unit Altitude
        {
            get
            {
                if (IsImperial)
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Foot;
                else
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Meter;
            }
        }

        public static MathEx.ExternalBallistic.Units.Distance.Unit Range
        {
            get
            {
                if (IsImperial)
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Yard;
                else
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Meter;
            }
        }

        public static MathEx.ExternalBallistic.Units.Distance.Unit Drop
        {
            get
            {
                if (IsImperial)
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
                else
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Centimeter;
            }
        }

        public static MathEx.ExternalBallistic.Units.Distance.Unit Windage
        {
            get
            {
                if (IsImperial)
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
                else
                    return MathEx.ExternalBallistic.Units.Distance.Unit.Centimeter;
            }
        }
    }
}