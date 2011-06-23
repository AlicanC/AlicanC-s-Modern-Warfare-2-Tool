using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace ACMW2Tool.Extensions
{
	enum Endianness
	{
		Little,
		Big
	}

	static class Extensions
	{
		public static byte[] ReadBytes(this BinaryReader binaryReader, Int32 count, Endianness endianness)
		{
			if (endianness == Endianness.Little)
				return binaryReader.ReadBytes(count);

			return binaryReader.ReadBytes(count).Reverse().ToArray();
		}

		public static UInt16 ReadUInt16(this BinaryReader binaryReader, Endianness endianness)
		{
			return BitConverter.ToUInt16(binaryReader.ReadBytes(2, endianness), 0);
		}

		public static UInt32 ReadUInt32(this BinaryReader binaryReader, Endianness endianness)
		{
			return BitConverter.ToUInt32(binaryReader.ReadBytes(4, endianness), 0);
		}

		public static String ReadNullTerminatedString(this BinaryReader binaryReader, Encoding encoding)
		{
			List<Byte> byteList = new List<Byte>();

			Byte nextByte;
			while ((nextByte = binaryReader.ReadByte()) != 0)
				byteList.Add(nextByte);

			return encoding.GetString(byteList.ToArray());
		}
	}
}
