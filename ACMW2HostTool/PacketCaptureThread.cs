using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading;
using System.Net;
using System.Windows.Forms;

using SharpPcap;
using PacketDotNet;

using ACMW2Tool.Properties;
using ACMW2Tool.MW2Packets;

namespace ACMW2Tool
{
	class PacketCaptureThread
	{
		private ICaptureDevice captureDevice;
		private ToolUI toolUI;
		private Dictionary<IPAddress, MW2PartystatePlayer> partystatePlayers = new Dictionary<IPAddress, MW2PartystatePlayer>();
		private Thread packetCaptureThread;

		private LookupService lookupService = new LookupService(Settings.Default.GeoIPDatabasePath, LookupService.GEOIP_MEMORY_CACHE);

		public PacketCaptureThread(ICaptureDevice captureDevice, ToolUI toolUI)
		{
			//Store passed objects
			this.captureDevice = captureDevice;
			this.toolUI = toolUI;

			//Start the thread
			packetCaptureThread = new Thread(PacketCaptureThreadStart);
#if DEBUG
			packetCaptureThread.Name = "Packet Capture Thread";
#endif
			packetCaptureThread.Start();
		}

		public void StopCapture()
		{
			//Stop capture
			if (captureDevice.Started)
				captureDevice.StopCapture();

			//Abort the thread
			packetCaptureThread.Abort();
		}

		private void PacketCaptureThreadStart()
		{
			//Open the device
			captureDevice.Open();

			//Set filter to capture only port 28960
			captureDevice.Filter = "udp port 28960";

			//Add the event handler
			captureDevice.OnPacketArrival += new PacketArrivalEventHandler(OnPacketArrival);

			//Start capturing
			captureDevice.StartCapture();
		}

		private void OnPacketArrival(Object sender, CaptureEventArgs captureEventArgs)
		{
			IPv4Packet ipv4Packet = (IPv4Packet)Packet.ParsePacket(LinkLayers.Ethernet, captureEventArgs.Packet.Data).PayloadPacket;

			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(ipv4Packet.Bytes)))
			{
				//This is not fully functional so we will just try
				try
				{
					//Read the packet header
					MW2PacketHeader packetHeader = new MW2PacketHeader(binaryReader);
					
					try
					{
						//Read the party state header
						if (packetHeader.packetType == "0partystate")
						{
								MW2PartystateHeader partystateHeader = new MW2PartystateHeader(binaryReader);

								//Read player entries
								while (binaryReader.BaseStream.Length > binaryReader.BaseStream.Position)
								{
									try
									{
										MW2PartystatePlayer partystatePlayer = new MW2PartystatePlayer(binaryReader);

										if (partystatePlayer.externalIP == ipv4Packet.SourceAddress
											|| partystatePlayer.internalIP == ipv4Packet.SourceAddress)
											partystatePlayer.IsHost = true;

										partystatePlayers[partystatePlayer.externalIP] = partystatePlayer;
									}
									catch (Exception e)
									{
#if DEBUG
										Program.LogGAF(packetHeader.packetType + "-player-fail-" + DateTime.Now.Ticks + ".bytes", ipv4Packet.Bytes);
										Program.LogGAF(packetHeader.packetType + "-player-fail-" + DateTime.Now.Ticks + ".txt", new String[] { e.Message, e.StackTrace });
#endif
									}
								}
						}
#if DEBUG
						Program.LogGAF(packetHeader.packetType + "-good-" + DateTime.Now.Ticks + ".bytes", ipv4Packet.Bytes);
#endif
					}
					catch (Exception e)
					{
#if DEBUG
						Program.LogGAF(packetHeader.packetType + "-fail-" + DateTime.Now.Ticks + ".bytes", ipv4Packet.Bytes);
						Program.LogGAF(packetHeader.packetType + "-fail-" + DateTime.Now.Ticks + ".txt", new String[] { e.Message, e.StackTrace });
#endif
					}
				}
				catch (Exception e)
				{
#if DEBUG
					Program.LogGAF("header-fail-" + DateTime.Now.Ticks + ".bytes", ipv4Packet.Bytes);
					Program.LogGAF("header-fail-" + DateTime.Now.Ticks + ".txt", new String[] { e.Message, e.StackTrace });
#endif
				}
			}

			//Invoke the player list to asynchronously update it
			toolUI.playerList.BeginInvoke(new UpdatePlayerListDelegate(UpdatePlayerList), ipv4Packet.SourceAddress, ipv4Packet.DestinationAddress);
		}

		public delegate void UpdatePlayerListDelegate(IPAddress sourceIP, IPAddress destinationIP);
		private void UpdatePlayerList(IPAddress sourceIP, IPAddress destinationIP)
		{
			ListView.ListViewItemCollection playerItems = toolUI.playerList.Items;

			//Update the source
			if (playerItems.ContainsKey(sourceIP.ToString()))
				((ListViewPlayerItem)playerItems[sourceIP.ToString()]).PlayerLastTime = DateTime.Now;
			else
				playerItems.Add(new ListViewPlayerItem(lookupService, sourceIP));

			//Update the destination
			if (playerItems.ContainsKey(destinationIP.ToString()))
				((ListViewPlayerItem)playerItems[destinationIP.ToString()]).PlayerLastTime = DateTime.Now;
			else
				playerItems.Add(new ListViewPlayerItem(lookupService, destinationIP));

			//Update entries
			foreach (ListViewPlayerItem playerItem in playerItems)
				if (partystatePlayers.ContainsKey(IPAddress.Parse(playerItem.PlayerIP)))
					playerItem.PartystatePlayer = partystatePlayers[IPAddress.Parse(playerItem.PlayerIP)];
		}
	}
}
