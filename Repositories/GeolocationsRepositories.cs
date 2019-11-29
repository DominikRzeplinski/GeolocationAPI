using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GeolocationsRepositories : IGeolocationsRepositories
    {
        public Entities GetGeolocationEntities()
        {
            return new Entities();
        }
    }
}
