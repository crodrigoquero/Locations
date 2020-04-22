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
    [Route("api/v{version:apiVersion}/locations")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "LocationsOpenAPISpecNP")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class LocationController : ControllerBase
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public LocationController(INationalParkRepository npRepo, IMapper mapper)
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
            var objList = _npRepo.GetNationalParks();

            var objDto = new List<LocationDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<LocationDto>(obj));
            }

            return Ok(objDto);

        }

        /// <summary>
        /// Get individual location 
        /// </summary>
        /// <param name="locationId">The id of the location</param>
        /// <returns></returns>
        [HttpGet("{locationId:int}", Name = "GetLocation")]
        [ProducesResponseType(200, Type = typeof(LocationDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetLocation(int locationId)
        {
            var obj = _npRepo.GetNationalPark(locationId);

            if(obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<LocationDto>(obj);

            return Ok(objDto);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LocationDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] LocationDto nationalParkDto)
        {
            if(nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_npRepo.NationalParkExist(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "Location already exist");
                return StatusCode(404, ModelState);
            }

            var nationalParkObj = _mapper.Map<Location>(nationalParkDto);

            if (!_npRepo.CreateNationalPark(nationalParkObj)){

                ModelState.AddModelError("", $"Something went wrong when saving the the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetLocations", new {nationalParkId = nationalParkObj.Id}, nationalParkObj);
        }


        [HttpPatch("{locationId:int}", Name = "UpdateLocation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLocation(int locationId, [FromBody] LocationDto nationalParkDto)
        {
            if (nationalParkDto == null || locationId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<Location>(nationalParkDto);

            if (!_npRepo.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

        [HttpDelete("{locationId:int}", Name = "DeleteLocation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLocation(int locationId)
        {

            if (!_npRepo.NationalParkExist(locationId))
            {
                return NotFound();
            }

            var nationalParkObj = _npRepo.GetNationalPark(locationId);

            if (!_npRepo.DeleteNationalPark(nationalParkObj))
            {

                ModelState.AddModelError("", $"Something went wrong when deleting the the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

    }
}