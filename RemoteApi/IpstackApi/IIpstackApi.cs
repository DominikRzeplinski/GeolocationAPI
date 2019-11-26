using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteApi
{
    public interface IIpstackApi
    {
        IpstackResponse Get(List<String> ipAddress);
    }
}
