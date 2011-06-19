using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

using System.IO;

namespace ACMW2Tool
{
    static class Program
    {
		public static string geoIPDatabasePath = "GeoIP.dat";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//Create a temporary GeoIP file if it doesn't exist
			if (!File.Exists(geoIPDatabasePath))
			{
				geoIPDatabasePath = Path.GetTempFileName();
				File.WriteAllBytes(geoIPDatabasePath, Properties.Resources.GeoIP);
			}

			Form.CheckForIllegalCrossThreadCalls = false;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ToolUI());
        }
    }
}
