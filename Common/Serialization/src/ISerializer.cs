using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.Serialization
{
    public enum SerializationType
    {
        Xml,
    }

    public interface ISerializer
    {
        SerializationType SerializationType { get; }
        string Name { get; }
        string WriteToString(ISerializationContainer container);
        void WriteToFile(ISerializationContainer container, string name);
        ISerializationContainer ReadFromString(string content);
        ISerializationContainer ReadFromFile(string filename);
        ISerializationContainer NewContainer();
    }
}
