using System;
using System.Globalization;

namespace MathEx.ExternalBallistic.Units
{
    public class Weight
    {
        private double mValue;            //value in grain
        static private Weight mConvertor = new Weight(0, Unit.Grain);
        static private object mMutex = new object();

        public enum Unit
        {
            Grain,
            Gram,
            Pound,
            Kilogram,
        }

        public Weight(double value, Unit unit)
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
            case    Unit.Grain:
                    mValue = value;
                    break;
            case    Unit.Gram:
                    mValue = value * 15.4323584;
                    break;
            case    Unit.Kilogram:
                    mValue = value * 1000 * 15.4323584;
                    break;
            case    Unit.Pound:
                    mValue = value / 0.000142857143;
                    break;
            }
        }

        public double Get(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Grain:
                    return mValue;
            case    Unit.Gram:
                    return mValue / 15.4323584;
            case    Unit.Kilogram:
                    return mValue / 15.4323584 / 1000;
            case    Unit.Pound:
                    return mValue * 0.000142857143;
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
            case    Unit.Grain:
                    return "gr";
            case    Unit.Gram:
                    return "g";
            case    Unit.Kilogram:
                    return "kg";
            case    Unit.Pound:
                    return "lb";
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit NameToUnit(string name)
        {
            if (name == "gr")
                return Unit.Grain;
            else if (name == "g")
                return Unit.Gram;
            else if (name == "kg")
                return Unit.Kilogram;
            else if (name == "lb")
                return Unit.Pound;
            throw new ArgumentException("Unknown unit");
        }

        static public int DefaultDisplayPrecision(Unit unit)
        {
            switch (unit)
            {
            case    Unit.Grain:
                    return 0;
            case    Unit.Gram:
                    return 1;
            case    Unit.Pound:
                    return 3;
            case    Unit.Kilogram:
                    return 3;
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit DefaultUnit
        {
            get
            {
                return Unit.Grain;
            }
        }

        public Weight ToUnit(Unit unit)
        {
            return new Weight(Get(unit), unit);
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


        static public bool TryParse(string text, out Weight value, out Unit unit)
        {
            return TryParse(text, CultureInfo.CurrentCulture, out value, out unit);
        }

        static public bool TryParse(string text, CultureInfo culture, out Weight value, out Unit unit)
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

            value = new Weight(v, unit);
            return true;
        }

    }
}