using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Reticle
{
    public partial class ReticleControl : UserControl
    {
        public ReticleControl()
        {
            InitializeComponent();
            mShotIcon = Properties.Resources.shotIcon;
        }

        private Reticle mReticle = null;
        public Reticle Reticle
        {
            get
            {
                return mReticle;
            }
            set
            {
                mReticle = value;
                mZoom = 1;
                UpdatePicture();
            }
        }

        private BallisticInfoCollection mLongRangeBallistic;
        public BallisticInfoCollection FarRangeBallistic
        {
            get
            {
                return mLongRangeBallistic;
            }
            set
            {
                mLongRangeBallistic = value;
                InvalidateBDC();
            }
        }

        private BallisticInfoCollection mShortRangeBallistic;
        public BallisticInfoCollection ShortRangeBallistic
        {
            get
            {
                return mShortRangeBallistic;
            }
            set
            {
                mShortRangeBallistic = value;
                InvalidateBDC();
            }
        }

        private bool mShowShortRangeBDC, mShowLongRangeBDC;

        public bool ShowShortRangeBDC
        {
            get
            {
                return mShowShortRangeBDC;
            }
            set
            {
                mShowShortRangeBDC = value;
                InvalidateBDC();
            }
        }

        public bool ShowLongRangeBDC
        {
            get
            {
                return mShowLongRangeBDC;
            }
            set
            {
                mShowLongRangeBDC = value;
                InvalidateBDC();
            }
        }

        public class ShotInfo
        {
            private Distance mDistance, mWidth, mHeight;
            private Control mControl;

            internal ShotInfo(Control control)
            {
                mControl = control;
            }

            public Distance Distance
            {
                get
                {
                    return mDistance;
                }
                set
                {
                    mDistance = value;
                    mControl.Invalidate(true);
                }
            }

            public Distance Width
            {
                get
                {
                    return mWidth;
                }
                set
                {
                    mWidth = value;
                    mControl.Invalidate(true);
                }
            }

            public Distance Height
            {
                get
                {
                    return mHeight;
                }
                set
                {
                    mHeight = value;
                    mControl.Invalidate(true);
                }
            }
        }

        private ShotInfo mShot;
        public ShotInfo Shot
        {
            get
            {
                if (mShot == null)
                    mShot = new ShotInfo(this);
                return mShot;
            }
        }

        private Distance mZero = new Distance(100, Distance.Unit.Yard);

        public Distance Zero
        {
            get
            {
                return mZero;
            }
            set
            {
                mZero = value;
                InvalidateBDC();
            }
        }

        private double mZoomFactor = 1;

        public double ZoomFactor
        {
            get
            {
                return mZoomFactor;
            }
            set
            {
                mZoomFactor = value;
                InvalidateBDC();
            }
        }

        private Icon mShotIcon;

        private double mResizeFactor = 1;
        private double mResolution;

        int mZoom = 1;
        public int Zoom
        {
            get
            {
                return mZoom;
            }
            set
            {
                if (mReticle != null && !mReticle.Raster)
                {
                    mZoom = value;
                    UpdatePicture();
                }
            }
        }

        class BDCView
        {
            public bool found = false;
            public int x;
            public int y;
            public int ix;
            public int iy;
            public Angle holdAngle;
            public Distance distance;
        }

        private BDCView[] mBDCs = null;

        Font mFont = new Font("Microsoft Sans Serif", 6, FontStyle.Regular);
        Brush mRedBrush = new SolidBrush(Color.Red);
        Brush mWhiteBrush = new SolidBrush(Color.White);
        Brush mGrayBrush = new SolidBrush(Color.Gray);
        Pen mTargetPen;

        private void UpdatePicture()
        {
            Image b;
            Graphics g;
            int w1, h1;

            if (mReticle == null)
            {
                pictureBox.Image = null;
                return;
            }

            if (!mReticle.Raster)
            {
                int w, h;
                w = mReticle.Image.Width;
                h = mReticle.Image.Height;

                float width_factor = (float)w / (float)(this.Width * mZoom);
                float height_factor = (float)h / (float)(this.Height * mZoom);
                float factor = Math.Max(width_factor, height_factor);

                w1 = (int)(w / factor);
                h1 = (int)(h / factor);

                mResizeFactor = (1 / factor);
            }
            else
            {
                int w, h;
                w = mReticle.Image.Width;
                h = mReticle.Image.Height;

                w1 = w;
                h1 = h;
                mResizeFactor = 1;
            }
            b = new Bitmap(w1, h1);
            g = Graphics.FromImage(b);

            g.DrawImage(mReticle.Image, 0, 0, w1, h1);
            pictureBox.Image = b;
            pictureBox.Width = b.Width;
            pictureBox.Height = b.Height;
            InvalidateBDC();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs pe)
        {
            if (mZoomFactor != 1)
                pe.Graphics.DrawString("Scale: " + mZoomFactor.ToString("g", CultureInfo.InvariantCulture) + "x", mFont, mRedBrush, 0, 0);

            if (mReticle != null)
            {
                int zx, zy;

                zx = (int)Math.Round(mReticle.ZeroX * mResizeFactor);
                zy = (int)Math.Round(mReticle.ZeroY * mResizeFactor);

                if (mBDCs != null)
                {
                    for (int i = 0; i < mBDCs.Length; i++)
                    {
                        if (mBDCs[i].found)
                        {
                            if (mBDCs[i].iy >= 0 && mBDCs[i].iy < pictureBox.Image.Height)
                            {
                                int precision = 0;
                                if (mBDCs[i].distance.Get(mBDCs[i].distance.SetUnit) < 10)
                                    precision = 1;
                                string t = mBDCs[i].distance.ToString(mZero.SetUnit, false, precision);
                                SizeF sz = pe.Graphics.MeasureString(t, mFont);
                                float y0 = mBDCs[i].iy - sz.Height / 2;

                                int ix = 0;
                                if (mBDCs[i].ix < 0)
                                {
                                    ix = zx + 20;
                                }
                                else
                                {
                                    if (mBDCs[i].ix < zx)
                                    {
                                        ix = (int)(mBDCs[i].ix - sz.Width);
                                    }
                                    else
                                        ix = mBDCs[i].ix;
                                }
                                pe.Graphics.FillRectangle(mWhiteBrush, ix, y0, sz.Width, sz.Height);
                                pe.Graphics.DrawString(t, mFont, mRedBrush, ix, y0);
                            }
                        }
                    }
                }


                if (mShot != null && mShot.Distance != null)
                {
                    bool found = false;
                    Angle hold = null, windage = null;

                    if (mShot.Distance.Get(Distance.Unit.Yard) < mZero.Get(Distance.Unit.Yard) && mShortRangeBallistic != null)
                    {
                        found = BallisticInfoController.Calculation.GetHoldAndAdjustmentByDistance(mShortRangeBallistic, mShot.Distance, true, out hold, out windage);
                    }
                    if (!found && mLongRangeBallistic != null)
                    {
                        found = BallisticInfoController.Calculation.GetHoldAndAdjustmentByDistance(mLongRangeBallistic, mShot.Distance, true, out hold, out windage);
                    }

                    if (found)
                    {
                        hold = new Angle(-hold.Get(hold.SetUnit), hold.SetUnit);
                        windage = new Angle(-windage.Get(windage.SetUnit), windage.SetUnit);

                        int iy = (int)Math.Round(ReticleController.Calculation.AngleToPixels(hold, mReticle.ZeroY, mResolution) * mResizeFactor);
                        int ix = (int)Math.Round(ReticleController.Calculation.AngleToPixels(windage, mReticle.ZeroX, mResolution) * mResizeFactor);

                        if (mShot.Width == null || mShot.Height == null)
                        {
                            ix -= 15;
                            iy -= 16;
                            pe.Graphics.DrawIcon(mShotIcon, ix, iy);
                        }
                        else
                        {
                            double moaWidth = mShot.Width.Get(Distance.Unit.Inch) * 100 / mShot.Distance.Get(Distance.Unit.Yard);
                            double moaHeight = mShot.Height.Get(Distance.Unit.Inch) * 100 / mShot.Distance.Get(Distance.Unit.Yard);
                            int wx = (int)Math.Round(ReticleController.Calculation.AngleToPixels(new Angle(moaWidth, Angle.Unit.Moa), 0, mResolution) * mResizeFactor);
                            int wy = (int)Math.Round(ReticleController.Calculation.AngleToPixels(new Angle(moaHeight, Angle.Unit.Moa), 0, mResolution) * mResizeFactor);

                            if (wx < 1)
                                wx = 1;
                            if (wy < 1)
                                wy = 1;

                            if (mTargetPen == null)
                            {
                                mTargetPen = new Pen(Color.DarkRed, 2);
                                mTargetPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            }

                            pe.Graphics.DrawRectangle(mTargetPen, ix - wx / 2, iy - wy / 2, wx, wy);
                        }
                    }
                }
            }
        }

        private Image ToImage()
        {
            Bitmap bm = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);
            PaintEventArgs pe = new PaintEventArgs(Graphics.FromImage(bm), new Rectangle(0, 0, pictureBox.Image.Width, pictureBox.Image.Height));
            OnPaint(pe);

            return bm;
        }

        private void InvalidateBDC()
        {
            mBDCs = null;
            if (mReticle == null)
                return;

            mResolution = ReticleController.Calculation.getResultion(mReticle.CalibrationX1, mReticle.CalibrationY1, mReticle.CalibrationX2, mReticle.CalibrationY2, mReticle.CalibrationAngle) / mZoomFactor;

            if ((mShowShortRangeBDC || mShowLongRangeBDC) && mLongRangeBallistic != null && mReticle.BDCPoints.Count > 0)
            {
                BDCView[] views = new BDCView[mReticle.BDCPoints.Count];
                int fc = 0;
                for (int i = 0; i < views.Length; i++)
                {
                    views[i] = new BDCView();
                    views[i].x = mReticle.BDCPoints[i].X;
                    views[i].y = mReticle.BDCPoints[i].Y;
                    views[i].ix = (int)Math.Round(views[i].x * mResizeFactor);
                    views[i].iy = (int)Math.Round(views[i].y * mResizeFactor);
                    views[i].holdAngle = ReticleController.Calculation.ImagePixelsToAngle(views[i].y, mReticle.ZeroY, mResolution, Angle.Unit.Mil);
                    views[i].holdAngle = new Angle(-views[i].holdAngle.Get(views[i].holdAngle.SetUnit), views[i].holdAngle.SetUnit);


                    Distance distance = null;

                    if (mShortRangeBallistic != null && mShowShortRangeBDC)
                        if (BallisticInfoController.Calculation.GetDistanceByHold(mShortRangeBallistic, views[i].holdAngle, false, out distance))
                            views[i].distance = new Distance(distance.Get(mZero.SetUnit), mZero.SetUnit);

                    if (mLongRangeBallistic != null && mShowLongRangeBDC)
                        if (BallisticInfoController.Calculation.GetDistanceByHold(mLongRangeBallistic, views[i].holdAngle, true, out distance))
                            views[i].distance = new Distance(distance.Get(mZero.SetUnit), mZero.SetUnit);

                    if (views[i].distance != null)
                    {
                        views[i].found = true;
                        fc++;
                    }
                }
                if (fc > 0)
                    mBDCs = views;
            }
            Invalidate(true);
        }

        private void ReticleControl_SizeChanged(object sender, EventArgs e)
        {
            if (mReticle != null && !mReticle.Raster)
                UpdatePicture();
        }

        public delegate void ReticleControlMouseEventDelegate(object sender, ReticleControlMouseEventArgs e);

        public event ReticleControlMouseEventDelegate ReticleMouseMove;
        public event ReticleControlMouseEventDelegate ReticleMouseDown;

        Angle.Unit mAngleUnits;

        public Angle.Unit AngleUnits
        {
            get
            {
                return mAngleUnits;
            }
            set
            {
                mAngleUnits = value;
            }
        }

        private void ReticleControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mReticle != null)
            {
                int ix = (int)Math.Round(e.X / mResizeFactor);
                int iy = (int)Math.Round(e.Y / mResizeFactor);


                Angle hold, windage;

                windage = ReticleController.Calculation.ImagePixelsToAngle(ix, mReticle.ZeroX, mResolution, mAngleUnits);
                hold = ReticleController.Calculation.ImagePixelsToAngle(iy, mReticle.ZeroY, mResolution, mAngleUnits);

                ReticleControlMouseEventArgs rcme = new ReticleControlMouseEventArgs(hold, windage, e);

                if (ReticleMouseMove != null)
                    ReticleMouseMove(this, rcme);
            }
        }

        private void ReticleControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (mReticle != null)
            {
                int ix = (int)Math.Round(e.X / mResizeFactor);
                int iy = (int)Math.Round(e.Y / mResizeFactor);


                Angle hold, windage;

                windage = ReticleController.Calculation.ImagePixelsToAngle(ix, mReticle.ZeroX, mResolution, mAngleUnits);
                hold = ReticleController.Calculation.ImagePixelsToAngle(iy, mReticle.ZeroY, mResolution, mAngleUnits);
                ReticleControlMouseEventArgs rcme = new ReticleControlMouseEventArgs(hold, windage, e);

                if (ReticleMouseDown != null)
                    ReticleMouseDown(this, rcme);
            }
        }

    }
}
