namespace Gehtsoft.BallisticCalculator.UI
{
    partial class CustomControlValue
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.comboBoxUnits = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxValue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxUnits);
            this.splitContainer1.Size = new System.Drawing.Size(244, 22);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 0;
            // 
            // textBoxValue
            // 
            this.textBoxValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxValue.Location = new System.Drawing.Point(0, 0);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(159, 22);
            this.textBoxValue.TabIndex = 0;
            this.textBoxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            this.textBoxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            this.textBoxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxValue_KeyPress);
            // 
            // comboBoxUnits
            // 
            this.comboBoxUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxUnits.FormattingEnabled = true;
            this.comboBoxUnits.Location = new System.Drawing.Point(0, 0);
            this.comboBoxUnits.Name = "comboBoxUnits";
            this.comboBoxUnits.Size = new System.Drawing.Size(81, 24);
            this.comboBoxUnits.TabIndex = 0;
            this.comboBoxUnits.SelectedValueChanged += new System.EventHandler(this.comboBoxUnits_SelectedValueChanged);
            this.comboBoxUnits.SizeChanged += new System.EventHandler(this.comboBoxUnits_SizeChanged);
            // 
            // CustomControlValue
            // 
            this.Controls.Add(this.splitContainer1);
            this.Name = "CustomControlValue";
            this.Size = new System.Drawing.Size(244, 22);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ComboBox comboBoxUnits;

    }
}
