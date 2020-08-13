using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.JBM
{
    class Vector
    {
        private double mX, mY, mZ;

        public double X
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

        public double Y
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

        public double Z
        {
            get
            {
                return mZ;
            }
            set
            {
                mZ = value;
            }
        }

        public Vector()
        {
            mX = mY = mZ = 0;
        }

        public Vector(double x, double y, double z)
        {
            mX = x;
            mY = y;
            mZ = z;
        }

        public static double DotX(Vector u, Vector v)
        {
            return u.X * v.X;
        }

        public static double DotY(Vector u, Vector v)
        {
            return u.Y * v.Y;
        }

        public static double DotZ(Vector u, Vector v)
        {
            return u.Z * v.Z;
        }

        public static double Dot(Vector u, Vector v)
        {
            return DotX(u, v) + DotY(u, v) + DotZ(u, v);
        }

        public double Modulus
        {
            get
            {
                return Dot(this, this);
            }
        }

        public double Length
        {
            get
            {
                return System.Math.Sqrt(Modulus);
            }
        }

        static public Vector Add(Vector u, Vector v)
        {
            return new Vector((u.X) + (v.X), (u.Y) + (v.Y), (u.Z) + (v.Z));
        }

        static public Vector Multiply(double a, Vector v)
        {
            return new Vector((a) * (v.X), (a) * (v.Y), (a) * (v.Z));
        }

        static public Vector Subtract(Vector u, Vector v)
        {
            return new Vector((u.X) - (v.X), (u.Y) - (v.Y), (u.Z) - (v.Z));
        }

        public Vector Reverse()
        {
            return new Vector(-this.X, -this.Y, -this.Z);
        }

        static public double Distance(Vector u, Vector v)
        {
            return Subtract(u, v).Length;
        }

        public Vector Normalize()
        {
            double l = Length;
            if (l > 0)
                return Vector.Multiply(1.0 / l, this);
            else
                return new Vector();
        }
    }
}
