using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clipboarder
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            AppDomain.CurrentDomain.UnhandledException += (v, e) =>
            {
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCB());
        }
    }
}
