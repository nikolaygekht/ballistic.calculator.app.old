using System;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Utils
{
    public interface IUnitsAdapter
    {
        double Get(string unit);
        void Set(double value, string unit);

        int DefaultDisplayPrecision();
        string GetDefaultUnit();

        string CurrentUnit();
        double CurrentValue();
        void ChangeUnit(string unit);

        event EventHandler ValueChanged;
    }

    class AngleAdapter : Angle, IUnitsAdapter
    {
        private Angle.Unit _currentUnit;

        public AngleAdapter(Angle angle)
            : base(angle.Get(angle.SetUnit), angle.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public event EventHandler ValueChanged;

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Angle.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Angle.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Angle.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Angle.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }

    class DistanceAdapter : Distance, IUnitsAdapter
    {
        private Distance.Unit _currentUnit;

        public DistanceAdapter(Distance distance)
            : base(distance.Get(distance.SetUnit), distance.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public event EventHandler ValueChanged;

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Distance.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Distance.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Distance.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Distance.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }

    class EnergyAdapter : Energy, IUnitsAdapter
    {
        private Energy.Unit _currentUnit;

        public EnergyAdapter(Energy energy)
            : base(energy.Get(energy.SetUnit), energy.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public event EventHandler ValueChanged;

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Energy.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Energy.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Energy.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Energy.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }

    class PressureAdapter : Pressure, IUnitsAdapter
    {
        private Pressure.Unit _currentUnit;

        public event EventHandler ValueChanged;

        public PressureAdapter(Pressure pressure)
            : base(pressure.Get(pressure.SetUnit), pressure.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Pressure.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Pressure.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Pressure.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Pressure.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }

    class TemperatureAdapter : Temperature, IUnitsAdapter
    {
        private Temperature.Unit _currentUnit;

        public event EventHandler ValueChanged;

        public TemperatureAdapter(Temperature temperature)
            : base(temperature.Get(temperature.SetUnit), temperature.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Temperature.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Temperature.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Temperature.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Temperature.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }

    class VelocityAdapter : Velocity, IUnitsAdapter
    {
        private Velocity.Unit _currentUnit;

        public event EventHandler ValueChanged;

        public VelocityAdapter(Velocity velocity)
            : base(velocity.Get(velocity.SetUnit), velocity.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Velocity.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Velocity.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Velocity.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Velocity.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }

    class WeightAdapter : Weight, IUnitsAdapter
    {
        private Weight.Unit _currentUnit;

        public WeightAdapter(Weight weight)
            : base(weight.Get(weight.SetUnit), weight.SetUnit)
        {
            _currentUnit = SetUnit;
        }

        public event EventHandler ValueChanged;

        public double Get(string unit)
        {
            return Get(NameToUnit(unit));
        }

        public void Set(double value, string unit)
        {
            Set(value, _currentUnit);
            _currentUnit = Weight.NameToUnit(unit);
            OnValueChanged();
        }

        public int DefaultDisplayPrecision()
        {
            return DefaultDisplayPrecision(_currentUnit);
        }

        public string GetDefaultUnit()
        {
            return UnitToName(Weight.DefaultUnit);
        }

        public string CurrentUnit()
        {
            return Weight.UnitToName(_currentUnit);
        }

        public double CurrentValue()
        {
            return Get(_currentUnit);
        }

        public void ChangeUnit(string unit)
        {
            _currentUnit = Weight.NameToUnit(unit);
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }
}
