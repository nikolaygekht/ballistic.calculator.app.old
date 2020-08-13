using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Utils;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;
using System;
using System.Collections.Generic;

namespace Gehtsoft.BallisticCalculator.Model
{
    public class Calculator
    {     
        public ICalculatorDelegate Delegate { get; set; }

        private BallisticDataProvider _ballisticDataProvider;

        public Calculator()
        {
            _ballisticDataProvider = BallisticDataProvider.Instance;
        }

        public void CalculateSingleShotInfo(Distance range, Distance rangeStep, Velocity windSpeed, Angle windAngle)
        {
            Utilities.RunOnNewlyCreatedThread(() => 
                CoreCalculateSingleShotInfo(range, rangeStep, windSpeed, windAngle));
        }

        public void CalculateBallisticInfo()
        {
            Utilities.RunOnNewlyCreatedThread(() => CoreCalculateBallisticInfo());
        }

        private void CoreCalculateSingleShotInfo(Distance range, Distance rangeStep, Velocity windSpeed, Angle windAngle)
        {
            if (range.Get(Distance.DefaultUnit) <= 0)
            {
                if (Delegate != null)
                    Utilities.RunOnMainThread(() => Delegate.OnSingleShotCalculated(null));
                return;
            }

            ShotInfo shotInfo = createShotInfo();
            shotInfo.Wind = null;

            ShotInfoController.Calculation.calculateZero(shotInfo);
            Angle zeroEvaluation = shotInfo.ElevationAngle;
            shotInfo = createShotInfo();
            shotInfo.ElevationAngle = zeroEvaluation;
            shotInfo.Wind = new WindInfo(windAngle, windSpeed);
            shotInfo.Step = rangeStep;

            _ballisticDataProvider.BallisticInfo = ShotInfoController.Calculation.calculateShot(shotInfo);

            double step = shotInfo.Step.Get(DefaultUnits.Range);

            double minDiff = double.PositiveInfinity;
            int minDiffIndex = -1;

            for (int i = 0; i < _ballisticDataProvider.BallisticInfo.Count; i++)
            {
                var info = _ballisticDataProvider.BallisticInfo[i];
                double diff = System.Math.Abs(info.Range.Get(DefaultUnits.Range) - range.Get(DefaultUnits.Range));
                if (minDiff > diff)
                {
                    minDiff = diff;
                    minDiffIndex = i;
                }
            }


            if (Delegate != null)
            {
                if (minDiffIndex != -1)
                    Utilities.RunOnMainThread(() =>
                    Delegate.OnSingleShotCalculated(_ballisticDataProvider.BallisticInfo[minDiffIndex]));
                else
                    Utilities.RunOnMainThread(() =>
                    Delegate.OnSingleShotCalculated(null));
            }
        }

        private void CoreCalculateBallisticInfo()
        {
            ShotInfo shotInfo = createShotInfo();
            shotInfo.Wind = null;

            ShotInfoController.Calculation.calculateZero(shotInfo);
            Angle zeroEvaluation = shotInfo.ElevationAngle;
            shotInfo = createShotInfo();
            shotInfo.ElevationAngle = zeroEvaluation;
            
            _ballisticDataProvider.BallisticInfo = ShotInfoController
                                                   .Calculation
                                                   .calculateShot(shotInfo);

            if (Delegate != null)
                Utilities.RunOnMainThread(() => Delegate.OnBallisticInfoCalculated());
        }

        private ShotInfo createShotInfo()
        {
            ShotInfoBuilder sib = new ShotInfoBuilder();
            sib.AmmoInfo = _ballisticDataProvider.TraceData.AmmoInfo;
            sib.AtmosphereInfo = _ballisticDataProvider.AtmosphereData.AtmosphereInfo;
            sib.WindInfo = _ballisticDataProvider.AtmosphereData.WindInfo;
            sib.CantAngle = _ballisticDataProvider.ShotData.CantAngle;
            sib.Clics = _ballisticDataProvider.ShotData.Clics;
            sib.MaxDistanstance = _ballisticDataProvider.ShotData.MaxDistance;
            sib.NearZero = _ballisticDataProvider.ShotData.NearZero;
            sib.ShotAngle = _ballisticDataProvider.ShotData.ShotAngle;
            sib.TargetSize = _ballisticDataProvider.ShotData.TargetSize;
            sib.TraceInfo = _ballisticDataProvider.TraceData.SelectedTraceInfo;
            sib.Step = _ballisticDataProvider.ShotData.StepForBallisticTable;
            

            return sib.Build();
        }
    }

    public enum MeasurementSystem
    {
        Metric,
        Imperial
    }
}