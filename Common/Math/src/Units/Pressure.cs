using System;
using System.Globalization;

namespace MathEx.ExternalBallistic.Units
{
    public class Pressure
    {
        private double mValue;            //value in mmHg
        static private Pressure mConvertor = new Pressure(0, Unit.MmHg);
        static private object mMutex = new object();

        public enum Unit
        {
            MmHg,
            InchHg,
            Bar,
            hPa,
        }

        public Pressure(double value, Unit unit)
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
            case    Unit.MmHg:
                    mValue = value;
                    break;
            case    Unit.InchHg:
                    mValue = value * 25.4;
                    break;
            case    Unit.Bar:
                    mValue = value * 750.061683;
                    break;
            case    Unit.hPa:
                    mValue = value * 750.061683 / 1000;
                    break;
            }
        }

        public double Get(Unit unit)
        {
            switch (unit)
            {
            case    Unit.MmHg:
                    return mValue;
            case    Unit.InchHg:
                    return mValue / 25.4;
            case    Unit.Bar:
                    return mValue / 750.061683;
            case    Unit.hPa:
                    return mValue / 750.061683 * 1000;

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
            case    Unit.MmHg:
                    return "mmHg";
            case    Unit.InchHg:
                    return "inHg";
            case    Unit.Bar:
                    return "bar";
                case Unit.hPa:
                    return "hPa";

            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit NameToUnit(string name)
        {
            if (name == "mmHg")
                return Unit.MmHg;
            else if (name == "inHg")
                return Unit.InchHg;
            else if (name == "bar")
                return Unit.Bar;
            else if (name == "hPa")
                return Unit.hPa;
            throw new ArgumentException("Unknown unit");
        }

        static public int DefaultDisplayPrecision(Unit unit)
        {
            switch (unit)
            {
            case    Unit.MmHg:
                    return 0;
            case    Unit.hPa:
                    return 0;
            case    Unit.InchHg:
                    return 2;
            case    Unit.Bar:
                    return 4;
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit DefaultUnit
        {
            get
            {
                return Unit.MmHg;
            }
        }

        public Pressure ToUnit(Unit unit)
        {
            return new Pressure(Get(unit), unit);
        }

        public Pressure ToUnit(Unit unit, int precision)
        {
            return new Pressure(Math.Round(Get(unit), precision), unit);
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

        static public bool TryParse(string text, out Pressure value, out Unit unit)
        {
            return TryParse(text, CultureInfo.CurrentCulture, out value, out unit);
        }

        static public bool TryParse(string text, CultureInfo culture, out Pressure value, out Unit unit)
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

            value = new Pressure(v, unit);
            return true;
        }

    }
}