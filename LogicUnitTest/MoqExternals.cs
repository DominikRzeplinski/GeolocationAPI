using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using RemoteApi;
using System.Collections.Generic;
using AutoMapper;
using GeolocationApi;
using Logic.Domain;
using Repositories;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace LogicUnitTest
{
    /// <summary>
    /// Moq remote Api IpstackApi
    /// </summary>
    public class RemoteApiMock : IIpstackApi
    {
        /// <summary>
        /// Simulate connection with remote API,  
        /// If passed empty string method will return error. 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Succes - one address pased, Faild - empty string passed</returns>
        public IpstackResponse Get(List<string> ipAddress)
        {
            IpstackResponse ipstackResponse = new IpstackResponse();
            if (string.IsNullOrWhiteSpace(ipAddress[0]))
            {
                ipstackResponse.ipstackErrorData = new IpstackErrorData
                {
                    success = false,
                    error = new Error()
                };
            }
            else
            {
                ipstackResponse.ipstackErrorData = new IpstackErrorData
                {
                    success = true
                };
                ipstackResponse.ipstackData = new IpstackData
                {
                    city = "City",
                    ip = ipAddress[0],
                    type = "mock",
                    continent_code = "C",
                    continent_name = "N",
                    country_code = "CC",
                    region_code = "RC",
                    region_name = "RN",
                    zip = "00",
                    latitude = 1,
                    longitude = 1
                };
            }
            return ipstackResponse;
        }
    }

    /// <summary>
    /// Class contains defoult geolocation data
    /// </summary>
    public class DefoultGeolocation
    {
        public static geolocations GetDef1()
        {
            return new geolocations
            {
                ip = "0.0.0.1",
                city = "City",
                ipType = "mock",
                continentCode = "C",
                continentName = "N",
                countryCode = "CC",
                regionCode = "RC",
                regionName = "RN",
                zip = "00",
                latitude = 1,
                longitude = 1
            };
        }
        public static geolocations GetDef2()
        {
            return new geolocations
            {
                ip = "0.0.0.2",
                city = "City",
                ipType = "mock",
                continentCode = "C",
                continentName = "N",
                countryCode = "CC",
                regionCode = "RC",
                regionName = "RN",
                zip = "00",
                latitude = 1,
                longitude = 1
            };
        }
    }

    /// <summary>
    /// Class to moq entity freamwork
    /// Initialized with DefoultGeolocation
    /// </summary>
    public class EntityFreamworkMoq : IGeolocationsRepositories
    {
        private Mock<DbSet<geolocations>> mockDbSet;
        public Mock<Entities> mockContext;
        private IQueryable<geolocations> data;
        public EntityFreamworkMoq()
        {
            mockDbSet = new Mock<DbSet<geolocations>>();
            mockContext = new Mock<Entities>();
            SetDataDefoult();
            mockContext.Setup(m => m.geolocations).Returns(mockDbSet.Object);

        }
        public void SetDataDefoult()
        {
            data = new List<geolocations> {
               DefoultGeolocation.GetDef1(),
               DefoultGeolocation.GetDef2()
            }.AsQueryable();
            mockDbSet.As<IQueryable<geolocations>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<geolocations>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<geolocations>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<geolocations>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }

        public Entities GetGeolocationEntities()
        {
            return mockContext.Object;
        }
    }
}
