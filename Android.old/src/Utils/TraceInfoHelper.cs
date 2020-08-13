using System;
using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic.Units;

namespace BallisticCalculator
{
    class TraceInfoHelper
    {
        private TraceInfo _traceInfo;
        public TraceInfoHelper(TraceInfo traceInfo)
        {
            _traceInfo = traceInfo;

            _ammoInfo = new AmmoInfo(
                    _traceInfo.DrageTable,
                    _traceInfo.BallisticCoefficient,
                    _traceInfo.MuzzleVelocity,
                    _traceInfo.BulletWeight);

            _atmosphereInfo = new AtmosphereInfo(
                new Distance(100, Distance.Unit.Meter),
                new Pressure(756, Pressure.Unit.MmHg),
                new Temperature(0, Temperature.Unit.Celsius),
                80);

            _windInfo = new WindInfo(
                new Angle(45, Angle.Unit.Degree),
                new Velocity(2, Velocity.Unit.MeterPerSecond));

            _shotAngle = new Angle(0, Angle.Unit.Degree);

            _cantAngle = new Angle(0, Angle.Unit.Degree);

            if (_traceInfo.DriftInfo)
            {
                _driftInfo = new DriftInfo(
                    _traceInfo.BulletLength,
                    _traceInfo.BulletDiameter,
                    _traceInfo.RiflingTwist,
                    _traceInfo.RiflingRightHandTwist);
            }
        }

        private AmmoInfo _ammoInfo;
        public AmmoInfo AmmoInfo
        {
            get
            {
                return _ammoInfo;
            }
        }

        private AtmosphereInfo _atmosphereInfo;
        public AtmosphereInfo AtmosphereInfo
        {
            get
            {
                return _atmosphereInfo;
            }
        }

        private WindInfo _windInfo;
        public WindInfo WindInfo
        {
            get
            {
                return _windInfo;
            }
        }

        public Angle ElevationAngle
        {
            get
            {
                return _traceInfo.ZeroElevationAngle;
            }
        }

        private Angle _shotAngle;
        public Angle ShotAngle
        {
            get
            {
                return _shotAngle;
            }
        }

        private Angle _cantAngle;
        public Angle CantAngle
        {
            get
            {
                return _cantAngle;
            }
        }

        private DriftInfo _driftInfo;
        public DriftInfo DriftInfo
        {
            get
            {
                return _driftInfo;
            }
        }

        public Distance SightHeight
        {
            get
            {
                return _traceInfo.SightHeight;
            }
        }
    }
}