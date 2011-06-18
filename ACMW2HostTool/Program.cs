using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ACMW2Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form.CheckForIllegalCrossThreadCalls = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ToolUI());
        }
    }
}
