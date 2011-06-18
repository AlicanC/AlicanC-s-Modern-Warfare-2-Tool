using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ACMW2Tool.MW2Stuff
{
	public enum Endianness
	{
		Little,
		Big
	}

	public static class BinaryReaderExtension
	{
		public static void Read(this BinaryReader binaryReader, ref Object refObject)
		{
			Type objectType = refObject.GetType();

			GCHandle handle = GCHandle.Alloc(binaryReader.ReadBytes(Marshal.SizeOf(objectType)));
			refObject = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), objectType);
			handle.Free();
		}

		public static byte[] ReadBytes(this BinaryReader binaryReader, int count, Endianness endianness)
		{
			if (endianness == Endianness.Little)
				return binaryReader.ReadBytes(count);
            
			return binaryReader.ReadBytes(count).Reverse().ToArray();
		}

		public static UInt16 ReadUInt16(this BinaryReader binaryReader, Endianness endianness)
		{
			if (endianness == Endianness.Little)
				return binaryReader.ReadUInt16();

			return BitConverter.ToUInt16(binaryReader.ReadBytes(2, endianness), 0);
		}

		public static UInt32 ReadUInt32(this BinaryReader binaryReader, Endianness endianness)
		{
			if (endianness == Endianness.Little)
				return binaryReader.ReadUInt32();

			return BitConverter.ToUInt32(binaryReader.ReadBytes(4, endianness), 0);
		}

		public static String ReadNullTerminatedString(this BinaryReader binaryReader, Encoding encoding)
		{
			List<Byte> byteList = new List<Byte>();

			Byte nextByte = binaryReader.ReadByte();

			while (nextByte != 0)
			{
				byteList.Add(nextByte);
				try
				{
					nextByte = binaryReader.ReadByte();
				}
				catch
				{
					nextByte = 0;
				}
			}
            
			return encoding.GetString(byteList.ToArray());
		}
	}

	public class MW2PacketHeader //Length 256/8=32 + String + \0
	{
		//Big endian
		public UInt16 magic;
		public UInt16 packetSize;				//Includes this header
		public UInt32 unknown3;
		public UInt32 unknown4;
		public IPAddress sourceIP;
		public IPAddress destinationIP;
		public UInt16 sourcePort;
		public UInt16 destinationPort;
		public UInt32 unknown9;
		public UInt32 unknown10;
		public String packetType;
		
		public MW2PacketHeader(BinaryReader binaryReader)
		{
			magic = binaryReader.ReadUInt16();
			packetSize = binaryReader.ReadUInt16(Endianness.Big);
			unknown3 = binaryReader.ReadUInt32();
			unknown4 = binaryReader.ReadUInt32();
			sourceIP = new IPAddress(binaryReader.ReadBytes(4));
			destinationIP = new IPAddress(binaryReader.ReadBytes(4));
			sourcePort = binaryReader.ReadUInt16(Endianness.Big);
			destinationPort = binaryReader.ReadUInt16(Endianness.Big);
			unknown9 = binaryReader.ReadUInt32();
			unknown10 = binaryReader.ReadUInt32();
			packetType = binaryReader.ReadNullTerminatedString(Encoding.ASCII);
		}
	}

	public class MW2PartystateHeader //Length: 928/8=116
	{
		public UInt32 unknown1;
		public Byte unknown2;
		public Byte playerCount;
		public UInt32 unknown4;				//Always 0?
		public UInt32 unknown5;				//Always 0?
		public UInt32 unknown6;				//Always 46?
		public UInt32 unknown7;				//46?
		public Byte unknown8;
		public UInt32 unknown9;
		public UInt16 unknown10;			//Always 8?
		public Byte unknown11;
		public Byte[/*9*/] unknown12;
		public IPAddress hostIP2;			//Host's internal or external IP
		public IPAddress hostIP1;			//Host's external IP
		public UInt16 hostPort2;            
		public UInt16 hostPort1;            
		public UInt32[/*10*/] unknown17;	//Always 0?
		public UInt32 unknown18;
		public UInt32 unknown19;
		public Byte unknown20;
		public UInt32 unknown21;
		public UInt32 unknown22;
		public UInt32 unknown23;
		public UInt32 unknown24;

		public MW2PartystateHeader(BinaryReader binaryReader)
		{
			unknown1 = binaryReader.ReadUInt32();
			unknown2 = binaryReader.ReadByte();
			playerCount = binaryReader.ReadByte();
			if (unknown2 != Convert.ToByte("29", 16))
			{
				unknown4 = binaryReader.ReadUInt32();
				unknown5 = binaryReader.ReadUInt32();
				unknown6 = binaryReader.ReadUInt32();
				unknown7 = binaryReader.ReadUInt32();
				unknown8 = binaryReader.ReadByte();
				unknown9 = binaryReader.ReadUInt32();
				unknown10 = binaryReader.ReadUInt16();
				unknown11 = binaryReader.ReadByte();
				unknown12 = new byte[9];
				for (int i = 0; i < 9; i++)
					unknown12[i] = binaryReader.ReadByte();
				hostIP2 = new IPAddress(binaryReader.ReadBytes(4));
				hostIP1 = new IPAddress(binaryReader.ReadBytes(4));
				hostPort2 = binaryReader.ReadUInt16();
				hostPort1 = binaryReader.ReadUInt16();
				unknown17 = new UInt32[10];
				for (int i = 0; i < 10; i++)
					unknown17[i] = binaryReader.ReadUInt32();
				unknown18 = binaryReader.ReadUInt32();
				unknown19 = binaryReader.ReadUInt32();
				unknown20 = binaryReader.ReadByte();
				unknown21 = binaryReader.ReadUInt32();
				unknown22 = binaryReader.ReadUInt32();
				unknown23 = binaryReader.ReadUInt32();
				unknown24 = binaryReader.ReadUInt16();
			}
		}
	}

	public class MW2PartystatePlayer
	{
		public Byte playerID;
		public Byte[/*3*/] unknown2;
		public String playerName;
		public UInt32 unknown4;
		public UInt64 steamID;
		public IPAddress internalIP;
		public IPAddress externalIP;
		public UInt16 internalPort;
		public UInt16 externalPort;
		public Byte[/*24*/] unknown10;
		public UInt32 unknown11;
		public UInt32 unknown12;
		public UInt16 unknown13;
		public Byte unknown14;
		public Byte unknown15;
		public UInt32 unknown16;
		public UInt32 unknown17;
		public Byte unknown18;
		public Byte unknown19;					//This doesn't exist if the previous byte is 0x0C

		public String strippedPlayerName;		//This one will not have the color codes (^0, ^1, ..., ^9)
		
		public MW2PartystatePlayer(BinaryReader binaryReader)
		{
			//Наркобарон!
			playerID = binaryReader.ReadByte();
			unknown2 = binaryReader.ReadBytes(3);
			playerName = binaryReader.ReadNullTerminatedString(Encoding.UTF8); //Is the encoding right?
			unknown4 = binaryReader.ReadUInt32();
			steamID = binaryReader.ReadUInt64();
			internalIP = new IPAddress(binaryReader.ReadBytes(4));
			externalIP = new IPAddress(binaryReader.ReadBytes(4));
			internalPort = binaryReader.ReadUInt16();
			externalPort = binaryReader.ReadUInt16();
			unknown10 = binaryReader.ReadBytes(24);
			unknown11 = binaryReader.ReadUInt32();
			unknown12 = binaryReader.ReadUInt32();
			unknown13 = binaryReader.ReadUInt16();
			unknown14 = binaryReader.ReadByte();
			unknown15 = binaryReader.ReadByte();
			unknown16 = binaryReader.ReadUInt32();
			unknown17 = binaryReader.ReadUInt32();
			unknown18 = binaryReader.ReadByte();
			if (unknown18 != Convert.ToByte("0C", 16))
				unknown19 = binaryReader.ReadByte();

			Regex regex = new Regex("\\^[0-9]");
			
			strippedPlayerName = regex.Replace(playerName, "");
		}
	}
}
