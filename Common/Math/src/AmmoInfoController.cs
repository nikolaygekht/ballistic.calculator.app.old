using System;
using System.IO;
using System.Globalization;
using System.Text;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Serialization;

namespace MathEx.ExternalBallistic
{
    public static class AmmoInfoController
    {
        public static class Serialization
        {
            public static void Write(ISerializationObject parent, AmmoInfo ammo)
            {
                ISerializationObject node = parent.Children.Add("ammo-info");
                UnitSerialization.WriteDragTable(node, "table", ammo.Table);
                UnitSerialization.WriteDouble(node, "bc", ammo.BallisticCoefficient);
                UnitSerialization.WriteWeight(node, "bullet-weight", ammo.BulletWeight);
                UnitSerialization.WriteVelocity(node, "muzzle-velocity", ammo.MuzzleVelocity);
            }

            public static void Write(ISerializationContainer doc, ISerializationObject parent, AmmoInfoEx ammo)
            {
                ISerializationObject node;

                if (parent == null)
                    node = doc.CreateRoot("ammo-info-ex");
                else
                    node = parent.Children.Add("ammo-info-ex");
                UnitSerialization.WriteDragTable(node, "table", ammo.Table);
                UnitSerialization.WriteDouble(node, "bc", ammo.BallisticCoefficient);
                UnitSerialization.WriteWeight(node, "bullet-weight", ammo.BulletWeight);
                UnitSerialization.WriteVelocity(node, "muzzle-velocity", ammo.MuzzleVelocity);
                UnitSerialization.WriteDistance(node, "barrel-length", ammo.BarrelLength);
                UnitSerialization.WriteDistance(node, "bullet-length", ammo.BulletLength);
                UnitSerialization.WriteDistance(node, "bullet-diameter", ammo.BulletDiameter);
                UnitSerialization.WriteString(node, "name", ammo.Name);
                UnitSerialization.WriteString(node, "source", ammo.Source);
                UnitSerialization.WriteString(node, "caliber", ammo.Caliber);
                UnitSerialization.WriteString(node, "bullet-type", ammo.BulletType);
            }

            public static AmmoInfo Read(ISerializationObject node)
            {
                AmmoInfo ammo = new AmmoInfo();
                ammo.Table = UnitSerialization.ReadDragTable(node, "table");
                ammo.BallisticCoefficient = UnitSerialization.ReadDouble(node, "bc");
                ammo.BulletWeight = UnitSerialization.ReadWeight(node, "bullet-weight");
                ammo.MuzzleVelocity = UnitSerialization.ReadVelocity(node, "muzzle-velocity");
                return ammo;
            }

            public static AmmoInfoEx ReadEx(ISerializationObject node)
            {
                AmmoInfoEx ammo = new AmmoInfoEx();
                ammo.Table = UnitSerialization.ReadDragTable(node, "table");
                ammo.BallisticCoefficient = UnitSerialization.ReadDouble(node, "bc");
                ammo.BulletWeight = UnitSerialization.ReadWeight(node, "bullet-weight");
                ammo.MuzzleVelocity = UnitSerialization.ReadVelocity(node, "muzzle-velocity");
                ammo.BarrelLength = UnitSerialization.ReadDistance(node, "barrel-length");
                ammo.Name = UnitSerialization.ReadString(node, "name");
                ammo.Source = UnitSerialization.ReadString(node, "source");
                ammo.BulletType = UnitSerialization.ReadString(node, "bullet-type");
                ammo.Caliber = UnitSerialization.ReadString(node, "caliber");
                ammo.BulletLength = UnitSerialization.ReadDistance(node, "bullet-length");
                ammo.BulletDiameter = UnitSerialization.ReadDistance(node, "bullet-diameter");
                return ammo;
            }

            public static AmmoInfo FindAndRead(ISerializationObject parent)
            {
                foreach (ISerializationObject node in parent.Children)
                    if (node.Name == "ammo-info")
                        return Read(node);
                return null;
            }

            public static AmmoInfoEx FindAndReadEx(ISerializationObject parent)
            {
                foreach (ISerializationObject node in parent.Children)
                    if (node.Name == "ammo-info-ex")
                        return ReadEx(node);
                return null;
            }

            public static void WriteFileEx(string name, AmmoInfoEx info)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).NewContainer();
                Write(doc, null, info);
                SerializerFactory.getSerializer(SerializationType.Xml).WriteToFile(doc, name);

            }

            public static AmmoInfoEx ReadFileEx(string name)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromFile(name);
                return ReadEx(doc.Root);
            }

            public static AmmoInfoLibrary ReadLibrary(string name)
            {
                ISerializationContainer doc = SerializerFactory.getSerializer(SerializationType.Xml).ReadFromFile(name);
                AmmoInfoEx info;
                AmmoInfoLibrary lib = new AmmoInfoLibrary();

                for (int i = 0; i < doc.Root.Children.Count; i++)
                {
                    if (doc.Root.Children[i].Name == "ammo-info-ex")
                    {
                        info = ReadEx(doc.Root.Children[i]);
                        if (info.IsComplete())
                            lib.Add(info);
                    }
                }

                return lib;
            }
        }
    }
}