using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace Gehtsoft.BallisticCalculator
{
    public class WebServerState
    {
        public int Port { get; set; }
        public bool Autostart { get; set; }
        
        TraceInfoCollection mTraces = new TraceInfoCollection();

        public TraceInfoCollection Traces
        {
            get
            {
                return mTraces;
            }
        }

        public WebServerState()
        {
            Port = 9000;
            Autostart = false;
        }
    }

    public static class WebServerStateController
    {
        public static class Serialization
        {
            public static string StateName
            {
                get
                {
                    return Program.GetSubDir("web-server-state.xml");
                }
            }

            public static void Load(WebServerState state)
            {
                ISerializationContainer doc = null;

                try
                {
                    doc = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromFile(StateName);
                }
                catch (Exception)
                {
                    return;
                }

                if (doc != null)
                {
                    ISerializationObject root = doc.Root;
                    foreach (ISerializationObject node in root.Children)
                    {
                        if (node.Name == "server")
                        {
                            state.Port = UnitSerialization.ReadInt(node, "port");
                            state.Autostart = UnitSerialization.ReadBool(node, "auto-start");
                        }
                        else if (node.Name == "traces")
                        {
                            foreach (ISerializationObject node1 in node.Children)
                            {
                                if (node1.Name == "trace")
                                {
                                    TraceInfo ti = TraceInfoController.Serialization.ReadFromNode(node1);
                                    if (ti != null)
                                        state.Traces.Add(ti);
                                }
                            }
                        }
                    }
                }
            }

            public static void Save(WebServerState state)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).NewContainer();
                ISerializationObject root = doc.CreateRoot("web-server-state");
                ISerializationObject server = root.Children.Add("server");
                UnitSerialization.WriteInt(server, "port", state.Port);
                UnitSerialization.WriteBool(server, "auto-start", state.Autostart);

                ISerializationObject traces = root.Children.Add("traces");
                foreach (TraceInfo ti in state.Traces)
                    TraceInfoController.Serialization.WriteToNode(doc, traces, ti);

                SerializerFactory.getSerializer(SerializationType.Xml).WriteToFile(doc, StateName);

            }
        }

    }
}
