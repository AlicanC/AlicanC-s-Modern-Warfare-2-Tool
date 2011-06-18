using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading;
using System.Net;
using System.Windows.Forms;

using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

using GameWatch.Utils.Net;

using ACMW2Tool.MW2Stuff;

namespace ACMW2Tool
{
	class PacketSnifferThread
	{
		private LivePacketDevice livePacketDevice;
		private PacketCommunicator packetCommunicator;
		private Thread packetThread;
		private ToolUI toolUI;
		private Dictionary<IPAddress, MW2PartystatePlayer> partystatePlayers = new Dictionary<IPAddress, MW2PartystatePlayer>();

		private IPToCountry ip2Country = new IPToCountry();

		public PacketSnifferThread(ToolUI toolUI, LivePacketDevice livePacketDevice)
		{
			//Store passed objects
			this.toolUI = toolUI;
			this.livePacketDevice = livePacketDevice;

			//Start the thread
			packetThread = new Thread(PacketThreadStart);
			packetThread.Start();
		}

		public void End()
		{
			if (packetCommunicator != null)
				packetCommunicator.Break();
			packetThread.Abort();
		}

		private void PacketThreadStart()
		{
			//Load IP2Country
			ip2Country.Load(@"IP2Country\Databases\delegated-apnic-latest.txt");
			ip2Country.Load(@"IP2Country\Databases\delegated-arin-latest.txt");
			ip2Country.Load(@"IP2Country\Databases\delegated-lacnic-latest.txt");
			ip2Country.Load(@"IP2Country\Databases\delegated-ripencc-latest.txt");

			//Open the communicatior
			packetCommunicator = livePacketDevice.Open();

			//Set filter to listen only to port 28960
			packetCommunicator.SetFilter("port 28960");

			//Start receiving packets
			packetCommunicator.ReceivePackets(0, PacketHandler);
		}

		private void PacketHandler(Packet packet)
		{
			IpV4Datagram ipDatagram = packet.Ethernet.IpV4;

			MemoryStream memoryStream = new MemoryStream(packet.Ethernet.Payload.ToArray());

			BinaryReader binaryReader = new BinaryReader(memoryStream);

			//Read the packet header
			MW2PacketHeader packetHeader = new MW2PacketHeader(binaryReader);

			//The rest is not fully functional so we will be logging any exceptions
			try
			{
				//Read the party state header
				if (packetHeader.packetType == "0partystate")
				{
					MW2PartystateHeader partystateHeader = new MW2PartystateHeader(binaryReader);

					//Read player entries
					while (binaryReader.BaseStream.Length > binaryReader.BaseStream.Position)
					{
						MW2PartystatePlayer partyStatePlayer = new MW2PartystatePlayer(binaryReader);

						partystatePlayers[partyStatePlayer.externalIP] = partyStatePlayer;
					}
				}
			}
			catch (Exception e)
			{
				//Create the directory if it doesn't exist
				if (!Directory.Exists(@"FailedPackets"))
					Directory.CreateDirectory("FailedPackets");

				//Create files
				String filePath = @"FailedPackets\0partystate-" + DateTime.Now.Ticks;
				File.WriteAllBytes(filePath + ".bytes", packet.Ethernet.Payload.ToArray());
				File.WriteAllLines(filePath + ".txt", new String[] { e.Message, e.StackTrace });
			}

			//Close the reader and the stream
			binaryReader.Close();
			memoryStream.Close(); // Is this line really needed?


			//Lock the UI
			lock (toolUI)
			{
				ListView.ListViewItemCollection playerItems = toolUI.playerList.Items;

				//Update the source
				if (playerItems.ContainsKey(ipDatagram.Source.ToString()))
					((ListViewPlayerItem)playerItems[ipDatagram.Source.ToString()]).PlayerLastTime = DateTime.Now;
				else
					playerItems.Add(new ListViewPlayerItem(ip2Country, ipDatagram.Source));

				//Update the destination
				if (playerItems.ContainsKey(ipDatagram.Destination.ToString()))
					((ListViewPlayerItem)playerItems[ipDatagram.Destination.ToString()]).PlayerLastTime = DateTime.Now;
				else
					playerItems.Add(new ListViewPlayerItem(ip2Country, ipDatagram.Destination));

				//Update entries
				foreach (ListViewPlayerItem playerItem in playerItems)
				{
					//Remove the entry if it wasn't updated for a long time
					if (TimeSpan.FromTicks(DateTime.Now.Ticks - playerItem.PlayerLastTime.Ticks).Seconds > 15)
					{
						playerItem.Remove();
						continue;
					}


					if (partystatePlayers.ContainsKey(IPAddress.Parse(playerItem.PlayerIP)) && playerItem.PartystatePlayer == null)
						playerItem.PartystatePlayer = partystatePlayers[IPAddress.Parse(playerItem.PlayerIP)];

				}
			}
		}
	}
}
