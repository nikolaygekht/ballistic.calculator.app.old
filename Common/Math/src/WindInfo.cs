using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public class WindInfo
    {
        private Angle mDirection;

        public Angle Direction
        {
            get
            {
                return mDirection;
            }
            set
            {
                mDirection = value;
            }
        }

        private Velocity mSpeed;

        public Velocity Speed
        {
            get
            {
                return mSpeed;
            }
            set
            {
                mSpeed = value;
            }
        }

        internal WindInfo()
        {
        }

        public WindInfo(Angle direction, Velocity speed)
        {
            mDirection = direction;
            mSpeed = speed;
        }

        public bool IsComplete()
        {
            return mDirection != null && mSpeed != null;
        }

        override public string ToString()
        {
            StringBuilder b = new StringBuilder();
            if (Direction != null)
            {
                b.Append("Direction=");
                b.Append(Direction.ToString(Direction.SetUnit));
                b.AppendLine();
            }
            if (Speed != null)
            {
                b.Append("Speed=");
                b.Append(Speed.ToString(Speed.SetUnit));
                b.AppendLine();
            }
            return b.ToString();
        }
    }


}