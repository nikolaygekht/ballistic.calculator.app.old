using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.Serialization
{
    public static class SerializerFactory
    {
        private static Dictionary<SerializationType, ISerializer> mSerializers = new Dictionary<SerializationType, ISerializer>();

        public static ISerializer getSerializer(SerializationType type)
        {
            ISerializer serializer;
            if (!mSerializers.TryGetValue(type, out serializer))
                serializer = null;
            return serializer;
        }

        public static void registerSerialization(ISerializer serializer)
        {
            mSerializers[serializer.SerializationType] = serializer;
        }
    }
}
