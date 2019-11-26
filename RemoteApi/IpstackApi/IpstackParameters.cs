using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteApi
{
    /// <summary>
    /// Container for request parameters/
    /// </summary>
    public class IpstackParameters
    {
        public IpstackParameters()
        {
            IpAddresses = new List<string>();
        }
        ///Required Section
        ///<value>List of ip adresses or urls</value>
        public List<string> IpAddresses { get; set; }
        ///<value>Authorisation token</value>
        public string AccessKey { get; set; }
        ///Optional not implemented
    }
}
