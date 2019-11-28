using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// Nlog Logger class implements logger interface
    /// </summary>
    public class NLogLogger : ILogger
    {
        /// <summary>
        /// Logger 
        /// </summary>
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            logger.Log(LogLevel.Debug, message);
        }

        public void Trace(string message)
        {
            logger.Log(LogLevel.Trace, message);
        }

        public void Info(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        public void Error(string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        public void Error(Exception exception, string message = "")
        {
            logger.Log(LogLevel.Error, exception, message);
        }

        public void Fatal(string message)
        {
            logger.Log(LogLevel.Fatal, message);
        }

        public void Fatal(Exception exception, string message = "")
        {
            logger.Log(LogLevel.Fatal, exception, message);
        }
    }
}
