using System;
using System.Globalization;

namespace MathEx.ExternalBallistic.Units
{
    public class Distance
    {
        private double mValue;      //distance in inches
        private static Distance mConvertor = new Distance(0, Unit.Inch);
        private static object mMutex = new object();

        public enum Unit
        {
            Inch,
            Foot,
            Yard,
            Mile,
            Millimeter,
            Centimeter,
            Meter,
            Kilometer,
        }

        public Distance(double value, Unit unit)
        {
            Set(value, unit);
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
            case    Unit.Inch:
                    mValue = value;
                    break;
            case    Unit.Foot:
                    mValue = value * 12;
                    break;
            case    Unit.Yard:
                    mValue = value * 36;
                    break;
            case    Unit.Mile:
                    mValue = value * 63360;
                    break;
            case    Unit.Millimeter:
                    mValue = value / 25.4;
                    break;
            case    Unit.Centimeter:
                    mValue = value / 2.54;
                    break;
            case    Unit.Meter:
                    mValue = value / 25.4 * 1000;
                    break;
            case    Unit.Kilometer:
                    mValue = value / 25.4 * 1000000;
                    break;
            }
        }

        public double Get(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Inch:
                    return mValue;
            case    Unit.Foot:
                    return mValue / 12;
            case    Unit.Yard:
                    return mValue / 36;
            case    Unit.Mile:
                    return mValue / 63360;
            case    Unit.Millimeter:
                    return mValue * 25.4;
            case    Unit.Centimeter:
                    return mValue * 2.54;
            case    Unit.Meter:
                    return mValue * 25.4 / 1000;
            case    Unit.Kilometer:
                    return mValue * 25.4 / 1000000;
            }
            throw new ArgumentException("Unknown unit");
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
            case    Unit.Inch:
                    return "in";
            case    Unit.Foot:
                    return "ft";
            case    Unit.Yard:
                    return "yd";
            case    Unit.Mile:
                    return "mi";
            case    Unit.Millimeter:
                    return "mm";
            case    Unit.Centimeter:
                    return "cm";
            case    Unit.Meter:
                    return "m";
            case    Unit.Kilometer:
                    return "km";
            }
            throw new ArgumentException("Unknown unit");

        }

        static public Unit NameToUnit(string name)
        {
            if (name == "in" || name == "\"")
                return Unit.Inch;
            else if (name == "ft" || name == "\'")
                return Unit.Foot;
            else if (name == "yd")
                return Unit.Yard;
            else if (name == "mi")
                return Unit.Mile;
            else if (name == "mm")
                return Unit.Millimeter;
            else if (name == "cm")
                return Unit.Centimeter;
            else if (name == "m")
                return Unit.Meter;
            else if (name == "km")
                return Unit.Kilometer;
            throw new ArgumentException("Unknown unit");
        }

        static public int DefaultDisplayPrecision(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Inch:
                    return 1;
            case    Unit.Foot:
                    return 2;
            case    Unit.Yard:
                    return 2;
            case    Unit.Mile:
                    return 3;
            case    Unit.Millimeter:
                    return 0;
            case    Unit.Centimeter:
                    return 1;
            case    Unit.Meter:
                    return 2;
            case    Unit.Kilometer:
                    return 3;
            }
            throw new ArgumentException("Unknown unit");

        }

        static public Unit DefaultUnit
        {
            get
            {
                return Unit.Inch;
            }
        }

        public Distance ToUnit(Unit unit)
        {
            return new Distance(Get(unit), unit);
        }

        public Distance ToUnit(Unit unit, int precision)
        {
            return new Distance(Math.Round(Get(unit), precision), unit);
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
                fi.NumberGroupSizes = new int[] { 3, 0 };
            else
                fi.NumberGroupSizes = new int[] { 0 };
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


        static public bool TryParse(string text, out Distance value, out Unit unit)
        {
            return TryParse(text, CultureInfo.CurrentCulture, out value, out unit);
        }

        static public bool TryParse(string text, CultureInfo culture, out Distance value, out Unit unit)
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

            value = new Distance(v, unit);
            return true;
        }
    }
}