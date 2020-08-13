namespace Gehtsoft.BallisticCalculator.UI
{
    partial class BulletInfoInput
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelSource = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.weightControlBullet = new Gehtsoft.BallisticCalculator.UI.CustomWeightControl();
            this.numericBC = new System.Windows.Forms.NumericUpDown();
            this.comboBoxBCType = new System.Windows.Forms.ComboBox();
            this.velocityAtMuzzle = new Gehtsoft.BallisticCalculator.UI.CustomVelocityControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAdvanced = new System.Windows.Forms.Button();
            this.comboBoxCaliber = new System.Windows.Forms.ComboBox();
            this.labelCaliber = new System.Windows.Forms.Label();
            this.labelBarrelLength = new System.Windows.Forms.Label();
            this.distanceBarrelLength = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.comboBoxBullet = new System.Windows.Forms.ComboBox();
            this.labelCompactShape = new System.Windows.Forms.Label();
            this.labelFullShape = new System.Windows.Forms.Label();
            this.distanceBulletInfoLength = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.labelDiameter = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.customDistanceControl2 = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceBulletInfoDiameter = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericBC)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(119, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(204, 22);
            this.textBoxName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(328, 3);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(409, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(3, 175);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(53, 17);
            this.labelSource.TabIndex = 5;
            this.labelSource.Text = "Source";
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(119, 172);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.Size = new System.Drawing.Size(204, 22);
            this.textBoxSource.TabIndex = 11;
            // 
            // weightControlBullet
            // 
            this.weightControlBullet.Location = new System.Drawing.Point(119, 32);
            this.weightControlBullet.Name = "weightControlBullet";
            this.weightControlBullet.ReadOnly = false;
            this.weightControlBullet.Size = new System.Drawing.Size(204, 22);
            this.weightControlBullet.TabIndex = 3;
            this.weightControlBullet.Unit = MathEx.ExternalBallistic.Units.Weight.Unit.Grain;
            this.weightControlBullet.UnitName = "gr";
            // 
            // numericBC
            // 
            this.numericBC.DecimalPlaces = 3;
            this.numericBC.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericBC.Location = new System.Drawing.Point(119, 60);
            this.numericBC.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericBC.Name = "numericBC";
            this.numericBC.Size = new System.Drawing.Size(126, 22);
            this.numericBC.TabIndex = 5;
            this.numericBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboBoxBCType
            // 
            this.comboBoxBCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBCType.FormattingEnabled = true;
            this.comboBoxBCType.Items.AddRange(new object[] {
            "G1",
            "G2",
            "G5",
            "G6",
            "G7",
            "G8",
            "GI",
            "GL"});
            this.comboBoxBCType.Location = new System.Drawing.Point(251, 58);
            this.comboBoxBCType.Name = "comboBoxBCType";
            this.comboBoxBCType.Size = new System.Drawing.Size(72, 24);
            this.comboBoxBCType.TabIndex = 6;
            // 
            // velocityAtMuzzle
            // 
            this.velocityAtMuzzle.Location = new System.Drawing.Point(119, 88);
            this.velocityAtMuzzle.Name = "velocityAtMuzzle";
            this.velocityAtMuzzle.ReadOnly = false;
            this.velocityAtMuzzle.Size = new System.Drawing.Size(204, 22);
            this.velocityAtMuzzle.TabIndex = 7;
            this.velocityAtMuzzle.Unit = MathEx.ExternalBallistic.Units.Velocity.Unit.FeetPerSecond;
            this.velocityAtMuzzle.UnitName = "ft/s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Bullet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "BC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Muzzle Velocity";
            // 
            // buttonAdvanced
            // 
            this.buttonAdvanced.Location = new System.Drawing.Point(329, 90);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(111, 23);
            this.buttonAdvanced.TabIndex = 8;
            this.buttonAdvanced.Text = "Show More";
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            this.buttonAdvanced.Click += new System.EventHandler(this.buttonAdvanced_Click);
            // 
            // comboBoxCaliber
            // 
            this.comboBoxCaliber.FormattingEnabled = true;
            this.comboBoxCaliber.Location = new System.Drawing.Point(119, 200);
            this.comboBoxCaliber.Name = "comboBoxCaliber";
            this.comboBoxCaliber.Size = new System.Drawing.Size(204, 24);
            this.comboBoxCaliber.TabIndex = 12;
            // 
            // labelCaliber
            // 
            this.labelCaliber.AutoSize = true;
            this.labelCaliber.Location = new System.Drawing.Point(3, 207);
            this.labelCaliber.Name = "labelCaliber";
            this.labelCaliber.Size = new System.Drawing.Size(52, 17);
            this.labelCaliber.TabIndex = 16;
            this.labelCaliber.Text = "Caliber";
            // 
            // labelBarrelLength
            // 
            this.labelBarrelLength.AutoSize = true;
            this.labelBarrelLength.Location = new System.Drawing.Point(3, 235);
            this.labelBarrelLength.Name = "labelBarrelLength";
            this.labelBarrelLength.Size = new System.Drawing.Size(94, 17);
            this.labelBarrelLength.TabIndex = 17;
            this.labelBarrelLength.Text = "Barrel Length";
            // 
            // distanceBarrelLength
            // 
            this.distanceBarrelLength.Location = new System.Drawing.Point(119, 230);
            this.distanceBarrelLength.Name = "distanceBarrelLength";
            this.distanceBarrelLength.ReadOnly = false;
            this.distanceBarrelLength.Size = new System.Drawing.Size(204, 22);
            this.distanceBarrelLength.TabIndex = 13;
            this.distanceBarrelLength.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceBarrelLength.UnitName = "in";
            // 
            // comboBoxBullet
            // 
            this.comboBoxBullet.FormattingEnabled = true;
            this.comboBoxBullet.Location = new System.Drawing.Point(329, 30);
            this.comboBoxBullet.Name = "comboBoxBullet";
            this.comboBoxBullet.Size = new System.Drawing.Size(155, 24);
            this.comboBoxBullet.TabIndex = 4;
            // 
            // labelCompactShape
            // 
            this.labelCompactShape.AutoSize = true;
            this.labelCompactShape.Location = new System.Drawing.Point(430, 116);
            this.labelCompactShape.Name = "labelCompactShape";
            this.labelCompactShape.Size = new System.Drawing.Size(12, 17);
            this.labelCompactShape.TabIndex = 18;
            this.labelCompactShape.Text = ".";
            this.labelCompactShape.Visible = false;
            // 
            // labelFullShape
            // 
            this.labelFullShape.AutoSize = true;
            this.labelFullShape.Location = new System.Drawing.Point(483, 237);
            this.labelFullShape.Name = "labelFullShape";
            this.labelFullShape.Size = new System.Drawing.Size(12, 17);
            this.labelFullShape.TabIndex = 19;
            this.labelFullShape.Text = ".";
            this.labelFullShape.Visible = false;
            // 
            // distanceBulletInfoLength
            // 
            this.distanceBulletInfoLength.Location = new System.Drawing.Point(119, 145);
            this.distanceBulletInfoLength.Name = "distanceBulletInfoLength";
            this.distanceBulletInfoLength.ReadOnly = false;
            this.distanceBulletInfoLength.Size = new System.Drawing.Size(204, 22);
            this.distanceBulletInfoLength.TabIndex = 10;
            this.distanceBulletInfoLength.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceBulletInfoLength.UnitName = "in";
            // 
            // labelDiameter
            // 
            this.labelDiameter.AutoSize = true;
            this.labelDiameter.Location = new System.Drawing.Point(3, 121);
            this.labelDiameter.Name = "labelDiameter";
            this.labelDiameter.Size = new System.Drawing.Size(104, 17);
            this.labelDiameter.TabIndex = 22;
            this.labelDiameter.Text = "Bullet Diameter";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(3, 150);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(91, 17);
            this.labelLength.TabIndex = 23;
            this.labelLength.Text = "Bullet Length";
            this.labelLength.Click += new System.EventHandler(this.label6_Click);
            // 
            // customDistanceControl2
            // 
            this.customDistanceControl2.Location = new System.Drawing.Point(119, 145);
            this.customDistanceControl2.Name = "customDistanceControl2";
            this.customDistanceControl2.ReadOnly = false;
            this.customDistanceControl2.Size = new System.Drawing.Size(204, 22);
            this.customDistanceControl2.TabIndex = 21;
            this.customDistanceControl2.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.customDistanceControl2.UnitName = "in";
            // 
            // distanceBulletInfoDiameter
            // 
            this.distanceBulletInfoDiameter.Location = new System.Drawing.Point(119, 117);
            this.distanceBulletInfoDiameter.Name = "distanceBulletInfoDiameter";
            this.distanceBulletInfoDiameter.ReadOnly = false;
            this.distanceBulletInfoDiameter.Size = new System.Drawing.Size(204, 22);
            this.distanceBulletInfoDiameter.TabIndex = 9;
            this.distanceBulletInfoDiameter.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceBulletInfoDiameter.UnitName = "in";
            // 
            // BulletInfoInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.distanceBulletInfoDiameter);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelDiameter);
            this.Controls.Add(this.distanceBulletInfoLength);
            this.Controls.Add(this.labelFullShape);
            this.Controls.Add(this.labelCompactShape);
            this.Controls.Add(this.comboBoxBullet);
            this.Controls.Add(this.distanceBarrelLength);
            this.Controls.Add(this.labelBarrelLength);
            this.Controls.Add(this.labelCaliber);
            this.Controls.Add(this.comboBoxCaliber);
            this.Controls.Add(this.buttonAdvanced);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.velocityAtMuzzle);
            this.Controls.Add(this.comboBoxBCType);
            this.Controls.Add(this.numericBC);
            this.Controls.Add(this.weightControlBullet);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Name = "BulletInfoInput";
            this.Size = new System.Drawing.Size(496, 264);
            this.EnabledChanged += new System.EventHandler(this.BulletInfoInput_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numericBC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.TextBox textBoxSource;
        private CustomWeightControl weightControlBullet;
        private System.Windows.Forms.NumericUpDown numericBC;
        private System.Windows.Forms.ComboBox comboBoxBCType;
        private CustomVelocityControl velocityAtMuzzle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAdvanced;
        private System.Windows.Forms.ComboBox comboBoxCaliber;
        private System.Windows.Forms.Label labelCaliber;
        private System.Windows.Forms.Label labelBarrelLength;
        private CustomDistanceControl distanceBarrelLength;
        private System.Windows.Forms.ComboBox comboBoxBullet;
        private System.Windows.Forms.Label labelCompactShape;
        private System.Windows.Forms.Label labelFullShape;
        private CustomDistanceControl distanceBulletInfoLength;
        private System.Windows.Forms.Label labelDiameter;
        private System.Windows.Forms.Label labelLength;
        private CustomDistanceControl customDistanceControl2;
        private CustomDistanceControl distanceBulletInfoDiameter;
    }
}
