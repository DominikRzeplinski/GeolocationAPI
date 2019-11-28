using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Log;

namespace RemoteApi
{
    public class IpstackApi : IIpstackApi
    {
        public IpstackApi()
        {

        }
        /// <summary>
        /// Call remote API ipstack to retrive information about ip geolocation.
        /// </summary>
        /// <param name="ipAddress">List of ip address or urls to retrive geolocation data</param>
        public IpstackResponse Get(List<string> ipAddress)
        {
            IpstackParameters ipstackParameters = new IpstackParameters();
            ipstackParameters.AccessKey = GetAuthorisationKey();
            bool first = true;
            foreach (var addres in ipAddress)
            {
                if (!first)
                    ipstackParameters.IpAddresses.Add(",");
                first = false;
                ipstackParameters.IpAddresses.Add(addres);
            }
            IpstackResponse ipstackResponse = new IpstackResponse();
            try
            {
                WebRequest webReq = WebRequest.Create(GetApiUrl() + String.Join(String.Empty, ipstackParameters.IpAddresses) + "?access_key=" + GetAuthorisationKey());
                webReq.Timeout = GetApiTimeout();
                webReq.Method = "GET";
                webReq.ContentType = "";
                webReq.ContentLength = 0;
                WebResponse response = webReq.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    ipstackResponse.ipstackData = JsonConvert.DeserializeObject<IpstackData>(responseFromServer,new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Ignore });
                    ipstackResponse.ipstackErrorData = JsonConvert.DeserializeObject<IpstackErrorData>(responseFromServer, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
            }
            catch (Exception ex)
            {
                ipstackResponse.ipstackErrorData.success = false; 
                ipstackResponse.ipstackErrorData.error.code = -1; 
                ipstackResponse.ipstackErrorData.error.info = ex.Message;
                Logger.Log.Error(ex);
            }
            
            return ipstackResponse;
        }

        /// <summary>
        /// Retrive api key
        /// </summary>
        /// <returns>api key</returns>
        private string GetAuthorisationKey()
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["IpstackKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                Logger.Log.Error("Webconfig does not contain ipstack key.");
            return apiKey;
        }
        /// <summary>
        /// Retrive api url
        /// </summary>
        /// <returns>api url</returns>
        private string GetApiUrl()
        {
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["IpstackUrl"];
            if (string.IsNullOrWhiteSpace(apiUrl))
                Logger.Log.Error("Webconfig does not contain ipstack url.");
            return apiUrl;
        }
        /// <summary>
        /// Retrive api connection timeout
        /// </summary>
        /// <returns>api connection timeout</returns>
        private int GetApiTimeout()
        {
            string apiTimeout = System.Configuration.ConfigurationManager.AppSettings["IpstackTimeOut"];
            if (string.IsNullOrWhiteSpace(apiTimeout))
                Logger.Log.Error("Webconfig does not contain ipstack connection timeout.");
            return Convert.ToInt32(apiTimeout);
        }

    }
}
