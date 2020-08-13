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
    public partial class CustomVelocityControl : CustomControlValue
    {
        new public Velocity Value
        {
            set
            {
                base.Value = value.Get(value.SetUnit);
                base.UnitName = Velocity.UnitToName(value.SetUnit);
                mOldUnit = value.SetUnit;
            }
            get
            {
                double v = base.Value;
                Velocity.Unit u = Velocity.NameToUnit(base.UnitName);
                return new Velocity(v, u);
            }
        }

        public CustomVelocityControl()
        {
            InitializeComponent();
            string[] units = new string[] {
                Velocity.UnitToName(Velocity.Unit.FeetPerSecond),
                Velocity.UnitToName(Velocity.Unit.MilesPerHour),
                Velocity.UnitToName(Velocity.Unit.MeterPerSecond),
                Velocity.UnitToName(Velocity.Unit.KilometersPerHour),
            };
            base.Units = units;
        }

        private Velocity.Unit mOldUnit = Velocity.Unit.FeetPerSecond;

        public Velocity.Unit Unit
        {
            get
            {
                return mOldUnit;
            }
            set
            {
                double v = base.Value;
                base.Value = Velocity.Convert(v, mOldUnit, value);
                base.UnitName = Velocity.UnitToName(value);
                mOldUnit = value;
            }
        }

        protected override void  OnUnitChanged()
        {
            Unit = Velocity.NameToUnit(base.UnitName);
        }

    }
}
