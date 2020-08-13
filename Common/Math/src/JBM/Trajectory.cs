using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.JBM
{
    public class Bullet
    {
        private BallisticCoefficitent mBallisticCoefficient;
        public BallisticCoefficitent BallisticCoefficient
        {
            get
            {
                return mBallisticCoefficient;
            }
        }

        public Bullet(double bc, DragTable table)
        {
            mBallisticCoefficient = new BallisticCoefficitent(bc, table);
        }
    }

    public class Rifle
    {
        private double mVelocity;
        /** Muzzle velocity in fps. */
        public double Velocity
        {
            get
            {
                return mVelocity;
            }
        }

        private double mSightHeight;
        /** Muzzle velocity in feet. */
        public double SightHeight
        {
            get
            {
                return mSightHeight;
            }
        }

        public Rifle(double velocity, double sightHeight)
        {
            mVelocity = velocity;
            mSightHeight = sightHeight;
        }
    }

    public class Wind
    {
        private double mVelocity;
        /** Wind velocity in fps. */
        public double Velocity
        {
            get
            {
                return mVelocity;
            }
        }

        private double mDirection;
        /** Wind velocity in radians, 0 is straight from front. */
        public double Direction
        {
            get
            {
                return mDirection;
            }
        }

        public Wind(double velocity, double direction)
        {
            mVelocity = velocity;
            mDirection = direction;
        }
    }

    public class Shot
    {
        private double mLineOfSightAngle;
        /** Angle between line of sight and surface in radians. */
        public double LineOfSightAngle
        {
            get
            {
                return mLineOfSightAngle;
            }
        }

        /** Angle between the line connecting center of sight and center of bore and
         * the normal to the surface in radians. */
        private double mCantAngle;
        public double CantAngle
        {
            get
            {
                return mCantAngle;
            }
        }

        private double mElevation;
        /** Angle of the elevation of the barrel, in radians. */
        public double Elevation
        {
            get
            {
                return mElevation;
            }
            set
            {
                mElevation = value;
            }
        }

        public Shot(double los, double cant, double elevation)
        {
            mLineOfSightAngle = los;
            mCantAngle = cant;
            mElevation = elevation;
        }
    }

    public class RangeData
    {
        private double mTime, mRange, mVelocity, mDrop, mWindage, mMach;

        /** Time in flight, in seconds. */
        public double Time
        {
            get
            {
                return mTime;
            }
        }

        /** Range in feet. */
        public double Range
        {
            get
            {
                return mRange;
            }
        }

        /** Velocity in ft/s. */
        public double Velocity
        {
            get
            {
                return mVelocity;
            }
        }

        /** Velocity in ft/s. */
        public double Mach
        {
            get
            {
                return mMach;
            }
        }

        /** Drop in ft. */
        public double Drop
        {
            get
            {
                return mDrop;
            }
        }

        /** Windage in ft. */
        public double Windage
        {
            get
            {
                return mWindage;
            }
        }

        internal RangeData(double time, double range, double velocity, double mach, double drop, double windage)
        {
            mTime = time;
            mRange = range;
            mVelocity = velocity;
            mMach = mach;
            mDrop = drop;
            mWindage = windage;
        }
    }


    public static class JbmCalculator
    {
        private const double TRAJ_ERROR = 0.02 / 12.0;
        private const double TRAJ_ABSMAXVEL = 5000.0;
        private const double TRAJ_ABSMINVX = 50.0;
        private const double TRAJ_ABSMINY = -199999.9 / 12.0;
        private const double TRAJ_MAXRANGE = 2000;
        private const int TRAJ_MAXITCNT = (10);

        private const double TRAJ_GM = -32.17;
        private static readonly Vector TRAJ_GRAVITY = new Vector(0, TRAJ_GM, 0);

        public enum CalculateTarget
        {
            ElevationAngle,
            ElvationAngleAndRange,
            Range,
            DangerZone,
        }

        /** all ranges are in feet.

         * zeroRange, rangeTo and step are in feet.
         * calculateElevation is true in case zeroRange is specified and the barrel elevation
         * shall be calculated and saved to shot.elevation for the zero range specified. If this flag is false, zeroRange is ignored and show.elevation
         * is used instead.
         *
         * calculateRanges is used in case RangeData[] array shall be generated.
          */
        public static RangeData[] calculate(Bullet bullet, Rifle rifle, Atmo atmo, Shot shot, Wind wind, double zeroRange, bool nearzero, double rangeToOrSize, double step, CalculateTarget target)
        {
            bool calculateElevation = (target == CalculateTarget.DangerZone || target == CalculateTarget.ElevationAngle || target == CalculateTarget.ElvationAngleAndRange);
            bool calculateRanges = (target == CalculateTarget.Range || target == CalculateTarget.ElvationAngleAndRange);
            bool calculateDangerZone = (target == CalculateTarget.DangerZone);
            double rangeTo = 0, halfTarget = 0;

            if (target == CalculateTarget.DangerZone)
            {
                rangeTo = 3000;
                if (rangeTo < zeroRange)
                    rangeTo = zeroRange * 2;
                step = 1;
                halfTarget = rangeToOrSize / 2;
            }
            else
                rangeTo = rangeToOrSize;

            double TRAJ_DX = step / 10;

            while (TRAJ_DX > 1)
                TRAJ_DX /= 10;


            if (atmo == null)
                atmo = new Atmo();

            if (wind == null)
                wind = new Wind(0, 0);

            Vector dr, r, tv, v, w, g, z;
            double mv, vm, azim, elev;
            double dt, eq, t, mach, drg, err, dy, dz;
            double k, n;
            int j, itcnt;

            /** Expected number of range items. */
            j = (int)((rangeTo + 1) / step) + 1;
            RangeData[] ranges = null;

            /** Position of the zero point. */
            z = new Vector(zeroRange, 0, 0);

            if (calculateRanges)
                ranges = new RangeData[j];
            else if (calculateDangerZone)
                ranges = new RangeData[2];

            /** Mach velocity for the current atmosphere. */
            mach = atmo.Mach;
            /** Atmospeshpere factor for adjusting drag. */
            eq = atmo.Density / Atmo.ATMOS_DENSSTD;

            /* correct the gravity vector according */
            g = getGravity(shot);

            /* get wind components as a vector of velocities. */
            w = correctWind(shot, wind);

            /** muzzle velocity. */
            mv = rifle.Velocity;

            /** Azimuth and elevation of the barrel vs the target. */
            azim = 0.0;
            if (calculateElevation)
                elev = 0;
            else
                elev = shot.Elevation;

            err = 0.0;      //error of zero finding
            itcnt = 0;      //number of iteration to find zero

            while (((err > TRAJ_ERROR) && (itcnt < TRAJ_MAXITCNT)) || (itcnt == 0))
            {
                vm = mv;
                t = 0.0;

                //initial position: x - distance towards target, y - drop and z - widage. */
                r = new Vector(0.0, -rifle.SightHeight, 0);

                //find out the components of velocity.
                v = Vector.Multiply(vm, new Vector(System.Math.Cos(elev) * System.Math.Cos(azim), System.Math.Sin(elev), System.Math.Cos(elev) * System.Math.Sin(azim)));

                j = 0;
                k = System.Math.Max((calculateRanges || calculateDangerZone) ? rangeTo + 1 : z.X + 1, calculateElevation ? z.X + 1 : 0);
                n = 0;

                bool inDangerZone = false, hitDuringDangerZone = false, dangerZoneFound = false;

                //run all the way down the range
                while (r.X <= k + TRAJ_DX)
                {
                    //if bullet too slow and fell too low - stop
                    if ((vm < TRAJ_ABSMINVX) || (r.Y < TRAJ_ABSMINY))
                    {
                        break;
                    }

                    //if calculate the range records and the next range record is reached
                    if (calculateRanges && r.X >= n)
                    {
                        //save range
                        ranges[j] = new RangeData(t, r.X, vm, vm / mach, r.Y, r.Z);
                        //and find out the next step
                        n += step;

                        j++;
                        if (j == ranges.Length)
                            break;
                    }

                    if (calculateDangerZone && !dangerZoneFound && r.Y <= halfTarget && r.Y >= -halfTarget)
                    {
                        if (!inDangerZone)
                        {
                            inDangerZone = true;
                            ranges[0] = new RangeData(t, r.X, vm, vm / mach, r.Y, r.Z);
                        }
                        else
                        {
                            if (System.Math.Abs(r.X - z.X) < 0.5 * TRAJ_DX)
                            {
                                hitDuringDangerZone = true;
                            }
                        }
                    }
                    else
                    {
                        if (inDangerZone)
                        {
                            if (hitDuringDangerZone)
                            {
                                ranges[1] = new RangeData(t, r.X, vm, vm / mach, r.Y, r.Z);
                                dangerZoneFound = true;
                            }
                            inDangerZone = false;
                        }
                    }

                    //calculate velocity change for 1/2 of a step.
                    dt = 0.5 * TRAJ_DX / v.X;
                    tv = Vector.Subtract(v, w);                                     //adjust wind
                    vm = tv.Length;
                    drg = eq * vm * bullet.BallisticCoefficient.getDrag(vm / mach); //find drag (change of velocity)
                    tv = Vector.Subtract(v, Vector.Multiply(dt, Vector.Subtract(Vector.Multiply(drg, tv), g))); //find new velocity

                    //calculate velocity change for another 1/2 of a step.
                    dt = TRAJ_DX / tv.X;
                    tv = Vector.Subtract(tv, w);
                    vm = tv.Length;
                    drg = eq * vm * bullet.BallisticCoefficient.getDrag(vm / mach);
                    v = Vector.Subtract(v, Vector.Multiply(dt, Vector.Subtract(Vector.Multiply(drg, tv), g)));

                    //calculate new position
                    dr = new Vector(TRAJ_DX, v.Y * dt, v.Z * dt);
                    r = Vector.Add(r, dr);
                    vm = v.Length;
                    //and time in fly
                    t = t + dr.Length / vm;

                    //if elevation is being calculate and we are at zero range
                    if (calculateElevation && (System.Math.Abs(r.X - z.X) < 0.5 * TRAJ_DX))
                    {
                        //find error and adjust the elevation angle for the next iteration.
                        dy = r.Y - z.Y;
                        dz = r.Z - z.Z;
                        err = System.Math.Abs(dy);
                        elev = elev - dy / r.X;
                        //no reason to finish calculation if error is too big, go to
                        //next iteration immeditelly.
                        if (err > TRAJ_ERROR) break;
                    }
                }
                if (inDangerZone && hitDuringDangerZone)
                    ranges[1] = new RangeData(t, r.X, vm, vm / mach, r.Y, r.Z);
                itcnt++;
            }
            if (calculateElevation)
                shot.Elevation = elev;
            return ranges;
        }

        private static Vector getGravity(Shot shot)
        {
            double cl, sl, cc, sc;
            cl = System.Math.Cos(shot.LineOfSightAngle);
            sl = System.Math.Sin(shot.LineOfSightAngle);
            cc = System.Math.Cos(shot.CantAngle);
            sc = System.Math.Sin(shot.CantAngle);

            return new Vector(TRAJ_GM * sl, TRAJ_GM * cl * cc, -TRAJ_GM * cl * sc);
        }

        private static Vector correctWind(Shot shot, Wind wind)
        {
            double cl, sl, cc, sc;
            cl = System.Math.Cos(shot.LineOfSightAngle);
            sl = System.Math.Sin(shot.LineOfSightAngle);
            cc = System.Math.Cos(shot.CantAngle);
            sc = System.Math.Sin(shot.CantAngle);

            double wx, wz, tmp;
            Vector w;

            /* range speed. */
            wx = wind.Velocity * System.Math.Cos(wind.Direction);
            /* cross speed. */
            wz = wind.Velocity * System.Math.Sin(wind.Direction);
            w = new Vector(wx, 0, wz);

            tmp = w.Y * cl - w.X * sl;
            return new Vector(w.X * cl + w.Y * sl, tmp * cc + w.Z * sc, w.Z * cc - tmp * sc);
        }
    }

    class Zero
    {
        /** Return Elevation for the specified zero. */
        public static double calculateElevation(Bullet bullet, Rifle rifle, Atmo atmo, double zeroDistance)
        {
            return 0;
        }
    }
}
