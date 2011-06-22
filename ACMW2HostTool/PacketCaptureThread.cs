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
				//Read the packet header
				MW2PacketHeader packetHeader = new MW2PacketHeader(binaryReader);

				//The rest is not fully functional so we will be logging any exceptions
				try
				{
					//Read the party state header
					if (packetHeader.packetType == "0partystate")
					{
						/* DEBUG */System.Diagnostics.Debug.WriteLine("0partystate from {0}", ipv4Packet.SourceAddress);

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
					if (!Directory.Exists("FailedPackets"))
						Directory.CreateDirectory("FailedPackets");

					//Create files
					String filePath = @"FailedPackets\0partystate-" + DateTime.Now.Ticks;
					//File.WriteAllBytes(filePath + ".bytes", packet.Ethernet.Payload.ToArray());
					File.WriteAllLines(filePath + ".txt", new String[] { e.Message, e.StackTrace });
				}
			}

			//Invoke the player list to asynchronously update it
			//toolUI.playerList.BeginInvoke(new UpdatePlayerListDelegate(UpdatePlayerList), new Object[] { ipv4Packet.SourceAddress, ipv4Packet.DestinationAddress });
			toolUI.playerList.BeginInvoke(new UpdatePlayerListDelegate(UpdatePlayerList), ipv4Packet.SourceAddress, ipv4Packet.DestinationAddress);
		}

		public delegate void UpdatePlayerListDelegate(IPAddress sourceIP, IPAddress destinationIP);
		private void UpdatePlayerList(IPAddress sourceIP, IPAddress destinationIP)
		{
			ListView.ListViewItemCollection playerItems = toolUI.playerList.Items;

			//Update the source
			if (!playerItems.ContainsKey(sourceIP.ToString()))
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
