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
using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Utils;

namespace Gehtsoft.BallisticCalculator.DataProviders
{
    public class AtmosphereDataProvider
    {
        public AtmosphereInfo AtmosphereInfo {
            get;
            set; }
        public WindInfo WindInfo {
            get;
            set; }

        public readonly int MaxWindSpeed = 73;
        public readonly int WindDirectionMin = 1;
        public readonly int WindDirectionMax = 12;
    }
}