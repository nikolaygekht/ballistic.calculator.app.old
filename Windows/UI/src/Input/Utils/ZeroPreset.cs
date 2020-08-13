using System;
using System.Collections.Generic;
using System.Text;
using MathEx.ExternalBallistic.Units;
using System.Windows.Forms;

namespace Gehtsoft.BallisticCalculator.UI
{
    class ZeroPreset
    {
        private string mName;
        private Distance mZeroHeight;
        private Distance mZeroDistance;
        private bool mNearZero;

        public string Name
        {
            get
            {
                return mName;
            }
        }

        public Distance ZeroHeight
        {
            get
            {
                return mZeroHeight;
            }
        }

        public Distance ZeroDistance
        {
            get
            {
                return mZeroDistance;
            }
        }

        public bool NearZero
        {
            get
            {
                return mNearZero;
            }
        }

        public ZeroPreset(string name, Distance height, Distance distance, bool nearZero)
        {
            mName = name;
            mZeroHeight = height;
            mZeroDistance = distance;
            mNearZero = nearZero;
        }

        public override string  ToString()
        {
 	        return mName;
        }
    }

    static class ZeroPresetController
    {
        private static ZeroPreset[] PRESETS = new ZeroPreset[] {
                    new ZeroPreset(".22LR Iron Sight/Pistol", new Distance(0.5, Distance.Unit.Inch), new Distance(25, Distance.Unit.Yard), false),
                    new ZeroPreset(".22LR Optics", new Distance(1.5, Distance.Unit.Inch), new Distance(25, Distance.Unit.Yard), false),
                    new ZeroPreset("Bolt Rifle Optics", new Distance(2, Distance.Unit.Inch), new Distance(100, Distance.Unit.Yard), false),
                    new ZeroPreset("AR-15 Iron Sight", new Distance(2.5, Distance.Unit.Inch), new Distance(25, Distance.Unit.Yard), true),
                    new ZeroPreset("AR-15 EoTech", new Distance(3, Distance.Unit.Inch), new Distance(25, Distance.Unit.Yard), true),
                    new ZeroPreset("AR-15 Elcan", new Distance(3.2, Distance.Unit.Inch), new Distance(100, Distance.Unit.Yard), false),
                    new ZeroPreset("AR-15 ACOG on Carry Handle", new Distance(3.5, Distance.Unit.Inch), new Distance(100, Distance.Unit.Yard), false),                   
                    new ZeroPreset("AK-47 Iron Sight", new Distance(5, Distance.Unit.Centimeter), new Distance(100, Distance.Unit.Meter), false),
                    new ZeroPreset("AK-47 Optics", new Distance(8, Distance.Unit.Centimeter), new Distance(100, Distance.Unit.Meter), false)
                    };


        public static void FillComboBox(ComboBox combo)
        {
            foreach (ZeroPreset zps in PRESETS)
                combo.Items.Add(zps);
        }
    }
}
