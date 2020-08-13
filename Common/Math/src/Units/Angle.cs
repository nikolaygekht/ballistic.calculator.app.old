using System;
using System.Globalization;

namespace MathEx.ExternalBallistic.Units
{
    public class Angle
    {
        //angle in radians
        private double mValue;
        private static Angle mConvertor = new Angle(0, Unit.Radian);
        private static object mMutex = new object();

        public enum Unit
        {
            Radian,
            Degree,
            Moa,
            Mil,
            MilDot,
            AMil,
            InPer100Yards,
            CmPer100Meters,
        }

        public Angle(double value, Unit unit)
        {
            Set(value, unit);
        }

        public double Get(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Radian:
                    return mValue;
            case    Unit.Degree:
                    return mValue * 180 / Math.PI;
            case    Unit.MilDot:
                    return mValue * 3200 / Math.PI;
            case    Unit.AMil:
                    return mValue * 3000 / Math.PI;
            case    Unit.Moa:
                    return mValue * 60 * 180 / Math.PI;
            case    Unit.Mil:
                    return mValue * 1000;
            case    Unit.InPer100Yards:
                    return Math.Tan(mValue) * 300 * 12;
            case    Unit.CmPer100Meters:
                    return Math.Tan(mValue) * 100 * 100;

            }
            throw new ArgumentException("Unknown unit");
        }

        private Unit mSetUnit;

        public Unit SetUnit
        {
            get
            {
                return mSetUnit;
            }
        }

        protected void Set(double value, Unit unit)
        {
            mSetUnit = unit;
            switch (unit)
            {
            case    Unit.Radian:
                    mValue = value;
                    break;
            case    Unit.Degree:
                    mValue = value * Math.PI / 180;
                    break;
            case    Unit.MilDot:
                    mValue = value * Math.PI / 3200;
                    break;
            case    Unit.AMil:
                    mValue = value * Math.PI / 3000;
                    break;
            case    Unit.Moa:
                    mValue = value / 60 * Math.PI / 180;
                    break;
            case    Unit.Mil:
                    mValue = value / 1000;
                    break;
            case    Unit.InPer100Yards:
                    mValue = Math.Atan(value / 12 / 300);
                    break;
            case    Unit.CmPer100Meters:
                    mValue = Math.Atan(value / 100 / 100);
                    break;

            }
        }

        static public double Convert(double value, Unit from, Unit to)
        {
            lock (mMutex)
            {
                mConvertor.Set(value, from);
                return mConvertor.Get(to);
            }
        }

        static public string UnitToName(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Radian:
                    return "rad";
            case    Unit.Degree:
                    return "°";
            case    Unit.Moa:
                    return "moa";
            case    Unit.Mil:
                    return "mil";
            case    Unit.AMil:
                    return "am";
            case    Unit.MilDot:
                    return "mdot";
            case    Unit.InPer100Yards:
                    return "in/100y";
            case    Unit.CmPer100Meters:
                    return "cm/100m";

            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit NameToUnit(string name)
        {
            if (name == "rad")
                return Unit.Radian;
            else if (name == "deg" || name == "°")
                return Unit.Degree;
            else if (name == "moa")
                return Unit.Moa;
            else if (name == "mil")
                return Unit.Mil;
            else if (name == "am")
                return Unit.AMil;
            else if (name == "mdot")
                return Unit.MilDot;
            else if (name == "in/100y")
                return Unit.InPer100Yards;
            else if (name == "cm/100m")
                return Unit.CmPer100Meters;

            throw new ArgumentException("Unknown unit");
        }

        static public int DefaultDisplayPrecision(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Radian:
                    return 6;
            case    Unit.Degree:
                    return 1;
            case    Unit.Moa:
                    return 2;
            case    Unit.Mil:
                    return 1;
            case    Unit.MilDot:
                    return 1;
            case    Unit.AMil:
                    return 1;
            case    Unit.InPer100Yards:
                    return 1;
            case    Unit.CmPer100Meters:
                    return 1;
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit DefaultUnit
        {
            get
            {
                return Unit.Radian;
            }
        }

        public Angle ToUnit(Unit unit)
        {
            return new Angle(Get(unit), unit);
        }


        override public string ToString()
        {
            return ToString(DefaultUnit, true, true, DefaultDisplayPrecision(DefaultUnit));
        }

        public string ToString(CultureInfo culture)
        {
            return ToString(DefaultUnit, true, true, DefaultDisplayPrecision(DefaultUnit), culture);
        }

        public string ToString(Unit unit, bool unitName)
        {
            return ToString(unit, unitName, true, DefaultDisplayPrecision(unit));
        }

        public string ToString(Unit unit, bool unitName, CultureInfo culture)
        {
            return ToString(unit, unitName, true, DefaultDisplayPrecision(unit), culture);
        }

        public string ToString(Unit unit, bool unitName, bool groupThousands, int precision)
        {
            return ToString(unit, unitName, groupThousands, precision, CultureInfo.CurrentCulture);
        }

        public string ToString(Unit unit, bool unitName, bool groupThousands, int precision, CultureInfo culture)
        {
            NumberFormatInfo fi = culture.NumberFormat.Clone() as NumberFormatInfo;
            fi.NumberDecimalDigits = precision;
            if (groupThousands)
                fi.NumberGroupSizes = new int[] {3, 0};
            else
                fi.NumberGroupSizes = new int[] {0};
            string s = Get(unit).ToString("N", fi);
            if (unitName)
                return s + UnitToName(unit);
            else
                return s;
        }


        public string ToString(Unit unit)
        {
            return ToString(unit, true);
        }

        public string ToString(Unit unit, CultureInfo culture)
        {
            return ToString(unit, true, culture);
        }

        public string ToString(Unit unit, bool groupThousands, int precision)
        {
            return ToString(unit, true, groupThousands, precision, CultureInfo.CurrentCulture);
        }

        public string ToString(Unit unit, bool groupThousands, int precision, CultureInfo culture)
        {
            return ToString(unit, true, groupThousands, precision, culture);
        }

        static public bool TryParse(string text, out Angle value, out Unit unit)
        {
            return TryParse(text, CultureInfo.CurrentCulture, out value, out unit);
        }

        static public bool TryParse(string text, CultureInfo culture, out Angle value, out Unit unit)
        {
            value = null;
            unit = DefaultUnit;

            //split value to text and unit name
            string number = null;
            string name = null;
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(text[i]))
                {
                    name = text.Substring(i + 1);
                    number = text.Substring(0, i + 1);
                    break;
                }
            }

            if (name == null || name.Length == 0)
                return false;

            try
            {
                unit = NameToUnit(name);
            }
            catch (Exception )
            {
                return false;
            }

            double v;
            if (!Double.TryParse(number, NumberStyles.Float | NumberStyles.AllowThousands, culture.NumberFormat, out v))
                return false;

            value = new Angle(v, unit);
            return true;
        }
    };
}