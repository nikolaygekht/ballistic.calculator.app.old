namespace Gehtsoft.BallisticCalculator.UI
{
    partial class ZeroInfoInput
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
            this.comboBoxPreset = new System.Windows.Forms.ComboBox();
            this.distanceSight = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceZero = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.checkBoxNearZero = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxOtherAmmo = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxOtherAtmo = new System.Windows.Forms.CheckBox();
            this.checkBoxUseElevationAngle = new System.Windows.Forms.CheckBox();
            this.angleSightElevationAngle = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.numericClicks = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.bulletInfoInput = new Gehtsoft.BallisticCalculator.UI.BulletInfoInput();
            this.atmoInfoInput = new Gehtsoft.BallisticCalculator.UI.src.Input.AtmoInfoInput();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericClicks)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPreset
            // 
            this.comboBoxPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPreset.FormattingEnabled = true;
            this.comboBoxPreset.Location = new System.Drawing.Point(119, 4);
            this.comboBoxPreset.Name = "comboBoxPreset";
            this.comboBoxPreset.Size = new System.Drawing.Size(187, 24);
            this.comboBoxPreset.TabIndex = 0;
            this.comboBoxPreset.SelectedIndexChanged += new System.EventHandler(this.comboBoxPreset_SelectedIndexChanged);
            // 
            // distanceSight
            // 
            this.distanceSight.Location = new System.Drawing.Point(119, 35);
            this.distanceSight.Name = "distanceSight";
            this.distanceSight.ReadOnly = false;
            this.distanceSight.Size = new System.Drawing.Size(187, 22);
            this.distanceSight.TabIndex = 1;
            this.distanceSight.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceSight.UnitName = "in";
            // 
            // distanceZero
            // 
            this.distanceZero.Location = new System.Drawing.Point(119, 63);
            this.distanceZero.Name = "distanceZero";
            this.distanceZero.ReadOnly = false;
            this.distanceZero.Size = new System.Drawing.Size(187, 22);
            this.distanceZero.TabIndex = 2;
            this.distanceZero.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Yard;
            this.distanceZero.UnitName = "yd";
            // 
            // checkBoxNearZero
            // 
            this.checkBoxNearZero.AutoSize = true;
            this.checkBoxNearZero.Location = new System.Drawing.Point(119, 91);
            this.checkBoxNearZero.Name = "checkBoxNearZero";
            this.checkBoxNearZero.Size = new System.Drawing.Size(95, 21);
            this.checkBoxNearZero.TabIndex = 3;
            this.checkBoxNearZero.Text = "Near Zero";
            this.checkBoxNearZero.UseVisualStyleBackColor = true;
            this.checkBoxNearZero.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Preset";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sight Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Zero Distance";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(312, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 251);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.checkBoxOtherAmmo);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(516, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ammo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.bulletInfoInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 192);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxOtherAmmo
            // 
            this.checkBoxOtherAmmo.AutoSize = true;
            this.checkBoxOtherAmmo.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxOtherAmmo.Location = new System.Drawing.Point(3, 3);
            this.checkBoxOtherAmmo.Name = "checkBoxOtherAmmo";
            this.checkBoxOtherAmmo.Size = new System.Drawing.Size(510, 21);
            this.checkBoxOtherAmmo.TabIndex = 0;
            this.checkBoxOtherAmmo.Text = "Use other ammo to zero";
            this.checkBoxOtherAmmo.UseVisualStyleBackColor = true;
            this.checkBoxOtherAmmo.CheckedChanged += new System.EventHandler(this.checkBoxOtherAmmo_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.checkBoxOtherAtmo);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Atmosphere";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.atmoInfoInput);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(454, 192);
            this.panel2.TabIndex = 1;
            // 
            // checkBoxOtherAtmo
            // 
            this.checkBoxOtherAtmo.AutoSize = true;
            this.checkBoxOtherAtmo.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxOtherAtmo.Location = new System.Drawing.Point(3, 3);
            this.checkBoxOtherAtmo.Name = "checkBoxOtherAtmo";
            this.checkBoxOtherAtmo.Size = new System.Drawing.Size(454, 21);
            this.checkBoxOtherAtmo.TabIndex = 0;
            this.checkBoxOtherAtmo.Text = "Use other atmosphere to zero";
            this.checkBoxOtherAtmo.UseVisualStyleBackColor = true;
            this.checkBoxOtherAtmo.CheckedChanged += new System.EventHandler(this.checkBoxOtherAtmo_CheckedChanged);
            // 
            // checkBoxUseElevationAngle
            // 
            this.checkBoxUseElevationAngle.AutoSize = true;
            this.checkBoxUseElevationAngle.Location = new System.Drawing.Point(6, 118);
            this.checkBoxUseElevationAngle.Name = "checkBoxUseElevationAngle";
            this.checkBoxUseElevationAngle.Size = new System.Drawing.Size(157, 21);
            this.checkBoxUseElevationAngle.TabIndex = 8;
            this.checkBoxUseElevationAngle.Text = "Use Elevation Angle";
            this.checkBoxUseElevationAngle.UseVisualStyleBackColor = true;
            this.checkBoxUseElevationAngle.CheckedChanged += new System.EventHandler(this.checkBoxUseElevationAngle_CheckedChanged);
            // 
            // angleSightElevationAngle
            // 
            this.angleSightElevationAngle.Location = new System.Drawing.Point(119, 145);
            this.angleSightElevationAngle.Name = "angleSightElevationAngle";
            this.angleSightElevationAngle.ReadOnly = true;
            this.angleSightElevationAngle.Size = new System.Drawing.Size(187, 22);
            this.angleSightElevationAngle.TabIndex = 9;
            this.angleSightElevationAngle.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Radian;
            this.angleSightElevationAngle.UnitName = "rad";
            // 
            // numericClicks
            // 
            this.numericClicks.Location = new System.Drawing.Point(119, 174);
            this.numericClicks.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericClicks.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericClicks.Name = "numericClicks";
            this.numericClicks.Size = new System.Drawing.Size(122, 22);
            this.numericClicks.TabIndex = 10;
            this.numericClicks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Vertical Clicks";
            // 
            // bulletInfoInput
            // 
            this.bulletInfoInput.AdvancedMode = false;
            this.bulletInfoInput.BallisticCoefficient = 0.5D;
            this.bulletInfoInput.BulletName = "";
            this.bulletInfoInput.DragTable = MathEx.ExternalBallistic.JBM.DragTable.G1;
            this.bulletInfoInput.Enabled = false;
            this.bulletInfoInput.Location = new System.Drawing.Point(16, 3);
            this.bulletInfoInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.bulletInfoInput.Name = "bulletInfoInput";
            this.bulletInfoInput.Size = new System.Drawing.Size(470, 148);
            this.bulletInfoInput.TabIndex = 0;
            // 
            // atmoInfoInput
            // 
            this.atmoInfoInput.Enabled = false;
            this.atmoInfoInput.Location = new System.Drawing.Point(4, 4);
            this.atmoInfoInput.MeasurementSystem = Gehtsoft.BallisticCalculator.UI.MeasurementSystem.Metric;
            this.atmoInfoInput.Name = "atmoInfoInput";
            this.atmoInfoInput.ShowWindInfo = false;
            this.atmoInfoInput.Size = new System.Drawing.Size(438, 162);
            this.atmoInfoInput.TabIndex = 0;
            // 
            // ZeroInfoInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericClicks);
            this.Controls.Add(this.angleSightElevationAngle);
            this.Controls.Add(this.checkBoxUseElevationAngle);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxNearZero);
            this.Controls.Add(this.distanceZero);
            this.Controls.Add(this.distanceSight);
            this.Controls.Add(this.comboBoxPreset);
            this.Name = "ZeroInfoInput";
            this.Size = new System.Drawing.Size(854, 254);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericClicks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPreset;
        private CustomDistanceControl distanceSight;
        private CustomDistanceControl distanceZero;
        private System.Windows.Forms.CheckBox checkBoxNearZero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxOtherAmmo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBoxOtherAtmo;
        private System.Windows.Forms.Panel panel1;
        private BulletInfoInput bulletInfoInput;
        private System.Windows.Forms.Panel panel2;
        private src.Input.AtmoInfoInput atmoInfoInput;
        private System.Windows.Forms.CheckBox checkBoxUseElevationAngle;
        private CustomAngleControl angleSightElevationAngle;
        private System.Windows.Forms.NumericUpDown numericClicks;
        private System.Windows.Forms.Label label4;
    }
}
