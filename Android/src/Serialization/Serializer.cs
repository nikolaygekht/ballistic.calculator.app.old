using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic.Serialization.Windows
{
    public static class Serialization
    {
        static public void Init()
        {
            SerializerFactory.registerSerialization(new Serializer());
        }
    }


    class Serializer : ISerializer
    {
        public string Name
        {
            get 
            {
                return "XmlDocument serializer";
            }
        }

        public ISerializationContainer NewContainer()
        {
            return new SerializationContainer(new XmlDocument());
        }

        public ISerializationContainer ReadFromFile(string filename)
        {
            XmlReaderSettings settings;
            settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            using (XmlReader reader = XmlReader.Create(filename))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                return new SerializationContainer(doc);
            }
        }

        public ISerializationContainer ReadFromString(string content)
        {
            XmlReaderSettings settings;
            settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            using (StringReader sr = new StringReader(content))
            {
                using (XmlReader reader = XmlReader.Create(sr, settings))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);
                    reader.Close();
                    return new SerializationContainer(doc);
                }
            }

        }

        public SerializationType SerializationType
        {
            get 
            {
                return SerializationType.Xml;
            }
        }

        public void WriteToFile(ISerializationContainer _container, string name)
        {
            SerializationContainer container = _container as SerializationContainer;
            if (container == null)
                throw new ArgumentException("Container shall be created by the same driver", "container");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = (" ");
            settings.OmitXmlDeclaration = true;

            using (XmlWriter writer = XmlWriter.Create(name, settings))
            {
                container.Document.Save(writer);
                writer.Close();
            }
        }

        public string WriteToString(ISerializationContainer _container)
        {
            SerializationContainer container = _container as SerializationContainer;
            if (container == null)
                throw new ArgumentException("Container shall be created by the same driver", "container");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = (" ");
            settings.OmitXmlDeclaration = true;

            StringBuilder output = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(output, settings))
            {
                container.Document.Save(writer);
                writer.Close();
            }
            return output.ToString();
        }
    }
}
