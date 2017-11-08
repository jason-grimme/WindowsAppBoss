using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAppBoss.Utilities;

namespace WindowsAppBoss
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool didLaunchWindow = WindowLauncher.LaunchWindowFromStartupArguments(args);
            
            if (didLaunchWindow == false)
            {
                Application.Run(new View.Main.PackageBossMainForm());
            }
            //else
            //{
            //    Application.Run();
            //}
        }
    }
}
