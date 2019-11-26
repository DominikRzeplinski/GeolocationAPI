using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Domain;
namespace Logic
{
    public interface IGeolocationLogic
    {
        GeolocationDomainResult GetValue(string ipAddress);
        GeolocationDomainResult DelteValue(string ipAddress);
        GeolocationDomainResult AddValue(GeolocationDomain data);
        GeolocationDomainResult PutValue(string ipAddress, GeolocationDomain data);
    }
}
