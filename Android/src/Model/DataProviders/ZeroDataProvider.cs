using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.DataProviders
{
    public class ZeroDataProvider
    {
        public Distance SightHeight { get; set; }
        public bool UseZeroElevationAngle { get; set; }
        public Distance ZeroDistance { get; set; }
        public Angle ZeroElevationAngle { get; set; }
    }
}