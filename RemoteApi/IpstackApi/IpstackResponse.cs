using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteApi
{   
    /// <summary>
    /// Container for Connection data
    /// </summary>
    public class Connection
    {
        public decimal asn { get; set; }
        public string isp { get; set; }
    }
    /// <summary>
    /// Container for courrency
    /// </summary>
    public class Courrency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string plural { get; set; }
        public string symbol { get; set; }
        public string symbol_native { get; set; }
    }
    /// <summary>
    /// Container for time zone
    /// </summary>
    public class TimeZoneIp
    {
        public string id { get; set; }
        public string current_time { get; set; }
        public decimal gmt_offset { get; set; }
        public string code { get; set; }
        public bool is_daylight_saving { get; set; }
    }
    /// <summary>
    /// Container for languages
    /// </summary>
    public class Languages
    {
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
    }
    /// <summary>
    /// Container for location data
    /// </summary>
    public class Location
    {
        public Location()
        {
            languages = new List<Languages>();
        }
        public decimal geoname_id { get; set; }
        public string capital { get; set; }
        public List<Languages> languages { get; set; }
        public string country_flag { get; set; }
        public string country_flag_emoji { get; set; }
        public string country_flag_emoji_unicode { get; set; }
        public string calling_code { get; set; }
        public bool is_eu { get; set; }
    }
    /// <summary>
    /// Container for single ip data
    /// </summary>
    public class IpstackData
    {
        public string ip { get; set; }
        public string type { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_code { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public Location location { get; set; }
        public TimeZoneIp time_zone { get; set; }
        public Courrency courrency { get; set; }
        public Connection connection { get; set; }
    }


    public class Error
    {
        public decimal code { get; set; }
        public string type { get; set; }
        public string info { get; set; }
    }

    /// <summary>
    /// Container for error response 
    /// </summary>
    public class IpstackErrorData
    {
        public IpstackErrorData()
        {
            success = true; 
        }
        public bool success { get; set; }
        public Error error { get; set; }
    }

    /// <summary>
    /// Container for response data
    /// </summary>
    public class IpstackResponse
    {
        public IpstackErrorData ipstackErrorData { get; set; }
        public IpstackData ipstackData { get; set; }
    }


}
