using System;
using System.IO;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic
{
    public static class WindInfoController
    {
        public static class Serialization
        {
            public static void Write(ISerializationContainer doc, ISerializationObject parent, WindInfo windInfo)
            {
                ISerializationObject node;
                if (parent == null)
                    node = doc.CreateRoot("wind-info");
                else
                    node = parent.Children.Add("wind-info");
                UnitSerialization.WriteVelocity(node, "speed", windInfo.Speed);
                UnitSerialization.WriteAngle(node, "direction", windInfo.Direction);
            }

            public static WindInfo Read(ISerializationObject node)
            {
                WindInfo windInfo = new WindInfo();
                windInfo.Speed = UnitSerialization.ReadVelocity(node, "speed");
                windInfo.Direction = UnitSerialization.ReadAngle(node, "direction");
                return windInfo;
            }

            public static WindInfo FindAndRead(ISerializationObject parent)
            {
                foreach (ISerializationObject node in parent.Children)
                    if (node.Name == "wind-info")
                        return Read(node);
                return null;
            }
        }
    }
}