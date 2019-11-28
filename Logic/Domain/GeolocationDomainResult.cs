using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Domain
{
    /// <summary>
    /// Response for methods in Geolocation Domain
    /// </summary>
    public class GeolocationDomainResult : BaseResult
    {
        public GeolocationDomain Geolocation { get; set; }
    }
}
