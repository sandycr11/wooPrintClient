using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using wooPrint.DesktopApp.Utils;

namespace wooPrint.DesktopApp
{
    internal static class Program
    {
        /// <summary>
        /// </summary>
        [STAThread]
        private static void Main()
        {
            SetProcessDPIAware();

            Application.CurrentCulture = new CultureInfo("es-ES");

            // ensure app run at startup
            ApplicationUtil.SetRunAtStartup("wooPrint",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wooPrint.exe"));

            var mutex = new Mutex(true, "wooPrint", out var mutexCreated);
            if (!mutexCreated)
            {
                // restore the other app window
                var processes = Process.GetProcessesByName("wooPrint")
                    .Where(p => p.Id != Process.GetCurrentProcess().Id).ToArray();

                foreach (var p in processes)
                    ProcessUtils.SendMessageToProcess(p, ProcessUtils.ProcessPassphrase);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

                mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetProcessDPIAware();
    }
}