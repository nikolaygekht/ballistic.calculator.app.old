using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public enum ReportUnits
    {
        Imperial,
        Metric,
    }


    public class BallisticInfo
    {
        public Distance Range {get; set;}
        public Distance Path {get; set;}
        public Angle Hold {get; set;}
        public TimeSpan Time {get; set;}
        public Distance Windage {get; set;}
        public Angle WindageCorrection {get; set;}
        public Velocity BulletVelocity  {get; set;}
        public double Mach  {get; set;}
        public Energy BulletEnergy  {get; set;}
        public Weight OptimalGameWeight  {get; set;}
        public int HoldClicks{ get; set;}
        public int WindageClicks{ get; set;}



        internal BallisticInfo(Distance range, Distance path, Angle hold, TimeSpan time,
                               Distance windage, Angle windageCorrection,
                               Velocity bulletVelocity, double mach, Energy bulletEnergy, Weight ogv, int holdClicks, int windageClicks)
        {
            Range = range;
            Path = path;
            Hold = hold;
            Time = time;
            Windage = windage;
            WindageCorrection = windageCorrection;
            BulletVelocity = bulletVelocity;
            Mach = mach;
            BulletEnergy = bulletEnergy;
            OptimalGameWeight = ogv;
            HoldClicks = holdClicks;
            WindageClicks = windageClicks;

        }

        override public string ToString()
        {
            return ToString(ReportUnits.Imperial);
        }

        public string ToString(ReportUnits units)
        {
            StringBuilder b = new StringBuilder();
            switch (units)
            {
            case    ReportUnits.Imperial:
                    b.Append(Range.ToString(Distance.Unit.Yard, true, 1));
                    b.Append(';');
                    b.Append(BulletVelocity.ToString(Velocity.Unit.FeetPerSecond, true, 0));
                    b.Append(';');
                    b.Append(BulletEnergy.ToString(Energy.Unit.FootPounds, true, 0));
                    b.Append(';');
                    b.Append(Path.ToString(Distance.Unit.Inch, true, 2));
                    b.Append(';');
                    b.Append(Hold.ToString(Angle.Unit.MilDot, true, 2));
                    b.Append(';');
                    b.Append(Time.ToString().Substring(3));
                    b.Append(';');
                    b.Append(Windage.ToString(Distance.Unit.Inch, true, 2));
                    b.Append(';');
                    b.Append(WindageCorrection.ToString(Angle.Unit.MilDot, true, 2));
                    break;
            case    ReportUnits.Metric:
                    b.Append(Range.ToString(Distance.Unit.Meter, true, 1));
                    b.Append(';');
                    b.Append(BulletVelocity.ToString(Velocity.Unit.MeterPerSecond, true, 0));
                    b.Append(';');
                    b.Append(BulletEnergy.ToString(Energy.Unit.Joule, true, 0));
                    b.Append(';');
                    b.Append(Path.ToString(Distance.Unit.Centimeter, true, 2));
                    b.Append(';');
                    b.Append(Hold.ToString(Angle.Unit.AMil, true, 2));
                    b.Append(';');
                    b.Append(Time.ToString().Substring(3));
                    b.Append(';');
                    b.Append(Windage.ToString(Distance.Unit.Centimeter, true, 2));
                    b.Append(';');
                    b.Append(WindageCorrection.ToString(Angle.Unit.AMil, true, 2));
                    break;
            }
            return b.ToString();
        }
    }

    public class BallisticInfoCollection : IEnumerable<BallisticInfo>
    {
        private string mName;

        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        private List<BallisticInfo> mList = new List<BallisticInfo>();

        public IEnumerator<BallisticInfo> GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return mList.Count;
            }

        }

        public BallisticInfo this[int index]
        {
            get
            {
                return mList[index];
            }

        }

        internal void Add(BallisticInfo info)
        {
            mList.Add(info);
        }

        internal void Reserve(int count)
        {
            mList.Capacity = count;
        }

        override public string ToString()
        {
            return ToString(ReportUnits.Imperial);
        }

        public string ToString(ReportUnits units)
        {
            StringBuilder b = new StringBuilder();
            b.Append("Distance;Velocity;Energy;Path;Hold;Time;Windage;Windage Adj");
            b.AppendLine();
            foreach (BallisticInfo info in mList)
            {
                b.Append(info.ToString(units));
                b.AppendLine();
            }
            return b.ToString();
        }
    }

}