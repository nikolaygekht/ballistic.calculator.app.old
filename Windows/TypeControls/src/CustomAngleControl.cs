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
    public partial class CustomAngleControl : CustomControlValue
    {
        new public Angle Value
        {
            set
            {
                base.Value = value.Get(value.SetUnit);
                base.UnitName = Angle.UnitToName(value.SetUnit);
                mOldUnit = value.SetUnit;
            }
            get
            {
                double v = base.Value;
                Angle.Unit u = Angle.NameToUnit(base.UnitName);
                return new Angle(v, u);
            }
        }

        public CustomAngleControl()
        {
            InitializeComponent();
            string[] units = new string[] {
                Angle.UnitToName(Angle.Unit.Radian),
                Angle.UnitToName(Angle.Unit.Degree),
                Angle.UnitToName(Angle.Unit.Moa),
                Angle.UnitToName(Angle.Unit.Mil),
                Angle.UnitToName(Angle.Unit.MilDot),
                Angle.UnitToName(Angle.Unit.AMil),
                Angle.UnitToName(Angle.Unit.InPer100Yards),
                Angle.UnitToName(Angle.Unit.CmPer100Meters),
            };
            base.Units = units;
        }

        private Angle.Unit mOldUnit = Angle.Unit.Radian;

        public Angle.Unit Unit
        {
            get
            {
                return mOldUnit;
            }
            set
            {
                double v = base.Value;
                base.Value = Angle.Convert(v, mOldUnit, value);
                base.UnitName = Angle.UnitToName(value);
                mOldUnit = value;
            }
        }

        protected override void  OnUnitChanged()
        {
            Unit = Angle.NameToUnit(base.UnitName);
        }
    }
}
