namespace Gehtsoft.BallisticCalculator.UI
{
    partial class ShotParametersInput
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
            this.distanceMax = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceStep = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.angleShot = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.angleCant = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.angleVClicks = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.angleHClicks = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.labelSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // distanceMax
            // 
            this.distanceMax.Location = new System.Drawing.Point(140, 4);
            this.distanceMax.Name = "distanceMax";
            this.distanceMax.Size = new System.Drawing.Size(186, 22);
            this.distanceMax.TabIndex = 0;
            this.distanceMax.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Yard;
            this.distanceMax.UnitName = "yd";
            // 
            // distanceStep
            // 
            this.distanceStep.Location = new System.Drawing.Point(140, 32);
            this.distanceStep.Name = "distanceStep";
            this.distanceStep.Size = new System.Drawing.Size(186, 22);
            this.distanceStep.TabIndex = 1;
            this.distanceStep.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Yard;
            this.distanceStep.UnitName = "yd";
            // 
            // angleShot
            // 
            this.angleShot.Location = new System.Drawing.Point(140, 61);
            this.angleShot.Name = "angleShot";
            this.angleShot.Size = new System.Drawing.Size(186, 22);
            this.angleShot.TabIndex = 2;
            this.angleShot.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Degree;
            this.angleShot.UnitName = "°";
            // 
            // angleCant
            // 
            this.angleCant.Location = new System.Drawing.Point(140, 89);
            this.angleCant.Name = "angleCant";
            this.angleCant.Size = new System.Drawing.Size(186, 22);
            this.angleCant.TabIndex = 3;
            this.angleCant.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Degree;
            this.angleCant.UnitName = "°";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Maximum Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Step";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Shot Angle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cant Angle";
            // 
            // angleVClicks
            // 
            this.angleVClicks.Location = new System.Drawing.Point(140, 118);
            this.angleVClicks.Name = "angleVClicks";
            this.angleVClicks.Size = new System.Drawing.Size(186, 22);
            this.angleVClicks.TabIndex = 4;
            this.angleVClicks.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Moa;
            this.angleVClicks.UnitName = "moa";
            // 
            // angleHClicks
            // 
            this.angleHClicks.Location = new System.Drawing.Point(140, 146);
            this.angleHClicks.Name = "angleHClicks";
            this.angleHClicks.Size = new System.Drawing.Size(186, 22);
            this.angleHClicks.TabIndex = 5;
            this.angleHClicks.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Moa;
            this.angleHClicks.UnitName = "moa";
            // 
            // labelSize
            // 
            this.labelSize.Location = new System.Drawing.Point(140, 175);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(186, 23);
            this.labelSize.TabIndex = 10;
            this.labelSize.Text = "label5";
            this.labelSize.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Vertical Click";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Horizontal Click";
            // 
            // ShotParametersInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.angleHClicks);
            this.Controls.Add(this.angleVClicks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.angleCant);
            this.Controls.Add(this.angleShot);
            this.Controls.Add(this.distanceStep);
            this.Controls.Add(this.distanceMax);
            this.Name = "ShotParametersInput";
            this.Size = new System.Drawing.Size(370, 222);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomDistanceControl distanceMax;
        private CustomDistanceControl distanceStep;
        private CustomAngleControl angleShot;
        private CustomAngleControl angleCant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CustomAngleControl angleVClicks;
        private CustomAngleControl angleHClicks;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
