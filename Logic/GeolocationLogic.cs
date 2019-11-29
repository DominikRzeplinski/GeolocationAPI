using System;
using System.Collections.Generic;
using System.Linq;
using RemoteApi;
using Logic.Domain;
using Repositories;
using AutoMapper;
using Log;
namespace Logic
{
    /// <summary>
    /// Class implement logic for geolocation domain
    /// </summary>
    public class GeolocationLogic : IGeolocationLogic
    {
        IIpstackApi _ipstackApi;
        IMapper _mapper;
        IGeolocationsRepositories _geolocationsRepositories;
        /// <summary>
        /// Constructor with DI
        /// </summary>
        /// <param name="ipstackApi">DI class for remote API</param>
        /// <param name="mapper">DI class for mapping data</param>
        /// <param name="geolocationsRepositories">DI class for EF</param>
        public GeolocationLogic(IIpstackApi ipstackApi, IMapper mapper, IGeolocationsRepositories geolocationsRepositories)
        {
            _ipstackApi = ipstackApi ?? throw new ArgumentNullException(nameof(ipstackApi));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _geolocationsRepositories = geolocationsRepositories ?? throw new ArgumentNullException(nameof(geolocationsRepositories));
        }
        /// <summary>
        /// Add geolocation data to database
        /// </summary>
        /// <param name="data">geolocation data</param>
        /// <returns>status of operation with added geolocation data</returns>
        public GeolocationDomainResult AddValue(GeolocationDomain data)
        {
            GeolocationDomainResult geolocationDomainResult = new GeolocationDomainResult();
            geolocationDomainResult.Succesful = true;
            using (var db = _geolocationsRepositories.GetGeolocationEntities())
            {
                try
                {
                    geolocations g = new geolocations();
                    g = db.geolocations.Where(p => p.ip == data.Ip).FirstOrDefault();
                    if (g == null)
                    {
                        g = new geolocations();
                        _mapper.Map(data,g);
                        db.geolocations.Add(g);
                        db.SaveChanges();
                        geolocationDomainResult.Geolocation = new GeolocationDomain();
                        _mapper.Map(g, geolocationDomainResult.Geolocation);
                    }
                    else
                    {
                        geolocationDomainResult.ErrorCode = 302;
                        geolocationDomainResult.ErrorMsg = "The given ip address exist.";
                        geolocationDomainResult.Succesful = false;
                    }
                }
                catch (Exception ex)
                {
                    geolocationDomainResult.ErrorCode = 0;
                    geolocationDomainResult.ErrorMsg = "Internal DataBase Error.";
                    geolocationDomainResult.Succesful = false;
                    Logger.Log.Error(ex);
                }
            }
            return geolocationDomainResult;
        }
        /// <summary>
        /// Remove geolocation information from database
        /// </summary>
        /// <param name="ipAddress">Ip address or URL</param>
        /// <returns>status of operation with deleted geolocation data</returns>
        public GeolocationDomainResult DelteValue(string ipAddress)
        {
            GeolocationDomainResult geolocationDomainResult = new GeolocationDomainResult();
            geolocationDomainResult.Succesful = true;
            using (var db = _geolocationsRepositories.GetGeolocationEntities())
            {
                try
                {
                    geolocations g = new geolocations();
                    g = db.geolocations.Where(p => p.ip == ipAddress).FirstOrDefault();
                    if (g != null)
                    {
                        geolocationDomainResult.Geolocation = new GeolocationDomain();
                        _mapper.Map(g, geolocationDomainResult.Geolocation);
                        db.geolocations.Remove(g);
                        db.SaveChanges();
                    }
                    else
                    {
                        geolocationDomainResult.ErrorCode = 404;
                        geolocationDomainResult.ErrorMsg = "The given ip address doesn't exist.";
                        geolocationDomainResult.Succesful = false;
                    }
                }
                catch (Exception ex)
                {
                    geolocationDomainResult.ErrorCode = 0;
                    geolocationDomainResult.ErrorMsg = "Internal DataBase Error.";
                    geolocationDomainResult.Succesful = false;
                    Logger.Log.Error(ex);
                }
            }
            return geolocationDomainResult;
        }
        /// <summary>
        /// Method allow collect information about geolocation.
        /// If @ipAddress exist in database the collection is get from there. 
        /// If @ipAddress not exist in database remote API is called and resoult is stored in database.
        /// </summary>
        /// <param name="ipAddress">Ip addres or URL</param>
        /// <returns>status of operation with collected geolocation data</returns>
        public GeolocationDomainResult GetValue(string ipAddress)
        {


            GeolocationDomainResult geolocationDomainResult = new GeolocationDomainResult();
            geolocationDomainResult.Succesful = true;

            #region ReadFromDataBase
            using (var db = _geolocationsRepositories.GetGeolocationEntities())
            {
                try
                {
                    geolocations g = new geolocations();
                    g = db.geolocations.Where(p => p.ip == ipAddress).FirstOrDefault();
                    if (g != null)
                    {
                        geolocationDomainResult.Geolocation = new GeolocationDomain();
                        _mapper.Map(g, geolocationDomainResult.Geolocation);
                        return geolocationDomainResult;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex);
                }
            }
            #endregion

            #region CallRemoteApi
            List<string> ipAddresses = new List<string> { ipAddress };
            var response = _ipstackApi.Get(ipAddresses);
            geolocationDomainResult.Succesful = response.ipstackErrorData.success;
            if (geolocationDomainResult.Succesful)
            {
                geolocationDomainResult.Geolocation = new GeolocationDomain();
                _mapper.Map(response.ipstackData, geolocationDomainResult.Geolocation);
                using (var db = _geolocationsRepositories.GetGeolocationEntities())
                {
                    ///Geolocation api alweys return ip not url
                    ///in database store ip/url from input 
                    geolocations g = new geolocations();
                    _mapper.Map(geolocationDomainResult.Geolocation, g);
                    g.ip = ipAddress;
                    try
                    {
                        db.geolocations.Add(g);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex);
                    }
                }
            }
            else
            {
                geolocationDomainResult.ErrorMsg = "Remote api IpStack returend error: " + response.ipstackErrorData.error.info;
                Logger.Log.Error(geolocationDomainResult.ErrorMsg);
            }
            #endregion

            return geolocationDomainResult;
        }

        /// <summary>
        /// Update existing geolocation in database
        /// </summary>
        /// <param name="data">geolocation data</param>
        /// <returns>status of operation with updated geolocation data</returns>
        public GeolocationDomainResult PutValue(GeolocationDomain data)
        {
            GeolocationDomainResult geolocationDomainResult = new GeolocationDomainResult();
            geolocationDomainResult.Succesful = true;
            using (var db = _geolocationsRepositories.GetGeolocationEntities())
            {
                try
                {
                    geolocations g = new geolocations();
                    g = db.geolocations.Where(p => p.ip == data.Ip).FirstOrDefault();
                    if (g != null)
                    {
                        geolocationDomainResult.Geolocation = new GeolocationDomain();
                        geolocationDomainResult.Geolocation = data;
                        _mapper.Map(data, g);
                        db.SaveChanges();
                    }
                    else
                    {
                        geolocationDomainResult.ErrorCode = 404;
                        geolocationDomainResult.ErrorMsg = "The given ip address doesn't exist.";
                        geolocationDomainResult.Succesful = false;
                    }
                }
                catch (Exception ex)
                {
                    geolocationDomainResult.ErrorCode = 0;
                    geolocationDomainResult.ErrorMsg = "Internal DataBase Error.";
                    geolocationDomainResult.Succesful = false;
                    Logger.Log.Error(ex);
                }
            }
            return geolocationDomainResult;
        }
    }
}
