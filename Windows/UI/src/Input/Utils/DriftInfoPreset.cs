using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    class DriftInfoAmmoPreset
    {
        string mName;
        Distance mBulletDiameter;
        Distance mBulletLength;

        public Distance BulletDiameter
        {
            get
            {
                return mBulletDiameter;
            }
        }

        public Distance BulletLength
        {
            get
            {
                return mBulletLength;
            }
        }

        internal DriftInfoAmmoPreset(string name, Distance bulletDiameter, Distance bulletLength)
        {
            mName = name;
            mBulletDiameter = bulletDiameter;
            mBulletLength = bulletLength;
        }

        override public string ToString()
        {
            return mName;
        }
    }

    public static class DriftInfoAmmoPresetList
    {
        static DriftInfoAmmoPreset[] mDriftPresets = new DriftInfoAmmoPreset[]
        {
            new DriftInfoAmmoPreset(".22 LR", new Distance(0.223, Distance.Unit.Inch), new Distance(0.56, Distance.Unit.Inch)),
            new DriftInfoAmmoPreset("5.45mm", new Distance(5.62, Distance.Unit.Millimeter), new Distance(25.5, Distance.Unit.Millimeter)),
            new DriftInfoAmmoPreset("5.56/.223", new Distance(0.224, Distance.Unit.Inch), new Distance(0.9, Distance.Unit.Inch)),
            new DriftInfoAmmoPreset(".270", new Distance(0.277, Distance.Unit.Inch), new Distance(1.2, Distance.Unit.Inch)),
            new DriftInfoAmmoPreset(".308", new Distance(0.308, Distance.Unit.Inch), new Distance(1.3, Distance.Unit.Inch)),
            new DriftInfoAmmoPreset("7.62mm", new Distance(7.92, Distance.Unit.Millimeter), new Distance(32, Distance.Unit.Millimeter)),
            new DriftInfoAmmoPreset(".338", new Distance(0.338, Distance.Unit.Inch), new Distance(1.5, Distance.Unit.Inch)),
            new DriftInfoAmmoPreset(".50", new Distance(0.51, Distance.Unit.Inch), new Distance(2.3, Distance.Unit.Inch))
        };

        public static void FillComboBox(ComboBox box)
        {
            foreach (DriftInfoAmmoPreset preset in mDriftPresets)
                box.Items.Add(preset);
        }
    }

    class DriftInfoGunPreset
    {
        string mName;
        Distance mRiflingTwist;
        bool mRightHand;


        public Distance RiflingTwist
        {
            get
            {
                return mRiflingTwist;
            }
        }

        public bool RightHandTwist
        {
            get
            {
                return mRightHand;
            }
        }

        internal DriftInfoGunPreset(string name, Distance riflingTwist, bool rightHand)
        {
            mName = name;
            mRiflingTwist = riflingTwist;
            mRightHand = true;
        }

        override public string ToString()
        {
            return mName;
        }
    }

    public static class DriftInfoGunPresetList
    {
        static DriftInfoGunPreset[] mDriftPresets = new DriftInfoGunPreset[]{
            new DriftInfoGunPreset("Ruger 10/22 (.22)", new Distance(16, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("Ruger 10/22 Match (.22)", new Distance(9, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("M16A1 (5.56)", new Distance(12, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("M16A3 (5.56)", new Distance(7, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("AAC MPW (.300BLK)", new Distance(7, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("M24 (7.62x51)", new Distance(11.25, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("AK-47 (7.62x39)", new Distance(240, Distance.Unit.Millimeter), true),
            new DriftInfoGunPreset("AK-74 (5.45x39)", new Distance(200, Distance.Unit.Millimeter), true),
            new DriftInfoGunPreset("SVD (7.62x54)", new Distance(240, Distance.Unit.Millimeter), true),
            new DriftInfoGunPreset("Vintorez (9x39)", new Distance(420, Distance.Unit.Millimeter), true),
            new DriftInfoGunPreset("LWRC Six8 (6.8SPC)", new Distance(10, Distance.Unit.Inch), true),
            new DriftInfoGunPreset("TRG 42 (.338LM)", new Distance(11, Distance.Unit.Inch), true),
	        new DriftInfoGunPreset("Barret M2 (.50BMG)", new Distance(11, Distance.Unit.Inch), true),
	        new DriftInfoGunPreset("K31 (7.5Swiss)", new Distance(270, Distance.Unit.Millimeter), true),

        };

        public static void FillComboBox(ComboBox box)
        {
            foreach (DriftInfoGunPreset preset in mDriftPresets)
                box.Items.Add(preset);
        }
    }



}
