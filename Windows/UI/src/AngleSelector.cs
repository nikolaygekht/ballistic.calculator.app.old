using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class AngleSelector : UserControl
    {
        private int mAngle;

        private Rectangle drawRegion;
        private Point origin;

        public AngleSelector()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void AngleSelector_Load(object sender, EventArgs e)
        {
            setDrawRegion();
        }

        private void AngleSelector_SizeChanged(object sender, EventArgs e)
        {
            this.Height = this.Width; //Keep it a square
            setDrawRegion();
        }

        private void setDrawRegion()
        {
            drawRegion = new Rectangle(0, 0, this.Width, this.Height);
            drawRegion.X += 2;
            drawRegion.Y += 2;
            drawRegion.Width -= 4;
            drawRegion.Height -= 4;

            int offset = 2;
            origin = new Point(drawRegion.Width / 2 + offset, drawRegion.Height / 2 + offset);

            this.Refresh();
        }

        public int Angle
        {
            get
            {
                return mAngle;
            }
            set
            {
                mAngle = value;

                if (mAngle > 180)
                {
                    mAngle = mAngle % 360;
                    if (mAngle > 180)
                        mAngle = mAngle - 360;
                }
                else if (mAngle < -180)
                {
                    mAngle = -mAngle % 360;
                    if (mAngle > 180)
                        mAngle = -(mAngle - 360);

                }

                if (!this.DesignMode && AngleChanged != null)
                    AngleChanged(); //Raise event

                this.Refresh();
            }
        }

        public delegate void AngleChangedDelegate();
        public event AngleChangedDelegate AngleChanged;
        private PointF DegreesToXY(float degrees, float radius, Point origin)
        {
            PointF xy = new PointF();
            double radians = degrees * Math.PI / 180.0;

            xy.X = (float)Math.Sin(radians) * radius + origin.X;
            xy.Y = origin.Y - (float)Math.Cos(radians) * radius;

            return xy;
        }

        private float XYToDegrees(Point xy, Point origin)
        {
            double angle = 0.0;

            //find quadrant
            //2   |    1
            // ---x----
            //3   |    4


            if (xy.X > origin.X && xy.Y == origin.Y)
            {
                angle = 90;
            }
            else if (xy.X < origin.X && xy.Y == origin.Y)
            {
                angle = -90;
            }
            else if (xy.X == origin.X && xy.Y <= origin.Y)
            {
                angle = 0;
            }
            else if (xy.X == origin.X && xy.Y > origin.Y)
            {
                angle = 180;
            }
            else if (xy.X > origin.X && xy.Y < origin.Y)
            {
                //1
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = 90 - angle;
            }
            else if (xy.X < origin.X && xy.Y < origin.Y)
            {
                //2
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = -(90 - angle);

            }
            else if (xy.X < origin.X && xy.Y > origin.Y)
            {
                //3
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = -(90 + angle);


            }
            else if (xy.X > origin.X && xy.Y > origin.Y)
            {
                //4
                angle = Math.Atan(Math.Abs((double)xy.Y - (double)origin.Y) / Math.Abs((double)xy.X - (double)origin.X));
                angle = angle * 180.0 / Math.PI;
                angle = (90 + angle);

            }
            return (float)angle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen outline = new Pen(Color.FromArgb(86, 103, 141), 2.0f);
            SolidBrush fill = new SolidBrush(Color.FromArgb(90, 255, 255, 255));

            PointF anglePoint = DegreesToXY(mAngle, origin.X - 2, origin);
            Rectangle originSquare = new Rectangle(origin.X - 1, origin.Y - 1, 3, 3);

            //Draw
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(outline, drawRegion);
            g.FillEllipse(fill, drawRegion);
            g.DrawLine(Pens.Black, origin, anglePoint);

            PointF arrowPoint = DegreesToXY(mAngle + 30, origin.X / 4, origin);
            g.DrawLine(Pens.Black, origin, arrowPoint);
            arrowPoint = DegreesToXY(mAngle - 30, origin.X / 4, origin);
            g.DrawLine(Pens.Black, origin, arrowPoint);

            //g.SmoothingMode = SmoothingMode.HighSpeed; //Make the square edges sharp
            g.FillRectangle(Brushes.Black, originSquare);

            fill.Dispose();
            outline.Dispose();

            base.OnPaint(e);
        }

        private void AngleSelector_MouseDown(object sender, MouseEventArgs e)
        {
            int thisAngle = findNearestAngle(new Point(e.X, e.Y));

            if (thisAngle != -1)
            {
                this.Angle = thisAngle;
                this.Refresh();
            }
        }

        private void AngleSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                int thisAngle = findNearestAngle(new Point(e.X, e.Y));

                if (thisAngle != -1)
                {
                    this.Angle = thisAngle;
                    this.Refresh();
                }
            }
        }

        private int findNearestAngle(Point mouseXY)
        {
            int thisAngle = (int)XYToDegrees(mouseXY, origin);
            return thisAngle;
        }
    }
}

