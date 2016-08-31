using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MyLog
{
    public class LogProviderConfigurationSection : ConfigurationSection
    {
        private readonly ConfigurationProperty defaultProvider =
            new ConfigurationProperty("defaultProvider", typeof(string), null);

        private readonly ConfigurationProperty providers =
            new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);

        private ConfigurationPropertyCollection properties =
            new ConfigurationPropertyCollection();

        public LogProviderConfigurationSection()
        {
            properties.Add(providers);
            properties.Add(defaultProvider);
        }

        [ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get { return (string)base[defaultProvider]; }
            set { base[defaultProvider] = value; }
        }

        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base[providers]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return properties; }
        }
    }
}
