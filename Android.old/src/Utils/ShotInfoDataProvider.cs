using System;
using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic.Units;

namespace BallisticCalculator.Utils
{
    class ShotInfoDataProvider
    {
        public ShotInfo ShotInfo
        {
            get
            {
                ShotInfo si = new ShotInfo(
                    _traceInfo.TraceName,
                    new AmmoInfo(
                        _traceInfo.DrageTable,
                        _traceInfo.BallisticCoefficient,
                        _traceInfo.MuzzleVelocity,
                        _traceInfo.BulletWeight),
                    _atmosphereInfo,
                    _windInfo,
                    _traceInfo.ZeroElevationAngle,
                    _shotAngle,
                    _cantAngle,
                    _traceInfo.DriftInfo ? new DriftInfo(
                        _traceInfo.BulletLength,
                        _traceInfo.BulletDiameter,
                        _traceInfo.RiflingTwist,
                        _traceInfo.RiflingRightHandTwist
                    ) : null
                );
                si.SightHeight = _traceInfo.SightHeight;
                si.Clicks = _clicks;
                si.HorizonalClick = _traceInfo.HorizonalClick;
                si.MaxDistance = _maxDistance;
                si.NearZero = _nearZero;
                si.Step = _step;
                si.TargetSize = _targetSize;
                si.VerticalClick = _traceInfo.VerticalClick;
                si.ZeroDistance = _traceInfo.ZeroDistance;

                return si;
            }
        }

        private TraceInfo _traceInfo = null;
        public TraceInfo TraceInfo
        {
            get
            {
                return _traceInfo;
            }
            set
            {
                _traceInfo = value;
            }
        }

        private AtmosphereInfo _atmosphereInfo = new AtmosphereInfo(
            new Distance(0, Distance.Unit.Meter),
            new Pressure(1, Pressure.Unit.Bar),
            new Temperature(15, Temperature.Unit.Celsius),
            0.78);
        public AtmosphereInfo AtmosphereInfo
        {
            set
            {
                _atmosphereInfo = value;
            }
            get
            {
                return _atmosphereInfo;
            }
        }

        private WindInfo _windInfo = new WindInfo(
            new Angle(0, Angle.Unit.Degree),
            new Velocity(0, Velocity.Unit.MeterPerSecond));
        public WindInfo WindInfo
        {
            set
            {
                _windInfo = value;
            }
            get
            {
                return _windInfo;
            }
        }


        private Angle _shotAngle = new Angle(0, Angle.Unit.Degree);
        public Angle ShotAngle
        {
            set
            {
                _shotAngle = value;
            }
        }

        private Angle _cantAngle = new Angle(0, Angle.Unit.Degree);
        public Angle CantAngle
        {
            set
            {
                _cantAngle = value;
            }
        }

        private int _clicks = 0;
        public int Clicks
        {
            set
            {
                _clicks = value;
            }
        }

        private Distance _maxDistance = new Distance(1000, Distance.Unit.Yard);
        public Distance MaxDistance
        {
            set
            {
                _maxDistance = value;
            }
        }
        private bool _nearZero = false;
        public bool NearZero
        {
            set
            {
                _nearZero = value;
            }
        }
        private Distance _step = new Distance(25, Distance.Unit.Yard);
        public Distance Step
        {
            set
            {
                _step = value;
            }
        }
        private Distance _targetSize;
        public Distance TargetSize
        {
            set
            {
                _targetSize = value;
            }
        }
    }
}