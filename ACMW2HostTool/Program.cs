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

#if DEBUG
		public static void LogGAF(String name, Byte[] bytes)
		{
			try
			{
				//Create the log directory if it doesn't exist
				String directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ACMW2TPackets\";
				Directory.CreateDirectory(directory);

				//Create the file
				File.WriteAllBytes(directory + name, bytes);
			}
			catch { }
		}

		public static void LogGAF(String name, String[] lines)
		{
			try
			{
				//Create the log directory if it doesn't exist
				String directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ACMW2TPackets\";
				Directory.CreateDirectory(directory);

				//Create the file
				File.WriteAllLines(directory + name, lines);
			}
			catch { }
		}
#endif
    }
}
