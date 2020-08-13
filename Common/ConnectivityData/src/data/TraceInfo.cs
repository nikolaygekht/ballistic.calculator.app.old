using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Connectivity
{
    public class TraceInfo
    {
        public string TraceName { get; set; }
        public double BallisticCoefficient { get; set; }
        public DragTable DrageTable { get; set; }
        public Weight BulletWeight { get; set; }
        public Velocity MuzzleVelocity { get; set; }

        public Distance SightHeight { get; set; }
        public Distance ZeroDistance { get; set; }
        public Angle ZeroElevationAngle { get; set; }

        public bool DriftInfo { get; set; }
        public Distance BulletLength { get; set; }
        public Distance BulletDiameter { get; set; }
        public Distance RiflingTwist { get; set; }
        public bool RiflingRightHandTwist { get; set; }

        public Angle VerticalClick { get; set; }
        public Angle HorizonalClick { get; set; }

        public bool Metric { get; set; }

        public TraceInfo()
        {
            DrageTable = DragTable.G1;
        }

        public bool Complete
        {
            get
            {
                return (TraceName != null &&
                        BulletWeight != null &&
                        MuzzleVelocity != null &&
                        SightHeight != null &&
                        ZeroDistance != null &&
                        ZeroElevationAngle != null &&
                        (DriftInfo ?
                            ( BulletDiameter != null &&
                              BulletLength != null &&
                              RiflingTwist != null )
                          : true) &&
                        VerticalClick != null &&
                        HorizonalClick != null);
            }
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        public string ToString(CultureInfo ci)
        {
            return TraceName;
        }

        public TraceInfo Clone()
        {
            TraceInfo ti = new TraceInfo();
            ti.TraceName = this.TraceName;
            ti.BallisticCoefficient = this.BallisticCoefficient;
            ti.DrageTable = this.DrageTable;
            ti.BulletWeight = this.BulletWeight.ToUnit(this.BulletWeight.SetUnit);
            ti.MuzzleVelocity = this.MuzzleVelocity.ToUnit(this.MuzzleVelocity.SetUnit);
            ti.VerticalClick = this.VerticalClick.ToUnit(this.VerticalClick.SetUnit);
            ti.HorizonalClick = this.HorizonalClick.ToUnit(this.HorizonalClick.SetUnit);
            ti.SightHeight = this.SightHeight.ToUnit(this.SightHeight.SetUnit);
            ti.ZeroDistance = this.ZeroDistance.ToUnit(this.ZeroDistance.SetUnit);
            ti.ZeroElevationAngle = this.ZeroElevationAngle.ToUnit(this.ZeroElevationAngle.SetUnit);
            ti.DriftInfo = this.DriftInfo;
            if (ti.DriftInfo)
            {
                ti.BulletDiameter = this.BulletDiameter.ToUnit(this.BulletDiameter.SetUnit);
                ti.BulletLength = this.BulletLength.ToUnit(this.BulletDiameter.SetUnit);
                ti.RiflingTwist = this.RiflingTwist.ToUnit(this.RiflingTwist.SetUnit);
                ti.RiflingRightHandTwist = this.RiflingRightHandTwist;
            }
            ti.Metric = this.Metric;
            return ti;
        }
    }
}
