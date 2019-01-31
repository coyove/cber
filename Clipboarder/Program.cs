using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clipboarder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (v, e) =>
            {
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show(ex.ToString());
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCB());
        }
    }
}
