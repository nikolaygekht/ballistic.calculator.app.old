using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic.Serialization.Windows
{
    class SerializationContainer : ISerializationContainer
    {
        XmlDocument mDocument;

        public XmlDocument Document
        {
            get
            {
                return mDocument;
            }
        }

        public SerializationContainer()
        {
            mDocument = new XmlDocument();
        }

        public SerializationContainer(XmlDocument document)
        {
            mDocument = document;
        }

        public ISerializationObject CreateRoot(string name)
        {
            XmlNode node = mDocument.CreateElement(name);
            mDocument.AppendChild(node);
            return new SerializationObject(mDocument, node);
        }

        public ISerializationObject Root
        {
            get 
            {
                return new SerializationObject(mDocument, mDocument.DocumentElement);
            }
        }
    }
}
