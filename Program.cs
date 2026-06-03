using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // initialize centralized theme settings
            ThemeManager.Initialize();

            // Centralized crash logging — record anything that slips through.
            Application.ThreadException += (s, e) =>
            {
                Logger.Log(e.Exception, "Unhandled UI-thread exception");
                MessageBox.Show("An unexpected error occurred and has been logged.\n\n" + e.Exception.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                Logger.Log(e.ExceptionObject as Exception, "Unhandled non-UI exception");

            Application.Run(new Login());
        }

    }
}
