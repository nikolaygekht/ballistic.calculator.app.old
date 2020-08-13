using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;
using ZedGraph;


namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class BallisticGraphControl : UserControl, IMeasurementSystemListener
    {
        private const int MAX_SERIA = 4;
        private BallisticInfoMultiModel mModel = new BallisticInfoMultiModel(MAX_SERIA);

        public MeasurementSystem MeasurementSystem
        {
            get
            {

                return mModel.MeasurementSystem;
            }
            set
            {
                bool changed = mModel.MeasurementSystem != value;
                mModel.MeasurementSystem = value;
                if (changed)
                    FillGraph();

            }
        }

        public Angle.Unit AngleUnits
        {
            get
            {
                return mModel.AngleUnits;
            }
            set
            {
                bool changed = mModel.AngleUnits != value;
                mModel.AngleUnits = value;
                if (changed)
                    FillGraph();
            }
        }

        public ColumnData GraphData
        {
            get
            {
                return mModel.GraphData;
            }
            set
            {
                mModel.GraphData = value;
                FillGraph();
            }
        }

        public bool SetData(int seria, BallisticInfoCollection collection)
        {
            if (seria >= 0 && seria < MAX_SERIA)
            {
                mModel.SetSeria(seria, collection);
                FillGraph();
            }
            for (int i = 0; i < MAX_SERIA; i++)
                if (mModel.HasSeria(i))
                    return true;
            return false;

        }


        public BallisticGraphControl()
        {
            InitializeComponent();
            zedGraphControl.IsShowHScrollBar = true;
            zedGraphControl.IsShowVScrollBar = true;
            zedGraphControl.IsShowContextMenu = true;
            zedGraphControl.PanButtons = System.Windows.Forms.MouseButtons.Left;
            zedGraphControl.PanModifierKeys = Keys.None;
            zedGraphControl.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            zedGraphControl.ZoomModifierKeys = Keys.Shift;

            zedGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl.GraphPane.XAxis.MinorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.MinorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.Scale.MaxAuto = true;
            zedGraphControl.GraphPane.YAxis.Scale.MinAuto = true;
            zedGraphControl.GraphPane.YAxis.Scale.MaxGrace = 0.1;
            zedGraphControl.GraphPane.XAxis.Scale.MaxGrace = 0;
            zedGraphControl.IsShowPointValues = true;
        }

        private static Color[] COLORS = new Color[] { Color.Red, Color.Blue, Color.Brown, Color.Green} ;

        public void FillGraph()
        {
            zedGraphControl.GraphPane.CurveList.Clear();
            zedGraphControl.GraphPane.Title.Text = mModel.GetGraphName(false);
            zedGraphControl.GraphPane.YAxis.Title.Text = mModel.GetGraphName(true);
            zedGraphControl.GraphPane.XAxis.Title.Text = "Range" + "(" + ((mModel.MeasurementSystem == MeasurementSystem.Imperial) ? "yd" : "m") + ")";

            for (int i = 0; i < MAX_SERIA; i++)
            {
                if (mModel.HasSeria(i))
                {
                    List<BallisticModel.GraphPair> pairs = mModel.GetGraphData(i);
                    PointPairList list = new PointPairList();
                    foreach (BallisticModel.GraphPair pair in pairs)
                        list.Add(new PointPair(pair.X, pair.Y, pair.Tag));
                    LineItem myCurve = zedGraphControl.GraphPane.AddCurve(mModel.SeriaName(i), list, COLORS[i], SymbolType.Circle);
                    myCurve.IsVisible = true;
                }
            }

            zedGraphControl.AxisChange();
            zedGraphControl.RestoreScale(zedGraphControl.GraphPane);
            zedGraphControl.Invalidate();
        }

        private void UpdateChartRange()
        {
            int found = 0;
            double x0, x1;
            double min = Double.MaxValue, max = Double.MinValue;
            x0 = zedGraphControl.GraphPane.XAxis.Scale.Min;
            x1 = zedGraphControl.GraphPane.XAxis.Scale.Max;

            for (int i = 0; i < zedGraphControl.GraphPane.CurveList.Count; i++)
            {
                CurveItem curve = zedGraphControl.GraphPane.CurveList[i];
                for (int j = 0; j < curve.Points.Count; j++)
                {
                    PointPair p = curve.Points[j];
                    if (p.X >= x0 && p.X <= x1)
                    {
                        if (p.Y > max)
                            max = p.Y;
                        if (p.Y < min)
                            min = p.Y;
                        found++;
                    }
                }
                if (found > 0)
                {
                    double r = max - min;
                    if (r == 0)
                        r = 10;
                    zedGraphControl.GraphPane.YAxis.Scale.Max = max + r / 10;
                    zedGraphControl.GraphPane.YAxis.Scale.Min = min - r / 10;
                    zedGraphControl.AxisChange();
                    zedGraphControl.Invalidate();
                }
            }
        }

        private void zedGraphControl_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            UpdateChartRange();
        }

        private void zedGraphControl_ScrollDoneEvent(ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState)
        {
            UpdateChartRange();
        }

        public void ZoomAll()
        {
            zedGraphControl.RestoreScale(zedGraphControl.GraphPane);
        }

        public void SavePicture()
        {
            zedGraphControl.SaveAsBitmap();
        }

        public void CopyPicture()
        {
            zedGraphControl.Copy(false);
        }
    }
}
