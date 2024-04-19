using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Data;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.ExtensionMethods;
using PeakPals_Project.Services;
using System.Security.Claims;

namespace PeakPals_Project.Controllers
{
    [Route("api/community")]
    [ApiController]
    public class CommunityApiController : ControllerBase
    {
        private readonly IFitnessDataEntryService _fitnessDataEntryService;
        private readonly IClimberService _climberService;
        private readonly IClimberRepository _climberRepository;

        public CommunityApiController(IClimberService climberService, IClimberRepository climberRepository)
        {
            _climberService = climberService;
            _climberRepository = climberRepository;
        }

        [HttpGet("search/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClimberDTO))]
        public ActionResult<List<ClimberDTO>> GetUserResults(string? username)
        {
            if (username == "") {
                return BadRequest(new { Message = "Name field cannot be empty."});
            }

            if (username != null) {
                var climbersDTO = _climberRepository.GetClimbersByUsername(username);

                if (climbersDTO != null)
                {
                    return Ok(climbersDTO);
                }
                else
                {
                    return NotFound(new { Message = "No user found with this username."});
                }
                
            } else {
                return BadRequest(new { Message = "Name field cannot be empty."});
            }
        }
    }
}