using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web.Security;
using System.Web;

namespace MyLog
{
    public class LogManager
    {
        private static LogProvider _provider;
        private static LogProviderCollection _providers;

        // 是否已经初始化
        private static bool _initialized = false;

        private static void Initialize()
        {
            LogProviderConfigurationSection myLogSection = null;
            if (!_initialized)
            {
                // 读取myLog结点的配置
                myLogSection = (LogProviderConfigurationSection)ConfigurationManager.GetSection("myLog");

                if (myLogSection == null)
                    throw new Exception("没有找到myLog的配置");

                // 初始化providers
                _providers = new LogProviderCollection();
                ProvidersHelper.InstantiateProviders(myLogSection.Providers, _providers, typeof(LogProvider));

                _provider = _providers[myLogSection.DefaultProvider];
                _initialized = true;
            }
        }


        public static void Log(LogType logType, string Message)
        {
            Provider.WriteLog(logType, Message);
        }

        public static LogProvider Provider
        {
            get
            {
                Initialize();
                return _provider;
            }
        }
    }
}
