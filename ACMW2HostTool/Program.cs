using System;
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
		static public String geoIPDatabasePath;

        [STAThread]
        static void Main()
        {
			//Create a temporary GeoIP file from resource if it doesn't exist
			if (!File.Exists(Settings.Default.GeoIPDatabasePath))
				File.WriteAllBytes(geoIPDatabasePath = Path.GetTempFileName(), Properties.Resources.GeoIP);
			else
				geoIPDatabasePath = Settings.Default.GeoIPDatabasePath;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ToolUI());
        }
    }
}
