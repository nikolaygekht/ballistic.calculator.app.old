using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic
{
    public static class ShotInfoController
    {
        public static class Calculation
        {
            public static bool calculateZero(ShotInfo zeroInfo)
            {
                if (!canCalculate(zeroInfo))
                    return false;


                Atmo atm = null;

                if (zeroInfo.Atmosphere != null)
                    atm = new Atmo(zeroInfo.Atmosphere.Temperature.Get(Temperature.Unit.Fahrenheit),
                                     zeroInfo.Atmosphere.Pressure.Get(Pressure.Unit.InchHg),
                                     zeroInfo.Atmosphere.Humidity,
                                     zeroInfo.Atmosphere.Altitude.Get(Distance.Unit.Foot));
                else
                    atm = new Atmo();

                Bullet bullet = new Bullet(zeroInfo.Ammo.BallisticCoefficient, zeroInfo.Ammo.Table);
                Rifle rifle = new Rifle(zeroInfo.Ammo.MuzzleVelocity.Get(Velocity.Unit.FeetPerSecond), zeroInfo.SightHeight.Get(Distance.Unit.Foot));

                Wind wind = null;
                if (zeroInfo.Wind != null)
                    wind = new Wind(zeroInfo.Wind.Speed.Get(Velocity.Unit.FeetPerSecond), zeroInfo.Wind.Direction.Get(Angle.Unit.Radian));

                Shot shot = new Shot(zeroInfo.ShotAngle != null ? zeroInfo.ShotAngle.Get(Angle.Unit.Radian) : 0, zeroInfo.CantAngle != null ? zeroInfo.CantAngle.Get(Angle.Unit.Radian) : 0, 0);

                double zero = zeroInfo.ZeroDistance.Get(Distance.Unit.Foot);

                JbmCalculator.calculate(bullet, rifle, atm, shot, wind, zero, zeroInfo.NearZero, 0, 1, JbmCalculator.CalculateTarget.ElevationAngle);

                zeroInfo.ElevationAngle = new Angle(shot.Elevation, Angle.Unit.Radian);

                return true;
            }

            public static BallisticInfoCollection calculateShot(ShotInfo shotInfo)
            {
                if (!canCalculate(shotInfo))
                    return null;

                Atmo atm = null;

                if (shotInfo.Atmosphere != null)
                    atm = new Atmo(shotInfo.Atmosphere.Temperature.Get(Temperature.Unit.Fahrenheit),
                                     shotInfo.Atmosphere.Pressure.Get(Pressure.Unit.InchHg),
                                     shotInfo.Atmosphere.Humidity,
                                     shotInfo.Atmosphere.Altitude.Get(Distance.Unit.Foot));
                else
                    atm = new Atmo();

                Bullet bullet = new Bullet(shotInfo.Ammo.BallisticCoefficient, shotInfo.Ammo.Table);
                Rifle rifle = new Rifle(shotInfo.Ammo.MuzzleVelocity.Get(Velocity.Unit.FeetPerSecond), shotInfo.SightHeight.Get(Distance.Unit.Foot));

                Wind wind = null;
                if (shotInfo.Wind != null)
                    wind = new Wind(shotInfo.Wind.Speed.Get(Velocity.Unit.FeetPerSecond), shotInfo.Wind.Direction.Get(Angle.Unit.Radian));

                double elevationAngle = shotInfo.ElevationAngle != null ? shotInfo.ElevationAngle.Get(Angle.Unit.Radian) : 0;
                if (shotInfo.Clicks != 0 && shotInfo.VerticalClick != null)
                    elevationAngle += shotInfo.Clicks * shotInfo.VerticalClick.Get(Angle.Unit.Radian);

                Shot shot = new Shot(shotInfo.ShotAngle != null ? shotInfo.ShotAngle.Get(Angle.Unit.Radian) : 0, shotInfo.CantAngle != null ? shotInfo.CantAngle.Get(Angle.Unit.Radian) : 0, elevationAngle);

                RangeData[] ballistic = JbmCalculator.calculate(bullet, rifle, atm, shot, wind, 0, false, shotInfo.MaxDistance.Get(Distance.Unit.Foot), shotInfo.Step.Get(Distance.Unit.Foot), JbmCalculator.CalculateTarget.Range);

                if (ballistic == null)
                    return null;

                BallisticInfoCollection result = new BallisticInfoCollection();
                result.Reserve(ballistic.Length);

                double bulletWeightGr = shotInfo.Ammo.BulletWeight.Get(Weight.Unit.Grain);
                double stablityCoefficient = 0;
                if (shotInfo.DriftInfo != null)
                    stablityCoefficient = DriftInfoController.Calculation.calculateStabilityCoefficient(shotInfo.Ammo, shotInfo.DriftInfo, shotInfo.Atmosphere);

                for (int i = 0; i < ballistic.Length && ballistic[i] != null; i++)
                {
                    double v = ballistic[i].Velocity;
                    double wi = ballistic[i].Windage;
                    double si = 0;
                    if (shotInfo.DriftInfo != null)
                        si = DriftInfoController.Calculation.calculateDrift(shotInfo.DriftInfo, stablityCoefficient, ballistic[i].Time).Get(Distance.Unit.Foot);
                    wi += si;


                    Distance range = new Distance(ballistic[i].Range, Distance.Unit.Foot);
                    Distance path = new Distance(ballistic[i].Drop, Distance.Unit.Foot);
                    Angle hold = new Angle(Math.Atan(ballistic[i].Drop / ballistic[i].Range), Angle.Unit.Radian);
                    Velocity velocity = new Velocity(v, Velocity.Unit.FeetPerSecond);
                    Distance windage = new Distance(wi, Distance.Unit.Foot);
                    Angle windageCorrection = new Angle(Math.Atan(wi / ballistic[i].Range), Angle.Unit.Radian);
                    Weight ogv = new Weight(Math.Pow(v, 3) * Math.Pow(bulletWeightGr, 2) * 1.5e-12, Weight.Unit.Pound);
                    Energy energy = new Energy(bulletWeightGr * v * v / 450400, Energy.Unit.FootPounds);
                    int holdClicks = 0, windageClicks = 0;
                    if (shotInfo.HorizonalClick != null && shotInfo.HorizonalClick.Get(Angle.Unit.Mil) != 0)
                        windageClicks = (int)Math.Round(windageCorrection.Get(Angle.Unit.Mil) / shotInfo.HorizonalClick.Get(Angle.Unit.Mil));
                    if (shotInfo.VerticalClick != null && shotInfo.VerticalClick.Get(Angle.Unit.Mil) != 0)
                        holdClicks = (int)Math.Round(hold.Get(Angle.Unit.Mil) / shotInfo.VerticalClick.Get(Angle.Unit.Mil));

                    BallisticInfo ballisticInfo = new BallisticInfo(range, path, hold, TimeSpan.FromSeconds(ballistic[i].Time), windage, windageCorrection, velocity, ballistic[i].Mach, energy, ogv, holdClicks, windageClicks);
                    result.Add(ballisticInfo);
                }
                return result;
            }

            public static BallisticInfoCollection calculateDangerZone(ShotInfo shotInfo)
            {
                if (!canCalculate(shotInfo))
                    return null;


                Atmo atm = null;

                if (shotInfo.Atmosphere != null)
                    atm = new Atmo(shotInfo.Atmosphere.Temperature.Get(Temperature.Unit.Fahrenheit),
                                   shotInfo.Atmosphere.Pressure.Get(Pressure.Unit.InchHg),
                                   shotInfo.Atmosphere.Humidity,
                                   shotInfo.Atmosphere.Altitude.Get(Distance.Unit.Foot));
                else
                    atm = new Atmo();

                Bullet bullet = new Bullet(shotInfo.Ammo.BallisticCoefficient, shotInfo.Ammo.Table);
                Rifle rifle = new Rifle(shotInfo.Ammo.MuzzleVelocity.Get(Velocity.Unit.FeetPerSecond), shotInfo.SightHeight.Get(Distance.Unit.Foot));

                Wind wind = null;

                Shot shot = new Shot(shotInfo.ShotAngle != null ? shotInfo.ShotAngle.Get(Angle.Unit.Radian) : 0, shotInfo.CantAngle != null ? shotInfo.CantAngle.Get(Angle.Unit.Radian) : 0, 0);

                double zero = shotInfo.ZeroDistance.Get(Distance.Unit.Foot);
                double targetSize = shotInfo.TargetSize.Get(Distance.Unit.Foot);
                
                RangeData[] ballistic = JbmCalculator.calculate(bullet, rifle, atm, shot, wind, zero, shotInfo.NearZero, targetSize, 0, JbmCalculator.CalculateTarget.DangerZone);
                
                if (ballistic == null)
                    return null;

                BallisticInfoCollection result = new BallisticInfoCollection();
                result.Reserve(ballistic.Length);

                double bulletWeightGr = shotInfo.Ammo.BulletWeight.Get(Weight.Unit.Grain);
                double stablityCoefficient = 0;
                if (shotInfo.DriftInfo != null)
                    stablityCoefficient = DriftInfoController.Calculation.calculateStabilityCoefficient(shotInfo.Ammo, shotInfo.DriftInfo, shotInfo.Atmosphere);

                for (int i = 0; i < ballistic.Length && ballistic[i] != null; i++)
                {
                    double v = ballistic[i].Velocity;
                    double wi = ballistic[i].Windage;
                    double si = 0;
                    if (shotInfo.DriftInfo != null)
                        si = DriftInfoController.Calculation.calculateDrift(shotInfo.DriftInfo, stablityCoefficient, ballistic[i].Time).Get(Distance.Unit.Foot);
                    wi += si;


                    Distance range = new Distance(ballistic[i].Range, Distance.Unit.Foot);
                    Distance path = new Distance(ballistic[i].Drop, Distance.Unit.Foot);
                    Angle hold = new Angle(Math.Atan(ballistic[i].Drop / ballistic[i].Range), Angle.Unit.Radian);
                    Velocity velocity = new Velocity(v, Velocity.Unit.FeetPerSecond);
                    Distance windage = new Distance(wi, Distance.Unit.Foot);
                    Angle windageCorrection = new Angle(Math.Atan(wi / ballistic[i].Range), Angle.Unit.Radian);
                    Weight ogv = new Weight(Math.Pow(v, 3) * Math.Pow(bulletWeightGr, 2) * 1.5e-12, Weight.Unit.Pound);
                    Energy energy = new Energy(bulletWeightGr * v * v / 450400, Energy.Unit.FootPounds);
                    int holdClicks = 0, windageClicks = 0;
                    if (shotInfo.HorizonalClick != null && shotInfo.HorizonalClick.Get(Angle.Unit.Mil) != 0)
                        windageClicks = (int)Math.Round(windageCorrection.Get(Angle.Unit.Mil) / shotInfo.HorizonalClick.Get(Angle.Unit.Mil));
                    if (shotInfo.VerticalClick != null && shotInfo.VerticalClick.Get(Angle.Unit.Mil) != 0)
                        holdClicks = (int)Math.Round(hold.Get(Angle.Unit.Mil) / shotInfo.VerticalClick.Get(Angle.Unit.Mil));

                    BallisticInfo ballisticInfo = new BallisticInfo(range, path, hold, TimeSpan.FromSeconds(ballistic[i].Time), windage, windageCorrection, velocity, ballistic[i].Mach, energy, ogv, holdClicks, windageClicks);
                    result.Add(ballisticInfo);
                }
                return result;
            }


            public static bool canCalculate(ShotInfo shot)
            {
                return shot != null &&
                       shot.Ammo != null &&
                       shot.SightHeight != null;
            }
        }

        public static class Serialization
        {
            public static void Write(ISerializationContainer doc, ISerializationObject parent, ShotInfo info)
            {
                ISerializationObject node;
                if (parent == null)
                    node = doc.CreateRoot("shot-info");
                else
                    node = parent.Children.Add("shot-info");
                UnitSerialization.WriteString(node, "name", info.Name);
                UnitSerialization.WriteAngle(node, "shot-angle", info.ShotAngle);
                UnitSerialization.WriteAngle(node, "cant-angle", info.CantAngle);
                UnitSerialization.WriteAngle(node, "elevation-angle", info.ElevationAngle);
                UnitSerialization.WriteDistance(node, "max-distance", info.MaxDistance);
                UnitSerialization.WriteDistance(node, "sight-height", info.SightHeight);
                UnitSerialization.WriteDistance(node, "zero-distance", info.ZeroDistance);
                UnitSerialization.WriteDistance(node, "step", info.Step);
                if (info.Ammo != null)
                    AmmoInfoController.Serialization.Write(node, info.Ammo);

                if (info.Atmosphere != null)
                    AtmosphereInfoController.Serialization.Write(node, info.Atmosphere);

                if (info.Wind != null)
                    WindInfoController.Serialization.Write(doc, node, info.Wind);

                if (info.DriftInfo != null)
                    DriftInfoController.Serialization.Write(doc, node, info.DriftInfo);
            }

            public static ShotInfo Read(ISerializationObject node)
            {
                ShotInfo shot = new ShotInfo();
                shot.Name = UnitSerialization.ReadString(node, "name");
                shot.ShotAngle = UnitSerialization.ReadAngle(node, "shot-angle");
                shot.CantAngle = UnitSerialization.ReadAngle(node, "cant-angle");
                shot.ElevationAngle = UnitSerialization.ReadAngle(node, "elevation-angle");
                shot.MaxDistance = UnitSerialization.ReadDistance(node, "max-distance");
                shot.SightHeight = UnitSerialization.ReadDistance(node, "sight-height");
                shot.ZeroDistance = UnitSerialization.ReadDistance(node, "zero-distance");
                shot.Step = UnitSerialization.ReadDistance(node, "step");

                shot.Ammo = AmmoInfoController.Serialization.FindAndRead(node);
                shot.Atmosphere = AtmosphereInfoController.Serialization.FindAndRead(node);
                shot.Wind = WindInfoController.Serialization.FindAndRead(node);
                shot.DriftInfo = DriftInfoController.Serialization.FindAndRead(node);
                return shot;
            }

            public static void WriteFile(string name, ShotInfo info)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).NewContainer();
                Write(doc, null, info);
                SerializerFactory.getSerializer(SerializationType.Xml).WriteToFile(doc, name);
            }

            public static ShotInfo ReadFile(string name)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromFile(name);
                return Read(doc.Root);
            }
        }
    }
}