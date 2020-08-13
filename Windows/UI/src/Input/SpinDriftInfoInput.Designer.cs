namespace Gehtsoft.BallisticCalculator.UI
{
    partial class SpinDriftInfoInput
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
            this.label1 = new System.Windows.Forms.Label();
            this.distanceDiameter = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceLength = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.distanceRifling = new Gehtsoft.BallisticCalculator.UI.CustomDistanceControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonLH = new System.Windows.Forms.RadioButton();
            this.radioButtonRH = new System.Windows.Forms.RadioButton();
            this.checkBoxCalculate = new System.Windows.Forms.CheckBox();
            this.comboBoxPreset1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxPreset
            // 
            this.comboBoxPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPreset.FormattingEnabled = true;
            this.comboBoxPreset.Location = new System.Drawing.Point(121, 30);
            this.comboBoxPreset.Name = "comboBoxPreset";
            this.comboBoxPreset.Size = new System.Drawing.Size(170, 24);
            this.comboBoxPreset.TabIndex = 0;
            this.comboBoxPreset.SelectedIndexChanged += new System.EventHandler(this.comboBoxPreset_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Preset";
            // 
            // distanceDiameter
            // 
            this.distanceDiameter.Location = new System.Drawing.Point(121, 61);
            this.distanceDiameter.Name = "distanceDiameter";
            this.distanceDiameter.ReadOnly = false;
            this.distanceDiameter.Size = new System.Drawing.Size(170, 22);
            this.distanceDiameter.TabIndex = 1;
            this.distanceDiameter.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceDiameter.UnitName = "in";
            // 
            // distanceLength
            // 
            this.distanceLength.Location = new System.Drawing.Point(121, 89);
            this.distanceLength.Name = "distanceLength";
            this.distanceLength.ReadOnly = false;
            this.distanceLength.Size = new System.Drawing.Size(170, 22);
            this.distanceLength.TabIndex = 2;
            this.distanceLength.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceLength.UnitName = "in";
            // 
            // distanceRifling
            // 
            this.distanceRifling.Location = new System.Drawing.Point(121, 145);
            this.distanceRifling.Name = "distanceRifling";
            this.distanceRifling.ReadOnly = false;
            this.distanceRifling.Size = new System.Drawing.Size(170, 22);
            this.distanceRifling.TabIndex = 4;
            this.distanceRifling.Unit = MathEx.ExternalBallistic.Units.Distance.Unit.Inch;
            this.distanceRifling.UnitName = "in";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bullet Diameter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Bullet Lenght";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Rifling";
            // 
            // radioButtonLH
            // 
            this.radioButtonLH.AutoSize = true;
            this.radioButtonLH.Location = new System.Drawing.Point(121, 173);
            this.radioButtonLH.Name = "radioButtonLH";
            this.radioButtonLH.Size = new System.Drawing.Size(47, 21);
            this.radioButtonLH.TabIndex = 5;
            this.radioButtonLH.TabStop = true;
            this.radioButtonLH.Text = "LH";
            this.radioButtonLH.UseVisualStyleBackColor = true;
            // 
            // radioButtonRH
            // 
            this.radioButtonRH.AutoSize = true;
            this.radioButtonRH.Location = new System.Drawing.Point(174, 173);
            this.radioButtonRH.Name = "radioButtonRH";
            this.radioButtonRH.Size = new System.Drawing.Size(49, 21);
            this.radioButtonRH.TabIndex = 6;
            this.radioButtonRH.TabStop = true;
            this.radioButtonRH.Text = "RH";
            this.radioButtonRH.UseVisualStyleBackColor = true;
            // 
            // checkBoxCalculate
            // 
            this.checkBoxCalculate.AutoSize = true;
            this.checkBoxCalculate.Location = new System.Drawing.Point(7, 3);
            this.checkBoxCalculate.Name = "checkBoxCalculate";
            this.checkBoxCalculate.Size = new System.Drawing.Size(150, 21);
            this.checkBoxCalculate.TabIndex = 10;
            this.checkBoxCalculate.Text = "Calculate Spin Drift";
            this.checkBoxCalculate.UseVisualStyleBackColor = true;
            // 
            // comboBoxPreset1
            // 
            this.comboBoxPreset1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPreset1.FormattingEnabled = true;
            this.comboBoxPreset1.Location = new System.Drawing.Point(121, 115);
            this.comboBoxPreset1.Name = "comboBoxPreset1";
            this.comboBoxPreset1.Size = new System.Drawing.Size(170, 24);
            this.comboBoxPreset1.Sorted = true;
            this.comboBoxPreset1.TabIndex = 3;
            this.comboBoxPreset1.SelectedIndexChanged += new System.EventHandler(this.comboBoxPreset1_SelectedIndexChanged);
            // 
            // SpinDriftInfoInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxPreset1);
            this.Controls.Add(this.checkBoxCalculate);
            this.Controls.Add(this.radioButtonRH);
            this.Controls.Add(this.radioButtonLH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.distanceRifling);
            this.Controls.Add(this.distanceLength);
            this.Controls.Add(this.distanceDiameter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPreset);
            this.Name = "SpinDriftInfoInput";
            this.Size = new System.Drawing.Size(330, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPreset;
        private System.Windows.Forms.Label label1;
        private CustomDistanceControl distanceDiameter;
        private CustomDistanceControl distanceLength;
        private CustomDistanceControl distanceRifling;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonLH;
        private System.Windows.Forms.RadioButton radioButtonRH;
        private System.Windows.Forms.CheckBox checkBoxCalculate;
        private System.Windows.Forms.ComboBox comboBoxPreset1;
    }
}
