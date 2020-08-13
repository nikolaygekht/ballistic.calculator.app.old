using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.Serialization
{
    public interface ISerializationObject
    {
        string Name { get; }
        string Value { get; set; }
        ISerializationObjectCollection Children { get;  }
        ISerializationValueCollection Values { get; }
    }

    public interface ISerializationObjectCollection : IEnumerable<ISerializationObject>
    {
        int Count { get; }
        ISerializationObject this[int index] { get; }
        ISerializationObject Add(string name);
        void RemoveAt(int index);
    }
}
