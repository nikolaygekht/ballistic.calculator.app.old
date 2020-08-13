using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gehtsoft.BallisticCalculator.Model;
using MathEx.ExternalBallistic.Units;
using Gehtsoft.BallisticCalculator.Utils;

namespace Gehtsoft.BallisticCalculator.DataProviders
{
    public class ShotDataProvider
    {
        public Angle CantAngle { get; set; }
        public int Clics { get; set; }
        public Distance MaxDistance
        {
            get
            {
                return new Distance(1000, DefaultUnits.Range);
            }
        }
        public bool NearZero { get; set; }
        public Distance StepForBallisticTable { get; set; }
        public Distance StepForSingleShot { get; set; }
        public Distance TargetSize { get; set; }
        public Angle ShotAngle { get; set; }

        public int iStepForSingleShot
        {
            get
            {
                return (int)StepForSingleShot.Get(DefaultUnits.Range);
            }
        }

        public ShotDataProvider()
        {
            CantAngle = DefaultValues.CreateAngleWithDefaultValue();
            StepForBallisticTable = DefaultValues.CreateDistanceeWithDefaultValue(25);
            StepForSingleShot = DefaultValues.CreateDistanceeWithDefaultValue(5);
            ShotAngle = DefaultValues.CreateAngleWithDefaultValue();
        }
    }
}