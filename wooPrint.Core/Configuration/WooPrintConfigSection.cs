using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace wooPrint.Core.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class WooPrintConfiguration : WooPrintConfigurationSection, IEquatable<WooPrintConfiguration>
    {
        private static string _filePath;
        private static WooPrintConfiguration _instance;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static WooPrintConfiguration Config()
        {
            return Config(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wooPrint.exe"));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static WooPrintConfiguration Config(string path)
        {
            if (_instance != null)
                return _instance;

            _filePath = path.EndsWith(".config", StringComparison.InvariantCultureIgnoreCase)
                ? path.Remove(path.Length - 7)
                : path;

            var config = ConfigurationManager.OpenExeConfiguration(_filePath);
            if (config.Sections["wooprintConfiguration"] == null)
            {
                _instance = new WooPrintConfiguration();
                config.Sections.Add("wooprintConfiguration", _instance);
                config.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                _instance = (WooPrintConfiguration)config.Sections["wooprintConfiguration"];
            }

            return _instance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public WooPrintConfiguration Copy()
        {
            var copy = new WooPrintConfiguration();
            var xml = SerializeSection(this, "wooprintConfiguration", ConfigurationSaveMode.Full);
            XmlReader rdr = new XmlTextReader(new StringReader(xml));
            copy.DeserializeSection(rdr);
            return copy;
        }

        /// <summary>
        ///
        /// </summary>
        public void Restore()
        {
            _instance = null;
            Config();
        }

        /// <summary>
        ///
        /// </summary>
        public void Save()
        {
            var config = ConfigurationManager.OpenExeConfiguration(_filePath);
            var section = (WooPrintConfiguration)config.Sections["wooprintConfiguration"];

            section.ApiService = ApiService;

            config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(WooPrintConfiguration other)
        {
            return ApiService.Equals(other.ApiService);
        }

        #region Properties

        [Browsable(false)]
        public bool IsEnabled { get; set; }

        [ConfigurationProperty("APIService")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Servidor")]
        [Browsable(true)]
        public ApiServiceConfiguration ApiService
        {
            get { return (ApiServiceConfiguration)this["APIService"]; }
            set { this["APIService"] = value; }
        }

        #endregion Properties
    }

    /// <summary>
    ///
    /// </summary>
    public class WooPrintConfigurationSection : ConfigurationSection
    {
        [Browsable(false)]
        public new SectionInformation SectionInformation { get; set; }

        [Browsable(false)]
        public new bool LockItem { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockAllElementsExcept { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockAllAttributesExcept { get; set; }

        [Browsable(false)]
        public new System.Configuration.Configuration CurrentConfiguration { get; set; }

        [Browsable(false)]
        public new ElementInformation ElementInformation { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockElements { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockAttributes { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class WooPrintConfigurationElement : ConfigurationElement
    {
        [Browsable(false)]
        public new bool LockItem { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockAllElementsExcept { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockAllAttributesExcept { get; set; }

        [Browsable(false)]
        public new System.Configuration.Configuration CurrentConfiguration { get; set; }

        [Browsable(false)]
        public new ElementInformation ElementInformation { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockElements { get; set; }

        [Browsable(false)]
        public new ConfigurationLockCollection LockAttributes { get; set; }
    }
}