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
using Android.Graphics;

using MathEx.ExternalBallistic.Units;
using System.Globalization;

namespace BallisticCalculator.Utils
{
    class Utilities
    {
        // Bug in android #35482.
        // Dirty hack from here: https://code.google.com/p/android/issues/detail?id=35482
        public static void FixNumberPickerBug(NumberPicker numberPicker)
        {
            Java.Lang.Reflect.Field fldInputText = numberPicker.Class.GetDeclaredField("mInputText");
            fldInputText.Accessible = true;
            ((EditText)(fldInputText.Get(numberPicker))).SetFilters(new Android.Text.IInputFilter[0]);
        }

        public static bool TryParseDouble(string str, out double dbl)
        {
            return double.TryParse(str.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out dbl);
        }

        public static decimal RoundDouble(double dbl, int prefferedDigits)
        {
            double result = 0;

            const int _maxDigits = 15;
            int _prefferedDigits = prefferedDigits;
            do
            {
                result = Math.Round(dbl, _prefferedDigits);
                _prefferedDigits++;
            }
            while (Math.Abs(result) < double.Epsilon && _prefferedDigits < _maxDigits);

            return (decimal)result;
        }

        public static Angle AngleFromHour(int clockHour)
        {
            int angleDegree = 0;
            if (clockHour > 12 || clockHour < 1)
                throw new ArgumentException();

            if (clockHour > 6)
                angleDegree = (clockHour - 12) * 30;
            else
                angleDegree = clockHour * 30;

            return new Angle(angleDegree, Angle.Unit.Degree);
        }

        public static int HourFromAngle(Angle angle)
        {
            double angleDegree = angle.Get(Angle.Unit.Degree);
            angleDegree /= 30;
            if (angleDegree <= 0)
                angleDegree += 12;
            return (int)(angleDegree + 0.5);
        }

        public static void SetButtonState(Button button, bool enabled)
        {
            button.Alpha = enabled ? 1f : 0.5f;
            button.Clickable = enabled;
        }

        public static PointF DegreesToXY(float degrees, float radius, Point origin)
        {
            PointF xy = new PointF();
            double radians = degrees * Math.PI / 180.0;

            xy.X = (float)Math.Sin(radians) * radius + origin.X;
            xy.Y = origin.Y - (float)Math.Cos(radians) * radius;

            return xy;
        }

        public static float XYToDegrees(Point xy, Point origin)
        {
            double angle = 0.0;

            //find quadrant
            //2   |    1
            // ---x----
            //3   |    4


            if (xy.X > origin.X && xy.Y == origin.Y)
            {
                angle = 90;
            }
            else if (xy.X < origin.X && xy.Y == origin.Y)
            {
                angle = -90;
            }
            else if (xy.X == origin.X && xy.Y <= origin.Y)
            {
                angle = 0;
            }
            else if (xy.X == origin.X && xy.Y > origin.Y)
            {
                angle = 180;
            }
            else if (xy.X > origin.X && xy.Y < origin.Y)
            {
                //1
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = 90 - angle;
            }
            else if (xy.X < origin.X && xy.Y < origin.Y)
            {
                //2
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = -(90 - angle);

            }
            else if (xy.X < origin.X && xy.Y > origin.Y)
            {
                //3
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = -(90 + angle);
            }
            else if (xy.X > origin.X && xy.Y > origin.Y)
            {
                //4
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = (90 + angle);
            }
            return (float)angle;
        }

        public static bool PointIsInCircle(Point pt, Point center, float radius)
        {
            return Math.Pow(pt.X - center.X, 2) + Math.Pow(pt.Y - center.Y, 2) <= Math.Pow(radius, 2);
        }

        public static MathEx.ExternalBallistic.JBM.DragTable DragTableFromString(string dragTable)
        {
            if (dragTable == "G1")
                return MathEx.ExternalBallistic.JBM.DragTable.G1;
            if (dragTable == "G2")
                return MathEx.ExternalBallistic.JBM.DragTable.G2;
            if (dragTable == "G5")
                return MathEx.ExternalBallistic.JBM.DragTable.G5;
            if (dragTable == "G6")
                return MathEx.ExternalBallistic.JBM.DragTable.G6;
            if (dragTable == "G7")
                return MathEx.ExternalBallistic.JBM.DragTable.G7;
            if (dragTable == "G8")
                return MathEx.ExternalBallistic.JBM.DragTable.G8;
            if (dragTable == "GI")
                return MathEx.ExternalBallistic.JBM.DragTable.GI;
            if (dragTable == "GL")
                return MathEx.ExternalBallistic.JBM.DragTable.GL;

            return MathEx.ExternalBallistic.JBM.DragTable.G1;
        }

        public static string DragTableToString(MathEx.ExternalBallistic.JBM.DragTable dragTable)
        {
            switch (dragTable)
            {
                case MathEx.ExternalBallistic.JBM.DragTable.G1:
                    return "G1";
                case MathEx.ExternalBallistic.JBM.DragTable.G2:
                    return "G2";
                case MathEx.ExternalBallistic.JBM.DragTable.G5:
                    return "G5";
                case MathEx.ExternalBallistic.JBM.DragTable.G6:
                    return "G6";
                case MathEx.ExternalBallistic.JBM.DragTable.G7:
                    return "G7";
                case MathEx.ExternalBallistic.JBM.DragTable.G8:
                    return "G8";
                case MathEx.ExternalBallistic.JBM.DragTable.GL:
                    return "GL";
                case MathEx.ExternalBallistic.JBM.DragTable.GI:
                    return "GI";
            }
            return "G1";
        }

    }
}