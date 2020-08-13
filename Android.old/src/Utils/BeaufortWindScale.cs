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

namespace BallisticCalculator.Utils
{
    class BeaufortWindScale
    {
        private string[] beaufortWindScales;
        private string[] beaufortWindScaleDescriptions;
        private double[,] beaufortWindScalesValues = new double[,]
        {
            {0, 1},
            {1, 3},
            {4, 7},
            {8, 12},
            {13, 18},
            {19, 24},
            {25, 31},
            {32, 38},
            {39, 46},
            {47, 54},
            {55, 63},
            {64, 72},
            {73, double.PositiveInfinity}
        };
        private Velocity.Unit beaufortWindScaleUnits = Velocity.Unit.MilesPerHour;
        private Velocity.Unit displayUnits = DefaultUnits.Wind.Velocity;

        public BeaufortWindScale(Context context, Velocity.Unit selectedUnits)
        {
            displayUnits = selectedUnits;
            beaufortWindScales = context.Resources.GetStringArray(Resource.Array.dd_lblsBeaufortWindScale);
            beaufortWindScaleDescriptions = context.Resources.GetStringArray(Resource.Array.dd_lblsBeaufortWindScaleDescription);
        }

        public ArrayAdapter<string> CreateTwoLineAdapter(Context context)
        {
            string[] windSpeedRangeStrings = new string[13]
            {
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[0, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[0, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[1, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[1, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[2, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[2, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[3, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[3, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[4, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[4, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[5, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[5, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[6, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[6, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[7, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[7, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[8, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[8, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[9, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[9, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[10, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[10, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    Velocity.Convert(beaufortWindScalesValues[11, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits),
                    Velocity.Convert(beaufortWindScalesValues[11, 1], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits)),
                string.Format("{0:0.00} {1} +",
                    Velocity.Convert(beaufortWindScalesValues[12, 0], beaufortWindScaleUnits, displayUnits), Velocity.UnitToName(displayUnits))

            };
            return new TwoLineAdapter<string>(context, beaufortWindScaleDescriptions, windSpeedRangeStrings); ;
        }
        
        public Velocity GetMinWindSpeed(int windForceLevel)
        {
            return new Velocity(beaufortWindScalesValues[windForceLevel, 0], beaufortWindScaleUnits);
        }

        // returns null if maximum speed is infinite
        public Velocity GetMaxWindSpeed(int windForceLevel)
        {
            if (double.IsPositiveInfinity(beaufortWindScalesValues[windForceLevel, 1]))
                return null;
            return new Velocity(beaufortWindScalesValues[windForceLevel, 1], beaufortWindScaleUnits);
        }

        public string GetScaleName(int windForceLevel)
        {
            return beaufortWindScales[windForceLevel];
        }

        public string GetScaleDescription(int windForceLevel)
        {
            return beaufortWindScaleDescriptions[windForceLevel];
        }

        private class TwoLineAdapter<T> : ArrayAdapter<T>
        {
            private T[] _firstLineElements;
            private T[] _secondLineElements;

            public TwoLineAdapter(Context context, T[] firstLineElements, T[] secondLineElements)
                : base(context, Android.Resource.Layout.SimpleListItem2, Android.Resource.Id.Text1, firstLineElements)
            {
                _firstLineElements = firstLineElements;
                _secondLineElements = secondLineElements;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = base.GetView(position, convertView, parent);
                TextView textView1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
                TextView textView2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);

                textView1.Text = _firstLineElements[position].ToString();
                textView2.Text = _secondLineElements[position].ToString();

                return view;
            }
        }
    }
}