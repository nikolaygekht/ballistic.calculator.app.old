using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class CustomTemperatureControl : CustomControlValue
    {
        new public Temperature Value
        {
            set
            {
                base.Value = value.Get(value.SetUnit);
                base.UnitName = Temperature.UnitToName(value.SetUnit);
                mOldUnit = value.SetUnit;
            }
            get
            {
                double v = base.Value;
                Temperature.Unit u = Temperature.NameToUnit(base.UnitName);
                return new Temperature(v, u);
            }
        }

        public CustomTemperatureControl()
        {
            InitializeComponent();
            string[] units = new string[] {
                Temperature.UnitToName(Temperature.Unit.Fahrenheit),
                Temperature.UnitToName(Temperature.Unit.Celsius),
            };
            base.Units = units;
        }

        private Temperature.Unit mOldUnit = Temperature.Unit.Fahrenheit;

        public Temperature.Unit Unit
        {
            get
            {
                return mOldUnit;
            }
            set
            {
                double v = base.Value;
                base.Value = Temperature.Convert(v, mOldUnit, value);
                base.UnitName = Temperature.UnitToName(value);
                mOldUnit = value;
            }
        }

        protected override void  OnUnitChanged()
        {
            Unit = Temperature.NameToUnit(base.UnitName);
        }

    }
}
