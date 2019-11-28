using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteApi
{
    /// <summary>
    /// Remote api interface for geolocation operations
    /// </summary>
    public interface IIpstackApi
    {
        /// <summary>
        /// Method allows to collect geolocation informations.
        /// </summary>
        /// <param name="ipAddress">Ip address or URL for which data should be collected</param>
        /// <returns>Remote Api response</returns>
        IpstackResponse Get(List<String> ipAddress);
    }
}
