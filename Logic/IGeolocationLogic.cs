using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Domain;
namespace Logic
{
    /// <summary>
    /// Interface for geolocation logic 
    /// </summary>
    public interface IGeolocationLogic
    {
        /// <summary>
        /// Collect data about geolocation
        /// </summary>
        /// <param name="ipAddress">Ip address or Url</param>
        /// <returns>Status of operation with geolocation data</returns>
        GeolocationDomainResult GetValue(string ipAddress);
        /// <summary>
        /// Remove stored data
        /// </summary>
        /// <param name="ipAddress">Ip address or Url</param>
        /// <returns>Status of operation with geolocation data</returns>
        GeolocationDomainResult DelteValue(string ipAddress);
        /// <summary>
        /// Add information about geolocation
        /// </summary>
        /// <param name="data">data to add</param>
        /// <returns>Status of operation with geolocation data</returns>
        GeolocationDomainResult AddValue(GeolocationDomain data);
        /// <summary>
        /// Updata information about geolocation
        /// </summary>
        /// <param name="data">data to update</param>
        /// <returns>Status of operation with geolocation data</returns>
        GeolocationDomainResult PutValue(GeolocationDomain data);
    }
}
