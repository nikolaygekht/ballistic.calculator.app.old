using System;
using System.Globalization;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic.Units
{

    public static class UnitSerialization
    {
        public static void WriteAngle(ISerializationObject node, string attribute, Angle v)
        {
            if (v == null)
                return;
            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Angle ReadAngle(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Angle v;
                    Angle.Unit u;
                    if (Angle.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WriteDistance(ISerializationObject node, string attribute, Distance v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Distance ReadDistance(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Distance v;
                    Distance.Unit u;
                    if (Distance.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WriteEnergy(ISerializationObject node, string attribute, Energy v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Energy ReadEnergy(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Energy v;
                    Energy.Unit u;
                    if (Energy.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WritePressure(ISerializationObject node, string attribute, Pressure v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Pressure ReadPressure(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Pressure v;
                    Pressure.Unit u;
                    if (Pressure.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WriteTemperature(ISerializationObject node, string attribute, Temperature v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Temperature ReadTemperature(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Temperature v;
                    Temperature.Unit u;
                    if (Temperature.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WriteVelocity(ISerializationObject node, string attribute, Velocity v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Velocity ReadVelocity(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Velocity v;
                    Velocity.Unit u;
                    if (Velocity.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WriteWeight(ISerializationObject node, string attribute, Weight v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v.ToString(v.SetUnit, false, 8, CultureInfo.InvariantCulture));
        }

        public static Weight ReadWeight(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    Weight v;
                    Weight.Unit u;
                    if (Weight.TryParse(attr.Value, CultureInfo.InvariantCulture, out v, out u))
                        return v;
                    else
                        return null;

                }
            }
            return null;
        }

        public static void WriteDouble(ISerializationObject node, string attribute, double v)
        {
            node.Values.Add(attribute, v.ToString("0.000", CultureInfo.InvariantCulture));
        }

        public static double ReadDouble(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    double v;
                    if (Double.TryParse(attr.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                        return v;
                    else
                        return 0;

                }
            }
            return 0;
        }

        public static void WriteInt(ISerializationObject node, string attribute, int v)
        {
            node.Values.Add(attribute, v.ToString(CultureInfo.InvariantCulture));
        }

        public static int ReadInt(ISerializationObject node, string attribute)
        {
            return ReadInt(node, attribute, 0);
        }

        public static int ReadInt(ISerializationObject node, string attribute, int defaultValue)
        {
            foreach (ISerializationValue attr in node.Values)
            {
                if (attr.Name == attribute)
                {
                    int v;
                    if (Int32.TryParse(attr.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out v))
                        return v;
                    else
                        return defaultValue;

                }
            }
            return defaultValue;
        }

        public static void WriteBool(ISerializationObject node, string attribute, bool v)
        {
            node.Values.Add(attribute, v ? "true" : "false");
        }

        public static bool ReadBool(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
                if (attr.Name == attribute)
                    return attr.Value == "true";
            return false;
        }

        public static bool ReadBool(ISerializationObject node, string attribute, bool defaultValue)
        {
            foreach (ISerializationValue attr in node.Values)
                if (attr.Name == attribute)
                    return attr.Value == "true";
            return defaultValue;
        }

        public static void WriteString(ISerializationObject node, string attribute, string v)
        {
            if (v == null)
                return;

            node.Values.Add(attribute, v);
        }

        public static string ReadString(ISerializationObject node, string attribute)
        {
            foreach (ISerializationValue attr in node.Values)
                if (attr.Name == attribute)
                    return attr.Value;
            return null;
        }

        public static string ReadString(ISerializationObject node, string attribute, string defaultValue)
        {
            foreach (ISerializationValue attr in node.Values)
                if (attr.Name == attribute)
                    return attr.Value;
            return defaultValue;
        }

        public static void WriteDragTable(ISerializationObject node, string attribute, DragTable t)
        {
            WriteString(node, attribute, DragTableFactory.DragTableName(t));
        }

        public static DragTable ReadDragTable(ISerializationObject node, string attribute)
        {
            string s = ReadString(node, attribute);
            if (s == null)
                return DragTable.G1;
            else
                return DragTableFactory.NameToDragTable(s);
        }
    }

}