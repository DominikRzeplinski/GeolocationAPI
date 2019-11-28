using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Domain
{
    /// <summary>
    /// Result for logic methods
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// indicator for succeful state of method execution
        /// </summary>
        public bool Succesful { get; set; }
        /// <summary>
        /// Error code.
        /// Should correspond to HTTP response
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Additional msg
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
