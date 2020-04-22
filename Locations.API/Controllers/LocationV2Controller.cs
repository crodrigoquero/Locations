using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Locations.API.Models;
using Locations.API.Models.DTOs;
using Locations.API.Repository.iRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locations.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/locations")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "LocationsOpenAPISpecNP")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class LocationV2Controller : ControllerBase
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public LocationV2Controller(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list with all the locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<LocationDto>))]
        public IActionResult GetLocations()
        {
            var obj = _npRepo.GetNationalParks().FirstOrDefault();

            return Ok(obj);

        }



    }
}