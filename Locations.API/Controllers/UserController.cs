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
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "LocationsOpenAPISpecTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UserController : ControllerBase
    {
        private readonly ITrailRepository _npRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserController(ITrailRepository trailRepo, IUserRepository userRepo, IMapper mapper)
        {
            _npRepo = trailRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Access GOVERMENT (HMRC) api
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{userId:int}", Name = "GetUserFromApi")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [Produces("application/json")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetUserFromApi(int userId)
        {

            var obj = await _userRepo.GetUserAsync(userId);

            if (obj == null)
            {
                return NotFound("");
            }

            var objDto = _mapper.Map<UserDto>(obj);


            return Ok(objDto);

        }

        /// <summary>
        /// Access GOVERMENT (HMRC) api
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]", Name = "GetUsersFromApi")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [Produces("application/json")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetUsersFromApi()
        {

            var objList = await _userRepo.GetUsers();

            if (objList == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<List<UserDto>>(objList);


            return Ok(objList);


        }


        /// <summary>
        /// Access GOVERMENT (HMRC) api
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]", Name = "GetLondonUsersFromApi")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [Produces("application/json")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetLondonUsersFromApi()
        {

            var objList = await _userRepo.GetUsersFromLondon();

            if (objList == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<List<UserDto>>(objList);


            return Ok(objList);


        }


    }
}