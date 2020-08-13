using System;
using System.Globalization;

namespace MathEx.ExternalBallistic.Units
{
    public class Temperature
    {
        private double mValue;            //value in Fahrenheit
        static private Temperature mConvertor = new Temperature(0, Unit.Fahrenheit);
        static private object mMutex = new object();

        public enum Unit
        {
            Celsius,
            Fahrenheit,
        }

        public Temperature(double value, Unit unit)
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
            case    Unit.Fahrenheit:
                    mValue = value;
                    break;
            case    Unit.Celsius:
                    mValue = value * 9 / 5 + 32;
                    break;
            }
        }

        public double Get(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Fahrenheit:
                    return mValue;
            case    Unit.Celsius:
                    return (mValue - 32) * 5 / 9;
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
            case    Unit.Fahrenheit:
                    return "°F";
            case    Unit.Celsius:
                    return "°C";
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit NameToUnit(string name)
        {
            if (name == "°F")
                return Unit.Fahrenheit;
            else if (name == "°C")
                return Unit.Celsius;
            throw new ArgumentException("Unknown unit");
        }

        static public int DefaultDisplayPrecision(Unit unit)
        {
            return 0;
        }

        static public Unit DefaultUnit
        {
            get
            {
                return Unit.Fahrenheit;
            }
        }

        public Temperature ToUnit(Unit unit)
        {
            return new Temperature(Get(unit), unit);
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


        static public bool TryParse(string text, out Temperature value, out Unit unit)
        {
            return TryParse(text, CultureInfo.CurrentCulture, out value, out unit);
        }

        static public bool TryParse(string text, CultureInfo culture, out Temperature value, out Unit unit)
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

            value = new Temperature(v, unit);
            return true;
        }

    }
}