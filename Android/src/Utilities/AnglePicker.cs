using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Runtime;
using System;
using Gehtsoft.BallisticCalculator.Utils;

namespace Gehtsoft.BallisticCalculator.Views
{
    [Register("ballisticcalculator.views.AnglePicker")]
    class AnglePicker : Android.Views.View
    {
        public class AngleEventArgs : EventArgs
        {
            public AngleEventArgs(int angle)
            {
                Angle = angle;
            }

            public int Angle { get; private set; }
        }

        public AnglePicker(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            _circlePaint.Color = Color.White;
            _circlePaint.SetStyle(Paint.Style.Fill);
            _circlePaint.Flags |= PaintFlags.AntiAlias;

            _borderPaint.Color = Color.Black;
            _borderPaint.SetStyle(Paint.Style.Stroke);
            _borderPaint.Flags |= PaintFlags.AntiAlias;

            _linePaint.Color = Color.Black;
            _linePaint.SetStyle(Paint.Style.Stroke);
            _linePaint.Flags |= PaintFlags.AntiAlias;
            _linePaint.StrokeWidth = 10;
            _linePaint.StrokeCap = Paint.Cap.Round;
        }

        private float _diameter = 0f;

        private Paint _circlePaint = new Paint();
        public Paint CirclePaint
        {
            set
            {
                if (_circlePaint != value)
                {
                    _circlePaint = value;
                    Invalidate();
                }
            }
        }

        private Paint _linePaint = new Paint();
        public Paint LinePaint
        {
            set
            {
                if (_linePaint != value)
                {
                    _linePaint = value;
                    Invalidate();
                }
            }
        }

        private Paint _borderPaint = new Paint();
        public Paint BorderPaint
        {
            set
            {
                if (_borderPaint != value)
                {
                    _borderPaint = value;
                    Invalidate();
                }
            }
        }

        private int _angle;
        public int Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                if (_angle != value)
                {
                    _angle = value;

                    if (_angle > 180)
                    {
                        _angle = _angle % 360;
                        if (_angle > 180)
                            _angle = _angle - 360;
                    }
                    else if (_angle < -180)
                    {
                        _angle = -_angle % 360;
                        if (_angle > 180)
                            _angle = -(_angle - 360);

                    }
                    if (AngleChanged != null)
                        AngleChanged(this, new AngleEventArgs(_angle));
                    Invalidate();
                }
            }
        }

        public event EventHandler<AngleEventArgs> AngleChanged;

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            Point centerPoint = new Point(Width / 2, Height / 2);
            PointF borderPoint = Utilities.DegreesToXY(_angle, _diameter / 2, centerPoint);

            PointF arrowLeftPoint = Utilities.DegreesToXY(_angle - 45, _diameter / 10, centerPoint);
            PointF arrowRightPoint = Utilities.DegreesToXY(_angle + 45, _diameter / 10, centerPoint);

            canvas.DrawCircle(centerPoint.X, centerPoint.Y, _diameter / 2, _circlePaint);
            canvas.DrawCircle(centerPoint.X, centerPoint.Y, _diameter / 2, _borderPaint);
            canvas.DrawPoint(centerPoint.X, centerPoint.Y, _linePaint);
            canvas.DrawLine(centerPoint.X, centerPoint.Y, borderPoint.X, borderPoint.Y, _linePaint);
            canvas.DrawLine(centerPoint.X, centerPoint.Y, arrowLeftPoint.X, arrowLeftPoint.Y, _linePaint);
            canvas.DrawPoint(arrowLeftPoint.X, arrowLeftPoint.Y, _linePaint);
            canvas.DrawLine(centerPoint.X, centerPoint.Y, arrowRightPoint.X, arrowRightPoint.Y, _linePaint);
            canvas.DrawPoint(arrowRightPoint.X, arrowRightPoint.Y, _linePaint);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            float x_padding = PaddingLeft + PaddingRight;
            float y_padding = PaddingTop + PaddingBottom;

            float ww = w - x_padding;
            float hh = h - y_padding;

            _diameter = Math.Min(ww, hh);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            int minw = PaddingLeft + PaddingRight + SuggestedMinimumWidth;
            int w = ResolveSizeAndState(minw, widthMeasureSpec, 1);
            int h = ResolveSizeAndState(minw, heightMeasureSpec, 1);

            SetMeasuredDimension(w, h);
        }

        private bool _dragging = false;

        public override bool OnTouchEvent(MotionEvent e)
        {
            Point touch = new Point((int)e.GetX(), (int)e.GetY());
            Point center = new Point(Width / 2, Height / 2);

            switch (e.Action)
            {
                case MotionEventActions.Down:

                    if (Utilities.PointIsInCircle(touch, center, _diameter / 2) == true)
                    {
                        Angle = (int)Utilities.XYToDegrees(touch, center);
                        _dragging = true;
                        return true;
                    }
                    else
                        return base.OnTouchEvent(e);
                case MotionEventActions.Up:
                    if (_dragging)
                    {
                        _dragging = false;
                        return true;
                    }
                    else
                        return base.OnTouchEvent(e);
                case MotionEventActions.Move:
                    if (_dragging)
                    {
                        Parent.RequestDisallowInterceptTouchEvent(true);
                        Angle = (int)Utilities.XYToDegrees(touch, center);
                        return true;
                    }
                    else
                        return base.OnTouchEvent(e);
                default:
                    return base.OnTouchEvent(e);
            }
        }
    }
}