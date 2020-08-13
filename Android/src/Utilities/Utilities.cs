using Android.Graphics;
using Android.OS;
using Android.Widget;
using MathEx.ExternalBallistic.Units;
using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

// prevent ambiguous reference error between Java.Lang.Math and System.Math
using Runnable = Java.Lang.Runnable;

namespace Gehtsoft.BallisticCalculator.Utils
{
    static class Utilities
    {
        private static Handler _uiHandler = new Handler(Looper.MainLooper);

        private static string _inetInetPattern = "^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\." +
                                                 "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\." +
                                                 "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\." +
                                                 "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";


        private static string _inetAddrPattern = "^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";


        public static void RunOnMainThread(Action action)
        {
            if (action != null)
            {
                Runnable runnable = new Runnable(() => action.Invoke());
                _uiHandler.Post(runnable);
            }
        }

        public static void RunOnNewlyCreatedThread(Action action)
        {
            new System.Threading.Thread(() => action.Invoke()).Start();
        }

        public static void FixNumberPickerBug(NumberPicker numberPicker)
        {
            const string declaredField = "mInputText";
            Java.Lang.Reflect.Field fldInputText = numberPicker.Class.GetDeclaredField(declaredField);
            fldInputText.Accessible = true;
            ((EditText) (fldInputText.Get(numberPicker))).SetFilters(new Android.Text.IInputFilter[0]);
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
            } while (Math.Abs(result) < double.Epsilon && _prefferedDigits < _maxDigits);

            return (decimal) result;
        }

        public static Angle AngleFromHour(int clockHour)
        {
            int angleDegree = 0;
            if (clockHour > 12 || clockHour < 1)
                throw new ArgumentException();

            if (clockHour > 6)
                angleDegree = (clockHour - 12)*30;
            else
                angleDegree = clockHour*30;

            return new Angle(angleDegree, Angle.Unit.Degree);
        }

        public static int HourFromAngle(Angle angle)
        {
            double angleDegree = angle.Get(Angle.Unit.Degree);
            angleDegree /= 30;
            if (angleDegree <= 0)
                angleDegree += 12;
            return (int) (angleDegree + 0.5);
        }

        public static void SetButtonState(Button button, bool enabled)
        {
            button.Alpha = enabled ? 1f : 0.5f;
            button.Clickable = enabled;
        }

        public static PointF DegreesToXY(float degrees, float radius, Point origin)
        {
            PointF xy = new PointF();
            double radians = degrees*Math.PI/180.0;

            xy.X = (float) Math.Sin(radians)*radius + origin.X;
            xy.Y = origin.Y - (float) Math.Cos(radians)*radius;

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
                angle =
                    Math.Atan(Math.Abs((double) xy.Y - (double) origin.Y)/Math.Abs((double) xy.X - (double) origin.X));
                angle = angle*180.0/Math.PI;
                angle = 90 - angle;
            }
            else if (xy.X < origin.X && xy.Y < origin.Y)
            {
                //2
                angle =
                    Math.Atan(Math.Abs((double) xy.Y - (double) origin.Y)/Math.Abs((double) xy.X - (double) origin.X));
                angle = angle*180.0/Math.PI;
                angle = -(90 - angle);

            }
            else if (xy.X < origin.X && xy.Y > origin.Y)
            {
                //3
                angle =
                    Math.Atan(Math.Abs((double) xy.Y - (double) origin.Y)/Math.Abs((double) xy.X - (double) origin.X));
                angle = angle*180.0/Math.PI;
                angle = -(90 + angle);
            }
            else if (xy.X > origin.X && xy.Y > origin.Y)
            {
                //4
                angle =
                    Math.Atan(Math.Abs((double) xy.Y - (double) origin.Y)/Math.Abs((double) xy.X - (double) origin.X));
                angle = angle*180.0/Math.PI;
                angle = (90 + angle);
            }
            return (float) angle;
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

        public static bool ValidateInetAddress(string inetAddress)
        {
            if (string.IsNullOrEmpty(inetAddress))
                return false;
            
            var match = Regex.Match(inetAddress, _inetInetPattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static bool ValidateInetPort(string inetPort)
        {
            if (string.IsNullOrEmpty(inetPort))
                return false;

            var match = Regex.Match(inetPort, _inetAddrPattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static string RemoveLeadingZerosInOctets(string ipAddresss)
        {
            return Regex.Replace(ipAddresss, "0*([0-9]+)", "${1}");
        }
    }
}