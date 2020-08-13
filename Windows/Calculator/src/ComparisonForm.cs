using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gehtsoft.BallisticCalculator.UI;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator
{
    public partial class ComparisonForm : Form
    {
        private MeasurementSystem mMeasurementSystem = MeasurementSystem.Imperial;

        public ComparisonForm()
        {
            InitializeComponent();
        }

        public void UpdateMeasurementSystem(MeasurementSystem ms)
        {
            ballisticGraph.MeasurementSystem = ms;
            menuItemTraceImperial.Enabled = ms != MeasurementSystem.Imperial;
            menuItemTraceImperial.Checked = ms == MeasurementSystem.Imperial;
            menuItemTraceMetric.Enabled = ms == MeasurementSystem.Imperial;
            menuItemTraceMetric.Checked = ms != MeasurementSystem.Imperial;
        }
        public void UpdateAngle(Angle.Unit unit)
        {
            ballisticGraph.AngleUnits = unit;

            menuItemTraceMil.Enabled = unit != Angle.Unit.Mil;
            menuItemTraceMil.Checked = unit == Angle.Unit.Mil;
            menuItemTraceMilDot.Enabled = unit != Angle.Unit.MilDot;
            menuItemTraceMilDot.Checked = unit == Angle.Unit.MilDot;
            menuItemTraceMOA.Enabled = unit != Angle.Unit.Moa;
            menuItemTraceMOA.Checked = unit == Angle.Unit.Moa;
            menuItemTraceAMil.Enabled = unit != Angle.Unit.AMil;
            menuItemTraceAMil.Checked = unit == Angle.Unit.AMil;
            menuItemTraceInPer100Y.Enabled = unit != Angle.Unit.InPer100Yards;
            menuItemTraceInPer100Y.Checked = unit == Angle.Unit.InPer100Yards;
            menuItemTraceCmPer100M.Enabled = unit != Angle.Unit.CmPer100Meters;
            menuItemTraceCmPer100M.Checked = unit == Angle.Unit.CmPer100Meters;

        }


        private void menuItemTraceImperial_Click(object sender, EventArgs e)
        {
            mMeasurementSystem = MeasurementSystem.Imperial;
            UpdateMeasurementSystem(mMeasurementSystem);
        }

        private void menuItemTraceMetric_Click(object sender, EventArgs e)
        {
            mMeasurementSystem = MeasurementSystem.Metric;
            UpdateMeasurementSystem(mMeasurementSystem);
        }

        private void menuItemTraceMil_Click(object sender, EventArgs e)
        {
            UpdateAngle(Angle.Unit.Mil);
        }

        private void menuItemTraceMilDot_Click(object sender, EventArgs e)
        {
            UpdateAngle(Angle.Unit.MilDot);
        }

        private void menuItemTraceAMil_Click(object sender, EventArgs e)
        {
            UpdateAngle(Angle.Unit.AMil);
        }

        private void menuItemTraceMOA_Click(object sender, EventArgs e)
        {
            UpdateAngle(Angle.Unit.Moa);
        }

        private void menuItemTraceInPer100Y_Click(object sender, EventArgs e)
        {
            UpdateAngle(Angle.Unit.InPer100Yards);
        }

        private void menuItemTraceCmPer100M_Click(object sender, EventArgs e)
        {
            UpdateAngle(Angle.Unit.CmPer100Meters);
        }


        class GraphMenuType
        {
            public ToolStripMenuItem menuItem;
            public ColumnData dataType;

            internal GraphMenuType(ToolStripMenuItem item, ColumnData type)
            {
                menuItem = item;
                dataType = type;
            }
        }


        GraphMenuType[] mGraphTypes = null;
        GraphMenuType[] GraphTypes
        {
            get
            {
                if (mGraphTypes == null)
                {
                    mGraphTypes = new GraphMenuType[] {
                        new GraphMenuType(menuItemTraceChartVelocity, ColumnData.Velocity),
                        new GraphMenuType(menuItemTraceChartMach, ColumnData.Mach),
                        new GraphMenuType(menuItemTraceChartTime, ColumnData.FlightTime),

                        new GraphMenuType(menuItemTraceChartHold, ColumnData.Hold),
                        new GraphMenuType(menuItemTraceChartPath, ColumnData.Path),

                        new GraphMenuType(menuItemTraceChartWindage, ColumnData.Windage),
                        new GraphMenuType(menuItemTraceChartWindageAdj, ColumnData.WindageAdj),

                        new GraphMenuType(menuItemTraceChartEnergy, ColumnData.Energy),
                        new GraphMenuType(menuItemTraceChartOGW, ColumnData.OGW),


                    };

                }
                return mGraphTypes;
            }
        }

        private void graphTypeMenuItem_Click(object sender, EventArgs e)
        {
            GraphMenuType[] types = GraphTypes;
            foreach (GraphMenuType t in types)
                if (t.menuItem == sender)
                    SetGraphType(t.dataType);
        }

        private void SetGraphType(ColumnData data)
        {
            ballisticGraph.GraphData = data;
            GraphMenuType[] types = GraphTypes;
            foreach (GraphMenuType t in types)
            {
                t.menuItem.Checked = (data == t.dataType);
                t.menuItem.Enabled = (data != t.dataType);
            }
        }

        public void AddSeria(int seria, BallisticInfoCollection collection)
        {
            ballisticGraph.SetData(seria, collection);
        }

        private void removeFirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ballisticGraph.SetData(0, null))
                this.Close();
        }

        private void removeSecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ballisticGraph.SetData(1, null))
                this.Close();
        }

        private void removeThirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ballisticGraph.SetData(2, null))
                this.Close();
        }

        private void removeFourthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ballisticGraph.SetData(3, null))
                this.Close();
        }

        private void zoomAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ballisticGraph.ZoomAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ballisticGraph.CopyPicture();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ballisticGraph.SavePicture();
        }
    }
}
