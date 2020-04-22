using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "LocationsOpenAPISpecTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _npRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public TrailController(ITrailRepository trailRepo, IUserRepository userRepo, IMapper mapper)
        {
            _npRepo = trailRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list with all the trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<LocationDto>))]
        public IActionResult GetTrails()
        {
            var objList = _npRepo.GetTrails();

            var objDto = new List<TrailDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }

            return Ok(objDto);

        }

       

        /// <summary>
        /// Get individual Trail
        /// </summary>
        /// <param name="trailId">The id of the trail</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailId)
        {
            var obj = _npRepo.GetTrail(trailId);

            if(obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<TrailDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Get trails of a National Park
        /// </summary>
        /// <param name="nationalParkId">The id of the location</param>
        /// <returns></returns>
        [HttpGet("[action]/{nationalParkId:int}", Name = "GetTrailsInNationalPark")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailsInNationalPark(int nationalParkId)
        {
            var objList = _npRepo.GetTrailsInNationalPark(nationalParkId);

            if (objList == null)
            {
                return NotFound();
            }

            var objDtoList = new List<TrailDto>();

            foreach(var obj in objList)
            {
                objDtoList.Add(_mapper.Map<TrailDto>(obj));
            }

            

            return Ok(objDtoList);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] LocationCreateDto trailDto)
        {
            if(trailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_npRepo.TrailExist(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail already exist");
                return StatusCode(404, ModelState);
            }

            var trailObj = _mapper.Map<Trail>(trailDto);

            if (!_npRepo.CreateTrail(trailObj)){

                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTrail", new {trailId = trailObj.Id}, trailObj);
        }


        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trailObj = _mapper.Map<Trail>(trailDto);

            if (!_npRepo.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Deletion error on record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {

            if (!_npRepo.TrailExist(trailId))
            {
                return NotFound();
            }

            var trailObj = _npRepo.GetTrail(trailId);

            if (!_npRepo.DeleteTrail(trailObj))
            {

                ModelState.AddModelError("", $"Something went wrong when deleting the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

    }
}