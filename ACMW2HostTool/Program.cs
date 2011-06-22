﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

using System.IO;

using ACMW2Tool.Properties;

namespace ACMW2Tool
{
    static class Program
    {
		[STAThread]
        static void Main()
        {
			//Create a temporary GeoIP file from resource if it doesn't exist
			if (!File.Exists(Settings.Default.GeoIPDatabasePath = "GeoIP.dat"))
				File.WriteAllBytes(Settings.Default.GeoIPDatabasePath = Path.GetTempFileName(), Properties.Resources.GeoIP);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ToolUI());
        }
    }
}
