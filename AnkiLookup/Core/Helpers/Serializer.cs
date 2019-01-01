using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnkiLookup.Core.Helpers
{
    // ------------------
    // Created By: AeonHack
    // Converted By: Mr. Trvp
    // Site: nimoru.com
    // Created: 9/12/2012
    // Version: 1.0.0.0
    // ------------------

    public static class Serializer
    {
        private static readonly Dictionary<Type, byte> Types;

        static Serializer()
        {
            Types = new Dictionary<Type, byte>
            {
                {typeof(bool), 0},
                {typeof(byte), 1},
                {typeof(byte[]), 2},
                {typeof(char), 3},
                {typeof(char[]), 4},
                {typeof(decimal), 5},
                {typeof(double), 6},
                {typeof(int), 7},
                {typeof(long), 8},
                {typeof(sbyte), 9},
                {typeof(short), 10},
                {typeof(float), 11},
                {typeof(string), 12},
                {typeof(uint), 13},
                {typeof(ulong), 14},
                {typeof(ushort), 15},
                {typeof(DateTime), 16}
            };
        }

        public static byte[] Serialize(params object[] data)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(data.Length);

                for (var i = 0; i < data.Length; i++)
                {
                    var currentIndex = Types[data[i].GetType()];
                    writer.Write(currentIndex);

                    switch (currentIndex)
                    {
                        case 0:
                            writer.Write((bool) data[i]);
                            break;
                        case 1:
                            writer.Write((byte) data[i]);
                            break;
                        case 2:
                            writer.Write(((byte[]) data[i]).Length);
                            writer.Write((byte[]) data[i]);
                            break;
                        case 3:
                            writer.Write((char) data[i]);
                            break;
                        case 4:
                            writer.Write(data[i].ToString());
                            break;
                        case 5:
                            writer.Write((decimal) data[i]);
                            break;
                        case 6:
                            writer.Write((double) data[i]);
                            break;
                        case 7:
                            writer.Write((int) data[i]);
                            break;
                        case 8:
                            writer.Write((long) data[i]);
                            break;
                        case 9:
                            writer.Write((sbyte) data[i]);
                            break;
                        case 10:
                            writer.Write((short) data[i]);
                            break;
                        case 11:
                            writer.Write((float) data[i]);
                            break;
                        case 12:
                            writer.Write((string) data[i]);
                            break;
                        case 13:
                            writer.Write((uint) data[i]);
                            break;
                        case 14:
                            writer.Write((ulong) data[i]);
                            break;
                        case 15:
                            writer.Write((ushort) data[i]);
                            break;
                        case 16:
                            writer.Write(((DateTime) data[i]).ToBinary());
                            break;
                    }
                }

                return stream.ToArray();
            }
        }

        public static object[] Deserialize(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                var items = new List<object>();
                var count = reader.ReadInt32();

                for (var i = 0; i < count; i++)
                {
                    var currentIndex = reader.ReadByte();
                    switch (currentIndex)
                    {
                        case 0:
                            items.Add(reader.ReadBoolean());
                            break;
                        case 1:
                            items.Add(reader.ReadByte());
                            break;
                        case 2:
                            items.Add(reader.ReadBytes(reader.ReadInt32()));
                            break;
                        case 3:
                            items.Add(reader.ReadChar());
                            break;
                        case 4:
                            items.Add(reader.ReadString().ToCharArray());
                            break;
                        case 5:
                            items.Add(reader.ReadDecimal());
                            break;
                        case 6:
                            items.Add(reader.ReadDouble());
                            break;
                        case 7:
                            items.Add(reader.ReadInt32());
                            break;
                        case 8:
                            items.Add(reader.ReadInt64());
                            break;
                        case 9:
                            items.Add(reader.ReadSByte());
                            break;
                        case 10:
                            items.Add(reader.ReadInt16());
                            break;
                        case 11:
                            items.Add(reader.ReadSingle());
                            break;
                        case 12:
                            items.Add(reader.ReadString());
                            break;
                        case 13:
                            items.Add(reader.ReadUInt32());
                            break;
                        case 14:
                            items.Add(reader.ReadUInt64());
                            break;
                        case 15:
                            items.Add(reader.ReadUInt16());
                            break;
                        case 16:
                            items.Add(DateTime.FromBinary(reader.ReadInt64()));
                            break;
                    }
                }

                return items.ToArray();
            }
        }
    }
}