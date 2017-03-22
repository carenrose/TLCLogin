using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLCLogin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // set DataDirectory for connectionstring
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new View.frmMdiContainer());
        }
    }
}
