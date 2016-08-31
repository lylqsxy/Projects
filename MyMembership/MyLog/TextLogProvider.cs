using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLog
{
    public class TextLogProvider : LogProvider
    {
        public override void WriteLog(LogType logType, string message)
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var sw = new StreamWriter(FilePath, true))
            {
                string s = string.Format("{0}, {1}, {2}", DateTime.Now, logType.ToString(), message);
                sw.WriteLine(s);
            }
        }
    }

    public class XmlLogProvider : LogProvider
    {
        public override void WriteLog(LogType logType, string message)
        {
            throw new NotImplementedException();
        }
    }
}
