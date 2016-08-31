using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;


namespace MyLog
{
    public class LogProviderCollection : ProviderCollection
    {
        public override void Add(ProviderBase provider)
        {
            string providerTypeName;

            if (provider == null)
                throw new ArgumentNullException("provider");

            if (provider as LogProvider == null)
            {
                providerTypeName = typeof(LogProvider).ToString();
                throw new ArgumentException("Provider 必须实现 LogProvider类型", providerTypeName);
            }
            base.Add(provider);
        }

        new public LogProvider this[string name]
        {
            get
            {
                return (LogProvider)base[name];
            }
        }
    }
}
