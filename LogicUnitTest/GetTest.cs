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
    
    [TestClass]
    public class GetTest
    {
        [TestMethod]
        public void WhenExistInDataBase()
        {
            //Arrange 
            IIpstackApi ipstackApi = new RemoteApiMock();
            IMapper mapper = AutomapperConfig.CreateConfiguration().CreateMapper();
            EntityFreamworkMoq geolocationsRepositories = new EntityFreamworkMoq();
            string ip = "0.0.0.1";

            //Act
            var logic = new GeolocationLogic(ipstackApi, mapper, geolocationsRepositories);
            var res = logic.GetValue(ip);

            //Assert
            Assert.IsTrue(res.Succesful);
            Assert.AreEqual(res.Geolocation.Ip,ip);
            geolocationsRepositories.mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void WhenNotExistInDataBase()
        {
            //Arrange 
            IIpstackApi ipstackApi = new RemoteApiMock();
            IMapper mapper = AutomapperConfig.CreateConfiguration().CreateMapper();
            EntityFreamworkMoq geolocationsRepositories = new EntityFreamworkMoq();
            string ip = "0.0.0.0";

            //Act
            var logic = new GeolocationLogic(ipstackApi, mapper, geolocationsRepositories);
            var res = logic.GetValue(ip);

            //Assert
            Assert.IsTrue(res.Succesful);
            Assert.AreEqual(res.Geolocation.Ip, ip);
            geolocationsRepositories.mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void WhenRemoteApiError()
        {
            //Arrange 
            IIpstackApi ipstackApi = new RemoteApiMock();
            IMapper mapper = AutomapperConfig.CreateConfiguration().CreateMapper();
            EntityFreamworkMoq geolocationsRepositories = new EntityFreamworkMoq();
            string ip = " ";

            //Act
            var logic = new GeolocationLogic(ipstackApi, mapper, geolocationsRepositories);
            var res = logic.GetValue(ip);

            //Assert
            Assert.IsFalse(res.Succesful);
            geolocationsRepositories.mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}
