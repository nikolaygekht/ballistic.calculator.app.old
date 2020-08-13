using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Gehtsoft.BallisticCalculator.Reticle
{
    public partial class ZoomAndScrollPicture : UserControl
    {
        private Image mOrgImage;
        private bool mVectorImage;
        private int mZoom = 1;
        private double mZoomFactor = 1;
        private Pen mLinePen = new Pen(Color.Gray);

        public int Zoom
        {
            get
            {
                return mZoom;
            }
            set
            {
                mZoom = value;
                UpdateImage();
            }
        }


        public void UpdateImage()
        {
            if (mOrgImage == null)
                pictureBox1.Image = null;
            else
            {
                int w1, h1;

                if (mVectorImage)
                {
                    int w, h;
                    w = mOrgImage.Width;
                    h = mOrgImage.Height;

                    float width_factor = mOrgImage.Width / (float)this.Width;
                    float height_factor = mOrgImage.Height / (float)this.Height;
                    float factor = Math.Max(width_factor, height_factor);

                    w1 = (int)(w / factor) * mZoom;
                    h1 = (int)(h / factor) * mZoom;

                    mZoomFactor = (1 / factor) * (double)mZoom;
                }
                else
                {
                    mZoomFactor = (double)mZoom;
                    
                    int w, h;
                    w = mOrgImage.Width;
                    h = mOrgImage.Height;

                    w1 = w * mZoom;
                    h1 = h * mZoom;
                }
                Bitmap b = new Bitmap(w1, h1);
                Graphics g = Graphics.FromImage(b);
                g.DrawImage(mOrgImage, 0, 0, w1, h1);
                if (mZoom >= 4)
                {
                    for (int i = mZoom; i < w1; i += mZoom)
                        g.DrawLine(mLinePen, i, 0, i, h1);
                    for (int i = mZoom; i < h1; i += mZoom)
                        g.DrawLine(mLinePen, 0, i, w1, i);

                }
                if (mShowObjects)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        PaintObject o = mObjects[i];
                        if (o != null && o.Type != PaintObject.ObjectType.None)
                        {
                            Pen p = new Pen(o.Color, 3);
                            int x, y;
                            x = (int)(o.X * mZoomFactor);
                            y = (int)(o.Y * mZoomFactor);
                            switch (o.Type)
                            {
                                case    PaintObject.ObjectType.Cross:
                                    g.DrawLine(p, x - 7, y, x + 7, y);
                                    g.DrawLine(p, x, y + 7, x, y - 7);
                                    break;
                                case    PaintObject.ObjectType.DiagonalCross:
                                    g.DrawLine(p, x - 7, y - 7, x + 7, y + 7);
                                    g.DrawLine(p, x - 7, y + 7, x + 7, y - 7);
                                    break;
                                case PaintObject.ObjectType.Circle:
                                    {
                                        Brush br = new SolidBrush(o.Color);
                                        g.DrawEllipse(p, x - 7, y - 7, 14, 14);
                                        g.FillRectangle(br, x, y, 1, 1);
                                    }
                                    break;
                            }
                        }
                    }
                }

                if (mHorizontalGuide > 0 || mVerticalGuide > 0)
                {
                    Pen guidePen = new Pen(Color.Blue);
                    guidePen.Width = 3;
                    guidePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                    if (mVerticalGuide > 0)
                    {
                        g.DrawLine(guidePen, (float)(mVerticalGuide * mZoomFactor), (float)0, (float)(mVerticalGuide * mZoomFactor), (float)(mOrgImage.Height * mZoomFactor));
                    }

                    if (mHorizontalGuide > 0)
                    {
                        g.DrawLine(guidePen, 0, (float)(mHorizontalGuide * mZoomFactor), (float)(mOrgImage.Width * mZoomFactor), (float)(mHorizontalGuide * mZoomFactor));
                    }
                }
                pictureBox1.Image = b;
            }
        }

        public void SetImage(Image img, bool vector)
        {
            mOrgImage = img;
            mVectorImage = vector;
            UpdateImage();
        }

        public ZoomAndScrollPicture()
        {
            InitializeComponent();
        }

        public delegate void MouseMoveEventDelegate(int imgX, int imgY);
        public event MouseMoveEventDelegate MouseMoveEvent;

        public delegate void MouseClickEventDelegate(int imgX, int imgY);
        public event MouseClickEventDelegate MouseClickEvent;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMoveEvent != null && mOrgImage != null)
            {
                MouseMoveEvent((int)(e.X / mZoomFactor), (int)(e.Y / mZoomFactor));
            }
        }

        public void PictureToView(int x, int y, out int _x, out int _y)
        {
            _x = (int)(x * mZoomFactor);
            _y = (int)(y * mZoomFactor);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseClickEvent != null && mOrgImage != null)
            {
                MouseClickEvent((int)(e.X / mZoomFactor), (int)(e.Y / mZoomFactor));
            }
        }

        public class PaintObject
        {
            ObjectType mType;
            private int mX, mY;
            private Color mColor;

            public enum ObjectType
            {
                None,
                Cross,
                DiagonalCross,
                Circle,
            }

            public PaintObject(ObjectType type, int x, int y, Color color)
            {
                mType = type;
                mX = x;
                mColor = color;
                mY = y;
            }

            public ObjectType Type
            {
                get
                {
                    return mType;
                }
                set
                {
                    mType = value;
                }
            }

            public int X
            {
                get
                {
                    return mX;
                }
                set
                {
                    mX = value;
                }
            }

            public int Y
            {
                get
                {
                    return mY;
                }
                set
                {
                    mY = value;
                }
            }

            public Color Color
            {
                get
                {
                    return mColor;
                }
                set
                {
                    mColor = value;
                }
            }
        }

        private PaintObject[] mObjects = new PaintObject[32];

        public PaintObject[] Objects
        {
            get
            {
                return mObjects;
            }
        }

        public void ClearObjects()
        {
            for (int i = 0; i < 32; i++)
                mObjects[i] = null;
        }

        private bool mShowObjects;

        public Point ImageAutoScrollPosition
        {
            get
            {
                return panel1.AutoScrollPosition;
            }
            set
            {
                panel1.AutoScrollPosition = value;
            }
        }

        public bool ShowObjects
        {
            get
            {
                return mShowObjects;
            }
            set
            {
                mShowObjects = value;
                UpdateImage();
            }
        }

        private int mHorizontalGuide = -1, mVerticalGuide = -1;

        public int HorizontalGuide
        {
            get
            {
                return mHorizontalGuide;
            }
            set
            {
                mHorizontalGuide = value;
                UpdateImage();
            }
        }
        
        public int VerticalGuide
        {
            get
            {
                return mVerticalGuide;
            }
            set
            {
                mVerticalGuide = value;
                UpdateImage();
            }
        }
    }
}
