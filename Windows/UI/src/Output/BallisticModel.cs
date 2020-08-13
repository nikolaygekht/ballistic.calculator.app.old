using System;
using System.Collections.Generic;
using System.Text;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    public enum MeasurementSystem
    {
        Metric,
        Imperial
    }

    public enum ColumnData : uint
    {
        Range = 0,
        Velocity = 1,
        Mach = 2,
        Energy = 3,
        Path = 4,
        Hold = 5,
        VClick = 6,
        Windage = 7,
        WindageAdj = 8,
        HClick = 9,
        FlightTime = 10,
        OGW = 11,
        __MAX = 12,
    }
    public class BallisticModel
    {
        private BallisticInfoCollection mBallisticInfo = null;

        public BallisticInfoCollection BallisticInfo
        {
            get
            {
                return mBallisticInfo;
            }
            set
            {
                mBallisticInfo = value;
            }
        }

        private MeasurementSystem mMeasurementSystem;

        public MeasurementSystem MeasurementSystem
        {
            get
            {

                return mMeasurementSystem;
            }
            set
            {
                mMeasurementSystem = value;
            }
        }

        private Angle.Unit mAngleUnits = Angle.Unit.MilDot;

        public Angle.Unit AngleUnits
        {
            get
            {
                return mAngleUnits;
            }
            set
            {
                mAngleUnits = value;
            }
        }


        public bool GetOneRowDataDisplay(int item, ref string[] arr)
        {
            return GetOneRowDataDisplay(mBallisticInfo, item, ref arr);
        }

        protected bool GetOneRowDataDisplay(BallisticInfoCollection collection, int item, ref string[] arr)
        {
            return GetOneRowDataExport(collection, true, item, ref arr);
        }

        public void GetHeaderDataExport(ref string[] arr)
        {
            GetHeaderDataExport(mBallisticInfo, ref arr);
        }

        protected void GetHeaderDataExport(BallisticInfoCollection collection, ref string[] arr)
        {
            if (arr == null || arr.Length != (uint)ColumnData.__MAX)
                arr = new string[(uint)ColumnData.__MAX];

            for (int i = 0; i < arr.Length; i++)
                arr[i] = null;

            if (mMeasurementSystem == UI.MeasurementSystem.Imperial)
            {
                arr[(uint)ColumnData.Range] = "Range(yd)";
                arr[(uint)ColumnData.Velocity] = "Velocity(ft/s)";
                arr[(uint)ColumnData.Energy] = "Energy(ft-lbs)";
                arr[(uint)ColumnData.Path] = "Path(in)";
                arr[(uint)ColumnData.Windage] = "Wng(in)";
                arr[(uint)ColumnData.OGW] = "OGW(lbs)";
            }
            else if (mMeasurementSystem == UI.MeasurementSystem.Metric)
            {
                arr[(uint)ColumnData.Range] = "Range(m)";
                arr[(uint)ColumnData.Velocity] = "Velocity(m/s)";
                arr[(uint)ColumnData.Energy] = "Energy(J)";
                arr[(uint)ColumnData.Path] = "Path(cm)";
                arr[(uint)ColumnData.Windage] = "Wng(cm)";
                arr[(uint)ColumnData.OGW] = "OGW(kg)";

            }

            string au = Angle.UnitToName(mAngleUnits);

            arr[(uint)ColumnData.Mach] = "Mach";
            arr[(uint)ColumnData.Hold] = "Hold(" + au + ")";
            arr[(uint)ColumnData.WindageAdj] = "Wng.Adj(" + au + ")";
            arr[(uint)ColumnData.FlightTime] = "Time(sec)";
            arr[(uint)ColumnData.VClick] = "Vclcks";
            arr[(uint)ColumnData.HClick] = "Hclcks";
        }

        public bool GetOneRowDataExport(int item, ref string[] arr)
        {
            return GetOneRowDataExport(mBallisticInfo, item, ref arr);
        }

        protected bool GetOneRowDataExport(BallisticInfoCollection collection, int item, ref string[] arr)
        {
            return GetOneRowDataExport(collection, false, item, ref arr);
        }

        public void GetFormat(ref string[] arr)
        {
            if (arr == null || arr.Length != (uint)ColumnData.__MAX)
                arr = new string[(uint)ColumnData.__MAX];

            arr[(uint)ColumnData.Range] = "#,##0";
            arr[(uint)ColumnData.Velocity] = "#,##0.0";
            arr[(uint)ColumnData.Energy] = "#,##0";
            arr[(uint)ColumnData.Path] = "#,##0.00";
            arr[(uint)ColumnData.Windage] = "#,##0.00";
            arr[(uint)ColumnData.OGW] = "#,##0";
            arr[(uint)ColumnData.Mach] = "#.000";
            arr[(uint)ColumnData.Hold] = "#.00" ;
            arr[(uint)ColumnData.WindageAdj] = "#.00";
            arr[(uint)ColumnData.VClick] = "#";
            arr[(uint)ColumnData.HClick] = "#";
            arr[(uint)ColumnData.FlightTime] = "@";
        }


        protected bool GetOneRowDataExport(BallisticInfoCollection collection, bool groupThousands, int item, ref string[] arr)
        {
            if (collection == null || item < 0 || item >= collection.Count)
                return false;

            BallisticInfo info = collection[item];

            if (arr == null || arr.Length != (uint)ColumnData.__MAX)
                arr = new string[(uint)ColumnData.__MAX];

            for (int i = 0; i < arr.Length; i++)
                arr[i] = null;

            if (mMeasurementSystem == UI.MeasurementSystem.Imperial)
            {
                arr[(uint)ColumnData.Range] = info.Range.ToString(Distance.Unit.Yard, false, groupThousands, 0);
                arr[(uint)ColumnData.Velocity] = info.BulletVelocity.ToString(Velocity.Unit.FeetPerSecond, false, groupThousands, 1);
                arr[(uint)ColumnData.Energy] = info.BulletEnergy.ToString(Energy.Unit.FootPounds, false, groupThousands, 0);
                arr[(uint)ColumnData.Path] = info.Path.ToString(Distance.Unit.Inch, false, groupThousands, 2);
                arr[(uint)ColumnData.Windage] = info.Windage.ToString(Distance.Unit.Inch, false, groupThousands, 2);
                arr[(uint)ColumnData.OGW] = info.OptimalGameWeight.ToString(Weight.Unit.Pound, false, groupThousands, 0);

            }
            else if (mMeasurementSystem == UI.MeasurementSystem.Metric)
            {
                arr[(uint)ColumnData.Range] = info.Range.ToString(Distance.Unit.Meter, false, groupThousands, 0);
                arr[(uint)ColumnData.Velocity] = info.BulletVelocity.ToString(Velocity.Unit.MeterPerSecond, false, groupThousands, 1);
                arr[(uint)ColumnData.Energy] = info.BulletEnergy.ToString(Energy.Unit.Joule, false, groupThousands, 0);
                arr[(uint)ColumnData.Path] = info.Path.ToString(Distance.Unit.Centimeter, false, groupThousands, 2);
                arr[(uint)ColumnData.Windage] = info.Windage.ToString(Distance.Unit.Centimeter, false, groupThousands, 2);
                arr[(uint)ColumnData.OGW] = info.OptimalGameWeight.ToString(Weight.Unit.Kilogram, false, groupThousands, 0);
            }
            arr[(uint)ColumnData.Mach] = info.Mach.ToString("f3");
            if (item == 0)
            {
                arr[(uint)ColumnData.Hold] = "";
                arr[(uint)ColumnData.WindageAdj] = "";
                arr[(uint)ColumnData.VClick] = "";
                arr[(uint)ColumnData.HClick] = "";

            }
            else
            {
                arr[(uint)ColumnData.Hold] = info.Hold.ToString(mAngleUnits, false, false, 2);
                arr[(uint)ColumnData.WindageAdj] = info.WindageCorrection.ToString(mAngleUnits, false, false, 2);
                arr[(uint)ColumnData.VClick] = info.HoldClicks.ToString();
                arr[(uint)ColumnData.HClick] = info.WindageClicks.ToString();

            }
            arr[(uint)ColumnData.FlightTime] = ConvertFlightTime(info.Time);
            return true;
        }

        protected string ConvertFlightTime(TimeSpan ts)
        {
            return ts.TotalSeconds.ToString("f3");
        }

        private ColumnData mGraphData = ColumnData.Path;

        public ColumnData GraphData
        {
            get
            {
                return mGraphData;
            }
            set
            {
                mGraphData = value;
            }
        }

        public class GraphPair
        {
            private double mX, mY;
            private string mTag;

            public double X
            {
                get
                {
                    return mX;
                }
            }

            public double Y
            {
                get
                {
                    return mY;
                }
            }

            public string Tag
            {
                get
                {
                    return mTag;
                }
            }

            public GraphPair(double x, double y, string tag)
            {
                mX = x;
                mY = y;
                mTag = tag;
            }
        }

        public List<GraphPair> GetGraphData()
        {
            return GetGraphData(mBallisticInfo);
        }

        public string GetGraphName(bool addUnit)
        {
            switch (mGraphData)
            {
                case ColumnData.Path:
                    return "Path" + (addUnit ? " (" + (MeasurementSystem == UI.MeasurementSystem.Imperial ? "in" : "cm") + ")" : "");
                case ColumnData.Hold:
                    return "Hold" + (addUnit ? " (" + Angle.UnitToName(AngleUnits) + ")" : "");
                case ColumnData.Mach:
                    return "Mach";
                case ColumnData.Windage:
                    return "Windage" + (addUnit ? " (" + (MeasurementSystem == UI.MeasurementSystem.Imperial ? "in" : "cm") + ")" : "");
                case ColumnData.WindageAdj:
                    return "Windage Adjustment" + (addUnit ? " (" + Angle.UnitToName(AngleUnits) + ")" : "");
                case ColumnData.OGW:
                    return "Optimal Game Weight" +  (addUnit ? " (" + (MeasurementSystem == UI.MeasurementSystem.Imperial ? "lbs" : "kg") + ")" : "");
                case ColumnData.Energy:
                    return "Energy" + (addUnit ? " (" + (MeasurementSystem == UI.MeasurementSystem.Imperial ? "ft-lbs" : "J") + ")" : "");
                case ColumnData.Velocity:
                    return "Velocity" + (addUnit ? " (" + (MeasurementSystem == UI.MeasurementSystem.Imperial ? "ft/s" : "m/s") + ")" : ""); ; 
                case ColumnData.FlightTime:
                    return "Flight Time" + (addUnit ? " (s)" : "");
                case ColumnData.HClick:
                    return "Horizontal Clicks";
                case ColumnData.VClick:
                    return "Vertical Clicks";

                default:
                    return "Unknown";
            }
        }

        protected List<GraphPair> GetGraphData(BallisticInfoCollection collection)
        {
            if (collection == null)
                return null;

            List<GraphPair> pairs = new List<GraphPair>();

            for (int i = 0; i < collection.Count; i++)
            {
                double x, y;
                string tag, at, value;

                x = collection[i].Range.Get(MeasurementSystem == UI.MeasurementSystem.Imperial ? Distance.Unit.Yard : Distance.Unit.Meter);
                at = collection[i].Range.ToString(MeasurementSystem == UI.MeasurementSystem.Imperial ? Distance.Unit.Yard : Distance.Unit.Meter, true, 0);

                switch (mGraphData)
                {
                    case ColumnData.Path:
                        y = collection[i].Path.Get(MeasurementSystem == UI.MeasurementSystem.Imperial ? Distance.Unit.Inch : Distance.Unit.Centimeter);
                        value = collection[i].Path.ToString(MeasurementSystem == UI.MeasurementSystem.Imperial ? Distance.Unit.Inch : Distance.Unit.Centimeter, true, 1);
                        break;
                    case ColumnData.Hold:
                        if (i == 0)
                            continue;
                        y = collection[i].Hold.Get(AngleUnits);
                        value = collection[i].Hold.ToString(AngleUnits, false, 1);
                        break;
                    case ColumnData.Mach:
                        y = collection[i].Mach;
                        value = y.ToString("f2");
                        break;
                    case ColumnData.Windage:
                        y = collection[i].Windage.Get(MeasurementSystem == UI.MeasurementSystem.Imperial ? Distance.Unit.Inch : Distance.Unit.Centimeter);
                        value = collection[i].Windage.ToString(MeasurementSystem == UI.MeasurementSystem.Imperial ? Distance.Unit.Inch : Distance.Unit.Centimeter, true, 1);
                        break;
                    case ColumnData.WindageAdj:
                        if (i == 0)
                            continue;
                        y = collection[i].WindageCorrection.Get(AngleUnits);
                        value = collection[i].WindageCorrection.ToString(AngleUnits, false, 1);
                        break;
                    case ColumnData.OGW:
                        y = collection[i].OptimalGameWeight.Get(MeasurementSystem == UI.MeasurementSystem.Imperial ? Weight.Unit.Pound : Weight.Unit.Kilogram);
                        value = collection[i].OptimalGameWeight.ToString(MeasurementSystem == UI.MeasurementSystem.Imperial ? Weight.Unit.Pound : Weight.Unit.Kilogram, false, 0);
                        break;
                    case ColumnData.Energy:
                        y = collection[i].BulletEnergy.Get(MeasurementSystem == UI.MeasurementSystem.Imperial ? Energy.Unit.FootPounds : Energy.Unit.Joule);
                        value = collection[i].BulletEnergy.ToString(MeasurementSystem == UI.MeasurementSystem.Imperial ? Energy.Unit.FootPounds : Energy.Unit.Joule, true, 0);
                        break;
                    case ColumnData.Velocity:
                        y = collection[i].BulletVelocity.Get(MeasurementSystem == UI.MeasurementSystem.Imperial ? Velocity.Unit.FeetPerSecond : Velocity.Unit.MeterPerSecond);
                        value = collection[i].BulletVelocity.ToString(MeasurementSystem == UI.MeasurementSystem.Imperial ? Velocity.Unit.FeetPerSecond : Velocity.Unit.MeterPerSecond, true, 1);
                        break;
                    case ColumnData.FlightTime:
                        y = collection[i].Time.TotalSeconds;
                        value = ConvertFlightTime(collection[i].Time);
                        break;
                    case ColumnData.HClick:
                        if (i == 0)
                            continue;
                        y = collection[i].WindageClicks;
                        value = y.ToString();
                        break;
                    case ColumnData.VClick:
                        if (i == 0)
                            continue;
                        y = collection[i].HoldClicks;
                        value = y.ToString();
                        break;
                    default:
                        return null;
                }

                tag = string.Format("{0} @ {1}", value, at);
                pairs.Add(new GraphPair(x, y, tag));
            }
            return pairs;
        }
    }

    public class BallisticInfoMultiModel : BallisticModel
    {
        private BallisticInfoCollection[] mSeries = null;

        public BallisticInfoMultiModel(int seriesCount)
        {
            mSeries = new BallisticInfoCollection[seriesCount];
        }

        public void SetSeria(int seria, BallisticInfoCollection collection)
        {
            if (seria >= 0 && seria < mSeries.Length)
                mSeries[seria] = collection;
            if (seria == 0)
                BallisticInfo = collection;
        }

        public bool HasSeria(int seria)
        {
            if (seria < 0 || seria >= mSeries.Length)
                return false;
            return mSeries[seria] != null;
        }

        public void RemoveSeria(int seria)
        {
            if (HasSeria(seria))
                mSeries[seria] = null;
            if (seria == 0)
                BallisticInfo = null;
        }

        public string SeriaName(int seria)
        {
            if (seria < 0 || seria >= mSeries.Length)
                return "";
            return mSeries[seria].Name;

        }

        public List<GraphPair> GetGraphData(int seria)
        {
            if (!HasSeria(seria))
                return null;

            return GetGraphData(mSeries[seria]);
        }

    }
}
