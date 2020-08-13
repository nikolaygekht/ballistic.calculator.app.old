namespace Gehtsoft.BallisticCalculator.UI
{
    partial class BallisticTable
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewData = new System.Windows.Forms.ListView();
            this.columnHeaderHidden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVelocity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMach = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEnergy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHold = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVClicks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderWindage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderWindageAdj = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHClicks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFlightTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOGW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewData
            // 
            this.listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderHidden,
            this.columnHeaderRange,
            this.columnHeaderVelocity,
            this.columnHeaderMach,
            this.columnHeaderEnergy,
            this.columnHeaderPath,
            this.columnHeaderHold,
            this.columnHeaderVClicks,
            this.columnHeaderWindage,
            this.columnHeaderWindageAdj,
            this.columnHeaderHClicks,
            this.columnHeaderFlightTime,
            this.columnHeaderOGW});
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewData.FullRowSelect = true;
            this.listViewData.GridLines = true;
            this.listViewData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewData.HideSelection = false;
            this.listViewData.Location = new System.Drawing.Point(0, 0);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(1170, 512);
            this.listViewData.TabIndex = 0;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderHidden
            // 
            this.columnHeaderHidden.Text = "Hidden";
            this.columnHeaderHidden.Width = 0;
            // 
            // columnHeaderRange
            // 
            this.columnHeaderRange.Text = "Range";
            this.columnHeaderRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderRange.Width = 90;
            // 
            // columnHeaderVelocity
            // 
            this.columnHeaderVelocity.Text = "Velocity";
            this.columnHeaderVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderVelocity.Width = 90;
            // 
            // columnHeaderMach
            // 
            this.columnHeaderMach.Text = "Mach";
            this.columnHeaderMach.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeaderEnergy
            // 
            this.columnHeaderEnergy.Text = "Energy";
            this.columnHeaderEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderEnergy.Width = 90;
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "Path";
            this.columnHeaderPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderPath.Width = 90;
            // 
            // columnHeaderHold
            // 
            this.columnHeaderHold.Text = "Hold";
            this.columnHeaderHold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderHold.Width = 90;
            // 
            // columnHeaderVClicks
            // 
            this.columnHeaderVClicks.Text = "Clicks";
            this.columnHeaderVClicks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeaderWindage
            // 
            this.columnHeaderWindage.Text = "Windage";
            this.columnHeaderWindage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderWindage.Width = 90;
            // 
            // columnHeaderWindageAdj
            // 
            this.columnHeaderWindageAdj.Text = "Win. Adj.";
            this.columnHeaderWindageAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderWindageAdj.Width = 90;
            // 
            // columnHeaderHClicks
            // 
            this.columnHeaderHClicks.Text = "Clicks";
            this.columnHeaderHClicks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeaderFlightTime
            // 
            this.columnHeaderFlightTime.Text = "Flight Time";
            this.columnHeaderFlightTime.Width = 80;
            // 
            // columnHeaderOGW
            // 
            this.columnHeaderOGW.Text = "O.G.W";
            this.columnHeaderOGW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderOGW.Width = 80;
            // 
            // BallisticTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewData);
            this.Name = "BallisticTable";
            this.Size = new System.Drawing.Size(1170, 512);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewData;
        private System.Windows.Forms.ColumnHeader columnHeaderRange;
        private System.Windows.Forms.ColumnHeader columnHeaderVelocity;
        private System.Windows.Forms.ColumnHeader columnHeaderMach;
        private System.Windows.Forms.ColumnHeader columnHeaderEnergy;
        private System.Windows.Forms.ColumnHeader columnHeaderPath;
        private System.Windows.Forms.ColumnHeader columnHeaderHold;
        private System.Windows.Forms.ColumnHeader columnHeaderWindage;
        private System.Windows.Forms.ColumnHeader columnHeaderWindageAdj;
        private System.Windows.Forms.ColumnHeader columnHeaderFlightTime;
        private System.Windows.Forms.ColumnHeader columnHeaderOGW;
        private System.Windows.Forms.ColumnHeader columnHeaderHidden;
        private System.Windows.Forms.ColumnHeader columnHeaderVClicks;
        private System.Windows.Forms.ColumnHeader columnHeaderHClicks;
    }
}
