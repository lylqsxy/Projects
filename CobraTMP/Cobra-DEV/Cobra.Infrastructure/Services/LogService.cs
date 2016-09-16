using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobra.Infrastructure.Contracts;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace Cobra.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private const string RootLoggerName = "(root)";

        private static readonly ILog _log4NetRootLogger = LogManager.GetLogger(RootLoggerName);
        private readonly ILog _logger;

        protected ILog Log4NetLogger
        {
            get { return _logger ?? _log4NetRootLogger; }
        }

        static LogService()
        {
            // Set up Log4Net
            XmlConfigurator.Configure();
            Hierarchy repo = (Hierarchy)LogManager.GetRepository();

            if (repo == null)
                throw new ApplicationException("LogManager repository was null. Error configuring logging.");

            Logger root = repo.Root;

            if (root == null)
                throw new ApplicationException("LogManager repository's root element was null. Error configuring logging.");

            AppenderCollection appenders = root.Appenders;

            if (appenders == null)
                throw new ApplicationException("The log4net configuration has no root appender. Error configuring logging.");

            IEnumerable<RollingFileAppender> rollingFileAppenders = appenders.OfType<RollingFileAppender>().ToArray();

            if (!rollingFileAppenders.Any())
                throw new ApplicationException("Unable to configure logging - no root appender available.");

            foreach (RollingFileAppender rollingFileAppender in rollingFileAppenders)
            {
                if (rollingFileAppender.Writer == null)
                    throw new ApplicationException(string.Format("RollingFileAppender with name '{0}' has a null writer. Error configuring logging.", rollingFileAppender.Name));

                rollingFileAppender.Writer.WriteLine();
                rollingFileAppender.Writer.WriteLine("-----------------------------------------------------------------------------");
                rollingFileAppender.Writer.WriteLine("Logging started:   {0}", DateTime.Now);
                rollingFileAppender.Writer.WriteLine("This log:          {0}", rollingFileAppender.File);
                //Will add this in production - Justin
#if !DEBUG // these values aren't really defined that well locally, so only log them in release mode
                rollingFileAppender.Writer.WriteLine("Release:           {0}", ConfigurationManager.AppSettings.Get("Version:ReleaseNumber"));
                rollingFileAppender.Writer.WriteLine("Revision:          {0}", ConfigurationManager.AppSettings.Get("Version:RevisionNumber"));
                rollingFileAppender.Writer.WriteLine("Build:             {0}", ConfigurationManager.AppSettings.Get("Version:BuildNumber"));
                rollingFileAppender.Writer.WriteLine("Build Date:        {0}", ConfigurationManager.AppSettings.Get("Version:Date"));
                rollingFileAppender.Writer.WriteLine("Deployment Number: {0}", ConfigurationManager.AppSettings.Get("Version:DeploymentNumber"));
                rollingFileAppender.Writer.WriteLine("Branch:            {0}", ConfigurationManager.AppSettings.Get("Version:Branch"));
#endif
                rollingFileAppender.Writer.WriteLine("-----------------------------------------------------------------------------");
            }
        }

        public LogService()
        {
        }

        public LogService(ILog logger)
        {
            _logger = logger;
        }

        // ------------------------------------------------
        // Logging
        // ------------------------------------------------
        public virtual void Debug(string message)
        {
            Log4NetLogger.Debug(message);
        }

        public virtual void Debug(string message, Exception e)
        {
            Log4NetLogger.Debug(message, e);
        }

        public virtual void Fatal(string message)
        {
            Log4NetLogger.Fatal(message);
        }

        public virtual void Fatal(string message, Exception e)
        {
            Log4NetLogger.Fatal(message, e);
        }

        public virtual void Info(string message)
        {
            Log4NetLogger.Info(message);
        }

        public virtual void Info(string message, Exception e)
        {
            Log4NetLogger.Info(message, e);
        }

        public virtual void Warn(string message)
        {
            Log4NetLogger.Warn(message);
        }

        public virtual void Warn(string message, Exception e)
        {
            Log4NetLogger.Warn(message, e);
        }

        public virtual void Error(string message)
        {
            Log4NetLogger.Error(message);
        }

        public virtual void Error(string message, Exception e)
        {
            Log4NetLogger.Error(message, e);
        }
    }
}
