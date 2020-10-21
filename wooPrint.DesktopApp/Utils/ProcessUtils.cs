using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace wooPrint.DesktopApp.Utils
{
    public static class ProcessUtils
    {
        public static bool IsProcessRunning(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
                return false;

            try
            {
                foreach (var proc in Process.GetProcesses())
                {
                    try
                    {
                        if (proc.ProcessName.Equals(processName, StringComparison.InvariantCultureIgnoreCase))
                            return true;
                    }
                    catch
                    {
                        // ignored
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }

        public static bool IsProcessRunning(string processName, string processPath)
        {
            if (IsProcessRunning(processName))
                return true;

            if (string.IsNullOrWhiteSpace(processPath))
                return false;

            try
            {
                foreach (var proc in Process.GetProcesses())
                {
                    try
                    {
                        if (proc.MainModule.FileName.Equals(processName, StringComparison.InvariantCultureIgnoreCase))
                            return true;
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }

        public static Process GetProcess(string processName)
        {
            var processList = Process.GetProcesses();
            foreach (var proc in processList)
            {
                try
                {
                    if (proc.MainModule.ModuleName.Equals(processName, StringComparison.InvariantCultureIgnoreCase))
                        return proc;
                }
                catch
                {
                    // ignored
                }
            }
            return null;
        }

        ///// <summary>
        ///// Bring the process windows to the front.
        ///// </summary>
        ///// <param name="process">The process.</param>
        //public static void BringToFront(Process process)
        //{
        //    foreach (var handle in EnumerateProcessWindowHandles(process.Id))
        //    {
        //        if (IsIconic(handle))
        //        {
        //            ShowWindow(handle, SW_RESTORE);
        //        }

        //        SetForegroundWindow(handle);
        //    }
        //}

        //private const int SW_RESTORE = 9;

        //[DllImport("User32.dll")]
        //private static extern bool SetForegroundWindow(IntPtr handle);

        //[DllImport("User32.dll")]
        //private static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        //[DllImport("User32.dll")]
        //private static extern bool IsIconic(IntPtr handle);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        public const int WM_USER = 0x400;
        public const int WM_COPYDATA = 0x4A;

        public const string ProcessPassphrase = "restoreWindow";

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                EnumThreadWindows(thread.Id, (hWnd, lParam) =>
                {
                    handles.Add(hWnd); return true;
                }, IntPtr.Zero);

            return handles;
        }

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        /// <summary>
        /// Send a message to the process.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int SendMessageToProcess(Process process, string message)
        {
            int result = 0;

            foreach (var handle in EnumerateProcessWindowHandles(process.Id))
            {
                if (handle.ToInt32() > 0)
                {
                    byte[] sarr = Encoding.Default.GetBytes(message);
                    int len = sarr.Length;

                    COPYDATASTRUCT cds;
                    cds.dwData = (IntPtr)100;
                    cds.lpData = message;
                    cds.cbData = len + 1;

                    result = result & SendMessage(handle.ToInt32(), WM_COPYDATA, 0, ref cds);
                }
            }

            return result;
        }

        /// <summary>
        /// Receive a process a message.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string ReceiveMessage(ref Message m)
        {
            string message = null;

            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                    message = mystr.lpData;
                    break;
            }

            return message;
        }
    }
}