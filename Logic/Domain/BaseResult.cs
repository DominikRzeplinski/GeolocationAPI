using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Domain
{
    public class BaseResult
    {
        public bool Succesful { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}
