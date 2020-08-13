using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.Serialization
{
    public interface ISerializationContainer
    {
        ISerializationObject Root { get; }
        ISerializationObject CreateRoot(string name);
    }
}
