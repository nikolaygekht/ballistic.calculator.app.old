using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic.Serialization;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    public class FormState
    {
        private ISerializationContainer mDocument;

        class Pair
        {
            private string name;
            private Control control;

            internal string Name
            {
                get
                {
                    return name;
                }
            }

            internal Control Control
            {
                get
                {
                    return control;
                }
            }

            internal Pair(string n, Control c)
            {
                name = n;
                control = c;
            }
        }

        List<Pair> mPairs = new List<Pair>();

        public FormState()
        {
        }

        public void AddControl(string name, Control control)
        {
            Pair pair = new Pair(name, control);
            mPairs.Add(pair);
        }

        public void GatherFrom()
        {
            ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).NewContainer();
            ISerializationObject parent = doc.CreateRoot("form");
            ISerializationObject prop;
            ISerializationValue value;

            foreach (Pair p in mPairs)
            {
                if (p.Control is CheckBox)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as CheckBox).Checked ? "true" : "false");
                }
                if (p.Control is RadioButton)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as RadioButton).Checked ? "true" : "false");
                }
                else if (p.Control is TextBox)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as TextBox).Text);
                }
                else if (p.Control is ComboBox)
                {
                    
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as ComboBox).Text);
                }
                else if (p.Control is NumericUpDown)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as NumericUpDown).Value.ToString(CultureInfo.InvariantCulture));
                }
                else if (p.Control is AngleSelector)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value",  (p.Control as AngleSelector).Angle.ToString(CultureInfo.InvariantCulture));
                }
                else if (p.Control is CustomAngleControl)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value",  (p.Control as CustomAngleControl).Value.ToString((p.Control as CustomAngleControl).Value.SetUnit, false, 8, CultureInfo.InvariantCulture));
                }
                else if (p.Control is CustomDistanceControl)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as CustomDistanceControl).Value.ToString((p.Control as CustomDistanceControl).Value.SetUnit, false, 8, CultureInfo.InvariantCulture));
                }
                else if (p.Control is CustomPressureControl)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value", (p.Control as CustomPressureControl).Value.ToString((p.Control as CustomPressureControl).Value.SetUnit, false, 8, CultureInfo.InvariantCulture));
                }
                else if (p.Control is CustomTemperatureControl)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value",  (p.Control as CustomTemperatureControl).Value.ToString((p.Control as CustomTemperatureControl).Value.SetUnit, false, 8, CultureInfo.InvariantCulture));
                }
                else if (p.Control is CustomVelocityControl)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value",  (p.Control as CustomVelocityControl).Value.ToString((p.Control as CustomVelocityControl).Value.SetUnit, false, 8, CultureInfo.InvariantCulture));
                }
                else if (p.Control is CustomWeightControl)
                {
                    prop = parent.Children.Add(p.Name);
                    value = prop.Values.Add("value",  (p.Control as CustomWeightControl).Value.ToString((p.Control as CustomWeightControl).Value.SetUnit, false, 8, CultureInfo.InvariantCulture));
                }
                else if (p.Control is BallisticTable)
                {
                    BallisticTable t = p.Control as BallisticTable;
                    prop = parent.Children.Add(p.Name);
                    prop.Values.Add("measurement-system", ((int)t.MeasurementSystem).ToString(CultureInfo.InvariantCulture));
                    prop.Values.Add("angle-unit", ((int)t.AngleUnits).ToString(CultureInfo.InvariantCulture));
                }
                else if (p.Control is BallisticGraphControl)
                {
                    BallisticGraphControl t = p.Control as BallisticGraphControl;
                    prop = parent.Children.Add(p.Name);
                    prop.Values.Add("data-displayed", ((int)t.GraphData).ToString(CultureInfo.InvariantCulture));
                }
                mDocument = doc;
            }
        }

        public void Save(string name)
        {
            SerializerFactory.getSerializer(SerializationType.Xml).WriteToFile(mDocument, name);
        }

        public void FillForm()
        {
            ISerializationObject root = mDocument.Root;
            foreach (ISerializationObject node in root.Children)
            {
                for (int i = 0; i < mPairs.Count; i++)
                {
                    if (node.Name == mPairs[i].Name)
                    {
                        Pair p = mPairs[i];
                        if (p.Control is CheckBox && node.Values.Contains("value"))
                            (p.Control as CheckBox).Checked = UnitSerialization.ReadBool(node, "value");
                        if (p.Control is RadioButton && node.Values.Contains("value"))
                            (p.Control as RadioButton).Checked = UnitSerialization.ReadBool(node, "value");
                        else if (p.Control is TextBox && node.Values.Contains("value"))
                            (p.Control as TextBox).Text = UnitSerialization.ReadString(node, "value", "");
                        else if (p.Control is ComboBox && node.Values.Contains("value"))
                            (p.Control as ComboBox).Text = UnitSerialization.ReadString(node, "value", "");
                        else if (p.Control is NumericUpDown && node.Values.Contains("value"))
                            (p.Control as NumericUpDown).Value = (decimal)UnitSerialization.ReadDouble(node, "value");
                        else if (p.Control is AngleSelector && node.Values.Contains("value"))
                            (p.Control as AngleSelector).Angle = UnitSerialization.ReadInt(node, "value");
                        else if (p.Control is CustomAngleControl && node.Values.Contains("value"))
                        {
                            Angle v = UnitSerialization.ReadAngle(node, "value");
                            if (v != null)
                                (p.Control as CustomAngleControl).Value = v;
                        }
                        else if (p.Control is CustomDistanceControl && node.Values.Contains("value"))
                        {
                            Distance v = UnitSerialization.ReadDistance(node, "value");
                            if (v != null)
                                (p.Control as CustomDistanceControl).Value = v;
                        }
                        else if (p.Control is CustomPressureControl && node.Values.Contains("value"))
                        {
                            Pressure v = UnitSerialization.ReadPressure(node, "value");
                            if (v != null)
                                (p.Control as CustomPressureControl).Value = v;
                        }
                        else if (p.Control is CustomTemperatureControl && node.Values.Contains("value"))
                        {
                            Temperature v = UnitSerialization.ReadTemperature(node, "value");
                            if (v != null)
                                (p.Control as CustomTemperatureControl).Value = v;
                        }
                        else if (p.Control is CustomVelocityControl && node.Values.Contains("value"))
                        {
                            Velocity v = UnitSerialization.ReadVelocity(node, "value");
                            if (v != null)
                                (p.Control as CustomVelocityControl).Value = v;
                        }
                        else if (p.Control is CustomWeightControl && node.Values.Contains("value"))
                        {
                            Weight v = UnitSerialization.ReadWeight(node, "value");
                            if (v != null)
                                (p.Control as CustomWeightControl).Value = v;
                        }
                        else if (p.Control is BallisticTable && node.Values.Contains("measurement-system") && node.Values.Contains("angle-unit"))
                        {
                            BallisticTable t = p.Control as BallisticTable;

                            t.MeasurementSystem = (MeasurementSystem)UnitSerialization.ReadInt(node, "measurement-system");
                            t.AngleUnits = (Angle.Unit)UnitSerialization.ReadInt(node, "angle-unit");
                        }
                        else if (p.Control is BallisticGraphControl && node.Values.Contains("data-displayed"))
                        {
                            BallisticGraphControl t = p.Control as BallisticGraphControl;

                            t.GraphData = (ColumnData)UnitSerialization.ReadInt(node, "data-displayed");
                        }
                    }
                }
            }
        }

        public void Load(string filename)
        {
            mDocument = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromFile(filename);
        }
    }
}
