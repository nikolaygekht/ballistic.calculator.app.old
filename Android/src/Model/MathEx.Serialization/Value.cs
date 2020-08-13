using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MathEx.ExternalBallistic.Serialization;

namespace BallisticCalculator.Serialization
{
    class SerializationValue : ISerializationValue
    {
        XmlAttribute mAttribute;

        public SerializationValue(XmlAttribute attribute)
        {
            mAttribute = attribute;
        }

        public string Name
        {
            get 
            {
                return mAttribute.Name;
            }
        }

        public string Value
        {
            get
            {
                return mAttribute.Value;
            }
            set
            {
                mAttribute.Value = value;
            }
        }
    }

    class SerializationValueCollectionEnumerator : IEnumerator<ISerializationValue>
    {
        IEnumerator mXmlEnumerator;

        public SerializationValueCollectionEnumerator(IEnumerator xmlEnumerator)
        {
            mXmlEnumerator = xmlEnumerator;
        }

        public ISerializationValue _Current
        {
            get 
            {
                XmlAttribute attr = mXmlEnumerator.Current as XmlAttribute;
                if (attr == null)
                    return null;
                else
                    return new SerializationValue(attr);
            }
        }

        public ISerializationValue Current
        {
            get
            {
                return _Current;
            }
        }

        public void Dispose()
        {
            mXmlEnumerator = null;
        }

        object System.Collections.IEnumerator.Current
        {
            get 
            {
                return this._Current;
            }
        }

        public bool MoveNext()
        {
            return mXmlEnumerator.MoveNext();
        }

        public void Reset()
        {
            mXmlEnumerator.Reset();
        }
    }

    class SerializationValueCollection : ISerializationValueCollection
    {
        XmlDocument mDocument;
        XmlNode mNode;

        public SerializationValueCollection(XmlDocument document, XmlNode node)
        {
            mDocument = document;
            mNode = node;
        }

        public ISerializationValue Add(string name, string value)
        {
            XmlAttribute attr = mDocument.CreateAttribute(name);
            attr.Value = value;
            mNode.Attributes.Append(attr);
            return new SerializationValue(attr);
        }

        public bool Contains(string name)
        {
            return Find(name) >= 0;
        }

        public int Count
        {
            get 
            {
                return mNode.Attributes.Count;
            }
        }

        public int Find(string name)
        {
            for (int i = 0; i < mNode.Attributes.Count; i++)
                if (mNode.Attributes[i].Name == name)
                    return i;
            return -1;
        }

        public void RemoveAt(int index)
        {
            mNode.Attributes.RemoveAt(index);
        }

        public ISerializationValue this[string name]
        {
            get
            {
                int index = Find(name);
                if (index < 0)
                    return null;
                XmlAttribute attr = mNode.Attributes[index];
                return new SerializationValue(attr);
            }
        }

        public ISerializationValue this[int index]
        {
            get 
            {
                return new SerializationValue(mNode.Attributes[index]);
            }
        }

        public IEnumerator<ISerializationValue> GetEnumerator()
        {
            return new SerializationValueCollectionEnumerator(mNode.Attributes.GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new SerializationValueCollectionEnumerator(mNode.Attributes.GetEnumerator());
        }
    }
}
