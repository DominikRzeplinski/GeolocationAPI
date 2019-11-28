using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// Interface for logging class
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Debug level message
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// Trace level message
        /// </summary>
        /// <param name="message"></param>
        void Trace(string message);
        /// <summary>
        /// Info level message
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        /// <summary>
        /// Error level message
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// Error level message
        /// </summary>
        /// <param name="exception">Exception to log</param>
        /// <param name="message">Message to log</param>
        void Error(Exception exception, string message = "");
        /// <summary>
        /// Fatal level message
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);
        /// <summary>
        /// Fatal level message
        /// </summary>
        /// <param name="exception">Exception to log</param>
        /// <param name="message">Message to log</param>
        void Fatal(Exception exception, string message = "");
    }
}

