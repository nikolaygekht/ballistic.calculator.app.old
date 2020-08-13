using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic.Serialization.Windows
{
    class SerializationObject : ISerializationObject
    {
        XmlDocument mDocument;
        XmlNode mNode;
        SerializationObjectCollection mChildren;
        SerializationValueCollection mValues;

        public XmlNode Node
        {
            get
            {
                return mNode;
            }

        }

        public SerializationObject(XmlDocument document, XmlNode node)
        {
            mDocument = document;
            mNode = node;
            mChildren = new SerializationObjectCollection(mDocument, mNode);
            mValues = new SerializationValueCollection(mDocument, mNode);
        }

        public ISerializationObjectCollection Children
        {
            get 
            {
                return mChildren;
            }
        }

        public string Name
        {
            get
            {
                return mNode.Name;
            }
        }

        public string Value
        {
            get
            {
                return mNode.InnerText;
            }
            set
            {
                mNode.InnerText = value;
            }
        }

        public ISerializationValueCollection Values
        {
            get 
            {
                return mValues;
            }
        }
    }

    class SerializationObjectEnumerator : IEnumerator<ISerializationObject>
    {
        XmlDocument mDocument;
        XmlNode mNode;
        IEnumerator<int> mXmlEnumerator;

        public SerializationObjectEnumerator(XmlDocument document, XmlNode node, IEnumerator<int> xmlEnumerator)
        {
            mDocument = document;
            mNode = node;
            mXmlEnumerator = xmlEnumerator;
        }

        public ISerializationObject _Current
        {
            get 
            {
                int nodeIndex = mXmlEnumerator.Current;
                if (nodeIndex < 0 || nodeIndex >= mNode.ChildNodes.Count)
                    return null;
                XmlNode node = mNode.ChildNodes[nodeIndex];
                return new SerializationObject(mDocument, node);
            }
        }

        public ISerializationObject Current
        {
            get
            {
                return _Current;
            }
        }

        public void Dispose()
        {
            if (mXmlEnumerator != null)
                mXmlEnumerator.Dispose();
            mXmlEnumerator = null;
        }

        object System.Collections.IEnumerator.Current
        {
            get 
            {
                return _Current;
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

    class SerializationObjectCollection : ISerializationObjectCollection
    {
        XmlDocument mDocument;
        XmlNode mNode;
        List<int> mIndex = new List<int>();

        public void UpdateIndex()
        {
            mIndex.Clear();
            for (int i = 0; i < mNode.ChildNodes.Count; i++)
            {
                if (mNode.ChildNodes[i].NodeType == XmlNodeType.Element)
                    mIndex.Add(i);
            }
        }

        public SerializationObjectCollection(XmlDocument document, XmlNode node)
        {
            mDocument = document;
            mNode = node;
            UpdateIndex();
        }

        public ISerializationObject Add(string name)
        {
            XmlNode node = mDocument.CreateElement(name);
            mNode.AppendChild(node);
            mIndex.Add(node.ChildNodes.Count);
            return new SerializationObject(mDocument, node);
        }

        public int Count
        {
            get 
            {
                return mIndex.Count;
            }
        }

        public void RemoveAt(int index)
        {
            int nodeIndex = mIndex[index];
            mNode.RemoveChild(mNode.ChildNodes[nodeIndex]);
            UpdateIndex();
        }

        public ISerializationObject this[int index]
        {
            get 
            { 
                return new SerializationObject(mDocument, mNode.ChildNodes[mIndex[index]]);
            }
        }

        public IEnumerator<ISerializationObject> GetEnumerator()
        {
            return new SerializationObjectEnumerator(mDocument, mNode, mIndex.GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new SerializationObjectEnumerator(mDocument, mNode, mIndex.GetEnumerator());
        }
    }

}
