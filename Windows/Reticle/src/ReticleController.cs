using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Windows.Forms;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.Serialization;

namespace Gehtsoft.BallisticCalculator.Reticle
{
    public static class ReticleController
    {
        public static class Calculation
        {
            public static double getResultion(int cx1, int cy1, int cx2, int cy2, Angle distance)
            {
                double pixels = Math.Sqrt(Math.Pow(cx1 - cx2, 2) + Math.Pow(cy1 - cy2, 2));
                return distance.Get(Angle.Unit.Mil) / pixels;
            }

            public static Angle ImagePixelsToAngle(int p, int z, double resolution, Angle.Unit setUnit)
            {
                int distance = p - z;
                double value = distance * resolution;
                Angle angle = new Angle(value, Angle.Unit.Mil);
                angle = angle.ToUnit(setUnit);
                return angle;
            }

            public static int AngleToPixels(Angle angle, int z, double resolution)
            {
                double value = angle.Get(Angle.Unit.Mil);
                return (int)Math.Round(z + value / resolution);
            }
        }


        public static class Serialization
        {
            public static Reticle loadReticle(string fileName, bool saveImageSource)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromFile(fileName);

                string name = "Unknown";
                int centerX = 0, centerY = 0, calibrationX1 = 0, calibrationY1 = 0, calibrationX2 = 0, calibrationY2 = 0;
                Angle calibrationAngle = new Angle(1, Angle.Unit.MilDot);

                Image image = null;
                bool imageRaster = true;
                byte[] imageSource = null;

                List<KeyValuePair<int, int>> bdcs = new List<KeyValuePair<int, int>>();

                ISerializationObject root = doc.Root;

                if (root.Name == "reticle")
                {
                    name = UnitSerialization.ReadString(root, "Name");
                    foreach (ISerializationObject node in root.Children)
                    {
                        if (node.Name == "center")
                        {
                            centerX = UnitSerialization.ReadInt(node, "x", -1);
                            centerY = UnitSerialization.ReadInt(node, "y", -1);
                        }
                        if (node.Name == "zero")
                        {
                            centerX = UnitSerialization.ReadInt(node, "x", -1);
                            centerY = UnitSerialization.ReadInt(node, "y", -1);
                        }
                        if (node.Name == "calibration")
                        {
                            calibrationX1 = UnitSerialization.ReadInt(node, "x1", -1);
                            calibrationY1 = UnitSerialization.ReadInt(node, "y1", -1);
                            calibrationX2 = UnitSerialization.ReadInt(node, "x2", -1);
                            calibrationY2 = UnitSerialization.ReadInt(node, "y2", -1);
                            calibrationAngle = UnitSerialization.ReadAngle(node, "angle");
                        }
                        else if (node.Name == "resolution")
                        {
                            //compat with 1.0
                            int r = -1;
                            r = UnitSerialization.ReadInt(node, "pixel", -1);
                            calibrationAngle = UnitSerialization.ReadAngle(node, "angle");
                            if (calibrationAngle != null && r >= 0)
                            {
                                calibrationX1 = centerX;
                                calibrationX2 = centerX;
                                calibrationY1 = centerY;
                                calibrationY2 = centerY + r;
                            }
                        }
                        else if (node.Name == "bdc")
                        {
                            foreach (ISerializationObject node1 in node.Children)
                            {
                                if (node1.Name == "point")
                                {
                                    int xx = UnitSerialization.ReadInt(node1, "x", -1);
                                    int yy = UnitSerialization.ReadInt(node1, "y", -1);
                                    if (yy >= 0)
                                    {
                                        if (xx < 0)
                                            xx = centerX;       //compat with 0.9

                                        bdcs.Add(new KeyValuePair<int, int>(xx, yy));
                                    }
                                }
                            }
                        }
                        else if (node.Name == "image")
                        {
                            string type = UnitSerialization.ReadString(node, "type");
                            if (type == null || type == "image/png")
                            {
                                string text = node.Value;
                                try
                                {
                                    byte[] arr = Convert.FromBase64CharArray(text.ToCharArray(), 0, text.Length);
                                    imageSource = arr;
                                    using (MemoryStream ms = new MemoryStream(arr))
                                        image = new Bitmap(Image.FromStream(ms));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Can't read reticle image:\r\n" + ex.ToString(), "Reticle Reader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    image = null;
                                }
                            }
                            else if (type == "windows/metafile")
                            {
                                string text = node.Value;
                                try
                                {
                                    byte[] arr = Convert.FromBase64CharArray(text.ToCharArray(), 0, text.Length);
                                    imageSource = arr;
                                    using (MemoryStream ms = new MemoryStream(arr))
                                        image = new Metafile(ms);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Can't read reticle vector image:\r\n" + ex.ToString(), "Reticle Reader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    image = null;
                                }
                                imageRaster = false;
                            }
                        }
                    }
                }
                Reticle reticle = new Reticle();
                reticle.Name = name;
                reticle.Image = image;
                reticle.Raster = imageRaster;
                reticle.ZeroX = centerX;
                reticle.ZeroY = centerY;
                reticle.CalibrationX1 = calibrationX1;
                reticle.CalibrationY1 = calibrationY1;
                reticle.CalibrationX2 = calibrationX2;
                reticle.CalibrationY2 = calibrationY2;
                reticle.CalibrationAngle = calibrationAngle;
                if (saveImageSource)
                    reticle.ImageSource = imageSource;
                foreach (KeyValuePair<int, int> point in bdcs)
                    reticle.BDCPoints.Add(new Reticle.BDCPoint(point.Key, point.Value));
                return reticle;
            }

            public static void saveReticle(Reticle reticle, string filename)
            {
                if (reticle == null ||
                    reticle.Name == null ||
                    reticle.Image == null ||
                    reticle.CalibrationAngle == null ||
                    reticle.ImageSource == null ||
                    reticle.ZeroX < 0 ||
                    reticle.ZeroY < 0 ||
                    reticle.CalibrationX1 < 0 ||
                    reticle.CalibrationX2 < 0 ||
                    reticle.CalibrationY1 < 0 ||
                    reticle.CalibrationY2 < 0)
                    throw new ArgumentException("Reticle is not completely filled", "reticle");

                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).NewContainer();
                ISerializationObject root = doc.CreateRoot("reticle");
                root.Values.Add("name", reticle.Name);

                ISerializationObject zero = root.Children.Add("zero");
                UnitSerialization.WriteInt(zero, "x", reticle.ZeroX);
                UnitSerialization.WriteInt(zero, "y", reticle.ZeroY);

                ISerializationObject calibration = root.Children.Add("calibration");
                UnitSerialization.WriteInt(calibration, "x1", reticle.CalibrationX1);
                UnitSerialization.WriteInt(calibration, "y1", reticle.CalibrationY1);
                UnitSerialization.WriteInt(calibration, "x2", reticle.CalibrationX2);
                UnitSerialization.WriteInt(calibration, "y2", reticle.CalibrationY2);
                UnitSerialization.WriteAngle(calibration, "angle", reticle.CalibrationAngle);

                if (reticle.BDCPoints.Count > 0)
                {
                    ISerializationObject bdc = root.Children.Add("bdc");
                    foreach (Reticle.BDCPoint pt in reticle.BDCPoints)
                    {
                        ISerializationObject point = bdc.Children.Add("point");
                        UnitSerialization.WriteInt(point, "x", pt.X);
                        UnitSerialization.WriteInt(point, "y", pt.Y);
                    }
                }

                string b64, type;
                if (reticle.Raster)
                    type = "image/png";
                else
                    type = "windows/metafile";
                b64 = Convert.ToBase64String(reticle.ImageSource, Base64FormattingOptions.InsertLineBreaks);

                ISerializationObject image = root.Children.Add("image");
                image.Value = b64;
                image.Values.Add("type", type);

                SerializerFactory.getSerializer(SerializationType.Xml).WriteToFile(doc, filename);
            }
        }
    }
}
