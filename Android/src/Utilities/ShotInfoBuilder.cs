using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Utils
{
    sealed class ShotInfoBuilder
    {
        public TraceInfo TraceInfo { get; set; }
        public AmmoInfo AmmoInfo { get; set; }
        public AtmosphereInfo AtmosphereInfo { get; set; }
        public WindInfo WindInfo { get; set; }
        public Angle ShotAngle { get; set; }
        public Angle CantAngle { get; set; }
        public int Clics { get; set; }
        public Distance MaxDistanstance { get; set; }
        public bool NearZero { get; set; }
        public Distance Step { get; set; }
        public Distance TargetSize { get; set; }

  
        public ShotInfoBuilder()
        {
        }

        public ShotInfo Build()
        {
            if (TraceInfo == null)
                return null;

            AmmoInfo ammoInfo = new AmmoInfo(
                                    TraceInfo.DrageTable,
                                    TraceInfo.BallisticCoefficient,
                                    TraceInfo.MuzzleVelocity,
                                    TraceInfo.BulletWeight
                                    );

            DriftInfo driftInfo = null;
            if (TraceInfo.DriftInfo)
            {
                driftInfo = new DriftInfo(
                                    TraceInfo.BulletLength,
                                    TraceInfo.BulletDiameter,
                                    TraceInfo.RiflingTwist,
                                    TraceInfo.RiflingRightHandTwist
                                    );

            }

            ShotInfo shotInfo = new ShotInfo(
                                        TraceInfo.TraceName, 
                                        AmmoInfo,
                                        AtmosphereInfo,
                                        WindInfo,
                                        TraceInfo.ZeroElevationAngle,
                                        ShotAngle,
                                        CantAngle,
                                        driftInfo
                                        );

            shotInfo.SightHeight = TraceInfo.SightHeight;
            shotInfo.HorizonalClick = TraceInfo.HorizonalClick;
            shotInfo.Clicks = Clics;  
            shotInfo.MaxDistance = MaxDistanstance;
            shotInfo.VerticalClick = TraceInfo.VerticalClick;
            shotInfo.NearZero = NearZero;    
            shotInfo.ZeroDistance = TraceInfo.ZeroDistance;
            shotInfo.TargetSize = TargetSize;
            shotInfo.SightHeight = TraceInfo.SightHeight;
            shotInfo.Step = Step;

            return shotInfo;
        }
    }
}