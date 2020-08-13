using System;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic
{
    public static class DriftInfoController
    {
        public static class Calculation
        {
            public static double calculateStabilityCoefficient(AmmoInfo ammoInfo, DriftInfo driftInfo, AtmosphereInfo atmosphere)
            {

                double w = ammoInfo.BulletWeight.Get(Weight.Unit.Grain);
                double t = driftInfo.RiflingTwist.Get(Distance.Unit.Inch) / driftInfo.BulletDiameter.Get(Distance.Unit.Inch);
                double d = driftInfo.BulletDiameter.Get(Distance.Unit.Inch);
                double l = driftInfo.BulletLength.Get(Distance.Unit.Inch) / driftInfo.BulletDiameter.Get(Distance.Unit.Inch);


                double sd = 30 * w / (Math.Pow(t, 2) * Math.Pow(d, 3) * l * (1 + Math.Pow(l, 2)));


                //correct for muzzle velocity
                double fv = Math.Pow(ammoInfo.MuzzleVelocity.Get(Velocity.Unit.FeetPerSecond) / 2800, 1.0 / 3.0);


                double ftp = 1;

                if (atmosphere != null)
                {
                    double ft = atmosphere.Temperature.Get(Temperature.Unit.Fahrenheit);
                    double pt = atmosphere.Pressure.Get(Pressure.Unit.InchHg);
                    ftp = ((ft + 460) / (59 + 460)) * (29.92 / pt);
                }


                return sd * fv * ftp;
            }

            public static Distance calculateDrift(DriftInfo driftInfo, double stabilityCoefficient, double timeOfFly)
            {
                return new Distance(1.25 * (stabilityCoefficient + 1.2) * Math.Pow(timeOfFly, 1.83) * (driftInfo.RightHandTwist ? -1 : 1) , Distance.Unit.Inch);
            }
        }
        public static class Serialization
        {
                public static void Write(ISerializationContainer doc, ISerializationObject parent, DriftInfo driftInfo)
                {
                    ISerializationObject node;
                    if (parent == null)
                        node = doc.CreateRoot("drift-info");
                    else
                        node = parent.Children.Add("drift-info");
                    UnitSerialization.WriteDistance(node, "bullet-length", driftInfo.BulletLength);
                    UnitSerialization.WriteDistance(node, "bullet-diameter", driftInfo.BulletDiameter);
                    UnitSerialization.WriteDistance(node, "rifling-twist", driftInfo.RiflingTwist);
                    UnitSerialization.WriteBool(node, "right-hand-twist", driftInfo.RightHandTwist);
                }

                public static DriftInfo Read(ISerializationObject node)
                {
                    DriftInfo driftInfo = new DriftInfo();
                    driftInfo.BulletLength = UnitSerialization.ReadDistance(node, "bullet-length");
                    driftInfo.BulletDiameter = UnitSerialization.ReadDistance(node, "bullet-diameter");
                    driftInfo.RiflingTwist = UnitSerialization.ReadDistance(node, "rifling-twist");
                    driftInfo.RightHandTwist = UnitSerialization.ReadBool(node, "right-hand-twist");
                    return driftInfo;
                }

                public static DriftInfo FindAndRead(ISerializationObject parent)
                {
                    foreach (ISerializationObject node in parent.Children)
                        if (node.Name == "drift-info")
                            return Read(node);
                    return null;
                }

        }
    }

}