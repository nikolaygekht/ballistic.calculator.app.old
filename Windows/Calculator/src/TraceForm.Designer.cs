namespace Gehtsoft.BallisticCalculator
{
    partial class TraceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraceForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAmmo = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bulletInfoInput = new Gehtsoft.BallisticCalculator.UI.BulletInfoInput();
            this.tabPageZero = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.zeroInfoInput = new Gehtsoft.BallisticCalculator.UI.ZeroInfoInput();
            this.tabPageAtmo = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.atmoInfoInput = new Gehtsoft.BallisticCalculator.UI.src.Input.AtmoInfoInput();
            this.tabPageSpinDrift = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonTakeBulletFromAmmo = new System.Windows.Forms.Button();
            this.spinDriftInfoInput = new Gehtsoft.BallisticCalculator.UI.SpinDriftInfoInput();
            this.tabPageParameters = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.shotParametersInput = new Gehtsoft.BallisticCalculator.UI.ShotParametersInput();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ballisticTable = new Gehtsoft.BallisticCalculator.UI.BallisticTable();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ballisticGraph = new Gehtsoft.BallisticCalculator.UI.BallisticGraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.reticleControl1 = new Gehtsoft.BallisticCalculator.Reticle.ReticleControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.reticleStatusHold = new System.Windows.Forms.ToolStripStatusLabel();
            this.reticleStatusWindage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownZoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem100 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem400 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.labelReticleControlsWidth = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.distanceDangerZoneTo = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceDangerZoneFrom = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.checkBoxShowShot = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.distanceShotTargetHeight = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceShotTargetWidth = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceShotDistance = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.label2 = new System.Windows.Forms.Label();
            this.numericZoomOf = new System.Windows.Forms.NumericUpDown();
            this.numericZoom = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSRBDC = new System.Windows.Forms.CheckBox();
            this.checkBoxLRBDC = new System.Windows.Forms.CheckBox();
            this.buttonLoadReticle = new System.Windows.Forms.Button();
            this.traceMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuPopupTrace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceCalculate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceSaveAsCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceSaveAsExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceSendToMobile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPopupTraceSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceImperial = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceMetric = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPopupTraceAngle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceMil = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceMilDot = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceAMil = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceMOA = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceInPer100Y = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceCmPer100M = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPopupChart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartZoomAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartSaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTraceChartVelocity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartMach = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartPath = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartHold = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartWindage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartWindageAdj = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartTime = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartEnergy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartOGW = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPopupTraceComparison = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceComparisonFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceComparisonSecond = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceComparisonThird = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceComparisonFourth = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRecalcDanger = new System.Windows.Forms.Timer(this.components);
            this.menuItemTracePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabAmmo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageZero.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPageAtmo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPageSpinDrift.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPageParameters.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomOf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoom)).BeginInit();
            this.traceMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(996, 617);
            this.splitContainer1.SplitterDistance = 256;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabAmmo);
            this.tabControl1.Controls.Add(this.tabPageZero);
            this.tabControl1.Controls.Add(this.tabPageAtmo);
            this.tabControl1.Controls.Add(this.tabPageSpinDrift);
            this.tabControl1.Controls.Add(this.tabPageParameters);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(994, 254);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAmmo
            // 
            this.tabAmmo.Controls.Add(this.panel1);
            this.tabAmmo.Location = new System.Drawing.Point(4, 28);
            this.tabAmmo.Name = "tabAmmo";
            this.tabAmmo.Padding = new System.Windows.Forms.Padding(3);
            this.tabAmmo.Size = new System.Drawing.Size(986, 222);
            this.tabAmmo.TabIndex = 0;
            this.tabAmmo.Text = "Ammo";
            this.tabAmmo.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.bulletInfoInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 216);
            this.panel1.TabIndex = 0;
            // 
            // bulletInfoInput
            // 
            this.bulletInfoInput.AdvancedMode = false;
            this.bulletInfoInput.BallisticCoefficient = 0.5D;
            this.bulletInfoInput.BulletName = "";
            this.bulletInfoInput.DragTable = MathEx.ExternalBallistic.JBM.DragTable.G1;
            this.bulletInfoInput.Location = new System.Drawing.Point(4, 3);
            this.bulletInfoInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.bulletInfoInput.Name = "bulletInfoInput";
            this.bulletInfoInput.Size = new System.Drawing.Size(440, 116);
            this.bulletInfoInput.TabIndex = 0;
            this.bulletInfoInput.OnBulletNameChanged += new Gehtsoft.BallisticCalculator.UI.BulletInfoInput.BulletNameChangedDelegate(this.bulletInfoInput1_OnBulletNameChanged);
            // 
            // tabPageZero
            // 
            this.tabPageZero.Controls.Add(this.panel2);
            this.tabPageZero.Location = new System.Drawing.Point(4, 28);
            this.tabPageZero.Name = "tabPageZero";
            this.tabPageZero.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageZero.Size = new System.Drawing.Size(986, 222);
            this.tabPageZero.TabIndex = 1;
            this.tabPageZero.Text = "Zero";
            this.tabPageZero.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.zeroInfoInput);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(980, 216);
            this.panel2.TabIndex = 0;
            // 
            // zeroInfoInput
            // 
            this.zeroInfoInput.Location = new System.Drawing.Point(4, 4);
            this.zeroInfoInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.zeroInfoInput.Name = "zeroInfoInput";
            this.zeroInfoInput.Size = new System.Drawing.Size(820, 221);
            this.zeroInfoInput.TabIndex = 0;
            this.zeroInfoInput.UseZeroElevationAngle = false;
            // 
            // tabPageAtmo
            // 
            this.tabPageAtmo.Controls.Add(this.panel3);
            this.tabPageAtmo.Location = new System.Drawing.Point(4, 28);
            this.tabPageAtmo.Name = "tabPageAtmo";
            this.tabPageAtmo.Size = new System.Drawing.Size(986, 222);
            this.tabPageAtmo.TabIndex = 2;
            this.tabPageAtmo.Text = "Atmosphere";
            this.tabPageAtmo.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.atmoInfoInput);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(986, 222);
            this.panel3.TabIndex = 0;
            // 
            // atmoInfoInput
            // 
            this.atmoInfoInput.Location = new System.Drawing.Point(4, 4);
            this.atmoInfoInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.atmoInfoInput.Name = "atmoInfoInput";
            this.atmoInfoInput.ShowWindInfo = true;
            this.atmoInfoInput.Size = new System.Drawing.Size(423, 222);
            this.atmoInfoInput.TabIndex = 0;
            // 
            // tabPageSpinDrift
            // 
            this.tabPageSpinDrift.Controls.Add(this.panel4);
            this.tabPageSpinDrift.Location = new System.Drawing.Point(4, 28);
            this.tabPageSpinDrift.Name = "tabPageSpinDrift";
            this.tabPageSpinDrift.Size = new System.Drawing.Size(986, 222);
            this.tabPageSpinDrift.TabIndex = 3;
            this.tabPageSpinDrift.Text = "Spin Drift";
            this.tabPageSpinDrift.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.buttonTakeBulletFromAmmo);
            this.panel4.Controls.Add(this.spinDriftInfoInput);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(986, 222);
            this.panel4.TabIndex = 0;
            // 
            // buttonTakeBulletFromAmmo
            // 
            this.buttonTakeBulletFromAmmo.Location = new System.Drawing.Point(324, 64);
            this.buttonTakeBulletFromAmmo.Name = "buttonTakeBulletFromAmmo";
            this.buttonTakeBulletFromAmmo.Size = new System.Drawing.Size(161, 23);
            this.buttonTakeBulletFromAmmo.TabIndex = 1;
            this.buttonTakeBulletFromAmmo.Text = "Get from Ammo Tab";
            this.buttonTakeBulletFromAmmo.UseVisualStyleBackColor = true;
            this.buttonTakeBulletFromAmmo.Click += new System.EventHandler(this.buttonTakeBulletFromAmmo_Click);
            // 
            // spinDriftInfoInput
            // 
            this.spinDriftInfoInput.CalculateSpinDrift = false;
            this.spinDriftInfoInput.Location = new System.Drawing.Point(3, 3);
            this.spinDriftInfoInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.spinDriftInfoInput.Name = "spinDriftInfoInput";
            this.spinDriftInfoInput.RightHandRifling = true;
            this.spinDriftInfoInput.Size = new System.Drawing.Size(306, 210);
            this.spinDriftInfoInput.TabIndex = 0;
            // 
            // tabPageParameters
            // 
            this.tabPageParameters.Controls.Add(this.panel5);
            this.tabPageParameters.Location = new System.Drawing.Point(4, 28);
            this.tabPageParameters.Name = "tabPageParameters";
            this.tabPageParameters.Size = new System.Drawing.Size(986, 222);
            this.tabPageParameters.TabIndex = 4;
            this.tabPageParameters.Text = "Parameters";
            this.tabPageParameters.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.buttonCalculate);
            this.panel5.Controls.Add(this.shotParametersInput);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(986, 222);
            this.panel5.TabIndex = 0;
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(8, 197);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculate.TabIndex = 1;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.calculateTraceToolStripMenuItem_Click);
            // 
            // shotParametersInput
            // 
            this.shotParametersInput.Location = new System.Drawing.Point(8, 4);
            this.shotParametersInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.shotParametersInput.Name = "shotParametersInput";
            this.shotParametersInput.Size = new System.Drawing.Size(386, 177);
            this.shotParametersInput.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(994, 355);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ballisticTable);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(986, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Trace";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ballisticTable
            // 
            this.ballisticTable.AngleUnits = MathEx.ExternalBallistic.Units.Angle.Unit.MilDot;
            this.ballisticTable.BallisticInfo = null;
            this.ballisticTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ballisticTable.Location = new System.Drawing.Point(3, 3);
            this.ballisticTable.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.ballisticTable.Name = "ballisticTable";
            this.ballisticTable.Size = new System.Drawing.Size(980, 317);
            this.ballisticTable.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ballisticGraph);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(986, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chart";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ballisticGraph
            // 
            this.ballisticGraph.AngleUnits = MathEx.ExternalBallistic.Units.Angle.Unit.MilDot;
            this.ballisticGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ballisticGraph.GraphData = Gehtsoft.BallisticCalculator.UI.ColumnData.Path;
            this.ballisticGraph.Location = new System.Drawing.Point(3, 3);
            this.ballisticGraph.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.ballisticGraph.Name = "ballisticGraph";
            this.ballisticGraph.Size = new System.Drawing.Size(980, 317);
            this.ballisticGraph.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer2);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(986, 323);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reticle";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.reticleControl1);
            this.splitContainer2.Panel1.Controls.Add(this.statusStrip1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel6);
            this.splitContainer2.Size = new System.Drawing.Size(986, 323);
            this.splitContainer2.SplitterDistance = 371;
            this.splitContainer2.TabIndex = 0;
            // 
            // reticleControl1
            // 
            this.reticleControl1.AngleUnits = MathEx.ExternalBallistic.Units.Angle.Unit.Radian;
            this.reticleControl1.BackColor = System.Drawing.SystemColors.Window;
            this.reticleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reticleControl1.FarRangeBallistic = null;
            this.reticleControl1.Location = new System.Drawing.Point(0, 0);
            this.reticleControl1.Name = "reticleControl1";
            this.reticleControl1.Reticle = null;
            this.reticleControl1.ShortRangeBallistic = null;
            this.reticleControl1.ShowLongRangeBDC = false;
            this.reticleControl1.ShowShortRangeBDC = false;
            this.reticleControl1.Size = new System.Drawing.Size(369, 295);
            this.reticleControl1.TabIndex = 1;
            this.reticleControl1.TabStop = false;
            this.reticleControl1.Zoom = 1;
            this.reticleControl1.ZoomFactor = 1D;
            this.reticleControl1.ReticleMouseMove += new Gehtsoft.BallisticCalculator.Reticle.ReticleControl.ReticleControlMouseEventDelegate(this.reticleControl1_ReticleMouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.reticleStatusHold,
            this.reticleStatusWindage,
            this.toolStripDropDownZoom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 295);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(369, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStripReticle";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(242, 21);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // reticleStatusHold
            // 
            this.reticleStatusHold.Name = "reticleStatusHold";
            this.reticleStatusHold.Size = new System.Drawing.Size(27, 21);
            this.reticleStatusHold.Text = "H: ";
            // 
            // reticleStatusWindage
            // 
            this.reticleStatusWindage.Name = "reticleStatusWindage";
            this.reticleStatusWindage.Size = new System.Drawing.Size(26, 21);
            this.reticleStatusWindage.Text = "W:";
            // 
            // toolStripDropDownZoom
            // 
            this.toolStripDropDownZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem100,
            this.toolStripMenuItem200,
            this.toolStripMenuItem400});
            this.toolStripDropDownZoom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownZoom.Image")));
            this.toolStripDropDownZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownZoom.Name = "toolStripDropDownZoom";
            this.toolStripDropDownZoom.Size = new System.Drawing.Size(59, 24);
            this.toolStripDropDownZoom.Text = "100%";
            this.toolStripDropDownZoom.DropDownOpening += new System.EventHandler(this.toolStripDropDownZoom_DropDownOpening);
            // 
            // toolStripMenuItem100
            // 
            this.toolStripMenuItem100.Name = "toolStripMenuItem100";
            this.toolStripMenuItem100.Size = new System.Drawing.Size(120, 26);
            this.toolStripMenuItem100.Text = "100%";
            this.toolStripMenuItem100.Click += new System.EventHandler(this.toolStripMenuItem100_Click);
            // 
            // toolStripMenuItem200
            // 
            this.toolStripMenuItem200.Name = "toolStripMenuItem200";
            this.toolStripMenuItem200.Size = new System.Drawing.Size(120, 26);
            this.toolStripMenuItem200.Text = "200%";
            this.toolStripMenuItem200.Click += new System.EventHandler(this.toolStripMenuItem200_Click);
            // 
            // toolStripMenuItem400
            // 
            this.toolStripMenuItem400.Name = "toolStripMenuItem400";
            this.toolStripMenuItem400.Size = new System.Drawing.Size(120, 26);
            this.toolStripMenuItem400.Text = "400%";
            this.toolStripMenuItem400.Click += new System.EventHandler(this.toolStripMenuItem400_Click);
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.labelReticleControlsWidth);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.distanceDangerZoneTo);
            this.panel6.Controls.Add(this.distanceDangerZoneFrom);
            this.panel6.Controls.Add(this.checkBoxShowShot);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.distanceShotTargetHeight);
            this.panel6.Controls.Add(this.distanceShotTargetWidth);
            this.panel6.Controls.Add(this.distanceShotDistance);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.numericZoomOf);
            this.panel6.Controls.Add(this.numericZoom);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.checkBoxSRBDC);
            this.panel6.Controls.Add(this.checkBoxLRBDC);
            this.panel6.Controls.Add(this.buttonLoadReticle);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(609, 321);
            this.panel6.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "to";
            // 
            // labelReticleControlsWidth
            // 
            this.labelReticleControlsWidth.Location = new System.Drawing.Point(139, 231);
            this.labelReticleControlsWidth.Name = "labelReticleControlsWidth";
            this.labelReticleControlsWidth.Size = new System.Drawing.Size(174, 23);
            this.labelReticleControlsWidth.TabIndex = 19;
            this.labelReticleControlsWidth.Text = "label7";
            this.labelReticleControlsWidth.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Danger Zone";
            // 
            // distanceDangerZoneTo
            // 
            this.distanceDangerZoneTo.Location = new System.Drawing.Point(353, 202);
            this.distanceDangerZoneTo.Name = "distanceDangerZoneTo";
            this.distanceDangerZoneTo.ReadOnly = true;
            this.distanceDangerZoneTo.Size = new System.Drawing.Size(174, 22);
            this.distanceDangerZoneTo.TabIndex = 17;
            this.distanceDangerZoneTo.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceDangerZoneTo.UnitName = "in";
            // 
            // distanceDangerZoneFrom
            // 
            this.distanceDangerZoneFrom.Location = new System.Drawing.Point(139, 202);
            this.distanceDangerZoneFrom.Name = "distanceDangerZoneFrom";
            this.distanceDangerZoneFrom.ReadOnly = true;
            this.distanceDangerZoneFrom.Size = new System.Drawing.Size(174, 22);
            this.distanceDangerZoneFrom.TabIndex = 16;
            this.distanceDangerZoneFrom.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceDangerZoneFrom.UnitName = "in";
            // 
            // checkBoxShowShot
            // 
            this.checkBoxShowShot.AutoSize = true;
            this.checkBoxShowShot.Location = new System.Drawing.Point(139, 175);
            this.checkBoxShowShot.Name = "checkBoxShowShot";
            this.checkBoxShowShot.Size = new System.Drawing.Size(97, 21);
            this.checkBoxShowShot.TabIndex = 15;
            this.checkBoxShowShot.Text = "Show Shot";
            this.checkBoxShowShot.UseVisualStyleBackColor = true;
            this.checkBoxShowShot.CheckedChanged += new System.EventHandler(this.checkBoxShowShot_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Target size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Shot distance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "x";
            // 
            // distanceShotTargetHeight
            // 
            this.distanceShotTargetHeight.Location = new System.Drawing.Point(353, 146);
            this.distanceShotTargetHeight.Name = "distanceShotTargetHeight";
            this.distanceShotTargetHeight.ReadOnly = false;
            this.distanceShotTargetHeight.Size = new System.Drawing.Size(174, 22);
            this.distanceShotTargetHeight.TabIndex = 11;
            this.distanceShotTargetHeight.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceShotTargetHeight.UnitName = "in";
            this.distanceShotTargetHeight.ValueChanged += new Gehtsoft.BallisticCalculator.UI.CustomControlValue.ValueChangedDelegate(this.shotParameters_ValueChange);
            // 
            // distanceShotTargetWidth
            // 
            this.distanceShotTargetWidth.Location = new System.Drawing.Point(139, 146);
            this.distanceShotTargetWidth.Name = "distanceShotTargetWidth";
            this.distanceShotTargetWidth.ReadOnly = false;
            this.distanceShotTargetWidth.Size = new System.Drawing.Size(174, 22);
            this.distanceShotTargetWidth.TabIndex = 10;
            this.distanceShotTargetWidth.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceShotTargetWidth.UnitName = "in";
            this.distanceShotTargetWidth.ValueChanged += new Gehtsoft.BallisticCalculator.UI.CustomControlValue.ValueChangedDelegate(this.shotParameters_ValueChange);
            // 
            // distanceShotDistance
            // 
            this.distanceShotDistance.Location = new System.Drawing.Point(139, 118);
            this.distanceShotDistance.Name = "distanceShotDistance";
            this.distanceShotDistance.ReadOnly = false;
            this.distanceShotDistance.Size = new System.Drawing.Size(174, 22);
            this.distanceShotDistance.TabIndex = 9;
            this.distanceShotDistance.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Yard;
            this.distanceShotDistance.UnitName = "yd";
            this.distanceShotDistance.ValueChanged += new Gehtsoft.BallisticCalculator.UI.CustomControlValue.ValueChangedDelegate(this.shotParameters_ValueChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "of";
            // 
            // numericZoomOf
            // 
            this.numericZoomOf.Location = new System.Drawing.Point(162, 90);
            this.numericZoomOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericZoomOf.Name = "numericZoomOf";
            this.numericZoomOf.Size = new System.Drawing.Size(54, 22);
            this.numericZoomOf.TabIndex = 6;
            this.numericZoomOf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericZoomOf.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericZoomOf.ValueChanged += new System.EventHandler(this.numericZoomOf_ValueChanged);
            // 
            // numericZoom
            // 
            this.numericZoom.Location = new System.Drawing.Point(77, 90);
            this.numericZoom.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericZoom.Name = "numericZoom";
            this.numericZoom.Size = new System.Drawing.Size(53, 22);
            this.numericZoom.TabIndex = 5;
            this.numericZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericZoom.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericZoom.ValueChanged += new System.EventHandler(this.numericZoom_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Zoom";
            // 
            // checkBoxSRBDC
            // 
            this.checkBoxSRBDC.AutoSize = true;
            this.checkBoxSRBDC.Location = new System.Drawing.Point(4, 61);
            this.checkBoxSRBDC.Name = "checkBoxSRBDC";
            this.checkBoxSRBDC.Size = new System.Drawing.Size(142, 21);
            this.checkBoxSRBDC.TabIndex = 2;
            this.checkBoxSRBDC.Text = "Short Range BDC";
            this.checkBoxSRBDC.UseVisualStyleBackColor = true;
            this.checkBoxSRBDC.CheckedChanged += new System.EventHandler(this.checkBoxSRBDC_CheckedChanged);
            // 
            // checkBoxLRBDC
            // 
            this.checkBoxLRBDC.AutoSize = true;
            this.checkBoxLRBDC.Location = new System.Drawing.Point(4, 33);
            this.checkBoxLRBDC.Name = "checkBoxLRBDC";
            this.checkBoxLRBDC.Size = new System.Drawing.Size(140, 21);
            this.checkBoxLRBDC.TabIndex = 1;
            this.checkBoxLRBDC.Text = "Long Range BDC";
            this.checkBoxLRBDC.UseVisualStyleBackColor = true;
            this.checkBoxLRBDC.CheckedChanged += new System.EventHandler(this.checkBoxLRBDC_CheckedChanged);
            // 
            // buttonLoadReticle
            // 
            this.buttonLoadReticle.Location = new System.Drawing.Point(3, 3);
            this.buttonLoadReticle.Name = "buttonLoadReticle";
            this.buttonLoadReticle.Size = new System.Drawing.Size(127, 23);
            this.buttonLoadReticle.TabIndex = 0;
            this.buttonLoadReticle.Text = "Load Reticle";
            this.buttonLoadReticle.UseVisualStyleBackColor = true;
            this.buttonLoadReticle.Click += new System.EventHandler(this.buttonLoadReticle_Click);
            // 
            // traceMenuStrip
            // 
            this.traceMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.traceMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupTrace});
            this.traceMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.traceMenuStrip.Name = "traceMenuStrip";
            this.traceMenuStrip.Size = new System.Drawing.Size(996, 28);
            this.traceMenuStrip.TabIndex = 1;
            this.traceMenuStrip.Text = "menuStrip1";
            this.traceMenuStrip.Visible = false;
            // 
            // menuPopupTrace
            // 
            this.menuPopupTrace.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTraceCalculate,
            this.menuItemTraceSaveAsCsv,
            this.menuItemTraceSaveAsExcel,
            this.menuItemTraceSendToMobile,
            this.menuItemTracePrint,
            this.toolStripSeparator1,
            this.menuPopupTraceSystem,
            this.menuPopupTraceAngle,
            this.menuPopupChart,
            this.toolStripSeparator2,
            this.menuPopupTraceComparison});
            this.menuPopupTrace.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuPopupTrace.MergeIndex = 1;
            this.menuPopupTrace.Name = "menuPopupTrace";
            this.menuPopupTrace.Size = new System.Drawing.Size(56, 24);
            this.menuPopupTrace.Text = "Trace";
            this.menuPopupTrace.DropDownOpening += new System.EventHandler(this.menuPopupTrace_DropDownOpening);
            // 
            // menuItemTraceCalculate
            // 
            this.menuItemTraceCalculate.Name = "menuItemTraceCalculate";
            this.menuItemTraceCalculate.Size = new System.Drawing.Size(186, 26);
            this.menuItemTraceCalculate.Text = "Calculate";
            this.menuItemTraceCalculate.Click += new System.EventHandler(this.calculateTraceToolStripMenuItem_Click);
            // 
            // menuItemTraceSaveAsCsv
            // 
            this.menuItemTraceSaveAsCsv.Name = "menuItemTraceSaveAsCsv";
            this.menuItemTraceSaveAsCsv.Size = new System.Drawing.Size(186, 26);
            this.menuItemTraceSaveAsCsv.Text = "Save As Csv";
            this.menuItemTraceSaveAsCsv.Click += new System.EventHandler(this.menuItemTraceSaveAsCsv_Click);
            // 
            // menuItemTraceSaveAsExcel
            // 
            this.menuItemTraceSaveAsExcel.Name = "menuItemTraceSaveAsExcel";
            this.menuItemTraceSaveAsExcel.Size = new System.Drawing.Size(186, 26);
            this.menuItemTraceSaveAsExcel.Text = "Save as Excel";
            this.menuItemTraceSaveAsExcel.Click += new System.EventHandler(this.menuItemTraceSaveAsExcel_Click);
            // 
            // menuItemTraceSendToMobile
            // 
            this.menuItemTraceSendToMobile.Name = "menuItemTraceSendToMobile";
            this.menuItemTraceSendToMobile.Size = new System.Drawing.Size(186, 26);
            this.menuItemTraceSendToMobile.Text = "Send to Mobile";
            this.menuItemTraceSendToMobile.Click += new System.EventHandler(this.menuItemTraceSendToMobile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // menuPopupTraceSystem
            // 
            this.menuPopupTraceSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTraceImperial,
            this.menuItemTraceMetric});
            this.menuPopupTraceSystem.Name = "menuPopupTraceSystem";
            this.menuPopupTraceSystem.Size = new System.Drawing.Size(186, 26);
            this.menuPopupTraceSystem.Text = "System";
            // 
            // menuItemTraceImperial
            // 
            this.menuItemTraceImperial.Name = "menuItemTraceImperial";
            this.menuItemTraceImperial.Size = new System.Drawing.Size(139, 26);
            this.menuItemTraceImperial.Text = "Imperial";
            this.menuItemTraceImperial.Click += new System.EventHandler(this.menuItemTraceImperial_Click);
            // 
            // menuItemTraceMetric
            // 
            this.menuItemTraceMetric.Name = "menuItemTraceMetric";
            this.menuItemTraceMetric.Size = new System.Drawing.Size(139, 26);
            this.menuItemTraceMetric.Text = "Metric";
            this.menuItemTraceMetric.Click += new System.EventHandler(this.menuItemTraceMetric_Click);
            // 
            // menuPopupTraceAngle
            // 
            this.menuPopupTraceAngle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTraceMil,
            this.menuItemTraceMilDot,
            this.menuItemTraceAMil,
            this.menuItemTraceMOA,
            this.menuItemTraceInPer100Y,
            this.menuItemTraceCmPer100M});
            this.menuPopupTraceAngle.Name = "menuPopupTraceAngle";
            this.menuPopupTraceAngle.Size = new System.Drawing.Size(186, 26);
            this.menuPopupTraceAngle.Text = "Angle";
            // 
            // menuItemTraceMil
            // 
            this.menuItemTraceMil.Name = "menuItemTraceMil";
            this.menuItemTraceMil.Size = new System.Drawing.Size(222, 26);
            this.menuItemTraceMil.Text = "Mil (True milliradian)";
            this.menuItemTraceMil.Click += new System.EventHandler(this.menuItemTraceMil_Click);
            // 
            // menuItemTraceMilDot
            // 
            this.menuItemTraceMilDot.Name = "menuItemTraceMilDot";
            this.menuItemTraceMilDot.Size = new System.Drawing.Size(222, 26);
            this.menuItemTraceMilDot.Text = "Mil Dot (1/6400)";
            this.menuItemTraceMilDot.Click += new System.EventHandler(this.menuItemTraceMilDot_Click);
            // 
            // menuItemTraceAMil
            // 
            this.menuItemTraceAMil.Name = "menuItemTraceAMil";
            this.menuItemTraceAMil.Size = new System.Drawing.Size(222, 26);
            this.menuItemTraceAMil.Text = "A-Mil (1/6000)";
            this.menuItemTraceAMil.Click += new System.EventHandler(this.menuItemTraceAMil_Click);
            // 
            // menuItemTraceMOA
            // 
            this.menuItemTraceMOA.Name = "menuItemTraceMOA";
            this.menuItemTraceMOA.Size = new System.Drawing.Size(222, 26);
            this.menuItemTraceMOA.Text = "MOA (1/21600)";
            this.menuItemTraceMOA.Click += new System.EventHandler(this.menuItemTraceMOA_Click);
            // 
            // menuItemTraceInPer100Y
            // 
            this.menuItemTraceInPer100Y.Name = "menuItemTraceInPer100Y";
            this.menuItemTraceInPer100Y.Size = new System.Drawing.Size(222, 26);
            this.menuItemTraceInPer100Y.Text = "in/100y";
            this.menuItemTraceInPer100Y.Click += new System.EventHandler(this.menuItemTraceInPer100Y_Click);
            // 
            // menuItemTraceCmPer100M
            // 
            this.menuItemTraceCmPer100M.Name = "menuItemTraceCmPer100M";
            this.menuItemTraceCmPer100M.Size = new System.Drawing.Size(222, 26);
            this.menuItemTraceCmPer100M.Text = "cm/100m";
            this.menuItemTraceCmPer100M.Click += new System.EventHandler(this.menuItemTraceCmPer100M_Click);
            // 
            // menuPopupChart
            // 
            this.menuPopupChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTraceChartZoomAll,
            this.menuItemTraceChartCopyImage,
            this.menuItemTraceChartSaveImage,
            this.toolStripSeparator3,
            this.menuItemTraceChartVelocity,
            this.menuItemTraceChartMach,
            this.menuItemTraceChartPath,
            this.menuItemTraceChartHold,
            this.menuItemTraceChartWindage,
            this.menuItemTraceChartWindageAdj,
            this.menuItemTraceChartTime,
            this.menuItemTraceChartEnergy,
            this.menuItemTraceChartOGW});
            this.menuPopupChart.Name = "menuPopupChart";
            this.menuPopupChart.Size = new System.Drawing.Size(186, 26);
            this.menuPopupChart.Text = "Chart";
            // 
            // menuItemTraceChartZoomAll
            // 
            this.menuItemTraceChartZoomAll.Name = "menuItemTraceChartZoomAll";
            this.menuItemTraceChartZoomAll.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartZoomAll.Text = "Zoom All";
            this.menuItemTraceChartZoomAll.Click += new System.EventHandler(this.menuItemTraceChartZoomAll_Click);
            // 
            // menuItemTraceChartCopyImage
            // 
            this.menuItemTraceChartCopyImage.Name = "menuItemTraceChartCopyImage";
            this.menuItemTraceChartCopyImage.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartCopyImage.Text = "Copy Image";
            this.menuItemTraceChartCopyImage.Click += new System.EventHandler(this.menuItemTraceChartCopyImage_Click);
            // 
            // menuItemTraceChartSaveImage
            // 
            this.menuItemTraceChartSaveImage.Name = "menuItemTraceChartSaveImage";
            this.menuItemTraceChartSaveImage.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartSaveImage.Text = "Save Image";
            this.menuItemTraceChartSaveImage.Click += new System.EventHandler(this.menuItemTraceChartSaveImage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // menuItemTraceChartVelocity
            // 
            this.menuItemTraceChartVelocity.Name = "menuItemTraceChartVelocity";
            this.menuItemTraceChartVelocity.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartVelocity.Text = "Velocity";
            this.menuItemTraceChartVelocity.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartMach
            // 
            this.menuItemTraceChartMach.Name = "menuItemTraceChartMach";
            this.menuItemTraceChartMach.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartMach.Text = "Mach";
            this.menuItemTraceChartMach.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartPath
            // 
            this.menuItemTraceChartPath.Name = "menuItemTraceChartPath";
            this.menuItemTraceChartPath.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartPath.Text = "Path";
            this.menuItemTraceChartPath.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartHold
            // 
            this.menuItemTraceChartHold.Name = "menuItemTraceChartHold";
            this.menuItemTraceChartHold.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartHold.Text = "Hold";
            this.menuItemTraceChartHold.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartWindage
            // 
            this.menuItemTraceChartWindage.Name = "menuItemTraceChartWindage";
            this.menuItemTraceChartWindage.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartWindage.Text = "Windage";
            this.menuItemTraceChartWindage.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartWindageAdj
            // 
            this.menuItemTraceChartWindageAdj.Name = "menuItemTraceChartWindageAdj";
            this.menuItemTraceChartWindageAdj.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartWindageAdj.Text = "Windage Adjustment";
            this.menuItemTraceChartWindageAdj.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartTime
            // 
            this.menuItemTraceChartTime.Name = "menuItemTraceChartTime";
            this.menuItemTraceChartTime.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartTime.Text = "Flight Time";
            this.menuItemTraceChartTime.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartEnergy
            // 
            this.menuItemTraceChartEnergy.Name = "menuItemTraceChartEnergy";
            this.menuItemTraceChartEnergy.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartEnergy.Text = "Energy";
            this.menuItemTraceChartEnergy.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // menuItemTraceChartOGW
            // 
            this.menuItemTraceChartOGW.Name = "menuItemTraceChartOGW";
            this.menuItemTraceChartOGW.Size = new System.Drawing.Size(232, 26);
            this.menuItemTraceChartOGW.Text = "Optimal Game Weight";
            this.menuItemTraceChartOGW.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // menuPopupTraceComparison
            // 
            this.menuPopupTraceComparison.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTraceComparisonFirst,
            this.menuItemTraceComparisonSecond,
            this.menuItemTraceComparisonThird,
            this.menuItemTraceComparisonFourth});
            this.menuPopupTraceComparison.Name = "menuPopupTraceComparison";
            this.menuPopupTraceComparison.Size = new System.Drawing.Size(186, 26);
            this.menuPopupTraceComparison.Text = "Comparison";
            this.menuPopupTraceComparison.DropDownOpening += new System.EventHandler(this.menuPopupTraceComparison_DropDownOpening);
            // 
            // menuItemTraceComparisonFirst
            // 
            this.menuItemTraceComparisonFirst.Name = "menuItemTraceComparisonFirst";
            this.menuItemTraceComparisonFirst.Size = new System.Drawing.Size(183, 26);
            this.menuItemTraceComparisonFirst.Text = "Add as First";
            this.menuItemTraceComparisonFirst.Click += new System.EventHandler(this.menuItemTraceComparisonFirst_Click);
            // 
            // menuItemTraceComparisonSecond
            // 
            this.menuItemTraceComparisonSecond.Name = "menuItemTraceComparisonSecond";
            this.menuItemTraceComparisonSecond.Size = new System.Drawing.Size(183, 26);
            this.menuItemTraceComparisonSecond.Text = "Add as Second";
            this.menuItemTraceComparisonSecond.Click += new System.EventHandler(this.menuItemTraceComparisonSecond_Click);
            // 
            // menuItemTraceComparisonThird
            // 
            this.menuItemTraceComparisonThird.Name = "menuItemTraceComparisonThird";
            this.menuItemTraceComparisonThird.Size = new System.Drawing.Size(183, 26);
            this.menuItemTraceComparisonThird.Text = "Add as Third";
            this.menuItemTraceComparisonThird.Click += new System.EventHandler(this.menuItemTraceComparisonThird_Click);
            // 
            // menuItemTraceComparisonFourth
            // 
            this.menuItemTraceComparisonFourth.Name = "menuItemTraceComparisonFourth";
            this.menuItemTraceComparisonFourth.Size = new System.Drawing.Size(183, 26);
            this.menuItemTraceComparisonFourth.Text = "Add as Fourth";
            this.menuItemTraceComparisonFourth.Click += new System.EventHandler(this.menuItemTraceComparisonFourth_Click);
            // 
            // timerRecalcDanger
            // 
            this.timerRecalcDanger.Interval = 1000;
            this.timerRecalcDanger.Tick += new System.EventHandler(this.timerRecalcDanger_Tick);
            // 
            // menuItemTracePrint
            // 
            this.menuItemTracePrint.Name = "menuItemTracePrint";
            this.menuItemTracePrint.Size = new System.Drawing.Size(186, 26);
            this.menuItemTracePrint.Text = "Print";
            this.menuItemTracePrint.Click += new System.EventHandler(this.menuItemTracePrint_Click);
            // 
            // TraceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 645);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.traceMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.traceMenuStrip;
            this.Name = "TraceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Trace";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabAmmo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPageZero.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPageAtmo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPageSpinDrift.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabPageParameters.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomOf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoom)).EndInit();
            this.traceMenuStrip.ResumeLayout(false);
            this.traceMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAmmo;
        private System.Windows.Forms.TabPage tabPageZero;
        private System.Windows.Forms.TabPage tabPageAtmo;
        private System.Windows.Forms.TabPage tabPageSpinDrift;
        private System.Windows.Forms.TabPage tabPageParameters;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private UI.BulletInfoInput bulletInfoInput;
        private System.Windows.Forms.Panel panel2;
        private UI.ZeroInfoInput zeroInfoInput;
        private System.Windows.Forms.Panel panel3;
        private UI.src.Input.AtmoInfoInput atmoInfoInput;
        private System.Windows.Forms.Panel panel4;
        private UI.SpinDriftInfoInput spinDriftInfoInput;
        private System.Windows.Forms.Panel panel5;
        private UI.ShotParametersInput shotParametersInput;
        private UI.BallisticTable ballisticTable;
        private UI.BallisticGraphControl ballisticGraph;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Reticle.ReticleControl reticleControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.MenuStrip traceMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuPopupTrace;
        private System.Windows.Forms.ToolStripMenuItem menuPopupTraceSystem;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceImperial;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceMetric;
        private System.Windows.Forms.ToolStripMenuItem menuPopupTraceAngle;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceMil;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceMilDot;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceAMil;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceMOA;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceInPer100Y;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceCmPer100M;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceCalculate;
        private System.Windows.Forms.ToolStripMenuItem menuPopupChart;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartVelocity;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartMach;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartPath;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartHold;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartWindage;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartWindageAdj;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartTime;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartEnergy;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartOGW;
        private System.Windows.Forms.Button buttonLoadReticle;
        private System.Windows.Forms.CheckBox checkBoxSRBDC;
        private System.Windows.Forms.CheckBox checkBoxLRBDC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericZoomOf;
        private System.Windows.Forms.NumericUpDown numericZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel reticleStatusHold;
        private System.Windows.Forms.ToolStripStatusLabel reticleStatusWindage;
        private UI.CustomDistanceControl distanceShotDistance;
        private System.Windows.Forms.Label label3;
        private UI.CustomDistanceControl distanceShotTargetHeight;
        private UI.CustomDistanceControl distanceShotTargetWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuPopupTraceComparison;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceComparisonFirst;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceComparisonSecond;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceComparisonThird;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceComparisonFourth;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartZoomAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartCopyImage;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceChartSaveImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceSaveAsCsv;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownZoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem100;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem400;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceSendToMobile;
        private System.Windows.Forms.CheckBox checkBoxShowShot;
        private UI.CustomDistanceControl distanceDangerZoneTo;
        private UI.CustomDistanceControl distanceDangerZoneFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelReticleControlsWidth;
        private System.Windows.Forms.Timer timerRecalcDanger;
        private System.Windows.Forms.Button buttonTakeBulletFromAmmo;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceSaveAsExcel;
        private System.Windows.Forms.ToolStripMenuItem menuItemTracePrint;
    }
}