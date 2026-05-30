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
            //Application.Run(new Payment());
            //Application.Run(new userdashboard());
            //Application.Run(new Form1());
            //Application.Run(new signupform());
            //  Application.Run(new heartfulsharingform());
              Application.Run(new Login());
            //  Application.Run(new Pdf());
            // Application.Run(new heartfulsharingdash());
            //  Application.Run(new managecatagorydashboard());
          //  Application.Run(new AddBooks ());

        }

    }
}
