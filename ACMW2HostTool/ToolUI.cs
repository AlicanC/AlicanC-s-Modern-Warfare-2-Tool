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

using SharpPcap;

using ACMW2Tool.Properties;
using ACMW2Tool.MW2Stuff;

namespace ACMW2Tool
{
    public partial class ToolUI : Form
    {
		private CaptureDeviceList captureDevices;
		private ICaptureDevice captureDevice;
		private PacketCaptureThread packetCaptureThread;

        public ToolUI()
        {
            InitializeComponent();

			//Change the icon and add the version to the title
			Icon = Properties.Resources.iw4sp_1;
			Text += " " + Assembly.GetExecutingAssembly().GetName().Version;

			//Device listing
			try
			{
				//Get devices
				captureDevices = SharpPcap.CaptureDeviceList.Instance;

				//Add devices to the list
				foreach (ICaptureDevice captureDevice in captureDevices)
				{
					Int32 index= deviceList.Items.Add(captureDevice);

					if (captureDevice.Description == Settings.Default.CaptureDevice)
						deviceList.SelectedIndex = index;

				}

				//Select the first device if nothing is selected
				if (deviceList.SelectedIndex==-1 && deviceList.Items.Count > 0)
					deviceList.SelectedIndex = 0;
			}
			catch (InvalidOperationException exception)
			{
				MessageBox.Show(exception.Message);
			}
        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
			//Get the selected device
			captureDevice = (ICaptureDevice)deviceList.SelectedItem;

			//Save the device description
			Settings.Default.CaptureDevice = captureDevice.Description;
			Settings.Default.Save();

			//Start capturing
			StartCapture();
        }

		private void StartCapture()
		{
			//End the packet sniffer thread
			if (packetCaptureThread != null)
				packetCaptureThread.StopCapture();

			//Clear the list
			playerList.Items.Clear();

			//Start a new thread
			packetCaptureThread = new PacketCaptureThread(captureDevice, this);
		}

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
			//Restart capturing again
            if (e.KeyData == Keys.F5)
				StartCapture();
        }

		private void ToolUI_FormClosed(object sender, FormClosedEventArgs e)
		{
			//End the packet capture thread
			if (packetCaptureThread != null)
				packetCaptureThread.StopCapture();
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
			List<String> playerNames = new List<String>();
			
			foreach(ListViewPlayerItem playerItem in playerList.SelectedItems)
				playerNames.Add(playerItem.PlayerName);

			if(playerNames.Count>0)
				Clipboard.SetText(String.Join(", ", playerNames.ToArray()));
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.ShowDialog();
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

				return PartystatePlayer.strippedPlayerName;
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

        public ListViewPlayerItem(LookupService lookupService, IPAddress ip)
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
