using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ACMW2Tool.MW2Packets
{
	public enum Endianness
	{
		Little,
		Big
	}

	public static class BinaryReaderExtension
	{
		/*
		public static void Read(this BinaryReader binaryReader, ref Object refObject)
		{
			Type objectType = refObject.GetType();

			GCHandle handle = GCHandle.Alloc(binaryReader.ReadBytes(Marshal.SizeOf(objectType)));
			refObject = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), objectType);
			handle.Free();
		}
		*/

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
		public UInt16 unknown4;
		public UInt16 unknown5;
		public IPAddress sourceIP;
		public IPAddress destinationIP;
		public UInt16 sourcePort;
		public UInt16 destinationPort;
		public UInt32 unknown10;
		public UInt32 unknown11;
		public String packetType;
		
		public MW2PacketHeader(BinaryReader binaryReader)
		{
			//Is this really something special to MW2? Can this be a generic header?

			//The package seems to be big endian but this is only an identifier so it doesn't really matter how we read it.
			if ((magic = binaryReader.ReadUInt16()) != Convert.ToUInt16("2D00", 16))
				throw new InvalidDataException("First two bytes of the packet header must be 0x2D00.");

			//Da_Fileserver identified this as the packet size including this header.
			//The problem is that this value may be different for packets of the same size. This is even 0x0000 in some packets.
			packetSize = binaryReader.ReadUInt16(Endianness.Big);

			//This is an unknown field. Example values are (little endian) these:
			//From fail packets: 0x4F360000, 0x4F050000, 0x071D0000, 0x07020000, 0x072F0000, 0x073A0000, 0x073D0000, 0x07440000, 0x07000000, 0x071D0000
			//From good packets: 0x045A0000, 0x4F030000, 0x4F2D0000, 0x4F040000, 0x4F000000, 0x07250000, 0x07280000, 0x07490000, 0x07070000, 0x07000000
			//As you can see, these values start with 0x04, 0x07 or 0x4F and ends with 0x0000.
			//I am not sure if these are enough to investigate this field.
			//Any resemblance might be a coincidince since the example packets are taken from the same game.
			unknown3 = binaryReader.ReadUInt32();

			//This is an unknown field. Looking at the example packets, this seems to always be 0x480B.
			//Still, I don't want to throw an exception if this is not 0x480B
			unknown4 = binaryReader.ReadUInt16();

			//This is an unknown field. Example values are (little endian) these:
			//From fail packets: 0x0847, 0x0843, 0x0804, 0x5900, 0x0800, 0x0800, 0x0800, 0x0800, 0x5900, 0x0804
			//From good packets: 0x002B, 0x5900, 0x5900, 0x5900, 0x0806, 0x5800, 0x5800, 0x5800, 0x5800, 0x5800
			//Again, there are some resemblance but I don't know if that's proof of anything.
			unknown5 = binaryReader.ReadUInt16();

			//These are the IP's and ports of source and desination.
			//Source is always the host for 0partystate packets and destination is always you.
			//Intrestingly, the port number is 18196 when read big endian and 5191 when read little endian.
			//We only watch 28960 so this is something else?
			sourceIP = new IPAddress(binaryReader.ReadBytes(4));
			destinationIP = new IPAddress(binaryReader.ReadBytes(4));
			sourcePort = binaryReader.ReadUInt16(Endianness.Big);
			destinationPort = binaryReader.ReadUInt16(Endianness.Big);

			//I have no idea what this is. Example values are (little endian) these:
			//From fail packets: 0x0103034D, 0x01030548, 0x005B0000, 0x01000900, 0x00000005, 0x00000700, 0x00000000, 0x00000200, 0x01310005, 0x005A001D
			//From good packets: 0x031F0209, 0x02005C00, 0x02000000, 0x02000200, 0x021F1600, 0x02000445, 0x02000000, 0x02600909, 0x02600062, 0x025D0700
			//All packets with this field starting with 0x00 or 0x01 failed. We migth have something here.
			unknown10 = binaryReader.ReadUInt32();

			//Looks like this is always 0x000000.
			//This maybe tells that the packet type is coming or maybe a field that was reserved for the Xbox 360 version.
			//Or, I don't know...
			unknown11 = binaryReader.ReadUInt32();

			//This is the packet time. Makes you happy when you see this in your hex editor. Something my eyes can read, at last!
			//There are different types like 0partystate, 0ping, 0pong, etc.
			//Sometimes there is something like an identifier or somthing after the packet type, seperated with a space.
			//Those might be used to identify 0ping packets.
			//Other than 0xxx packets, there are some others with wild names or probably with no names at all.
			//I won't be inspecting them until I'm done with the 0partystate type.
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
		public Byte unknown19;								//This does not exist if the previous byte is 0x0C

		public String StrippedPlayerName { get; set; }		//This one will not have the color codes (^0, ^1, ..., ^9)
		public bool IsHost { get; set; }					//The source of the 0partystate packet is the host
		
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

			IsHost = false;
			StrippedPlayerName = regex.Replace(playerName, "");
		}
	}
}
