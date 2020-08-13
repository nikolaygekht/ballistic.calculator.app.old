using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Reticle
{
    public class Reticle
    {
        public delegate void ReticleImageChangedDelegate(Reticle reticle);
        public event ReticleImageChangedDelegate OnReticleImageChanged;
        public delegate void ReticleImageTypeChangedDelegate(Reticle reticle);
        public event ReticleImageChangedDelegate OnReticleImageTypeChanged;
        public delegate void ReticleCalibrationChangedDelegate(Reticle reticle);
        public event ReticleCalibrationChangedDelegate OnReticleCalibrationChanged;

        private string mName;

        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }

        }

        private Image mImage;

        public Image Image
        {
            get
            {
                return mImage;
            }
            set
            {
                mImage = value;
                if (OnReticleImageChanged != null)
                    OnReticleImageChanged(this);
            }
        }

        private bool mRaster;

        public bool Raster
        {
            get
            {
                return mRaster;
            }
            set
            {
                mRaster = value;
                if (OnReticleImageTypeChanged != null)
                    OnReticleImageTypeChanged(this);

            }
        }

        public int Width
        {
            get
            {
                if (mImage == null)
                    return 0;
                if (mRaster)
                    return mImage.Width;
                else
                    return mImage.Width;
            }
        }

        public int Height
        {
            get
            {
                if (mImage == null)
                    return 0;
                if (mRaster)
                    return mImage.Height;
                else
                    return mImage.Height;
            }
        }

        private int mZX = -1, mZY = -1;

        public int ZeroX
        {
            get
            {
                return mZX;
            }
            set
            {
                mZX = value;
            }
        }

        public int ZeroY
        {
            get
            {
                return mZY;
            }
            set
            {
                mZY = value;
            }
        }

        private int mCalibrationX1 = -1, mCalibrationY1 = -1;
        private int mCalibrationX2 = -1, mCalibrationY2 = -1;
        private Angle mCalibrationAngle;

        public int CalibrationX1
        {
            get
            {
                return mCalibrationX1;
            }
            set
            {
                mCalibrationX1 = value;
                if (OnReticleCalibrationChanged != null)
                    OnReticleCalibrationChanged(this);
            }
        }

        public int CalibrationY1
        {
            get
            {
                return mCalibrationY1;
            }
            set
            {
                mCalibrationY1 = value;
                if (OnReticleCalibrationChanged != null)
                    OnReticleCalibrationChanged(this);
            }
        }

        public int CalibrationX2
        {
            get
            {
                return mCalibrationX2;
            }
            set
            {
                mCalibrationX2 = value;
                if (OnReticleCalibrationChanged != null)
                    OnReticleCalibrationChanged(this);
            }
        }

        public int CalibrationY2
        {
            get
            {
                return mCalibrationY2;
            }
            set
            {
                mCalibrationY2 = value;
                if (OnReticleCalibrationChanged != null)
                    OnReticleCalibrationChanged(this);
            }
        }

        public Angle CalibrationAngle
        {
            get
            {
                return mCalibrationAngle;
            }
            set
            {
                mCalibrationAngle = value;
                if (OnReticleCalibrationChanged != null)
                    OnReticleCalibrationChanged(this);
            }
        }

        public class BDCPoint
        {
            private int mX, mY;

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

            public BDCPoint()
            {
                mX = mY = 0;
            }

            public BDCPoint(int x, int y)
            {
                mX = x;
                mY = y;
            }
        }

        public class BDCPointCollection : IEnumerable<BDCPoint>
        {
            private List<BDCPoint> mBDCs = new List<BDCPoint>();

            public int Count
            {
                get
                {
                    return mBDCs.Count;
                }
            }

            public BDCPoint this[int index]
            {
                get
                {
                    return mBDCs[index];
                }
                set
                {
                    mBDCs[index] = value;
                }
            }

            internal BDCPointCollection()
            {
            }

            public void Add(BDCPoint point)
            {
                mBDCs.Add(point);
            }

            public void RemoveAt(int index)
            {
                mBDCs.RemoveAt(index);
            }

            public void Clear()
            {
                mBDCs.Clear();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return mBDCs.GetEnumerator();
            }

            public IEnumerator<BDCPoint> GetEnumerator()
            {
                return mBDCs.GetEnumerator();
            }
        }

        private BDCPointCollection mBDCPoints = new BDCPointCollection();

        public BDCPointCollection BDCPoints
        {
            get
            {
                return mBDCPoints;
            }
        }

        private byte[] mImageSource;

        public byte[] ImageSource
        {
            get
            {
                return mImageSource;
            }
            set
            {
                mImageSource = value;
            }
        }

        public Reticle()
        {
        }
    }
}
