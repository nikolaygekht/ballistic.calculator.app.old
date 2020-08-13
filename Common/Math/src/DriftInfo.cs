using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;

namespace MathEx.ExternalBallistic
{
    public class DriftInfo
    {
        private Distance mBulletLength;

        public Distance BulletLength
        {
            get
            {
                return mBulletLength;
            }
            set
            {
                mBulletLength = value;
            }
        }


        private Distance mBulletDiameter;

        public Distance BulletDiameter
        {
            get
            {
                return mBulletDiameter;
            }
            set
            {
                mBulletDiameter = value;
            }
        }

        private Distance mRiflingTwist;

        public Distance RiflingTwist
        {
            get
            {
                return mRiflingTwist;
            }
            set
            {
                mRiflingTwist = value;
            }
        }

        private bool mRightHandTwist;

        public bool RightHandTwist
        {
            get
            {
                return mRightHandTwist;
            }
            set
            {
                mRightHandTwist = value;
            }
        }

        internal DriftInfo()
        {
        }

        public DriftInfo(Distance bulletLength, Distance bulletDiameter, Distance riflingTwist, bool rightHandTwist)
        {
            mBulletLength = bulletLength;
            mBulletDiameter = bulletDiameter;
            mRiflingTwist = riflingTwist;
            mRightHandTwist = rightHandTwist;
        }
    }
}