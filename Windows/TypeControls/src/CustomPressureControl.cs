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
    public partial class CustomPressureControl : CustomControlValue
    {
        new public Pressure Value
        {
            set
            {
                base.Value = value.Get(value.SetUnit);
                base.UnitName = Pressure.UnitToName(value.SetUnit);
                mOldUnit = value.SetUnit;
            }
            get
            {
                double v = base.Value;
                Pressure.Unit u = Pressure.NameToUnit(base.UnitName);
                return new Pressure(v, u);
            }
        }

        public CustomPressureControl()
        {
            InitializeComponent();
            string[] units = new string[] {
                Pressure.UnitToName(Pressure.Unit.InchHg),
                Pressure.UnitToName(Pressure.Unit.MmHg),
                Pressure.UnitToName(Pressure.Unit.Bar),
                Pressure.UnitToName(Pressure.Unit.hPa),
            };
            base.Units = units;
        }

        private Pressure.Unit mOldUnit = Pressure.Unit.InchHg;

        public Pressure.Unit Unit
        {
            get
            {
                return mOldUnit;
            }
            set
            {
                double v = base.Value;
                base.Value = Pressure.Convert(v, mOldUnit, value);
                base.UnitName = Pressure.UnitToName(value);
                mOldUnit = value;
            }
        }

        protected override void  OnUnitChanged()
        {
            Unit = Pressure.NameToUnit(base.UnitName);
        }

    }
}
