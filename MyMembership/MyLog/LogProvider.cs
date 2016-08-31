using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.IO;

namespace MyLog
{
    public enum LogType
    {
        Warning,
        Error,
        Info
    }

    public abstract class LogProvider : ProviderBase
    {
        protected string FilePath { private set; get; }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            base.Initialize(name, config);

            FilePath = config["filePath"];
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                throw new ProviderException("没有配置日志的文件位置");
            }
        }

        public abstract void WriteLog(LogType logType, string Message);
    }
}
