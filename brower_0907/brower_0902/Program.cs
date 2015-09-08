using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace brower_0902
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowerForm());
        }
    }
}
