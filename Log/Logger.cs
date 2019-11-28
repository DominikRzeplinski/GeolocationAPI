using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// Static representation of logger class.  
    /// </summary>
    static public class Logger
    {
        /// <summary>
        /// Static logger object
        /// </summary>
        private static ILogger _logger = null;

        /// <summary>
        /// Constructor create logger and attach to static object
        /// </summary>
        static Logger()
        {
            _logger = new NLogLogger();
        }

        /// <summary>
        /// Acces for Logger object 
        /// </summary>
        public static ILogger Log
        {
            get
            {
                return _logger;
            }
        }
    }
}
