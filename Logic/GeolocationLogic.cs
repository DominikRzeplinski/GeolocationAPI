using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteApi;
using Logic.Domain;

namespace Logic
{
    public class GeolocationLogic : IGeolocationLogic
    {
        IIpstackApi _ipstackApi;
        public GeolocationLogic(IIpstackApi ipstackApi)
        {
            _ipstackApi = ipstackApi ?? throw new ArgumentNullException(nameof(ipstackApi));
        }

        public GeolocationDomainResult AddValue(GeolocationDomain data)
        {
            throw new NotImplementedException();
        }

        public GeolocationDomainResult DelteValue(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public GeolocationDomainResult GetValue(string ipAddress)
        {
            List<string> ipAddresses = new List<string> { ipAddress };
            var response = _ipstackApi.Get(ipAddresses);
            GeolocationDomainResult geolocationDomain = new GeolocationDomainResult();
            geolocationDomain.Status.Succesful = response.ipstackErrorData.success;
            
            #region MapppingResponse 
            if (geolocationDomain.Status.Succesful)
            {
                geolocationDomain.City = response.ipstackData.city;
                geolocationDomain.ContinentCode = response.ipstackData.continent_code;
                geolocationDomain.ContinentName= response.ipstackData.continent_name;
                geolocationDomain.Ip= response.ipstackData.ip;
                geolocationDomain.CountryCode = response.ipstackData.country_code;
                geolocationDomain.Latitude= response.ipstackData.latitude;
                geolocationDomain.Longitude= response.ipstackData.longitude;
                geolocationDomain.RegionCode= response.ipstackData.region_code;
                geolocationDomain.RegionName= response.ipstackData.region_name;
                geolocationDomain.Type= response.ipstackData.type;
                geolocationDomain.Zip= response.ipstackData.zip;
            }
            else
            {
                geolocationDomain.Status.ErrorMsg = response.ipstackErrorData.error.info;
            }
            #endregion

            return geolocationDomain;
        }

        public GeolocationDomainResult PutValue(string ipAddress, GeolocationDomain data)
        {
            throw new NotImplementedException();
        }
    }
}
