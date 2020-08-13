using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public class ShotInfo
    {
        public string Name {get; set;} 
        public AmmoInfo Ammo {get; set;}
        public AtmosphereInfo Atmosphere {get; set;}
        public WindInfo Wind {get; set;}
        public Distance SightHeight {get; set;}
        public Distance ZeroDistance {get; set;}
        public bool NearZero {get; set;}
        public Angle ShotAngle {get; set;}
        public Angle CantAngle { get; set;}
        public Angle ElevationAngle { get; set; }
        public Distance MaxDistance { get; set; }
        public Distance Step { get; set; }
        public DriftInfo DriftInfo {get; set;}
        public Angle VerticalClick { get; set; }
        public Angle HorizonalClick { get; set; }
        public Distance TargetSize { get; set; }
        public int Clicks { get; set; }

        public ShotInfo()
        {
            MaxDistance = new Distance(5000, Distance.Unit.Yard);
            Step = new Distance(10, Distance.Unit.Yard);
        }

        public ShotInfo(string name, AmmoInfo ammo) : this()
        {
            Name = name;
            Ammo = ammo;
        }

        public ShotInfo(string name, AmmoInfo ammo, Angle elevationAngle)
            : this()
        {
            Name = name;
            Ammo = ammo;
            ElevationAngle = elevationAngle;

        }

        public ShotInfo(string name, AmmoInfo ammo, AtmosphereInfo atmo, WindInfo wind, Angle elevationAngle)
            : this()
        {
            Name = name;
            Ammo = ammo;
            Wind = wind;
            ElevationAngle = elevationAngle;
            Atmosphere = atmo;
        }

        public ShotInfo(string name, AmmoInfo ammo, AtmosphereInfo atmo, WindInfo wind, Angle elevationAngle, Angle shotAngle, Angle cantAngle, DriftInfo driftInfo)
            : this()
        {
            Name = name;
            Ammo = ammo;
            Wind = wind;
            DriftInfo = driftInfo;
            ShotAngle = shotAngle;
            CantAngle = cantAngle;
            ElevationAngle = elevationAngle;
            Atmosphere = atmo;
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
            if (Ammo != null)
            {
                b.Append("Ammo:");
                b.AppendLine();
                b.Append(Ammo.ToString());
            }
            if (Atmosphere != null)
            {
                b.Append("Atmosphere:");
                b.AppendLine();
                b.Append(Atmosphere.ToString());
            }

            if (Wind != null)
            {
                b.Append("Wind:");
                b.AppendLine();
                b.Append(Wind.ToString());
            }

            if (ShotAngle != null)
            {
                b.Append("Shooting Angle=");
                b.Append(ShotAngle.ToString(ShotAngle.SetUnit));
                b.AppendLine();
            }

            if (CantAngle != null)
            {
                b.Append("Shooting Angle=");
                b.Append(CantAngle.ToString(CantAngle.SetUnit));
                b.AppendLine();
            }

            if (ElevationAngle != null)
            {
                b.Append("Elevation Angle=");
                b.Append(ElevationAngle.ToString(ElevationAngle.SetUnit));
                b.AppendLine();
            }

            if (MaxDistance != null)
            {
                b.Append("Max distance=");
                b.Append(MaxDistance.ToString(MaxDistance.SetUnit));
                b.AppendLine();
            }
            if (Step != null)
            {
                b.Append("Step=");
                b.Append(Step.ToString(Step.SetUnit));
                b.AppendLine();
            }
            return b.ToString();
        }
    }
}