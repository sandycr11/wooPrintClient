using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wooPrint.Core.Utils
{
    /// <summary>
    ///     Static class that contains some utils methods for applications management.
    /// </summary>
    public static class ApplicationUtil
    {
        private static readonly List<string> SoftwaresInformationRegistryKeys = new List<string>
        {
            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
            "SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall"
        };

        /// <summary>
        ///
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