using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.JBM;

namespace MathEx.ExternalBallistic
{
    public class AmmoInfo
    {
        private Weight mBulletWeight = new Weight(150, Weight.Unit.Grain);
        private Velocity mMuzzleVelocity = new Velocity(3000, Velocity.Unit.FeetPerSecond);
        private DragTable mTable = DragTable.G1;
        private double mBallisticCoefficient = 0.5;

        public Weight BulletWeight
        {
            get
            {
                return mBulletWeight;
            }
            set
            {
                mBulletWeight = value;
            }
        }

        public Velocity MuzzleVelocity
        {
            get
            {
                return mMuzzleVelocity;
            }
            set
            {
                mMuzzleVelocity = value;
            }
        }

        public DragTable Table
        {
            get
            {
                return mTable;
            }
            set
            {
                mTable = value;
            }
        }

        public double BallisticCoefficient
        {
            get
            {
                return mBallisticCoefficient;
            }
            set
            {
                mBallisticCoefficient = value;
            }
        }

        internal AmmoInfo()
        {
        }

        public AmmoInfo(DragTable table, double ballisticCoefficient, Velocity muzzleVelocity, Weight bulletWeight)
        {
            mTable = table;
            mBallisticCoefficient = ballisticCoefficient;
            mMuzzleVelocity = muzzleVelocity;
            mBulletWeight = bulletWeight;
        }

        override public string ToString()
        {
            StringBuilder b = new StringBuilder();

            b.Append("Ballistic Coefficient=");
            b.Append(BallisticCoefficient.ToString("0.000", CultureInfo.InvariantCulture));
            b.Append('(');
            b.Append(DragTableFactory.DragTableName(mTable));
            b.Append(')');
            b.AppendLine();

            if (mMuzzleVelocity != null)
            {
                b.Append("Muzzle Velocity=");
                b.Append(MuzzleVelocity.ToString(MuzzleVelocity.SetUnit));
                b.AppendLine();
            }
            if (mBulletWeight != null)
            {
                b.Append("Bullet Weight=");
                b.Append(BulletWeight.ToString(BulletWeight.SetUnit));
                b.AppendLine();
            }
            return b.ToString();
        }

        public bool IsComplete()
        {
            return (mBulletWeight != null && mMuzzleVelocity != null && mBallisticCoefficient > 0);
        }
    }

    public class AmmoInfoEx : AmmoInfo
    {
        private string mName;
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        private string mCaliber;
        public string Caliber
        {
            get
            {
                return mCaliber;
            }
            set
            {
                mCaliber = value;
            }
        }

        private string mBulletType;
        public string BulletType
        {
            get
            {
                return mBulletType;
            }
            set
            {
                mBulletType = value;
            }
        }

        private string mSource;
        public string Source
        {
            get
            {
                return mSource;
            }
            set
            {
                mSource = value;
            }
        }

        private Distance mBarrelLength;

        public Distance BarrelLength
        {
            get
            {
                return mBarrelLength;
            }
            set
            {
                mBarrelLength = value;
            }
        }

        private Distance mBulletDiameter;
        public Distance BulletDiameter
        {
            get
            {
                return mBulletDiameter;
            }
            set
            {
                mBulletDiameter = value;
            }
        }

        private Distance mBulletLength;
        public Distance BulletLength
        {
            get
            {
                return mBulletLength;
            }
            set
            {
                mBulletLength = value;
            }
        }

        internal AmmoInfoEx()
        {
        }

        public AmmoInfoEx(string source, string name, string caliber, string bulletType, Weight bulletWeight,
                            DragTable table, double ballisticCoefficient, Velocity muzzleVelocity, Distance barrelLength, Distance bulletDiameter, Distance bulletLength) : base(table, ballisticCoefficient, muzzleVelocity, bulletWeight)
        {
            mSource = source;
            mName = name;
            mCaliber = caliber;
            mBulletType = bulletType;
            mBarrelLength = barrelLength;
            mBulletDiameter = bulletDiameter;
            mBulletLength = bulletLength;
        }

        override public string ToString()
        {
            StringBuilder b = new StringBuilder();
            if (Name != null)
            {
                b.Append("Name=");
                b.Append(Name);
                b.AppendLine();
            }
            if (Source != null)
            {
                b.Append("Source=");
                b.Append(Source);
                b.AppendLine();
            }
            if (Caliber != null)
            {
                b.Append("Caliber=");
                b.Append(Caliber);
                b.AppendLine();
            }
            if (BulletWeight != null && BulletType != null)
            {
                b.Append("Bullet=");
                b.Append(BulletWeight.ToString(BulletWeight.SetUnit));
                b.Append(BulletType);
                b.AppendLine();
            }
            b.Append("Ballistic Coefficient=");
            b.Append(BallisticCoefficient.ToString("0.000", CultureInfo.InvariantCulture));
            b.Append('(');
            b.Append(DragTableFactory.DragTableName(Table));
            b.Append(')');
            b.AppendLine();
            if (MuzzleVelocity != null)
            {
                b.Append("Muzzle Velocity=");
                b.Append(MuzzleVelocity.ToString(MuzzleVelocity.SetUnit));
                b.AppendLine();
            }
            if (BarrelLength != null)
            {
                b.Append("Barrel Length=");
                b.Append(BarrelLength.ToString(BarrelLength.SetUnit));
                b.AppendLine();
            }
            return b.ToString();
        }

        new public bool IsComplete()
        {
            return base.IsComplete() && (mSource != null && mName != null && mCaliber != null && mBulletType != null && mBarrelLength != null);
        }
    }

    public static class DragTableFactory
    {
        static public string DragTableName(DragTable table)
        {
            switch (table)
            {
                case DragTable.G1:
                    return "G1";
                case DragTable.G2:
                    return "G2";
                case DragTable.G5:
                    return "G5";
                case DragTable.G6:
                    return "G6";
                case DragTable.G7:
                    return "G7";
                case DragTable.G8:
                    return "G8";
                case DragTable.GI:
                    return "GI";
                case DragTable.GL:
                    return "GL";

                default:
                    throw new ArgumentException("Unknown drag table");
            }
        }

        static public DragTable NameToDragTable(string name)
        {
            if (name == "G1")
                return DragTable.G1;
            else if (name == "G2")
                return DragTable.G2;
            else if (name == "G5")
                return DragTable.G5;
            else if (name == "G6")
                return DragTable.G6;
            else if (name == "G7")
                return DragTable.G7;
            else if (name == "G8")
                return DragTable.G8;
            else if (name == "GL")
                return DragTable.GL;
            else if (name == "GI")
                return DragTable.GI;

            throw new ArgumentException("Unknown drag table");
        }
    }
}