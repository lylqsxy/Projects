using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Infrastructure.Contracts
{
    public interface ILogService
    {
        // Logging
        void Debug(string message);
        void Debug(string message, Exception e);

        void Info(string message);
        void Info(string message, Exception e);

        void Warn(string message);
        void Warn(string message, Exception e);

        void Error(string message);
        void Error(string message, Exception e);

        void Fatal(string message);
        void Fatal(string message, Exception e);
    }
}
