namespace ReticleEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.zoomAndScrollPicture1 = new Gehtsoft.BallisticCalculator.Reticle.ZoomAndScrollPicture();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusX = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusY = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusHold = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusWindage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripZoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem100 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem400 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem800 = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonBDC18 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC17 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC16 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC15 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC14 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC13 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC12 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC11 = new System.Windows.Forms.RadioButton();
            this.buttonFindPoint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonValidateReticle = new System.Windows.Forms.Button();
            this.buttonLoadReticle = new System.Windows.Forms.Button();
            this.buttonClearAllPoints = new System.Windows.Forms.Button();
            this.buttonClearPoint = new System.Windows.Forms.Button();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.checkBoxPreviewPoints = new System.Windows.Forms.CheckBox();
            this.radioButtonBDC10 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC9 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC8 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC7 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC6 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC5 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC4 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC3 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC2 = new System.Windows.Forms.RadioButton();
            this.radioButtonBDC1 = new System.Windows.Forms.RadioButton();
            this.customAngleControl1 = new Gehtsoft.BallisticCalculator.UI.CustomAngleControl();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonCalPoint2 = new System.Windows.Forms.RadioButton();
            this.radioButtonCalPoint1 = new System.Windows.Forms.RadioButton();
            this.radioButtonZeroPosition = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.checkBoxHorizontalGuide = new System.Windows.Forms.CheckBox();
            this.numericHorizontalGuide = new System.Windows.Forms.NumericUpDown();
            this.numericVerticalGuide = new System.Windows.Forms.NumericUpDown();
            this.checkBoxVerticalGuide = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHorizontalGuide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericVerticalGuide)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.zoomAndScrollPicture1);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.numericVerticalGuide);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxVerticalGuide);
            this.splitContainer1.Panel2.Controls.Add(this.numericHorizontalGuide);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxHorizontalGuide);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC18);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC17);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC16);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC15);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC14);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC13);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC12);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC11);
            this.splitContainer1.Panel2.Controls.Add(this.buttonFindPoint);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.buttonValidateReticle);
            this.splitContainer1.Panel2.Controls.Add(this.buttonLoadReticle);
            this.splitContainer1.Panel2.Controls.Add(this.buttonClearAllPoints);
            this.splitContainer1.Panel2.Controls.Add(this.buttonClearPoint);
            this.splitContainer1.Panel2.Controls.Add(this.buttonLoadImage);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxPreviewPoints);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC10);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC9);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC8);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC7);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC6);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC5);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC4);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC3);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC2);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonBDC1);
            this.splitContainer1.Panel2.Controls.Add(this.customAngleControl1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonCalPoint2);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonCalPoint1);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonZeroPosition);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxName);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.splitContainer1.Size = new System.Drawing.Size(986, 637);
            this.splitContainer1.SplitterDistance = 502;
            this.splitContainer1.TabIndex = 0;
            // 
            // zoomAndScrollPicture1
            // 
            this.zoomAndScrollPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomAndScrollPicture1.HorizontalGuide = -1;
            this.zoomAndScrollPicture1.ImageAutoScrollPosition = new System.Drawing.Point(0, 0);
            this.zoomAndScrollPicture1.Location = new System.Drawing.Point(0, 0);
            this.zoomAndScrollPicture1.Name = "zoomAndScrollPicture1";
            this.zoomAndScrollPicture1.ShowObjects = false;
            this.zoomAndScrollPicture1.Size = new System.Drawing.Size(498, 606);
            this.zoomAndScrollPicture1.TabIndex = 0;
            this.zoomAndScrollPicture1.VerticalGuide = -1;
            this.zoomAndScrollPicture1.Zoom = 1;
            this.zoomAndScrollPicture1.MouseMoveEvent += new Gehtsoft.BallisticCalculator.Reticle.ZoomAndScrollPicture.MouseMoveEventDelegate(this.zoomAndScrollPicture1_MouseMoveEvent);
            this.zoomAndScrollPicture1.MouseClickEvent += new Gehtsoft.BallisticCalculator.Reticle.ZoomAndScrollPicture.MouseClickEventDelegate(this.zoomAndScrollPicture1_MouseClickEvent);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain,
            this.toolStripStatusX,
            this.toolStripStatusY,
            this.toolStripStatusHold,
            this.toolStripStatusWindage,
            this.toolStripZoom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(498, 27);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(315, 22);
            this.toolStripStatusLabelMain.Spring = true;
            // 
            // toolStripStatusX
            // 
            this.toolStripStatusX.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusX.Name = "toolStripStatusX";
            this.toolStripStatusX.Size = new System.Drawing.Size(27, 22);
            this.toolStripStatusX.Text = "   ";
            // 
            // toolStripStatusY
            // 
            this.toolStripStatusY.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusY.Name = "toolStripStatusY";
            this.toolStripStatusY.Size = new System.Drawing.Size(27, 22);
            this.toolStripStatusY.Text = "   ";
            // 
            // toolStripStatusHold
            // 
            this.toolStripStatusHold.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusHold.Name = "toolStripStatusHold";
            this.toolStripStatusHold.Size = new System.Drawing.Size(27, 22);
            this.toolStripStatusHold.Text = "   ";
            // 
            // toolStripStatusWindage
            // 
            this.toolStripStatusWindage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusWindage.Name = "toolStripStatusWindage";
            this.toolStripStatusWindage.Size = new System.Drawing.Size(27, 22);
            this.toolStripStatusWindage.Text = "   ";
            // 
            // toolStripZoom
            // 
            this.toolStripZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem100,
            this.toolStripMenuItem200,
            this.toolStripMenuItem400,
            this.toolStripMenuItem800});
            this.toolStripZoom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripZoom.Image")));
            this.toolStripZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripZoom.Name = "toolStripZoom";
            this.toolStripZoom.Size = new System.Drawing.Size(60, 25);
            this.toolStripZoom.Text = "100%";
            // 
            // toolStripMenuItem100
            // 
            this.toolStripMenuItem100.Name = "toolStripMenuItem100";
            this.toolStripMenuItem100.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem100.Text = "100%";
            this.toolStripMenuItem100.Click += new System.EventHandler(this.toolStripMenuItem100_Click);
            // 
            // toolStripMenuItem200
            // 
            this.toolStripMenuItem200.Name = "toolStripMenuItem200";
            this.toolStripMenuItem200.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem200.Text = "200%";
            this.toolStripMenuItem200.Click += new System.EventHandler(this.toolStripMenuItem200_Click);
            // 
            // toolStripMenuItem400
            // 
            this.toolStripMenuItem400.Name = "toolStripMenuItem400";
            this.toolStripMenuItem400.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem400.Text = "400%";
            this.toolStripMenuItem400.Click += new System.EventHandler(this.toolStripMenuItem400_Click);
            // 
            // toolStripMenuItem800
            // 
            this.toolStripMenuItem800.Name = "toolStripMenuItem800";
            this.toolStripMenuItem800.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem800.Text = "800%";
            this.toolStripMenuItem800.Click += new System.EventHandler(this.toolStripMenuItem800_Click);
            // 
            // radioButtonBDC18
            // 
            this.radioButtonBDC18.AutoSize = true;
            this.radioButtonBDC18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC18.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC18.Location = new System.Drawing.Point(132, 366);
            this.radioButtonBDC18.Name = "radioButtonBDC18";
            this.radioButtonBDC18.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC18.TabIndex = 32;
            this.radioButtonBDC18.TabStop = true;
            this.radioButtonBDC18.Text = "BDC 18";
            this.radioButtonBDC18.UseVisualStyleBackColor = true;
            this.radioButtonBDC18.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC17
            // 
            this.radioButtonBDC17.AutoSize = true;
            this.radioButtonBDC17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC17.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC17.Location = new System.Drawing.Point(132, 339);
            this.radioButtonBDC17.Name = "radioButtonBDC17";
            this.radioButtonBDC17.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC17.TabIndex = 31;
            this.radioButtonBDC17.TabStop = true;
            this.radioButtonBDC17.Text = "BDC 17";
            this.radioButtonBDC17.UseVisualStyleBackColor = true;
            this.radioButtonBDC17.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC16
            // 
            this.radioButtonBDC16.AutoSize = true;
            this.radioButtonBDC16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC16.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC16.Location = new System.Drawing.Point(132, 312);
            this.radioButtonBDC16.Name = "radioButtonBDC16";
            this.radioButtonBDC16.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC16.TabIndex = 30;
            this.radioButtonBDC16.TabStop = true;
            this.radioButtonBDC16.Text = "BDC 16";
            this.radioButtonBDC16.UseVisualStyleBackColor = true;
            this.radioButtonBDC16.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC15
            // 
            this.radioButtonBDC15.AutoSize = true;
            this.radioButtonBDC15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC15.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC15.Location = new System.Drawing.Point(132, 285);
            this.radioButtonBDC15.Name = "radioButtonBDC15";
            this.radioButtonBDC15.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC15.TabIndex = 29;
            this.radioButtonBDC15.TabStop = true;
            this.radioButtonBDC15.Text = "BDC 15";
            this.radioButtonBDC15.UseVisualStyleBackColor = true;
            this.radioButtonBDC15.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC14
            // 
            this.radioButtonBDC14.AutoSize = true;
            this.radioButtonBDC14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC14.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC14.Location = new System.Drawing.Point(132, 258);
            this.radioButtonBDC14.Name = "radioButtonBDC14";
            this.radioButtonBDC14.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC14.TabIndex = 28;
            this.radioButtonBDC14.TabStop = true;
            this.radioButtonBDC14.Text = "BDC 14";
            this.radioButtonBDC14.UseVisualStyleBackColor = true;
            this.radioButtonBDC14.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC13
            // 
            this.radioButtonBDC13.AutoSize = true;
            this.radioButtonBDC13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC13.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC13.Location = new System.Drawing.Point(132, 231);
            this.radioButtonBDC13.Name = "radioButtonBDC13";
            this.radioButtonBDC13.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC13.TabIndex = 27;
            this.radioButtonBDC13.TabStop = true;
            this.radioButtonBDC13.Text = "BDC 13";
            this.radioButtonBDC13.UseVisualStyleBackColor = true;
            this.radioButtonBDC13.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC12
            // 
            this.radioButtonBDC12.AutoSize = true;
            this.radioButtonBDC12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC12.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC12.Location = new System.Drawing.Point(132, 204);
            this.radioButtonBDC12.Name = "radioButtonBDC12";
            this.radioButtonBDC12.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC12.TabIndex = 26;
            this.radioButtonBDC12.TabStop = true;
            this.radioButtonBDC12.Text = "BDC 12";
            this.radioButtonBDC12.UseVisualStyleBackColor = true;
            this.radioButtonBDC12.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC11
            // 
            this.radioButtonBDC11.AutoSize = true;
            this.radioButtonBDC11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC11.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC11.Location = new System.Drawing.Point(132, 177);
            this.radioButtonBDC11.Name = "radioButtonBDC11";
            this.radioButtonBDC11.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC11.TabIndex = 25;
            this.radioButtonBDC11.TabStop = true;
            this.radioButtonBDC11.Text = "BDC 11";
            this.radioButtonBDC11.UseVisualStyleBackColor = true;
            this.radioButtonBDC11.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // buttonFindPoint
            // 
            this.buttonFindPoint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonFindPoint.Location = new System.Drawing.Point(285, 447);
            this.buttonFindPoint.Name = "buttonFindPoint";
            this.buttonFindPoint.Size = new System.Drawing.Size(135, 23);
            this.buttonFindPoint.TabIndex = 24;
            this.buttonFindPoint.Text = "Find Point";
            this.buttonFindPoint.UseVisualStyleBackColor = true;
            this.buttonFindPoint.Click += new System.EventHandler(this.buttonFindPoint_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 506);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Save Reticle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonValidateReticle
            // 
            this.buttonValidateReticle.Location = new System.Drawing.Point(144, 506);
            this.buttonValidateReticle.Name = "buttonValidateReticle";
            this.buttonValidateReticle.Size = new System.Drawing.Size(135, 23);
            this.buttonValidateReticle.TabIndex = 22;
            this.buttonValidateReticle.Text = "Validate Reticle";
            this.buttonValidateReticle.UseVisualStyleBackColor = true;
            this.buttonValidateReticle.Click += new System.EventHandler(this.buttonValidateReticle_Click);
            // 
            // buttonLoadReticle
            // 
            this.buttonLoadReticle.Location = new System.Drawing.Point(3, 506);
            this.buttonLoadReticle.Name = "buttonLoadReticle";
            this.buttonLoadReticle.Size = new System.Drawing.Size(135, 23);
            this.buttonLoadReticle.TabIndex = 21;
            this.buttonLoadReticle.Text = "Load Reticle";
            this.buttonLoadReticle.UseVisualStyleBackColor = true;
            this.buttonLoadReticle.Click += new System.EventHandler(this.buttonLoadReticle_Click);
            // 
            // buttonClearAllPoints
            // 
            this.buttonClearAllPoints.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClearAllPoints.Location = new System.Drawing.Point(144, 447);
            this.buttonClearAllPoints.Name = "buttonClearAllPoints";
            this.buttonClearAllPoints.Size = new System.Drawing.Size(135, 23);
            this.buttonClearAllPoints.TabIndex = 20;
            this.buttonClearAllPoints.Text = "Clear All Points";
            this.buttonClearAllPoints.UseVisualStyleBackColor = true;
            this.buttonClearAllPoints.Click += new System.EventHandler(this.buttonClearAllPoints_Click);
            // 
            // buttonClearPoint
            // 
            this.buttonClearPoint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClearPoint.Location = new System.Drawing.Point(3, 447);
            this.buttonClearPoint.Name = "buttonClearPoint";
            this.buttonClearPoint.Size = new System.Drawing.Size(135, 23);
            this.buttonClearPoint.TabIndex = 19;
            this.buttonClearPoint.Text = "Clear Point";
            this.buttonClearPoint.UseVisualStyleBackColor = true;
            this.buttonClearPoint.Click += new System.EventHandler(this.buttonClearPoint_Click);
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonLoadImage.Location = new System.Drawing.Point(3, 477);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(135, 23);
            this.buttonLoadImage.TabIndex = 18;
            this.buttonLoadImage.Text = "Load Image";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.buttonLoadImage_Click);
            // 
            // checkBoxPreviewPoints
            // 
            this.checkBoxPreviewPoints.AutoSize = true;
            this.checkBoxPreviewPoints.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxPreviewPoints.Location = new System.Drawing.Point(6, 420);
            this.checkBoxPreviewPoints.Name = "checkBoxPreviewPoints";
            this.checkBoxPreviewPoints.Size = new System.Drawing.Size(122, 21);
            this.checkBoxPreviewPoints.TabIndex = 17;
            this.checkBoxPreviewPoints.Text = "Preview Points";
            this.checkBoxPreviewPoints.UseVisualStyleBackColor = true;
            this.checkBoxPreviewPoints.CheckedChanged += new System.EventHandler(this.checkBoxPreviewPoints_CheckedChanged);
            // 
            // radioButtonBDC10
            // 
            this.radioButtonBDC10.AutoSize = true;
            this.radioButtonBDC10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonBDC10.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC10.Location = new System.Drawing.Point(132, 150);
            this.radioButtonBDC10.Name = "radioButtonBDC10";
            this.radioButtonBDC10.Size = new System.Drawing.Size(77, 21);
            this.radioButtonBDC10.TabIndex = 16;
            this.radioButtonBDC10.TabStop = true;
            this.radioButtonBDC10.Text = "BDC 10";
            this.radioButtonBDC10.UseVisualStyleBackColor = true;
            this.radioButtonBDC10.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC9
            // 
            this.radioButtonBDC9.AutoSize = true;
            this.radioButtonBDC9.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC9.Location = new System.Drawing.Point(6, 366);
            this.radioButtonBDC9.Name = "radioButtonBDC9";
            this.radioButtonBDC9.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC9.TabIndex = 15;
            this.radioButtonBDC9.TabStop = true;
            this.radioButtonBDC9.Text = "BDC 9";
            this.radioButtonBDC9.UseVisualStyleBackColor = true;
            this.radioButtonBDC9.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC8
            // 
            this.radioButtonBDC8.AutoSize = true;
            this.radioButtonBDC8.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC8.Location = new System.Drawing.Point(6, 339);
            this.radioButtonBDC8.Name = "radioButtonBDC8";
            this.radioButtonBDC8.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC8.TabIndex = 14;
            this.radioButtonBDC8.TabStop = true;
            this.radioButtonBDC8.Text = "BDC 8";
            this.radioButtonBDC8.UseVisualStyleBackColor = true;
            this.radioButtonBDC8.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC7
            // 
            this.radioButtonBDC7.AutoSize = true;
            this.radioButtonBDC7.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC7.Location = new System.Drawing.Point(6, 312);
            this.radioButtonBDC7.Name = "radioButtonBDC7";
            this.radioButtonBDC7.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC7.TabIndex = 13;
            this.radioButtonBDC7.TabStop = true;
            this.radioButtonBDC7.Text = "BDC 7";
            this.radioButtonBDC7.UseVisualStyleBackColor = true;
            this.radioButtonBDC7.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC6
            // 
            this.radioButtonBDC6.AutoSize = true;
            this.radioButtonBDC6.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC6.Location = new System.Drawing.Point(6, 285);
            this.radioButtonBDC6.Name = "radioButtonBDC6";
            this.radioButtonBDC6.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC6.TabIndex = 12;
            this.radioButtonBDC6.TabStop = true;
            this.radioButtonBDC6.Text = "BDC 6";
            this.radioButtonBDC6.UseVisualStyleBackColor = true;
            this.radioButtonBDC6.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC5
            // 
            this.radioButtonBDC5.AutoSize = true;
            this.radioButtonBDC5.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC5.Location = new System.Drawing.Point(6, 258);
            this.radioButtonBDC5.Name = "radioButtonBDC5";
            this.radioButtonBDC5.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC5.TabIndex = 11;
            this.radioButtonBDC5.TabStop = true;
            this.radioButtonBDC5.Text = "BDC 5";
            this.radioButtonBDC5.UseVisualStyleBackColor = true;
            this.radioButtonBDC5.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC4
            // 
            this.radioButtonBDC4.AutoSize = true;
            this.radioButtonBDC4.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC4.Location = new System.Drawing.Point(6, 231);
            this.radioButtonBDC4.Name = "radioButtonBDC4";
            this.radioButtonBDC4.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC4.TabIndex = 10;
            this.radioButtonBDC4.TabStop = true;
            this.radioButtonBDC4.Text = "BDC 4";
            this.radioButtonBDC4.UseVisualStyleBackColor = true;
            this.radioButtonBDC4.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC3
            // 
            this.radioButtonBDC3.AutoSize = true;
            this.radioButtonBDC3.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC3.Location = new System.Drawing.Point(6, 204);
            this.radioButtonBDC3.Name = "radioButtonBDC3";
            this.radioButtonBDC3.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC3.TabIndex = 9;
            this.radioButtonBDC3.TabStop = true;
            this.radioButtonBDC3.Text = "BDC 3";
            this.radioButtonBDC3.UseVisualStyleBackColor = true;
            this.radioButtonBDC3.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC2
            // 
            this.radioButtonBDC2.AutoSize = true;
            this.radioButtonBDC2.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC2.Location = new System.Drawing.Point(6, 177);
            this.radioButtonBDC2.Name = "radioButtonBDC2";
            this.radioButtonBDC2.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC2.TabIndex = 8;
            this.radioButtonBDC2.TabStop = true;
            this.radioButtonBDC2.Text = "BDC 2";
            this.radioButtonBDC2.UseVisualStyleBackColor = true;
            this.radioButtonBDC2.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonBDC1
            // 
            this.radioButtonBDC1.AutoSize = true;
            this.radioButtonBDC1.ForeColor = System.Drawing.Color.Red;
            this.radioButtonBDC1.Location = new System.Drawing.Point(6, 150);
            this.radioButtonBDC1.Name = "radioButtonBDC1";
            this.radioButtonBDC1.Size = new System.Drawing.Size(69, 21);
            this.radioButtonBDC1.TabIndex = 7;
            this.radioButtonBDC1.TabStop = true;
            this.radioButtonBDC1.Text = "BDC 1";
            this.radioButtonBDC1.UseVisualStyleBackColor = true;
            this.radioButtonBDC1.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // customAngleControl1
            // 
            this.customAngleControl1.Location = new System.Drawing.Point(154, 115);
            this.customAngleControl1.Name = "customAngleControl1";
            this.customAngleControl1.ReadOnly = false;
            this.customAngleControl1.Size = new System.Drawing.Size(312, 22);
            this.customAngleControl1.TabIndex = 6;
            this.customAngleControl1.Unit = MathEx.ExternalBallistic.Units.Angle.Unit.Moa;
            this.customAngleControl1.UnitName = "moa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cal Angle";
            // 
            // radioButtonCalPoint2
            // 
            this.radioButtonCalPoint2.AutoSize = true;
            this.radioButtonCalPoint2.ForeColor = System.Drawing.Color.Red;
            this.radioButtonCalPoint2.Location = new System.Drawing.Point(6, 92);
            this.radioButtonCalPoint2.Name = "radioButtonCalPoint2";
            this.radioButtonCalPoint2.Size = new System.Drawing.Size(97, 21);
            this.radioButtonCalPoint2.TabIndex = 4;
            this.radioButtonCalPoint2.TabStop = true;
            this.radioButtonCalPoint2.Text = "Cal Point 2";
            this.radioButtonCalPoint2.UseVisualStyleBackColor = true;
            this.radioButtonCalPoint2.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonCalPoint1
            // 
            this.radioButtonCalPoint1.AutoSize = true;
            this.radioButtonCalPoint1.ForeColor = System.Drawing.Color.Red;
            this.radioButtonCalPoint1.Location = new System.Drawing.Point(6, 65);
            this.radioButtonCalPoint1.Name = "radioButtonCalPoint1";
            this.radioButtonCalPoint1.Size = new System.Drawing.Size(97, 21);
            this.radioButtonCalPoint1.TabIndex = 3;
            this.radioButtonCalPoint1.TabStop = true;
            this.radioButtonCalPoint1.Text = "Cal Point 1";
            this.radioButtonCalPoint1.UseVisualStyleBackColor = true;
            this.radioButtonCalPoint1.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // radioButtonZeroPosition
            // 
            this.radioButtonZeroPosition.AutoSize = true;
            this.radioButtonZeroPosition.ForeColor = System.Drawing.Color.Red;
            this.radioButtonZeroPosition.Location = new System.Drawing.Point(6, 38);
            this.radioButtonZeroPosition.Name = "radioButtonZeroPosition";
            this.radioButtonZeroPosition.Size = new System.Drawing.Size(59, 21);
            this.radioButtonZeroPosition.TabIndex = 2;
            this.radioButtonZeroPosition.TabStop = true;
            this.radioButtonZeroPosition.Text = "Zero";
            this.radioButtonZeroPosition.UseVisualStyleBackColor = true;
            this.radioButtonZeroPosition.Click += new System.EventHandler(this.anyRadioButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reticle Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(154, 10);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(312, 22);
            this.textBoxName.TabIndex = 0;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(6, 366);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(138, 21);
            this.radioButton8.TabIndex = 15;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "BDC 9";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // checkBoxHorizontalGuide
            // 
            this.checkBoxHorizontalGuide.AutoSize = true;
            this.checkBoxHorizontalGuide.Location = new System.Drawing.Point(3, 535);
            this.checkBoxHorizontalGuide.Name = "checkBoxHorizontalGuide";
            this.checkBoxHorizontalGuide.Size = new System.Drawing.Size(106, 21);
            this.checkBoxHorizontalGuide.TabIndex = 33;
            this.checkBoxHorizontalGuide.Text = "Horz. Guide";
            this.checkBoxHorizontalGuide.UseVisualStyleBackColor = true;
            this.checkBoxHorizontalGuide.CheckedChanged += new System.EventHandler(this.checkBoxHorizontalGuide_CheckedChanged);
            // 
            // numericHorizontalGuide
            // 
            this.numericHorizontalGuide.Enabled = false;
            this.numericHorizontalGuide.Location = new System.Drawing.Point(144, 534);
            this.numericHorizontalGuide.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericHorizontalGuide.Name = "numericHorizontalGuide";
            this.numericHorizontalGuide.Size = new System.Drawing.Size(120, 22);
            this.numericHorizontalGuide.TabIndex = 34;
            this.numericHorizontalGuide.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericHorizontalGuide.ValueChanged += new System.EventHandler(this.numericHorizontalGuide_ValueChanged);
            // 
            // numericVerticalGuide
            // 
            this.numericVerticalGuide.Enabled = false;
            this.numericVerticalGuide.Location = new System.Drawing.Point(144, 561);
            this.numericVerticalGuide.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericVerticalGuide.Name = "numericVerticalGuide";
            this.numericVerticalGuide.Size = new System.Drawing.Size(120, 22);
            this.numericVerticalGuide.TabIndex = 36;
            this.numericVerticalGuide.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericVerticalGuide.ValueChanged += new System.EventHandler(this.numericVerticalGuide_ValueChanged);
            // 
            // checkBoxVerticalGuide
            // 
            this.checkBoxVerticalGuide.AutoSize = true;
            this.checkBoxVerticalGuide.Location = new System.Drawing.Point(3, 562);
            this.checkBoxVerticalGuide.Name = "checkBoxVerticalGuide";
            this.checkBoxVerticalGuide.Size = new System.Drawing.Size(102, 21);
            this.checkBoxVerticalGuide.TabIndex = 35;
            this.checkBoxVerticalGuide.Text = "Vert. Guide";
            this.checkBoxVerticalGuide.UseVisualStyleBackColor = true;
            this.checkBoxVerticalGuide.CheckedChanged += new System.EventHandler(this.checkBoxVerticalGuide_CheckedChanged);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 637);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AppForm";
            this.Text = "Reticle Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHorizontalGuide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericVerticalGuide)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Gehtsoft.BallisticCalculator.Reticle.ZoomAndScrollPicture zoomAndScrollPicture1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusX;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusY;
        private System.Windows.Forms.RadioButton radioButtonBDC10;
        private System.Windows.Forms.RadioButton radioButtonBDC9;
        private System.Windows.Forms.RadioButton radioButtonBDC8;
        private System.Windows.Forms.RadioButton radioButtonBDC7;
        private System.Windows.Forms.RadioButton radioButtonBDC6;
        private System.Windows.Forms.RadioButton radioButtonBDC5;
        private System.Windows.Forms.RadioButton radioButtonBDC4;
        private System.Windows.Forms.RadioButton radioButtonBDC3;
        private System.Windows.Forms.RadioButton radioButtonBDC2;
        private System.Windows.Forms.RadioButton radioButtonBDC1;
        private Gehtsoft.BallisticCalculator.UI.CustomAngleControl customAngleControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonCalPoint2;
        private System.Windows.Forms.RadioButton radioButtonCalPoint1;
        private System.Windows.Forms.RadioButton radioButtonZeroPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.CheckBox checkBoxPreviewPoints;
        private System.Windows.Forms.ToolStripDropDownButton toolStripZoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem800;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem400;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem100;
        private System.Windows.Forms.Button buttonClearAllPoints;
        private System.Windows.Forms.Button buttonClearPoint;
        private System.Windows.Forms.Button buttonFindPoint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonValidateReticle;
        private System.Windows.Forms.Button buttonLoadReticle;
        private System.Windows.Forms.RadioButton radioButtonBDC12;
        private System.Windows.Forms.RadioButton radioButtonBDC11;
        private System.Windows.Forms.RadioButton radioButtonBDC18;
        private System.Windows.Forms.RadioButton radioButtonBDC17;
        private System.Windows.Forms.RadioButton radioButtonBDC16;
        private System.Windows.Forms.RadioButton radioButtonBDC15;
        private System.Windows.Forms.RadioButton radioButtonBDC14;
        private System.Windows.Forms.RadioButton radioButtonBDC13;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusHold;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusWindage;
        private System.Windows.Forms.NumericUpDown numericVerticalGuide;
        private System.Windows.Forms.CheckBox checkBoxVerticalGuide;
        private System.Windows.Forms.NumericUpDown numericHorizontalGuide;
        private System.Windows.Forms.CheckBox checkBoxHorizontalGuide;
    }
}

