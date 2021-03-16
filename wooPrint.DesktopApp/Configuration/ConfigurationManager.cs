using System;
using System.Diagnostics;
using System.IO;
using SharpConfig;

namespace wooPrint.DesktopApp.Configuration
{
    public class ConfigurationManager
    {
        private static string _filePath;
        private static ConfigurationManager _instance;

        private ConfigurationManager()
        {
            var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "wooPrint");

            if (!Directory.Exists(appDataFolder))
                Directory.CreateDirectory(appDataFolder);

            _filePath = Path.Combine(appDataFolder, "wooPrint.cfg");
            Load();
        }

        public WooPrintConfiguration Config { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static ConfigurationManager GetInstance()
        {
            if (_instance == null) _instance = new ConfigurationManager();
            return _instance;
        }

        /// <summary>
        /// </summary>
        private void Load()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var cfg = SharpConfig.Configuration.LoadFromFile(_filePath);
                    Config = cfg["Config"].ToObject<WooPrintConfiguration>();
                }
                else
                {
                    Config = new WooPrintConfiguration();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Config = new WooPrintConfiguration();
            }
        }

        /// <summary>
        /// </summary>
        public void Save()
        {
            try
            {
                var cfg = new SharpConfig.Configuration
                {
                    Section.FromObject("Config", Config)
                };

                cfg.SaveToFile(_filePath);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }
    }

    /// <summary>
    ///     Define the Woo Print configuration.
    /// </summary>
    public class WooPrintConfiguration
    {
        /// <summary>
        ///     Define the API URl.
        /// </summary>
        public string ApiUrl { get; set; } = "";

        /// <summary>
        ///     Define the API Key.
        /// </summary>
        public string ApiKey { get; set; } = "";

        /// <summary>
        ///     Define the API Secret
        /// </summary>
        public string ApiSecret { get; set; } = "";

        /// <summary>
        ///     Define the API request timeout.
        /// </summary>
        public int ApiTimeout { get; set; } = 30;

        /// <summary>
        ///     Store the date od the last checked order.
        /// </summary>
        public string LastOrderChecked { get; set; } = "";
    }
}