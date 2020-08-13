using System;
using MathEx.ExternalBallistic;

namespace Gehtsoft.BallisticCalculator.Model
{ 
    public interface ICalculatorDelegate
    { 
        /* Run on main thread */
        void OnBallisticInfoCalculated();
        /* Run on main thread */
        void OnSingleShotCalculated(BallisticInfo info);
    }
}