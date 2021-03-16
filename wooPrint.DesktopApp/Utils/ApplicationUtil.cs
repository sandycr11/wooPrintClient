using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace wooPrint.DesktopApp.Utils
{
    /// <summary>
    ///     Static class that contains some utils methods for applications management.
    /// </summary>
    public static class ApplicationUtil
    {
        /// <summary>
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="appPath"></param>
        public static void SetRunAtStartup(string appName, string appPath)
        {
            const string registrySubKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(registrySubKey,
                    true);
                registryKey?.SetValue(appName, appPath);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"error setting the key {appName} in {registrySubKey}. error: {ex}");
            }
        }
    }
}