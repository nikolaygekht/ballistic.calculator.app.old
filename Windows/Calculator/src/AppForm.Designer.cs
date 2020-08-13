namespace Gehtsoft.BallisticCalculator
{
    partial class AppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuPopupFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPopupFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewImperial = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewMetric = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemOpenTrace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPopupWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsTile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemStartServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupFile,
            this.menuPopupWindows});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.menuPopupWindows;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1104, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuPopupFile
            // 
            this.menuPopupFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupFileName,
            this.toolStripSeparator1,
            this.menuItemOpenTrace,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator3,
            this.menuItemStartServer,
            this.toolStripSeparator4,
            this.menuItemExit});
            this.menuPopupFile.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuPopupFile.MergeIndex = 0;
            this.menuPopupFile.Name = "menuPopupFile";
            this.menuPopupFile.Size = new System.Drawing.Size(40, 22);
            this.menuPopupFile.Text = "File";
            this.menuPopupFile.DropDownOpening += new System.EventHandler(this.menuPopupFile_DropDownOpening);
            // 
            // menuPopupFileName
            // 
            this.menuPopupFileName.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewImperial,
            this.menuItemNewMetric});
            this.menuPopupFileName.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuPopupFileName.MergeIndex = 0;
            this.menuPopupFileName.Name = "menuPopupFileName";
            this.menuPopupFileName.Size = new System.Drawing.Size(201, 22);
            this.menuPopupFileName.Text = "New";
            // 
            // menuItemNewImperial
            // 
            this.menuItemNewImperial.Name = "menuItemNewImperial";
            this.menuItemNewImperial.Size = new System.Drawing.Size(128, 22);
            this.menuItemNewImperial.Text = "Imperial";
            this.menuItemNewImperial.Click += new System.EventHandler(this.menuItemNewImperial_Click);
            // 
            // menuItemNewMetric
            // 
            this.menuItemNewMetric.Name = "menuItemNewMetric";
            this.menuItemNewMetric.Size = new System.Drawing.Size(128, 22);
            this.menuItemNewMetric.Text = "Metric";
            this.menuItemNewMetric.Click += new System.EventHandler(this.menuItemNewMetric_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 1;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // menuItemOpenTrace
            // 
            this.menuItemOpenTrace.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemOpenTrace.MergeIndex = 3;
            this.menuItemOpenTrace.Name = "menuItemOpenTrace";
            this.menuItemOpenTrace.Size = new System.Drawing.Size(201, 22);
            this.menuItemOpenTrace.Text = "Open";
            this.menuItemOpenTrace.Click += new System.EventHandler(this.menuItemOpenTrace_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Size = new System.Drawing.Size(201, 22);
            this.menuItemSave.Text = "Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            this.menuItemSaveAs.Size = new System.Drawing.Size(201, 22);
            this.menuItemSaveAs.Text = "Save As";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator3.MergeIndex = 30;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemExit.MergeIndex = 31;
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(201, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuPopupWindows
            // 
            this.menuPopupWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemWindowsTile,
            this.menuItemWindowsCascade,
            this.toolStripSeparator2});
            this.menuPopupWindows.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuPopupWindows.MergeIndex = 20;
            this.menuPopupWindows.Name = "menuPopupWindows";
            this.menuPopupWindows.Size = new System.Drawing.Size(77, 22);
            this.menuPopupWindows.Text = "Windows";
            // 
            // menuItemWindowsTile
            // 
            this.menuItemWindowsTile.Name = "menuItemWindowsTile";
            this.menuItemWindowsTile.Size = new System.Drawing.Size(131, 22);
            this.menuItemWindowsTile.Text = "Tile";
            this.menuItemWindowsTile.Click += new System.EventHandler(this.menuItemWindowsTile_Click);
            // 
            // menuItemWindowsCascade
            // 
            this.menuItemWindowsCascade.Name = "menuItemWindowsCascade";
            this.menuItemWindowsCascade.Size = new System.Drawing.Size(131, 22);
            this.menuItemWindowsCascade.Text = "Cascade";
            this.menuItemWindowsCascade.Click += new System.EventHandler(this.menuItemWindowsCascade_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // menuItemStartServer
            // 
            this.menuItemStartServer.Name = "menuItemStartServer";
            this.menuItemStartServer.Size = new System.Drawing.Size(201, 22);
            this.menuItemStartServer.Text = "Connectivity Server";
            this.menuItemStartServer.Click += new System.EventHandler(this.menuItemStartServer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(198, 6);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 643);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AppForm";
            this.Text = "Ballistic Calculator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuPopupFile;
        private System.Windows.Forms.ToolStripMenuItem menuPopupFileName;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewImperial;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewMetric;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuPopupWindows;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsTile;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsCascade;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenTrace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuItemStartServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}

