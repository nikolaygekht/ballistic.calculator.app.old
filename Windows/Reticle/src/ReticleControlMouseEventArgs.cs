using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.Reticle
{
    public class ReticleControlMouseEventArgs : MouseEventArgs
    {
        private Angle mHold, mWindage;

        public Angle Hold
        {
            get
            {
                return mHold;
            }
        }

        public Angle Windage
        {
            get
            {
                return mWindage;
            }
        }

        public ReticleControlMouseEventArgs(Angle hold, Angle windage, MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            mHold = hold;
            mWindage = windage;
        }
    }

}
