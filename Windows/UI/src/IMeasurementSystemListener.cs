using System;
using System.Collections.Generic;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    interface IMeasurementSystemListener
    {
        MeasurementSystem MeasurementSystem
        {
            set;
        }
    }

    interface IAngleUnitListener
    {
        Angle.Unit AngleUnits
        {
            get;
        }
    }
}
