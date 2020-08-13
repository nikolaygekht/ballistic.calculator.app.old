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
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.JBM;
using Gehtsoft.BallisticCalculator.Model;
using MathEx.ExternalBallistic;

namespace Gehtsoft.BallisticCalculator.DataProviders
{
    public class BulletDataProvider
    {
        public double BallisticCoefficient { get; set; }
        public Distance BulletDiameter { get; private set; }
        public Distance BulletLength { get; private set; }
        public string BulletName { get; set; }
        public Weight BulletWeight { get; set; }
        public DragTable DragTable { get; set; }
        public Velocity MuzzleVelocity { get; set; }
    }
}