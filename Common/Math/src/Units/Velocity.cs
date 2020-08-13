using System;
using System.Globalization;

namespace MathEx.ExternalBallistic.Units
{
    public class Velocity
    {
        private double mValue;            //value in m / s
        static private Velocity mConvertor = new Velocity(0, Unit.MeterPerSecond);
        static private object mMutex = new object();

        public enum Unit
        {
            MeterPerSecond,
            KilometersPerHour,
            FeetPerSecond,
            MilesPerHour,
        }

        public Velocity(double value, Unit unit)
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
            case    Unit.MeterPerSecond:
                    mValue = value;
                    break;
            case    Unit.KilometersPerHour:
                    mValue = value / 3.6;
                    break;
            case    Unit.FeetPerSecond:
                    mValue = value / 3.2808399;
                    break;
            case    Unit.MilesPerHour:
                    mValue = value / 2.23693629;
                    break;
            }
        }

        public double Get(Unit unit)
        {
            switch (unit)
            {
            case    Unit.MeterPerSecond:
                    return mValue;
            case    Unit.KilometersPerHour:
                    return mValue * 3.6;
            case    Unit.FeetPerSecond:
                    return mValue * 3.2808399;
            case    Unit.MilesPerHour:
                    return mValue * 2.23693629;
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
            case    Unit.MeterPerSecond:
                    return "m/s";
            case    Unit.KilometersPerHour:
                    return "km/h";
            case    Unit.FeetPerSecond:
                    return "ft/s";
            case    Unit.MilesPerHour:
                    return "mi/h";
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit NameToUnit(string name)
        {
            if (name == "m/s")
                return Unit.MeterPerSecond;
            else if (name == "km/h")
                return Unit.KilometersPerHour;
            else if (name == "ft/s")
                return Unit.FeetPerSecond;
            else if (name == "mi/h")
                return Unit.MilesPerHour;
            throw new ArgumentException("Unknown unit");
        }

        static public int DefaultDisplayPrecision(Unit unit)
        {
            switch (unit)
            {
            case    Unit.MeterPerSecond:
                    return 0;
            case    Unit.KilometersPerHour:
                    return 1;
            case    Unit.FeetPerSecond:
                    return 0;
            case    Unit.MilesPerHour:
                    return 1;
            }
            throw new ArgumentException("Unknown unit");
        }

        static public Unit DefaultUnit
        {
            get
            {
                return Unit.MeterPerSecond;
            }
        }

        public Velocity ToUnit(Unit unit)
        {
            return new Velocity(Get(unit), unit);
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


        static public bool TryParse(string text, out Velocity value, out Unit unit)
        {
            return TryParse(text, CultureInfo.CurrentCulture, out value, out unit);
        }

        static public bool TryParse(string text, CultureInfo culture, out Velocity value, out Unit unit)
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

            value = new Velocity(v, unit);
            return true;
        }

    }
}