using AutoMapper;
using Locations.API.Models;
using Locations.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.LocationsApiMapper
{
    public class LocationsApiMappings : Profile
    {
        public LocationsApiMappings()
        {
            CreateMap<Location, LocationDto>().ReverseMap();

            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, LocationCreateDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

        }    
    }
}
