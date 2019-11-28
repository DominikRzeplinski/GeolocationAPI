using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Repositories;
using Logic.Domain;
using RemoteApi;

namespace GeolocationApi
{
    public class AutomapperConfig
    {
        public static MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<geolocations, GeolocationDomain>(MemberList.Source);
                cfg.CreateMap<GeolocationDomain, geolocations>(MemberList.Source)
                    .ForMember(dest => dest.id, opt => opt.Ignore());
                cfg.CreateMap<IpstackData, GeolocationDomain>(MemberList.Source)
                    .ForMember(dest => dest.ContinentCode, opt => opt.MapFrom(src => src.continent_code))
                    .ForMember(dest => dest.ContinentName, opt => opt.MapFrom(src => src.continent_name))
                    .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.country_code))
                    .ForMember(dest => dest.RegionCode, opt => opt.MapFrom(src => src.region_code))
                    .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.region_name));
            });
            return config;
        }
    }
    
}