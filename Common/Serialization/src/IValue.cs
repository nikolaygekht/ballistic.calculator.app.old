using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.Serialization
{
    public interface ISerializationValue
    {
        string Name { get; }
        string Value { get; set; }
    }

    public interface ISerializationValueCollection : IEnumerable<ISerializationValue>
    {
        int Count { get; }
        ISerializationValue this[int index] { get; }
        ISerializationValue this[string name] { get; }
        int Find(string name);
        bool Contains(string name);
        ISerializationValue Add(string name, string value);
        void RemoveAt(int index);
    }
}
