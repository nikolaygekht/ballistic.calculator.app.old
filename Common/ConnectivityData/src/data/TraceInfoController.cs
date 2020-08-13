using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace Gehtsoft.BallisticCalculator.Connectivity
{
    public static class TraceInfoController
    {
        public static class Serialization
        {
            static public void WriteToNode(ISerializationContainer document, ISerializationObject parentNode, TraceInfo trace)
            {
                ISerializationObject node;
                if (parentNode == null)
                    node = document.CreateRoot("trace");
                else
                    node = parentNode.Children.Add("trace");
                UnitSerialization.WriteString(node, "name", trace.TraceName);
                UnitSerialization.WriteDouble(node, "bc", trace.BallisticCoefficient);
                UnitSerialization.WriteDragTable(node, "drag-table", trace.DrageTable);
                UnitSerialization.WriteWeight(node, "bullet-weight", trace.BulletWeight);
                UnitSerialization.WriteVelocity(node, "muzzle-velocity", trace.MuzzleVelocity);

                UnitSerialization.WriteDistance(node, "sight-height", trace.SightHeight);
                UnitSerialization.WriteDistance(node, "zero-distance", trace.ZeroDistance);
                UnitSerialization.WriteAngle(node, "elevation-angle", trace.ZeroElevationAngle);

                UnitSerialization.WriteBool(node, "calculate-drift", trace.DriftInfo);
                UnitSerialization.WriteDistance(node, "dirft-bullet-length", trace.BulletLength);
                UnitSerialization.WriteDistance(node, "dirft-bullet-diameter", trace.BulletDiameter);
                UnitSerialization.WriteDistance(node, "dirft-rifling-twist", trace.RiflingTwist);
                UnitSerialization.WriteBool(node, "drift-rifling-right-hand", trace.RiflingRightHandTwist);

                UnitSerialization.WriteAngle(node, "vertical-click", trace.VerticalClick);
                UnitSerialization.WriteAngle(node, "horizontal-click", trace.HorizonalClick);

                UnitSerialization.WriteBool(node, "metric", trace.Metric);
            }

            static public TraceInfo ReadFromNode(ISerializationObject node)
            {
                TraceInfo trace = new TraceInfo();

                trace.TraceName = UnitSerialization.ReadString(node, "name");
                trace.BallisticCoefficient = UnitSerialization.ReadDouble(node, "bc");
                trace.DrageTable = UnitSerialization.ReadDragTable(node, "drag-table");
                trace.BulletWeight = UnitSerialization.ReadWeight(node, "bullet-weight");
                trace.MuzzleVelocity = UnitSerialization.ReadVelocity(node, "muzzle-velocity");

                trace.SightHeight = UnitSerialization.ReadDistance(node, "sight-height");
                trace.ZeroDistance = UnitSerialization.ReadDistance(node, "zero-distance");
                trace.ZeroElevationAngle = UnitSerialization.ReadAngle(node, "elevation-angle");

                trace.DriftInfo = UnitSerialization.ReadBool(node, "calculate-drift");
                trace.BulletLength = UnitSerialization.ReadDistance(node, "dirft-bullet-length");
                trace.BulletDiameter = UnitSerialization.ReadDistance(node, "dirft-bullet-diameter");
                trace.RiflingTwist = UnitSerialization.ReadDistance(node, "dirft-rifling-twist");
                trace.RiflingRightHandTwist = UnitSerialization.ReadBool(node, "drift-rifling-right-hand");

                trace.VerticalClick = UnitSerialization.ReadAngle(node, "vertical-click");
                trace.HorizonalClick = UnitSerialization.ReadAngle(node, "horizontal-click");

                trace.Metric = UnitSerialization.ReadBool(node, "metric");


                if (trace.Complete)
                    return trace;
                else
                    return null;
            }

            static public string ArrayToXml(IEnumerable<TraceInfo> traces)
            {
                ISerializationContainer container = SerializerFactory.getSerializer(SerializationType.Xml).NewContainer();
                ISerializationObject root = container.CreateRoot("traces");
                int count = 0;
                foreach (TraceInfo trace in traces)
                {
                    WriteToNode(container, root, trace);
                    count++;
                }
                root.Values.Add("count", count.ToString(CultureInfo.InvariantCulture));
                return SerializerFactory.getSerializer(SerializationType.Xml).WriteToString(container);
            }

            static public TraceInfo[] XmlToArray(string xml)
            {
                List<TraceInfo> traces = new List<TraceInfo>();
                ISerializationContainer container = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromString(xml);

                foreach (ISerializationObject node in container.Root.Children)
                {
                    if (node.Name == "trace")
                    {
                        TraceInfo traceInfo = ReadFromNode(node);
                        if (traceInfo != null)
                            traces.Add(traceInfo);
                    }
                }


                return traces.Count > 0 ? traces.ToArray() : null;
            }
        }
    }
}
