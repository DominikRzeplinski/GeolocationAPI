using System.Web.Http;
using Unity;
using Unity.WebApi;
using Logic;
using RemoteApi;
using AutoMapper;
using Log;
using Repositories;

namespace GeolocationApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IGeolocationLogic, GeolocationLogic>();
            container.RegisterType<IIpstackApi, IpstackApi>();
            container.RegisterInstance<IMapper>(AutomapperConfig.CreateConfiguration().CreateMapper());
            container.RegisterType<ILogger, NLogLogger>();
            container.RegisterType<IGeolocationsRepositories, GeolocationsRepositories>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}