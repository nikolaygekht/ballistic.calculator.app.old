namespace Gehtsoft.BallisticCalculator
{
    partial class ComparisonForm
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
            this.comparisonMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuPopupTrace = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuItemTraceChartVelocity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartMach = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartPath = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartHold = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartWindage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartWindageAdj = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartTime = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartEnergy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceChartOGW = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFirstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeThirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFourthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ballisticGraph = new Gehtsoft.BallisticCalculator.UI.BallisticGraphControl();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comparisonMenuStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // comparisonMenuStrip
            //
            this.comparisonMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupTrace});
            this.comparisonMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.comparisonMenuStrip.Name = "comparisonMenuStrip";
            this.comparisonMenuStrip.Size = new System.Drawing.Size(680, 26);
            this.comparisonMenuStrip.TabIndex = 2;
            this.comparisonMenuStrip.Text = "menuStrip1";
            this.comparisonMenuStrip.Visible = false;
            //
            // menuPopupTrace
            //
            this.menuPopupTrace.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupTraceSystem,
            this.menuPopupTraceAngle,
            this.menuPopupChart,
            this.dataToolStripMenuItem,
            this.toolStripSeparator1,
            this.zoomAllToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.saveImageToolStripMenuItem});
            this.menuPopupTrace.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuPopupTrace.MergeIndex = 1;
            this.menuPopupTrace.Name = "menuPopupTrace";
            this.menuPopupTrace.Size = new System.Drawing.Size(96, 22);
            this.menuPopupTrace.Text = "Comparison";
            //
            // menuPopupTraceSystem
            //
            this.menuPopupTraceSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTraceImperial,
            this.menuItemTraceMetric});
            this.menuPopupTraceSystem.Name = "menuPopupTraceSystem";
            this.menuPopupTraceSystem.Size = new System.Drawing.Size(152, 22);
            this.menuPopupTraceSystem.Text = "System";
            //
            // menuItemTraceImperial
            //
            this.menuItemTraceImperial.Name = "menuItemTraceImperial";
            this.menuItemTraceImperial.Size = new System.Drawing.Size(128, 22);
            this.menuItemTraceImperial.Text = "Imperial";
            this.menuItemTraceImperial.Click += new System.EventHandler(this.menuItemTraceImperial_Click);
            //
            // menuItemTraceMetric
            //
            this.menuItemTraceMetric.Name = "menuItemTraceMetric";
            this.menuItemTraceMetric.Size = new System.Drawing.Size(128, 22);
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
            this.menuPopupTraceAngle.Size = new System.Drawing.Size(152, 22);
            this.menuPopupTraceAngle.Text = "Angle";
            //
            // menuItemTraceMil
            //
            this.menuItemTraceMil.Name = "menuItemTraceMil";
            this.menuItemTraceMil.Size = new System.Drawing.Size(205, 22);
            this.menuItemTraceMil.Text = "Mil (True milliradian)";
            this.menuItemTraceMil.Click += new System.EventHandler(this.menuItemTraceMil_Click);
            //
            // menuItemTraceMilDot
            //
            this.menuItemTraceMilDot.Name = "menuItemTraceMilDot";
            this.menuItemTraceMilDot.Size = new System.Drawing.Size(205, 22);
            this.menuItemTraceMilDot.Text = "Mil Dot (1/6400)";
            this.menuItemTraceMilDot.Click += new System.EventHandler(this.menuItemTraceMilDot_Click);
            //
            // menuItemTraceAMil
            //
            this.menuItemTraceAMil.Name = "menuItemTraceAMil";
            this.menuItemTraceAMil.Size = new System.Drawing.Size(205, 22);
            this.menuItemTraceAMil.Text = "A-Mil (1/6000)";
            this.menuItemTraceAMil.Click += new System.EventHandler(this.menuItemTraceAMil_Click);
            //
            // menuItemTraceMOA
            //
            this.menuItemTraceMOA.Name = "menuItemTraceMOA";
            this.menuItemTraceMOA.Size = new System.Drawing.Size(205, 22);
            this.menuItemTraceMOA.Text = "MOA (1/21600)";
            this.menuItemTraceMOA.Click += new System.EventHandler(this.menuItemTraceMOA_Click);
            //
            // menuItemTraceInPer100Y
            //
            this.menuItemTraceInPer100Y.Name = "menuItemTraceInPer100Y";
            this.menuItemTraceInPer100Y.Size = new System.Drawing.Size(205, 22);
            this.menuItemTraceInPer100Y.Text = "in/100y";
            this.menuItemTraceInPer100Y.Click += new System.EventHandler(this.menuItemTraceInPer100Y_Click);
            //
            // menuItemTraceCmPer100M
            //
            this.menuItemTraceCmPer100M.Name = "menuItemTraceCmPer100M";
            this.menuItemTraceCmPer100M.Size = new System.Drawing.Size(205, 22);
            this.menuItemTraceCmPer100M.Text = "cm/100m";
            this.menuItemTraceCmPer100M.Click += new System.EventHandler(this.menuItemTraceCmPer100M_Click);
            //
            // menuPopupChart
            //
            this.menuPopupChart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.menuPopupChart.Size = new System.Drawing.Size(152, 22);
            this.menuPopupChart.Text = "Chart";
            //
            // menuItemTraceChartVelocity
            //
            this.menuItemTraceChartVelocity.Name = "menuItemTraceChartVelocity";
            this.menuItemTraceChartVelocity.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartVelocity.Text = "Velocity";
            this.menuItemTraceChartVelocity.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartMach
            //
            this.menuItemTraceChartMach.Name = "menuItemTraceChartMach";
            this.menuItemTraceChartMach.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartMach.Text = "Mach";
            this.menuItemTraceChartMach.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartPath
            //
            this.menuItemTraceChartPath.Name = "menuItemTraceChartPath";
            this.menuItemTraceChartPath.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartPath.Text = "Path";
            this.menuItemTraceChartPath.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartHold
            //
            this.menuItemTraceChartHold.Name = "menuItemTraceChartHold";
            this.menuItemTraceChartHold.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartHold.Text = "Hold";
            this.menuItemTraceChartHold.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartWindage
            //
            this.menuItemTraceChartWindage.Name = "menuItemTraceChartWindage";
            this.menuItemTraceChartWindage.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartWindage.Text = "Windage";
            this.menuItemTraceChartWindage.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartWindageAdj
            //
            this.menuItemTraceChartWindageAdj.Name = "menuItemTraceChartWindageAdj";
            this.menuItemTraceChartWindageAdj.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartWindageAdj.Text = "Windage Adjustment";
            this.menuItemTraceChartWindageAdj.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartTime
            //
            this.menuItemTraceChartTime.Name = "menuItemTraceChartTime";
            this.menuItemTraceChartTime.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartTime.Text = "Flight Time";
            this.menuItemTraceChartTime.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartEnergy
            //
            this.menuItemTraceChartEnergy.Name = "menuItemTraceChartEnergy";
            this.menuItemTraceChartEnergy.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartEnergy.Text = "Energy";
            this.menuItemTraceChartEnergy.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // menuItemTraceChartOGW
            //
            this.menuItemTraceChartOGW.Name = "menuItemTraceChartOGW";
            this.menuItemTraceChartOGW.Size = new System.Drawing.Size(219, 22);
            this.menuItemTraceChartOGW.Text = "Optimal Game Weight";
            this.menuItemTraceChartOGW.Click += new System.EventHandler(this.graphTypeMenuItem_Click);
            //
            // dataToolStripMenuItem
            //
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFirstToolStripMenuItem,
            this.removeSecondToolStripMenuItem,
            this.removeThirdToolStripMenuItem,
            this.removeFourthToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataToolStripMenuItem.Text = "Data";
            //
            // removeFirstToolStripMenuItem
            //
            this.removeFirstToolStripMenuItem.Name = "removeFirstToolStripMenuItem";
            this.removeFirstToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.removeFirstToolStripMenuItem.Text = "Remove First";
            this.removeFirstToolStripMenuItem.Click += new System.EventHandler(this.removeFirstToolStripMenuItem_Click);
            //
            // removeSecondToolStripMenuItem
            //
            this.removeSecondToolStripMenuItem.Name = "removeSecondToolStripMenuItem";
            this.removeSecondToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.removeSecondToolStripMenuItem.Text = "Remove Second";
            this.removeSecondToolStripMenuItem.Click += new System.EventHandler(this.removeSecondToolStripMenuItem_Click);
            //
            // removeThirdToolStripMenuItem
            //
            this.removeThirdToolStripMenuItem.Name = "removeThirdToolStripMenuItem";
            this.removeThirdToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.removeThirdToolStripMenuItem.Text = "Remove Third";
            this.removeThirdToolStripMenuItem.Click += new System.EventHandler(this.removeThirdToolStripMenuItem_Click);
            //
            // removeFourthToolStripMenuItem
            //
            this.removeFourthToolStripMenuItem.Name = "removeFourthToolStripMenuItem";
            this.removeFourthToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.removeFourthToolStripMenuItem.Text = "Remove Fourth";
            this.removeFourthToolStripMenuItem.Click += new System.EventHandler(this.removeFourthToolStripMenuItem_Click);
            //
            // ballisticGraph
            //
            this.ballisticGraph.AngleUnits = MathEx.ExternalBallistic.Units.Angle.Unit.MilDot;
            this.ballisticGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ballisticGraph.GraphData = Gehtsoft.BallisticCalculator.UI.ColumnData.Path;
            this.ballisticGraph.Location = new System.Drawing.Point(0, 26);
            this.ballisticGraph.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.ballisticGraph.Name = "ballisticGraph";
            this.ballisticGraph.Size = new System.Drawing.Size(680, 241);
            this.ballisticGraph.TabIndex = 3;
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            //
            // zoomAllToolStripMenuItem
            //
            this.zoomAllToolStripMenuItem.Name = "zoomAllToolStripMenuItem";
            this.zoomAllToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.zoomAllToolStripMenuItem.Text = "Zoom All";
            this.zoomAllToolStripMenuItem.Click += new System.EventHandler(this.zoomAllToolStripMenuItem_Click);
            //
            // copyToolStripMenuItem
            //
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            //
            // saveImageToolStripMenuItem
            //
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            //
            // ComparisonForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 267);
            this.Controls.Add(this.ballisticGraph);
            this.Controls.Add(this.comparisonMenuStrip);
            this.Name = "ComparisonForm";
            this.Text = "Comparison";
            this.comparisonMenuStrip.ResumeLayout(false);
            this.comparisonMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip comparisonMenuStrip;
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
        private UI.BallisticGraphControl ballisticGraph;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFirstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeThirdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFourthToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem zoomAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
    }
}