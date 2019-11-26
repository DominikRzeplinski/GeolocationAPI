using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logic;
using Logic.Domain;
using Newtonsoft.Json;

namespace GeolocationApi.Controllers
{
    public class GeolocationController : ApiController
    {

        private Lazy<IGeolocationLogic> _geolocationLogic;

        public GeolocationController(
            Lazy<IGeolocationLogic> geolocationLogic) : base()
        {
            _geolocationLogic = geolocationLogic ?? throw new ArgumentNullException(nameof(geolocationLogic));
        }
        

        // GET api/values/192.168.1.1
        public IHttpActionResult Get(string ipAddress)
        {
            GeolocationDomainResult geolocationDomain = _geolocationLogic.Value.GetValue(ipAddress);
            if (!geolocationDomain.Status.Succesful)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(geolocationDomain.Status.ErrorMsg)
                };
                throw new HttpResponseException(resp);
            }
            return Json(geolocationDomain);
        }

        
        // POST api/values
        public IHttpActionResult Post([FromBody]GeolocationDomain data)
        {
            GeolocationDomainResult geolocationDomain = _geolocationLogic.Value.AddValue(data);
            if (!geolocationDomain.Status.Succesful)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(geolocationDomain.Status.ErrorMsg)
                };
                throw new HttpResponseException(resp);
            }
            return Json(geolocationDomain);
        }

        // PUT api/values/192.168.1.1
        public IHttpActionResult Put(string ipAddress, [FromBody]GeolocationDomain data)
        {
            GeolocationDomainResult geolocationDomain = _geolocationLogic.Value.PutValue(ipAddress, data);
            if (!geolocationDomain.Status.Succesful)
            {
                if (geolocationDomain.Status.ErrorCode == 404)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(geolocationDomain.Status.ErrorMsg)
                    };
                    throw new HttpResponseException(resp);
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(geolocationDomain.Status.ErrorMsg)
                    };
                    throw new HttpResponseException(resp);
                }
            }
            return Json(geolocationDomain);
        }


        // DELETE api/values/192.168.1.1
        public IHttpActionResult Delete(string ipAddress)
        {
            GeolocationDomainResult geolocationDomain = _geolocationLogic.Value.DelteValue(ipAddress);
            if (!geolocationDomain.Status.Succesful)
            {
                if (geolocationDomain.Status.ErrorCode == 404)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(geolocationDomain.Status.ErrorMsg)
                    };
                    throw new HttpResponseException(resp);
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(geolocationDomain.Status.ErrorMsg)
                    };
                    throw new HttpResponseException(resp);
                }
            }
            return Json(geolocationDomain);
        }
    }
}
