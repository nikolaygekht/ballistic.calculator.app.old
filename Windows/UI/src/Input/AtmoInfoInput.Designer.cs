namespace Gehtsoft.BallisticCalculator.UI.src.Input
{
    partial class AtmoInfoInput
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
            this.distanceAltitude = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.pressureAtm = new Gehtsoft.BallisticCalculator.UI.CustomPressureControl();
            this.temperatureAtm = new Gehtsoft.BallisticCalculator.UI.CustomTemperatureControl();
            this.numericHumidity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonResetTemperature = new System.Windows.Forms.Button();
            this.velocityWind = new Gehtsoft.BallisticCalculator.UI.CustomVelocityControl();
            this.angleWind = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.labelWind2 = new System.Windows.Forms.Label();
            this.labelWind1 = new System.Windows.Forms.Label();
            this.angleSelector1 = new Gehtsoft.BallisticCalculator.UI.AngleSelector();
            this.labelCompactShape = new System.Windows.Forms.Label();
            this.labelFullShape = new System.Windows.Forms.Label();
            this.labelT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericHumidity)).BeginInit();
            this.SuspendLayout();
            // 
            // distanceAltitude
            // 
            this.distanceAltitude.Location = new System.Drawing.Point(134, 3);
            this.distanceAltitude.Name = "distanceAltitude";
            this.distanceAltitude.Size = new System.Drawing.Size(218, 22);
            this.distanceAltitude.TabIndex = 0;
            this.distanceAltitude.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Foot;
            this.distanceAltitude.UnitName = "ft";
            // 
            // pressureAtm
            // 
            this.pressureAtm.Location = new System.Drawing.Point(134, 31);
            this.pressureAtm.Name = "pressureAtm";
            this.pressureAtm.Size = new System.Drawing.Size(218, 22);
            this.pressureAtm.TabIndex = 1;
            this.pressureAtm.Unit = MathEx.ExternalBallistic.Units.Pressure.Unit.InchHg;
            this.pressureAtm.UnitName = "inHg";
            // 
            // temperatureAtm
            // 
            this.temperatureAtm.Location = new System.Drawing.Point(134, 59);
            this.temperatureAtm.Name = "temperatureAtm";
            this.temperatureAtm.Size = new System.Drawing.Size(218, 22);
            this.temperatureAtm.TabIndex = 2;
            this.temperatureAtm.Unit = MathEx.ExternalBallistic.Units.Temperature.Unit.Fahrenheit;
            this.temperatureAtm.UnitName = "°F";
            // 
            // numericHumidity
            // 
            this.numericHumidity.Location = new System.Drawing.Point(134, 88);
            this.numericHumidity.Name = "numericHumidity";
            this.numericHumidity.Size = new System.Drawing.Size(101, 22);
            this.numericHumidity.TabIndex = 3;
            this.numericHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Altitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pressure";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Temperature";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Relative Humidity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "%";
            // 
            // buttonResetTemperature
            // 
            this.buttonResetTemperature.Location = new System.Drawing.Point(134, 117);
            this.buttonResetTemperature.Name = "buttonResetTemperature";
            this.buttonResetTemperature.Size = new System.Drawing.Size(147, 23);
            this.buttonResetTemperature.TabIndex = 9;
            this.buttonResetTemperature.Text = "Reset to Default";
            this.buttonResetTemperature.UseVisualStyleBackColor = true;
            this.buttonResetTemperature.Click += new System.EventHandler(this.buttonResetTemperature_Click);
            // 
            // velocityWind
            // 
            this.velocityWind.Location = new System.Drawing.Point(134, 156);
            this.velocityWind.Name = "velocityWind";
            this.velocityWind.Size = new System.Drawing.Size(218, 22);
            this.velocityWind.TabIndex = 11;
            this.velocityWind.Unit = MathEx.ExternalBallistic.Units.Velocity.Unit.MilesPerHour;
            this.velocityWind.UnitName = "mi/h";
            // 
            // angleWind
            // 
            this.angleWind.Location = new System.Drawing.Point(134, 185);
            this.angleWind.Name = "angleWind";
            this.angleWind.Size = new System.Drawing.Size(218, 22);
            this.angleWind.TabIndex = 12;
            this.angleWind.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Degree;
            this.angleWind.UnitName = "°";
            this.angleWind.ValueChanged += new Gehtsoft.BallisticCalculator.UI.CustomControlValue.ValueChangedDelegate(this.angleWind_ValueChanged);
            // 
            // labelWind2
            // 
            this.labelWind2.AutoSize = true;
            this.labelWind2.Location = new System.Drawing.Point(3, 161);
            this.labelWind2.Name = "labelWind2";
            this.labelWind2.Size = new System.Drawing.Size(85, 17);
            this.labelWind2.TabIndex = 13;
            this.labelWind2.Text = "Wind Speed";
            // 
            // labelWind1
            // 
            this.labelWind1.AutoSize = true;
            this.labelWind1.Location = new System.Drawing.Point(3, 190);
            this.labelWind1.Name = "labelWind1";
            this.labelWind1.Size = new System.Drawing.Size(80, 17);
            this.labelWind1.TabIndex = 14;
            this.labelWind1.Text = "Wind Angle";
            // 
            // angleSelector1
            // 
            this.angleSelector1.Angle = 0;
            this.angleSelector1.Location = new System.Drawing.Point(358, 156);
            this.angleSelector1.Name = "angleSelector1";
            this.angleSelector1.Size = new System.Drawing.Size(51, 51);
            this.angleSelector1.TabIndex = 18;
            this.angleSelector1.AngleChanged += new Gehtsoft.BallisticCalculator.UI.AngleSelector.AngleChangedDelegate(this.angleSelector1_AngleChanged);
            // 
            // labelCompactShape
            // 
            this.labelCompactShape.AutoSize = true;
            this.labelCompactShape.Location = new System.Drawing.Point(343, 133);
            this.labelCompactShape.Name = "labelCompactShape";
            this.labelCompactShape.Size = new System.Drawing.Size(12, 17);
            this.labelCompactShape.TabIndex = 19;
            this.labelCompactShape.Text = ".";
            this.labelCompactShape.Visible = false;
            // 
            // labelFullShape
            // 
            this.labelFullShape.AutoSize = true;
            this.labelFullShape.Location = new System.Drawing.Point(446, 199);
            this.labelFullShape.Name = "labelFullShape";
            this.labelFullShape.Size = new System.Drawing.Size(12, 17);
            this.labelFullShape.TabIndex = 20;
            this.labelFullShape.Text = ".";
            this.labelFullShape.Visible = false;
            // 
            // labelT
            // 
            this.labelT.Location = new System.Drawing.Point(134, 214);
            this.labelT.Name = "labelT";
            this.labelT.Size = new System.Drawing.Size(218, 23);
            this.labelT.TabIndex = 21;
            this.labelT.Text = "label6";
            this.labelT.Visible = false;
            // 
            // AtmoInfoInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelT);
            this.Controls.Add(this.labelFullShape);
            this.Controls.Add(this.labelCompactShape);
            this.Controls.Add(this.angleSelector1);
            this.Controls.Add(this.labelWind1);
            this.Controls.Add(this.labelWind2);
            this.Controls.Add(this.angleWind);
            this.Controls.Add(this.velocityWind);
            this.Controls.Add(this.buttonResetTemperature);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericHumidity);
            this.Controls.Add(this.temperatureAtm);
            this.Controls.Add(this.pressureAtm);
            this.Controls.Add(this.distanceAltitude);
            this.Name = "AtmoInfoInput";
            this.Size = new System.Drawing.Size(460, 240);
            ((System.ComponentModel.ISupportInitialize)(this.numericHumidity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomDistanceControl distanceAltitude;
        private CustomPressureControl pressureAtm;
        private CustomTemperatureControl temperatureAtm;
        private System.Windows.Forms.NumericUpDown numericHumidity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonResetTemperature;
        private CustomVelocityControl velocityWind;
        private CustomAngleControl angleWind;
        private System.Windows.Forms.Label labelWind2;
        private System.Windows.Forms.Label labelWind1;
        private AngleSelector angleSelector1;
        private System.Windows.Forms.Label labelCompactShape;
        private System.Windows.Forms.Label labelFullShape;
        private System.Windows.Forms.Label labelT;
    }
}
