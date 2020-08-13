using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Gehtsoft.BallisticCalculator.Reticle;
using MathEx.ExternalBallistic.Units;

namespace ReticleEditor
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();
            zoomAndScrollPicture1.Zoom = 1;
            checkBoxPreviewPoints.Checked = true;
            zoomAndScrollPicture1.ShowObjects = true;
            for (int i = 0; i < 32; i++)
                zoomAndScrollPicture1.Objects[i] = new ZoomAndScrollPicture.PaintObject(ZoomAndScrollPicture.PaintObject.ObjectType.None, 0, 0, Color.Black);
        }

        private void zoomAndScrollPicture1_MouseClickEvent(int imgX, int imgY)
        {
            if (checkBoxHorizontalGuide.Checked && Math.Abs(imgY - (int)numericHorizontalGuide.Value) < 30)
                imgY = (int)numericHorizontalGuide.Value;
            if (checkBoxVerticalGuide.Checked && Math.Abs(imgX - (int)numericVerticalGuide.Value) < 30)
                imgX = (int)numericVerticalGuide.Value;


            if (radioButtonZeroPosition.Checked)
            {
                zoomAndScrollPicture1.Objects[0].Color = Color.Red;
                zoomAndScrollPicture1.Objects[0].Type = ZoomAndScrollPicture.PaintObject.ObjectType.Cross;
                zoomAndScrollPicture1.Objects[0].X = imgX;
                zoomAndScrollPicture1.Objects[0].Y = imgY;
                radioButtonZeroPosition.ForeColor = Color.Black;
            }
            else if (radioButtonCalPoint1.Checked)
            {
                zoomAndScrollPicture1.Objects[1].Color = Color.Green;
                zoomAndScrollPicture1.Objects[1].Type = ZoomAndScrollPicture.PaintObject.ObjectType.DiagonalCross;
                zoomAndScrollPicture1.Objects[1].X = imgX;
                zoomAndScrollPicture1.Objects[1].Y = imgY;
                radioButtonCalPoint1.ForeColor = Color.Black;
            }
            else if (radioButtonCalPoint2.Checked)
            {
                zoomAndScrollPicture1.Objects[2].Color = Color.Green;
                zoomAndScrollPicture1.Objects[2].Type = ZoomAndScrollPicture.PaintObject.ObjectType.DiagonalCross;
                zoomAndScrollPicture1.Objects[2].X = imgX;
                zoomAndScrollPicture1.Objects[2].Y = imgY;
                radioButtonCalPoint2.ForeColor = Color.Black;
            }
            else
            {
                for (int i = 0; i < BdcPointsRadio.Length; i++)
                    if (BdcPointsRadio[i].Checked)
                    {
                        zoomAndScrollPicture1.Objects[10 + i].Color = Color.Blue;
                        zoomAndScrollPicture1.Objects[10 + i].Type = ZoomAndScrollPicture.PaintObject.ObjectType.Circle;
                        zoomAndScrollPicture1.Objects[10 + i].X = imgX;
                        zoomAndScrollPicture1.Objects[10 + i].Y = imgY;
                        BdcPointsRadio[i].ForeColor = Color.Black;
                    }
            }
            if (checkBoxPreviewPoints.Checked)
                zoomAndScrollPicture1.UpdateImage();
        }

        private void zoomAndScrollPicture1_MouseMoveEvent(int imgX, int imgY)
        {
            toolStripStatusX.Text = imgX.ToString();
            toolStripStatusY.Text = imgY.ToString();

            if (zoomAndScrollPicture1.Objects[0].Type != ZoomAndScrollPicture.PaintObject.ObjectType.None &&
                zoomAndScrollPicture1.Objects[1].Type != ZoomAndScrollPicture.PaintObject.ObjectType.None &&
                zoomAndScrollPicture1.Objects[2].Type != ZoomAndScrollPicture.PaintObject.ObjectType.None &&
                customAngleControl1.Value.Get(Angle.Unit.Mil) > 0)
            {
                double resolution = ReticleController.Calculation.getResultion(zoomAndScrollPicture1.Objects[1].X,
                                                                              zoomAndScrollPicture1.Objects[1].Y,
                                                                              zoomAndScrollPicture1.Objects[2].X,
                                                                              zoomAndScrollPicture1.Objects[2].Y,
                                                                              customAngleControl1.Value);
                if (resolution > 0)
                {
                    Angle a = ReticleController.Calculation.ImagePixelsToAngle(imgX, zoomAndScrollPicture1.Objects[0].X, resolution, customAngleControl1.Value.SetUnit);
                    toolStripStatusWindage.Text = "W:" + a.ToString(a.SetUnit);
                    a = ReticleController.Calculation.ImagePixelsToAngle(imgY, zoomAndScrollPicture1.Objects[0].Y, resolution, customAngleControl1.Value.SetUnit);
                    toolStripStatusHold.Text = "H:" + a.ToString(a.SetUnit);

                }
            }
        }
        private bool mVector = false;
        private Image mCurrentImage = null;
        private byte[] mImageSource = null;

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Portable Network Graphic Bitmaps (*.png)|*.png|Windows metafiles (*.wmf)|*.wmf";
            dlg.FilterIndex = 2;
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                mVector = dlg.FilterIndex == 2;
                string filename = dlg.FileName;
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                {
                    mImageSource = new byte[fs.Length];
                    fs.Read(mImageSource, 0, (int)fs.Length);
                    fs.Close();
                }

                using (MemoryStream ms = new MemoryStream(mImageSource))
                {
                    if (mVector)
                        mCurrentImage = new Metafile(ms);
                    else
                        mCurrentImage = Image.FromStream(ms);
                }
                numericHorizontalGuide.Value = mCurrentImage.Height / 2;
                numericVerticalGuide.Value = mCurrentImage.Width / 2;
                zoomAndScrollPicture1.SetImage(mCurrentImage, mVector);
            }
        }

        private void toolStripMenuItem100_Click(object sender, EventArgs e)
        {
            if (mCurrentImage != null)
            {
                zoomAndScrollPicture1.Zoom = 1;
                toolStripZoom.Text = "100%";
            }
        }

        private void toolStripMenuItem200_Click(object sender, EventArgs e)
        {
            if (mCurrentImage != null)
            {
                zoomAndScrollPicture1.Zoom = 2;
                toolStripZoom.Text = "200%";
            }

        }

        private void toolStripMenuItem400_Click(object sender, EventArgs e)
        {
            if (mCurrentImage != null)
            {
                zoomAndScrollPicture1.Zoom = 4;
                toolStripZoom.Text = "400%";
            }

        }

        private void toolStripMenuItem800_Click(object sender, EventArgs e)
        {
            if (mCurrentImage != null)
            {
                zoomAndScrollPicture1.Zoom = 8;
                toolStripZoom.Text = "800%";
            }
        }

        private void checkBoxPreviewPoints_CheckedChanged(object sender, EventArgs e)
        {
            zoomAndScrollPicture1.ShowObjects = checkBoxPreviewPoints.Checked;
        }

        private void buttonClearPoint_Click(object sender, EventArgs e)
        {
            if (radioButtonZeroPosition.Checked)
            {
                zoomAndScrollPicture1.Objects[0].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
                radioButtonZeroPosition.ForeColor = Color.Red;
            }
            else if (radioButtonCalPoint1.Checked)
            {
                zoomAndScrollPicture1.Objects[1].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
                radioButtonCalPoint1.ForeColor = Color.Red;
            }
            else if (radioButtonCalPoint2.Checked)
            {
                zoomAndScrollPicture1.Objects[2].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
                radioButtonCalPoint2.ForeColor = Color.Red;
            }
            else
            {
                for (int i = 0; i < BdcPointsRadio.Length; i++)
                    if (BdcPointsRadio[i].Checked)
                    {
                        zoomAndScrollPicture1.Objects[10 + i].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
                        BdcPointsRadio[i].ForeColor = Color.Red;
                    }
            }
            if (checkBoxPreviewPoints.Checked)
                zoomAndScrollPicture1.UpdateImage();
        }

        private void buttonClearAllPoints_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
                zoomAndScrollPicture1.Objects[i] = new ZoomAndScrollPicture.PaintObject(ZoomAndScrollPicture.PaintObject.ObjectType.None, 0, 0, Color.Black);

            zoomAndScrollPicture1.Objects[0].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
            radioButtonZeroPosition.ForeColor = Color.Red;
            zoomAndScrollPicture1.Objects[1].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
            radioButtonCalPoint1.ForeColor = Color.Red;
            zoomAndScrollPicture1.Objects[2].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
            radioButtonCalPoint2.ForeColor = Color.Red;
            zoomAndScrollPicture1.Objects[10].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
            for (int i = 0; i < BdcPointsRadio.Length; i++)
            {
                zoomAndScrollPicture1.Objects[10 + i].Type = ZoomAndScrollPicture.PaintObject.ObjectType.None;
                BdcPointsRadio[i].ForeColor = Color.Red;
            }
            if (checkBoxPreviewPoints.Checked)
                zoomAndScrollPicture1.UpdateImage();

        }

        private void buttonFindPoint_Click(object sender, EventArgs e)
        {

            ZoomAndScrollPicture.PaintObject obj = null;
            if (radioButtonZeroPosition.Checked)
            {
                obj = zoomAndScrollPicture1.Objects[0];
            }
            else if (radioButtonCalPoint1.Checked)
            {
                obj = zoomAndScrollPicture1.Objects[1];
            }
            else if (radioButtonCalPoint2.Checked)
            {
                obj = zoomAndScrollPicture1.Objects[2];
            }
            else
            {
                for (int i = 0; i < BdcPointsRadio.Length; i++)
                {
                    if (BdcPointsRadio[i].Checked)
                    {
                        obj = zoomAndScrollPicture1.Objects[10 + i];
                        break;
                    }
                }
            }
            if (obj != null && obj.Type != ZoomAndScrollPicture.PaintObject.ObjectType.None)
            {
                int x, y;

                zoomAndScrollPicture1.PictureToView(obj.X, obj.Y, out x, out y);
                zoomAndScrollPicture1.ImageAutoScrollPosition = new Point(x - zoomAndScrollPicture1.Width / 2, y - zoomAndScrollPicture1.Height / 2);
            }
        }

        private bool ValidateReticle(bool showValidMessage)
        {
            string errors = "";

            if (textBoxName.Text.Length == 0)
            {
                errors += "The name of the reticle must be specified\r\n";
            }

            if (zoomAndScrollPicture1.Objects[0] == null ||
                zoomAndScrollPicture1.Objects[0].Type == ZoomAndScrollPicture.PaintObject.ObjectType.None)
            {
                errors += "The zero point of the reticle must be specified\r\n";
            }

            if (zoomAndScrollPicture1.Objects[1] == null ||
                zoomAndScrollPicture1.Objects[1].Type == ZoomAndScrollPicture.PaintObject.ObjectType.None)
            {
                errors += "The first calibration point of the reticle must be specified\r\n";
            }

            if (zoomAndScrollPicture1.Objects[2] == null ||
                zoomAndScrollPicture1.Objects[2].Type == ZoomAndScrollPicture.PaintObject.ObjectType.None)
            {
                errors += "The second calibration point of the reticle must be specified\r\n";
            }

            if (Math.Sqrt(Math.Pow(zoomAndScrollPicture1.Objects[1].X - zoomAndScrollPicture1.Objects[2].X, 2) +
                          Math.Pow(zoomAndScrollPicture1.Objects[1].Y - zoomAndScrollPicture1.Objects[2].Y, 2)) < 30)
            {
                errors += "The calibration points are too close\r\n";
            }

            if (customAngleControl1.Value.Get(MathEx.ExternalBallistic.Units.Angle.Unit.Mil) <= 0)
            {
                errors += "The calibration value must be greater than zero\r\n";
            }

            if (errors.Length > 0)
                MessageBox.Show(errors, "Reticle validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (showValidMessage)
                MessageBox.Show("The reticle is valid", "Reticle validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return errors.Length == 0;
        }

        private void buttonValidateReticle_Click(object sender, EventArgs e)
        {
            ValidateReticle(true);
        }

        private void anyRadioButton_Click(object sender, EventArgs e)
        {
            if (sender is RadioButton && (sender as RadioButton).Checked)
            {
                if (zoomAndScrollPicture1.Zoom > 1) 
                    buttonFindPoint_Click(sender, e);
            }
        }

        private RadioButton[] mBdcPointsRadio = null;

        private RadioButton[] BdcPointsRadio
        {
            get
            {
                if (mBdcPointsRadio == null)
                {
                    mBdcPointsRadio = new RadioButton[] {
                        radioButtonBDC1,
                        radioButtonBDC2,
                        radioButtonBDC3,
                        radioButtonBDC4,
                        radioButtonBDC5,
                        radioButtonBDC6,
                        radioButtonBDC7,
                        radioButtonBDC8,
                        radioButtonBDC9,
                        radioButtonBDC10,
                        radioButtonBDC11,
                        radioButtonBDC12,
                        radioButtonBDC13,
                        radioButtonBDC14,
                        radioButtonBDC15,
                        radioButtonBDC16,
                        radioButtonBDC17,
                        radioButtonBDC18,
                    };
                }
                return mBdcPointsRadio;
            }
        }



        private void buttonLoadReticle_Click(object sender, EventArgs e)
        {
            buttonClearAllPoints_Click(sender, e);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Reticles (*.reticle)|*.reticle|All files|*.*";
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Reticle r;
                try
                {
                    r = ReticleController.Serialization.loadReticle(dlg.FileName, true);
                    mImageSource = r.ImageSource;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (r.Name != null)
                    textBoxName.Text = r.Name;
                if (r.Image != null)
                {
                    mCurrentImage = r.Image;
                    mVector = !r.Raster;
                    numericHorizontalGuide.Value = mCurrentImage.Height / 2;
                    numericVerticalGuide.Value = mCurrentImage.Width / 2;
                    zoomAndScrollPicture1.SetImage(mCurrentImage, mVector);
                }
                if (r.ZeroX >= 0 && r.ZeroY >= 0)
                {
                    radioButtonZeroPosition.Checked = true;
                    zoomAndScrollPicture1_MouseClickEvent(r.ZeroX, r.ZeroY);
                }
                if (r.CalibrationX1 >= 0 && r.CalibrationY1 >= 0)
                {
                    radioButtonCalPoint1.Checked = true;
                    zoomAndScrollPicture1_MouseClickEvent(r.CalibrationX1, r.CalibrationY1);
                }
                if (r.CalibrationX2 >= 0 && r.CalibrationY2 >= 0)
                {
                    radioButtonCalPoint2.Checked = true;
                    zoomAndScrollPicture1_MouseClickEvent(r.CalibrationX2, r.CalibrationY2);
                }
                if (r.CalibrationAngle != null)
                    customAngleControl1.Value = r.CalibrationAngle;
                for (int i = 0; i < r.BDCPoints.Count; i++)
                {
                    Reticle.BDCPoint point = r.BDCPoints[i];
                    RadioButton rb = BdcPointsRadio[i];
                    rb.Checked = true;
                    zoomAndScrollPicture1_MouseClickEvent(point.X, point.Y);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (/*ValidateReticle(false)*/true)
            {
                Reticle r = new Reticle();
                r.Name = textBoxName.Text;
                r.Image = mCurrentImage;
                r.Raster = !mVector;
                r.ZeroX = zoomAndScrollPicture1.Objects[0].X;
                r.ZeroY = zoomAndScrollPicture1.Objects[0].Y;
                r.CalibrationX1 = zoomAndScrollPicture1.Objects[1].X;
                r.CalibrationY1 = zoomAndScrollPicture1.Objects[1].Y;
                r.CalibrationX2 = zoomAndScrollPicture1.Objects[2].X;
                r.CalibrationY2 = zoomAndScrollPicture1.Objects[2].Y;
                r.CalibrationAngle = customAngleControl1.Value;
                r.ImageSource = mImageSource;
                for (int i = 0; i < mBdcPointsRadio.Length; i++)
                {
                    if (zoomAndScrollPicture1.Objects[10 + i].Type != ZoomAndScrollPicture.PaintObject.ObjectType.None)
                    {
                        Reticle.BDCPoint bdc = new Reticle.BDCPoint();
                        bdc.X = zoomAndScrollPicture1.Objects[10 + i].X;
                        bdc.Y = zoomAndScrollPicture1.Objects[10 + i].Y;
                        r.BDCPoints.Add(bdc);
                    }
                }
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Reticles (*.reticle)|*.reticle|All files|*.*";
                dlg.AddExtension = true;
                dlg.DefaultExt = "reticle";
                dlg.CheckPathExists = true;
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        ReticleController.Serialization.saveReticle(r, dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void checkBoxHorizontalGuide_CheckedChanged(object sender, EventArgs e)
        {
            numericHorizontalGuide.Enabled = checkBoxHorizontalGuide.Checked;
            if (checkBoxHorizontalGuide.Checked)
                zoomAndScrollPicture1.HorizontalGuide = (int)numericHorizontalGuide.Value;
            else
                zoomAndScrollPicture1.HorizontalGuide = -1;
        }

        private void checkBoxVerticalGuide_CheckedChanged(object sender, EventArgs e)
        {
            numericVerticalGuide.Enabled = checkBoxVerticalGuide.Checked;
            if (checkBoxVerticalGuide.Checked)
                zoomAndScrollPicture1.VerticalGuide = (int)numericVerticalGuide.Value;
            else
                zoomAndScrollPicture1.VerticalGuide = -1;
        }

        private void numericHorizontalGuide_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxHorizontalGuide.Checked)
                zoomAndScrollPicture1.HorizontalGuide = (int)numericHorizontalGuide.Value;
            else
                zoomAndScrollPicture1.HorizontalGuide = -1;
        }

        private void numericVerticalGuide_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxVerticalGuide.Checked)
                zoomAndScrollPicture1.VerticalGuide = (int)numericVerticalGuide.Value;
            else
                zoomAndScrollPicture1.VerticalGuide = -1;

        }
    }
}
