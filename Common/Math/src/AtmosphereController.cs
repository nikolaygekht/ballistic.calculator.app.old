using System;
using System.Globalization;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic
{
    public static class AtmosphereInfoController
    {
        public static class Serialization
        {
            static public void Write(ISerializationObject parent, AtmosphereInfo atm)
            {
                ISerializationObject node = parent.Children.Add("atmosphere");
                UnitSerialization.WriteDistance(node, "altitude", atm.Altitude);
                UnitSerialization.WritePressure(node, "pressure", atm.Pressure);
                UnitSerialization.WriteTemperature(node, "temperature", atm.Temperature);
                UnitSerialization.WriteDouble(node, "humidty", atm.Humidity);
            }

            static public AtmosphereInfo Read(ISerializationObject node)
            {
                AtmosphereInfo atm = new AtmosphereInfo();
                atm.Altitude = UnitSerialization.ReadDistance(node, "altitude");
                atm.Pressure = UnitSerialization.ReadPressure(node, "pressure");
                atm.Temperature = UnitSerialization.ReadTemperature(node, "temperature");
                atm.Humidity = UnitSerialization.ReadDouble(node, "humidity");
                return atm;
            }

            static public AtmosphereInfo FindAndRead(ISerializationObject parent)
            {
                foreach (ISerializationObject node in parent.Children)
                    if (node.Name == "atmosphere")
                        return Read(node);
                return null;
            }
        }
    }
}