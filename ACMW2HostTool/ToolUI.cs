using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

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

			//Change the icon and add the version to the title
			Icon = Properties.Resources.iw4sp_1;
			Text += " " + Assembly.GetExecutingAssembly().GetName().Version;

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

			if (playerList.SelectedItems.Count > 0)
				copyNameToolStripMenuItem.Enabled = true;
		}

		private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Add support for multiple selections
			if (playerList.SelectedItems.Count > 0)
				Clipboard.SetText(((ListViewPlayerItem)playerList.SelectedItems[0]).PlayerName);
		}
    }

    class ListViewPlayerItem : ListViewItem
    {
		private MW2PartystatePlayer partystatePlayer;
		public MW2PartystatePlayer PartystatePlayer
		{
			get
			{
				return partystatePlayer;
			}
			set
			{
				partystatePlayer = value;
				
				SubItems["PlayerName"].Text = PartystatePlayer.strippedPlayerName;
			}
		}

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

		private LookupService lookupService;
        public String PlayerCountry
		{
			get
			{
				return lookupService.getCountry(PlayerIP).getName();
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

        public ListViewPlayerItem(LookupService lookupService, IpV4Address ip)
            : base(ip.ToString())
        {
			this.lookupService = lookupService;

            Name = ip.ToString();
			PlayerIP = ip.ToString();
            PlayerLastTime = DateTime.Now;

            SubItems.Add(PlayerName).Name= "PlayerName"; //I can has chaining in me sea sharp?
            SubItems.Add(PlayerCountry).Name = "PlayerCountry";
            SubItems.Add(PlayerHostName).Name = "PlayerHostName";
        }
    }
}
