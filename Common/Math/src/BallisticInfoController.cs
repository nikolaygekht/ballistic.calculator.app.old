using System;
using System.Collections.Generic;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public static class BallisticInfoController
    {
        public static class Calculation
        {
            private static bool IsBettween(double v, double r1, double r2)
            {
                if (r1 > r2)
                {
                    double x;
                    x = r2;
                    r2 = r1;
                    r1 = x;
                }
                return v >= r1 && v <= r2;
            }


            public static bool GetDistanceByHold(BallisticInfoCollection collection, Angle hold, bool foundLast, out Distance distance)
            {
                distance = null;

                if (collection == null || hold == null)
                    return false;

                double _hold = hold.Get(Angle.Unit.Radian);
                bool found = false;
                
                for (int i = 0; i < collection.Count - 1; i++)
                {
                    double h1, h2;
                    h1 = collection[i].Hold.Get(Angle.Unit.Radian);
                    h2 = collection[i + 1].Hold.Get(Angle.Unit.Radian);
                    if (IsBettween(_hold, h1, h2))
                    {
                        double d, d1, d2;
                        d1 = collection[i].Range.Get(Distance.Unit.Yard);
                        d2 = collection[i + 1].Range.Get(Distance.Unit.Yard);
                        d = d1 + ((_hold - h1) / (h2 - h1)) * (d2 - d1);
                        distance = new Distance(d, Distance.Unit.Yard);
                        found = true;
                        if (!foundLast)
                            break;
                    }
                }
                return found;
            }

            public static bool GetHoldAndAdjustmentByDistance(BallisticInfoCollection collection, Distance distance, bool foundLast, out Angle hold, out Angle windage)
            {
                hold = null;
                windage = null;

                if (collection == null || distance == null)
                    return false;


                double _distance = distance.Get(Distance.Unit.Yard);
                bool found = false;
                for (int i = 0; i < collection.Count - 1; i++)
                {
                    double d1, d2;
                    d1 = collection[i].Range.Get(Distance.Unit.Yard);
                    d2 = collection[i + 1].Range.Get(Distance.Unit.Yard);
                    if (IsBettween(_distance, d1, d2))
                    {
                        double h, h1, h2;
                        double w, w1, w2;

                        h1 = collection[i].Hold.Get(Angle.Unit.Radian);
                        h2 = collection[i + 1].Hold.Get(Angle.Unit.Radian);
                        h = h1 + ((_distance - d1) / (d2 - d1)) * (h2 - h1);
                        hold = new Angle(h, Angle.Unit.Radian);

                        w1 = collection[i].WindageCorrection.Get(Angle.Unit.Radian);
                        w2 = collection[i + 1].WindageCorrection.Get(Angle.Unit.Radian);
                        w = w1 + ((_distance - d1) / (d2 - d1)) * (w2 - w1);
                        windage = new Angle(w, Angle.Unit.Radian);

                        found = true;
                        if (!foundLast)
                            break;
                    }
                }
                return found;
            }
        }
    }
}
