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
    public partial class CustomWeightControl : CustomControlValue
    {
        new public Weight Value
        {
            set
            {
                base.Value = value.Get(value.SetUnit);
                base.UnitName = Weight.UnitToName(value.SetUnit);
                mOldUnit = value.SetUnit;
            }
            get
            {
                double v = base.Value;
                Weight.Unit u = Weight.NameToUnit(base.UnitName);
                return new Weight(v, u);
            }
        }

        public CustomWeightControl()
        {
            Value = new Weight(0, Weight.Unit.Grain);
            InitializeComponent();
            string[] units = new string[] {
                Weight.UnitToName(Weight.Unit.Grain),
                Weight.UnitToName(Weight.Unit.Gram),
            };
            base.Units = units;
        }

        private Weight.Unit mOldUnit = Weight.Unit.Grain;

        public Weight.Unit Unit
        {
            get
            {
                return mOldUnit;
            }
            set
            {
                double v = base.Value;
                base.Value = Weight.Convert(v, mOldUnit, value);
                base.UnitName = Weight.UnitToName(value);
                mOldUnit = value;
            }
        }

        protected override void  OnUnitChanged()
        {
            Unit = Weight.NameToUnit(base.UnitName);
        }

    }
}
