using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

using GameWatch.Utils.Net;

using ACMW2Tool.MW2Stuff;

namespace ACMW2Tool
{
    public partial class ToolUI : Form
    {
		private PacketSnifferThread packetSnifferThread;
		private LivePacketDevice livePacketDevice = null;
        private IList<LivePacketDevice> devices = new ReadOnlyCollection<LivePacketDevice>(new List<LivePacketDevice>());

        public ToolUI()
        {
            InitializeComponent();
			
            //Try to get the devices
            try
            {
                devices = LivePacketDevice.AllLocalMachine;
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }

			//Add devices to the list
			foreach (LivePacketDevice livePacketDevice in devices)
				deviceList.Items.Add(livePacketDevice);

            //Select the first device
            if (deviceList.Items.Count > 0)
                deviceList.SelectedIndex = 0;
        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
			//Get the selected device
			livePacketDevice = (LivePacketDevice)deviceList.SelectedItem;

			//Sniff
			StartSniffing();
        }

		private void StartSniffing()
		{
			//End the packet sniffer thread
			if (packetSnifferThread != null)
				packetSnifferThread.End();

			//Clear the list
			playerList.Items.Clear();

			//Start a new thread
			packetSnifferThread = new PacketSnifferThread(this, livePacketDevice);
		}

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
			//Start sniffing again
            if (e.KeyData == Keys.F5)
				StartSniffing();
        }

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//End the packet sniffer thread
			if (packetSnifferThread != null)
				packetSnifferThread.End();
		}

		private void playerListContextStrip_Opening(object sender, CancelEventArgs e)
		{
			foreach (ToolStripItem item in playerListContextStrip.Items)
				item.Enabled = false;
		}

		private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Add support for multiple selections
			Clipboard.SetText(((ListViewPlayerItem)playerList.SelectedItems[0]).PlayerName);
		}
    }

    class ListViewPlayerItem : ListViewItem
    {
		public MW2PartystatePlayer PartystatePlayer { get; set; }

        public String PlayerIP { get; set; }

		public String PlayerName
		{
			get
			{
				//Check if we have the information of this player
				if (PartystatePlayer == null)
					return "Unknown";

				return PartystatePlayer.strippedPlayerName + "(" + PartystatePlayer.playerName + ")";
			}
		}

		private IPToCountry ip2Country;
        public String PlayerCountry
		{
			get
			{
				//Fact: This IP2C class got gold in 1964 Failure Olympics.
				try
				{
					return Countries.Instance.ISOToCountryName[ip2Country.GetCountry(PlayerIP)].ToString();
				}
				catch (Exception e)
				{
					return e.Message;
				}
			}
		}

		public String PlayerHostName
		{
			get
			{
				try
				{
					return Dns.GetHostEntry(this.PlayerIP).HostName;
				}
				catch (Exception e)
				{
					return e.Message;
				}
			}
		}

        public DateTime PlayerLastTime { get; set; }

        public ListViewPlayerItem(IPToCountry ip2Country, IpV4Address ip)
            : base(ip.ToString())
        {
			this.ip2Country = ip2Country;

            Name = ip.ToString();
			PlayerIP = ip.ToString();
            PlayerLastTime = DateTime.Now;

            SubItems.Add(PlayerName).Name= "PlayerName"; //I can has chaining in me sea sharp?
            SubItems.Add(PlayerCountry).Name = "PlayerCountry";
            SubItems.Add(PlayerHostName).Name = "PlayerHostName";
        }
    }
}
