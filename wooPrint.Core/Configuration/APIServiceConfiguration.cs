using System;
using System.ComponentModel;
using System.Configuration;
using wooPrint.Core.Utils;

namespace wooPrint.Core.Configuration
{
    public class ApiServiceConfiguration : WooPrintConfigurationElement, IEquatable<ApiServiceConfiguration>
    {
        [ConfigurationProperty("Url", DefaultValue = "")]
        [Browsable(true)]
        [Description("Define la url del API de Woocommerce.")]
        [Category("Configuracion de la API")]
        [DisplayName("Url de la API")]
        public string Url
        {
            get { return (string)this["Url"]; }
            set
            {
                if (!ValidationsUtil.IsValidUrl(value))
                    throw new ArgumentException("La url proporcionada no es válida.");

                this[nameof(Url)] = value;
            }
        }

        [ConfigurationProperty("APIKey", DefaultValue = "")]
        [Browsable(true)]
        [Description("Define la llave de la API de Woocommerce.")]
        [Category("Configuracion de la API")]
        [DisplayName("API Key del Woocommerce")]
        public string APIKey
        {
            get { return (string)this["APIKey"]; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El API Key proporcionado no es válido.");

                this[nameof(APIKey)] = value;
            }
        }

        [ConfigurationProperty("APISecret", DefaultValue = "")]
        [Browsable(true)]
        [Description("Define la clave de la API de Woocommerce.")]
        [Category("Configuracion de la API")]
        [DisplayName("API Secret del Woocommerce")]
        public string APISecret
        {
            get { return (string)this["APISecret"]; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El API Secret proporcionado no es válido.");

                this[nameof(APISecret)] = value;
            }
        }

        public bool Equals(ApiServiceConfiguration other)
        {
            return Url.Equals(other.Url, StringComparison.CurrentCultureIgnoreCase)
                && APIKey.Equals(other.APIKey, StringComparison.CurrentCultureIgnoreCase)
                && APISecret.Equals(other.APISecret, StringComparison.CurrentCultureIgnoreCase);
        }

        public override string ToString() => string.Empty;
    }
}