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
    public partial class CustomDistanceControl : CustomControlValue
    {
        new public Distance Value
        {
            set
            {
                base.Value = value.Get(value.SetUnit);
                base.UnitName = Distance.UnitToName(value.SetUnit);
                mOldUnit = value.SetUnit;
            }
            get
            {
                double v = base.Value;
                Distance.Unit u = Distance.NameToUnit(base.UnitName);
                return new Distance(v, u);
            }
        }

        public CustomDistanceControl()
        {
            Value = new Distance(0, Distance.Unit.Yard);
            InitializeComponent();
            string[] units = new string[] {
                Distance.UnitToName(Distance.Unit.Inch),
                Distance.UnitToName(Distance.Unit.Foot),
                Distance.UnitToName(Distance.Unit.Yard),
                Distance.UnitToName(Distance.Unit.Mile),
                Distance.UnitToName(Distance.Unit.Millimeter),
                Distance.UnitToName(Distance.Unit.Centimeter),
                Distance.UnitToName(Distance.Unit.Meter),
                Distance.UnitToName(Distance.Unit.Kilometer),
            };
            base.Units = units;
        }

        private Distance.Unit mOldUnit = Distance.Unit.Yard;

        public Distance.Unit Unit
        {
            get
            {
                return mOldUnit;
            }
            set
            {
                double v = base.Value;
                base.Value = Distance.Convert(v, mOldUnit, value);
                base.UnitName = Distance.UnitToName(value);
                mOldUnit = value;
            }
        }

        protected override void  OnUnitChanged()
        {
            Unit = Distance.NameToUnit(base.UnitName);
        }


    }
}
