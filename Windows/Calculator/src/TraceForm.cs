using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gehtsoft.BallisticCalculator.UI;
using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator
{
    public partial class TraceForm : Form
    {
        FormState mState = new FormState();
        private TraceInfo mTraceInfo = null;

        public TraceForm()
        {
            InitializeComponent();
            bulletInfoInput.AddControlsToFormState(mState);
            atmoInfoInput.AddControlsToFormState(mState);
            zeroInfoInput.AddControlsToFormState(mState);
            spinDriftInfoInput.AddControlsToFormState(mState);
            shotParametersInput.AddControlsToFormState(mState);
            mState.AddControl("table", ballisticTable);
            mState.AddControl("graph", ballisticGraph);

            //prevent resizing under wine
            distanceShotDistance.Width = labelReticleControlsWidth.Width;
            distanceShotDistance.LockSize(true);
            distanceShotTargetHeight.Width = labelReticleControlsWidth.Width;
            distanceShotTargetHeight.LockSize(true);
            distanceShotTargetWidth.Width = labelReticleControlsWidth.Width;
            distanceShotTargetWidth.LockSize(true);
            distanceDangerZoneFrom.Width = labelReticleControlsWidth.Width;
            distanceDangerZoneFrom.LockSize(true);
            distanceDangerZoneTo.Width = labelReticleControlsWidth.Width;
            distanceDangerZoneTo.LockSize(true);
        }

        private MeasurementSystem mMeasurementSystem;

        public void RestoreTraceInfo(TraceInfo traceInfo)
        {
            mTraceInfo = traceInfo.Clone();

            SetDefault(mTraceInfo.Metric ? MeasurementSystem.Metric : MeasurementSystem.Imperial);

            bulletInfoInput.BulletName = traceInfo.TraceName;
            bulletInfoInput.BallisticCoefficient = traceInfo.BallisticCoefficient;
            bulletInfoInput.DragTable = traceInfo.DrageTable;
            bulletInfoInput.BulletWeight = traceInfo.BulletWeight;
            bulletInfoInput.MuzzleVelocity = traceInfo.MuzzleVelocity;

            zeroInfoInput.SightHeight = traceInfo.SightHeight;
            zeroInfoInput.ZeroDistance = traceInfo.ZeroDistance;
            zeroInfoInput.UseZeroElevationAngle = true;
            zeroInfoInput.ZeroElevationAngle = traceInfo.ZeroElevationAngle;

            spinDriftInfoInput.CalculateSpinDrift = traceInfo.DriftInfo;
            if (spinDriftInfoInput.CalculateSpinDrift)
            {
                spinDriftInfoInput.BulletDiameter = traceInfo.BulletDiameter;
                spinDriftInfoInput.BulletLength = traceInfo.BulletLength;
                spinDriftInfoInput.Rifling = traceInfo.RiflingTwist;
                spinDriftInfoInput.RightHandRifling = traceInfo.RiflingRightHandTwist;
            }

            shotParametersInput.VerticalClick = traceInfo.VerticalClick;
            shotParametersInput.HorizontalClick = traceInfo.HorizonalClick;
        }

        public void SetDefault(MeasurementSystem ms)
        {
            mMeasurementSystem = ms;
            Text = "Trace: New";
            bulletInfoInput.Initialize(ms);
            zeroInfoInput.Initialize(ms);
            atmoInfoInput.Initialize(ms);
            shotParametersInput.Initialize(ms);
            SetGraphType(ColumnData.Path);
            UpdateMeasurementSystem(ms);
            UpdateAngle(Angle.Unit.MilDot);
        }

        public void UpdateMeasurementSystem(MeasurementSystem ms)
        {
            bulletInfoInput.MeasurementSystem = ms;
            zeroInfoInput.MeasurementSystem = ms;
            atmoInfoInput.MeasurementSystem = ms;
            shotParametersInput.MeasurementSystem = ms;
            ballisticGraph.MeasurementSystem = ms;
            ballisticTable.MeasurementSystem = ms;
            spinDriftInfoInput.MeasurementSystem = ms;

            menuItemTraceImperial.Enabled = ms != MeasurementSystem.Imperial;
            menuItemTraceImperial.Checked = ms == MeasurementSystem.Imperial;
            menuItemTraceMetric.Enabled = ms == MeasurementSystem.Imperial;
            menuItemTraceMetric.Checked = ms != MeasurementSystem.Imperial;

            if (ms == MeasurementSystem.Imperial)
            {
                distanceShotDistance.Unit = Distance.Unit.Yard;
                distanceShotTargetWidth.Unit = Distance.Unit.Inch;
                distanceShotTargetHeight.Unit = Distance.Unit.Inch;
                distanceDangerZoneFrom.Unit = Distance.Unit.Yard;
                distanceDangerZoneTo.Unit = Distance.Unit.Yard;
            }
            else
            {
                distanceShotDistance.Unit = Distance.Unit.Meter;
                distanceShotTargetWidth.Unit = Distance.Unit.Centimeter;
                distanceShotTargetHeight.Unit = Distance.Unit.Centimeter;
                distanceDangerZoneFrom.Unit = Distance.Unit.Meter;
                distanceDangerZoneTo.Unit = Distance.Unit.Meter;

            }

        }

        public void UpdateAngle(Angle.Unit unit)
        {
            ballisticGraph.AngleUnits = unit;
            ballisticTable.AngleUnits = unit;
            reticleControl1.AngleUnits = unit;

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

        private void bulletInfoInput1_OnBulletNameChanged(object sender, EventArgs e)
        {
            string n = bulletInfoInput.BulletName;
            if (n == null || n == "")
                Text = "Trace: New";
            else
                Text = "Trace: " + n;
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

        private BallisticInfoCollection mBallisticInfo = null;
        private BallisticInfoCollection mShortDistanceBallisticInfo = null;

        private void calculateTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calculate zero
            ShotInfo shotInfo = new ShotInfo();
            shotInfo.Ammo = bulletInfoInput.GetAmmoInfo();
            shotInfo.Atmosphere = atmoInfoInput.GetAtmosphere();
            zeroInfoInput.FillShotInfo(shotInfo);
            Distance sightHeight = shotInfo.SightHeight;
            Distance zeroDistance = shotInfo.ZeroDistance;
            Angle zeroElevation = shotInfo.ElevationAngle;
            reticleControl1.Zero = shotInfo.ZeroDistance;
            int clicks = shotInfo.Clicks;

            if (shotInfo.ElevationAngle == null)
            {
                if (!ShotInfoController.Calculation.calculateZero(shotInfo))
                {
                    MessageBox.Show("Zero elevation cannot be calculated", "Trace calculation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                zeroElevation = shotInfo.ElevationAngle;
            }
            shotInfo = new ShotInfo();
            shotInfo.Ammo = bulletInfoInput.GetAmmoInfo();
            shotInfo.Atmosphere = atmoInfoInput.GetAtmosphere();
            shotInfo.Wind = atmoInfoInput.GetWind();
            shotInfo.ElevationAngle = zeroElevation;
            shotInfo.DriftInfo = spinDriftInfoInput.GetDriftInfo();
            shotInfo.SightHeight = sightHeight;
            shotInfo.Clicks = clicks;
            shotParametersInput.FillShotInfo(shotInfo);

            BallisticInfoCollection ballisticInfo = ShotInfoController.Calculation.calculateShot(shotInfo);
            ballisticInfo.Name = bulletInfoInput.BulletName;

            ballisticTable.BallisticInfo = ballisticInfo;
            ballisticGraph.SetData(0, ballisticInfo);
            mBallisticInfo = ballisticInfo;

            shotInfo.MaxDistance = new Distance(100, shotInfo.MaxDistance.SetUnit);
            shotInfo.Step = new Distance(1, shotInfo.MaxDistance.SetUnit);
            mShortDistanceBallisticInfo = ShotInfoController.Calculation.calculateShot(shotInfo);

            reticleControl1.FarRangeBallistic = ballisticInfo;
            reticleControl1.ShortRangeBallistic = mShortDistanceBallisticInfo;

            mTraceInfo = new TraceInfo();
            mTraceInfo.TraceName = bulletInfoInput.BulletName;
            mTraceInfo.BallisticCoefficient = shotInfo.Ammo.BallisticCoefficient;
            mTraceInfo.DrageTable = shotInfo.Ammo.Table;
            mTraceInfo.MuzzleVelocity = shotInfo.Ammo.MuzzleVelocity;
            mTraceInfo.BulletWeight = shotInfo.Ammo.BulletWeight;
            mTraceInfo.SightHeight = sightHeight;
            mTraceInfo.ZeroDistance = zeroDistance;
            mTraceInfo.ZeroElevationAngle = zeroElevation;
            if (shotInfo.DriftInfo != null)
            {
                mTraceInfo.DriftInfo = true;
                mTraceInfo.BulletLength = shotInfo.DriftInfo.BulletLength;
                mTraceInfo.BulletDiameter = shotInfo.DriftInfo.BulletDiameter;
                mTraceInfo.RiflingTwist = shotInfo.DriftInfo.RiflingTwist;
                mTraceInfo.RiflingRightHandTwist = shotInfo.DriftInfo.RightHandTwist;
            }
            else
                mTraceInfo.DriftInfo = false;

            mTraceInfo.VerticalClick = shotInfo.VerticalClick;
            mTraceInfo.HorizonalClick = shotInfo.HorizonalClick;
            mTraceInfo.Metric = mMeasurementSystem == MeasurementSystem.Metric;

            if (!zeroInfoInput.UseZeroElevationAngle)
                zeroInfoInput.ZeroElevationAngle = zeroElevation.ToUnit(zeroInfoInput.ZeroElevationAngle.SetUnit);
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

        private void buttonLoadReticle_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Reticle Files (*.reticle)|*.reticle|All Files|*.*";
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Program.GetSubDir("reticles");
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Reticle.Reticle reticle = Reticle.ReticleController.Serialization.loadReticle(dlg.FileName, false);
                    if (reticle != null && reticle.Image != null)
                        reticleControl1.Reticle = reticle;
                    reticleControl1.Zoom = 1;
                    toolStripDropDownZoom.Text = "100%";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Reticle Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
        }

        private void checkBoxLRBDC_CheckedChanged(object sender, EventArgs e)
        {
            reticleControl1.ShowLongRangeBDC = checkBoxLRBDC.Checked;
        }

        private void checkBoxSRBDC_CheckedChanged(object sender, EventArgs e)
        {
            reticleControl1.ShowShortRangeBDC = checkBoxSRBDC.Checked;
        }

        private void numericZoomOf_ValueChanged(object sender, EventArgs e)
        {
            numericZoom.Maximum = numericZoomOf.Value;
            reticleControl1.ZoomFactor = (double)(numericZoom.Value / numericZoomOf.Value);
        }

        private void numericZoom_ValueChanged(object sender, EventArgs e)
        {
            reticleControl1.ZoomFactor = (double)(numericZoom.Value / numericZoomOf.Value);
        }

        private void reticleControl1_ReticleMouseMove(object sender, Reticle.ReticleControlMouseEventArgs e)
        {
            reticleStatusHold.Text = e.Hold.ToString(e.Hold.SetUnit);
            reticleStatusWindage.Text = e.Windage.ToString(e.Hold.SetUnit);
        }

        private void menuPopupTraceComparison_DropDownOpening(object sender, EventArgs e)
        {
            menuItemTraceComparisonFirst.Enabled = mBallisticInfo != null;
            menuItemTraceComparisonSecond.Enabled = mBallisticInfo != null;
            menuItemTraceComparisonThird.Enabled = mBallisticInfo != null;
            menuItemTraceComparisonFourth.Enabled = mBallisticInfo != null;
        }

        private void menuItemTraceComparisonFirst_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddToComparsion(0, mBallisticInfo);
        }

        private void menuItemTraceComparisonSecond_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddToComparsion(1, mBallisticInfo);
        }

        private void menuItemTraceComparisonThird_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddToComparsion(2, mBallisticInfo);
        }

        private void menuItemTraceComparisonFourth_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddToComparsion(3, mBallisticInfo);
        }

        private void menuItemTraceChartZoomAll_Click(object sender, EventArgs e)
        {
            ballisticGraph.ZoomAll();
        }

        private void menuItemTraceChartCopyImage_Click(object sender, EventArgs e)
        {
            ballisticGraph.CopyPicture();
        }

        private void menuItemTraceChartSaveImage_Click(object sender, EventArgs e)
        {
            ballisticGraph.SavePicture();
        }

        private void menuItemTraceSaveAsCsv_Click(object sender, EventArgs e)
        {
            ballisticTable.ExportCsv(this);
        }

        private void menuItemTraceSaveAsExcel_Click(object sender, EventArgs e)
        {
            ballisticTable.ExportExcel(this);
        }


        private string mTraceFileName = null;

        public void SaveTrace()
        {
            if (mTraceFileName == null)
                SaveAsTrace();
            try
            {
                mState.GatherFrom();
                mState.Save(mTraceFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Reticle Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        public bool CanSaveTrace()
        {
            return mTraceFileName != null;
        }

        public void SaveAsTrace()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Trace Files (*.trace)|*.trace|All Files|*.*";
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Program.GetSubDir("traces");
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;
            dlg.DefaultExt = "trace";
            dlg.AddExtension = true;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                mTraceFileName = dlg.FileName;
                SaveTrace();
            }
        }

        public void RestoreTrace()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Trace Files (*.trace)|*.trace|All Files|*.*";
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Program.GetSubDir("traces");
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    mState.Load(dlg.FileName);
                    mState.FillForm();
                    if (bulletInfoInput.BulletName != "")
                        Text = "Trace: " + bulletInfoInput.BulletName;
                    UpdateMeasurementSystem(ballisticTable.MeasurementSystem);
                    UpdateAngle(ballisticTable.AngleUnits);
                    SetGraphType(ballisticGraph.GraphData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Trace Load", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

        }

        private void toolStripDropDownZoom_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItem100.Enabled = true;
            toolStripMenuItem100.Checked = reticleControl1.Zoom == 1;
            toolStripMenuItem200.Enabled = reticleControl1.Reticle != null && !reticleControl1.Reticle.Raster;
            toolStripMenuItem200.Checked = reticleControl1.Zoom == 2;
            toolStripMenuItem400.Enabled = reticleControl1.Reticle != null && !reticleControl1.Reticle.Raster;
            toolStripMenuItem400.Checked = reticleControl1.Zoom == 4;

        }

        private void toolStripMenuItem100_Click(object sender, EventArgs e)
        {
            reticleControl1.Zoom = 1;
            toolStripDropDownZoom.Text = "100%";
        }

        private void toolStripMenuItem200_Click(object sender, EventArgs e)
        {
            reticleControl1.Zoom = 2;
            toolStripDropDownZoom.Text = "200%";
        }

        private void toolStripMenuItem400_Click(object sender, EventArgs e)
        {
            reticleControl1.Zoom = 4;
            toolStripDropDownZoom.Text = "400%";
        }

        private void menuItemTraceSendToMobile_Click(object sender, EventArgs e)
        {
            if (mTraceInfo != null && mTraceInfo.Complete)
            {
                string s = bulletInfoInput.BulletName;
                if (s != null && s.Length > 0 && mTraceInfo.TraceName != s)
                    mTraceInfo.TraceName = s;
                Program.MainForm.AddTraceInfo(mTraceInfo.Clone());
            }
        }

        private void menuPopupTrace_DropDownOpening(object sender, EventArgs e)
        {
            menuItemTraceSendToMobile.Enabled = mTraceInfo != null && mTraceInfo.Complete;
            menuItemTraceSaveAsExcel.Enabled = ballisticTable.CanExportCsv;
            menuItemTraceSaveAsCsv.Enabled = ballisticTable.CanExportCsv;
            menuItemTracePrint.Enabled = ballisticTable.CanExportCsv;
        }

        private void checkBoxShowShot_CheckedChanged(object sender, EventArgs e)
        {
            UpdateReticleShot();
        }

        private void shotParameters_ValueChange(object sender, EventArgs e)
        {
            UpdateReticleShot();
        }

        private bool mRecalcDangerArea = false;

        private void UpdateReticleShot()
        {
            if (checkBoxShowShot.Checked)
            {
                reticleControl1.Shot.Distance = distanceShotDistance.Value;
                if (distanceShotTargetHeight.Value.Get(Distance.Unit.Inch) > 0 &&
                    distanceShotTargetWidth.Value.Get(Distance.Unit.Inch) > 0)
                {
                    reticleControl1.Shot.Width = distanceShotTargetWidth.Value;
                    reticleControl1.Shot.Height = distanceShotTargetHeight.Value;

                    if (mTraceInfo != null)
                    {
                        mRecalcDangerArea = true;
                        if (!timerRecalcDanger.Enabled)
                            timerRecalcDanger.Enabled = true;

                    }
                }
                else
                {
                    reticleControl1.Shot.Width = null;
                    reticleControl1.Shot.Height = null;
                    distanceDangerZoneFrom.Clear();
                    distanceDangerZoneTo.Clear();

                }
            }
            else
            {
                reticleControl1.Shot.Distance = null;
                reticleControl1.Shot.Width = null;
                reticleControl1.Shot.Height = null;
                distanceDangerZoneFrom.Clear();
                distanceDangerZoneTo.Clear();
            }
        }

        private void timerRecalcDanger_Tick(object sender, EventArgs e)
        {
            if (mRecalcDangerArea)
            {
                ShotInfo shotInfo = new ShotInfo();
                shotInfo.Ammo = bulletInfoInput.GetAmmoInfo();
                shotInfo.Atmosphere = atmoInfoInput.GetAtmosphere();
                shotInfo.SightHeight = zeroInfoInput.SightHeight;
                shotInfo.ZeroDistance = distanceShotDistance.Value;
                shotInfo.TargetSize = distanceShotTargetHeight.Value;

                BallisticInfoCollection ballisticInfo = ShotInfoController.Calculation.calculateDangerZone(shotInfo);
                if (ballisticInfo.Count == 2 && ballisticInfo[0] != null && ballisticInfo[1] != null)
                {
                    distanceDangerZoneFrom.Value = ballisticInfo[0].Range.ToUnit(distanceDangerZoneFrom.Unit, 0);
                    distanceDangerZoneTo.Value = ballisticInfo[1].Range.ToUnit(distanceDangerZoneFrom.Unit, 0);
                }
                else
                {
                    distanceDangerZoneFrom.Clear();
                    distanceDangerZoneTo.Clear();
                }
            }
            mRecalcDangerArea = false;
        }

        private void buttonTakeBulletFromAmmo_Click(object sender, EventArgs e)
        {
            if (bulletInfoInput.BulletDiameter.Get(bulletInfoInput.BulletDiameter.SetUnit) != 0)
                spinDriftInfoInput.BulletDiameter = bulletInfoInput.BulletDiameter;
            if (bulletInfoInput.BulletLength.Get(bulletInfoInput.BulletLength.SetUnit) != 0)
                spinDriftInfoInput.BulletLength = bulletInfoInput.BulletLength;

        }

        private void menuItemTracePrint_Click(object sender, EventArgs e)
        {
            ballisticTable.PrintPreview(this);
        }
    }
}
