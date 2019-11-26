using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Domain
{
    public class GeolocationDomainResult :GeolocationDomain
    {
        public GeolocationDomainResult()
        {
            Status = new BaseResult();
        }
        public BaseResult Status { get; set; }
    }
}
