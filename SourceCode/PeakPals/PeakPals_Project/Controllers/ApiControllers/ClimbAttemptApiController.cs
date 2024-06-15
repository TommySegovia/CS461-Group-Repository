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
using Microsoft.AspNetCore.Identity;
using PeakPals_Project.Areas.Identity.Data;
using Microsoft.IdentityModel.Tokens;


namespace PeakPals_Project.Controllers
{
    [Route("api/climb")]
    [ApiController]
    public class ClimbAttemptApiController : ControllerBase
    {
        private readonly IClimbAttemptRepository _climbAttemptRepository;
        private readonly IClimberRepository _climberRepository;
        private readonly IClimberService _climberService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClimbAttemptApiController> _logger;

        public ClimbAttemptApiController(IClimbAttemptRepository climbAttemptRepository, IClimberRepository climberRepository, IClimberService climberService, UserManager<ApplicationUser> userManager, ILogger<ClimbAttemptApiController> logger)
        {
            _climbAttemptRepository = climbAttemptRepository;
            _climberRepository = climberRepository;
            _userManager = userManager;
            _climberService = climberService;
            _logger = logger;
        }

        [HttpGet("log/view")]
        public ActionResult<List<ClimbAttemptDTO>> ViewAllClimbingAttemptsByUser()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null)
                {
                    return NotFound(new { Message = "No climber associated with this account." });
                }
                var climbAttemptsList = _climbAttemptRepository.ViewAllClimbingAttempts(climberDTO.Id);
                if (climbAttemptsList.IsNullOrEmpty())
                {
                    return NotFound(new { Message = "No climb attempts logged or found so far." });
                }

                return Ok(climbAttemptsList);
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        [HttpPost("log/view/list/{climbers}")]
        public ActionResult<List<ClimbAttemptDTO>> ViewAllClimbingAttemptsByListOfClimbers(List<Climber> climbers)
        {
            var climbAttemptsList = new List<ClimbAttemptDTO>();
            foreach (var climber in climbers)
            {
                var climberDTO = _climberRepository.GetClimberByUsername(climber.UserName);
                if (climberDTO == null)
                {
                    return NotFound(new { Message = "No climber associated with this account." });
                }
                var climbAttempts = _climbAttemptRepository.ViewAllClimbingAttempts(climberDTO.Id);
                if (!climbAttempts.IsNullOrEmpty())
                {
                    climbAttemptsList.AddRange(climbAttempts);
                }
            }
            if (climbAttemptsList.IsNullOrEmpty())
            {
                return NotFound(new { Message = "No climb attempts logged or found so far." });
            }

            return Ok(climbAttemptsList);
        }

        //view all climb attempts by username
        [HttpGet("log/view/{username}")]
        public ActionResult<List<ClimbAttemptDTO>> ViewAllClimbingAttemptsByUsername(string username)
        {
            var climberDTO = _climberRepository.GetClimberByUsername(username);
            if (climberDTO == null)
            {
                return NotFound();
            }
            var climbAttemptsList = _climbAttemptRepository.ViewAllClimbingAttempts(climberDTO.Id);
            if (climbAttemptsList.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(climbAttemptsList);
        }

        [HttpPost("log/record")]
        public ActionResult RecordClimbAttempt(ClimbAttemptDTO climbAttemptDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("User is authenticated");
                var aspNetIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = User.Identity.Name;
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null)
                {
                    //return NotFound(new { Message = "Climber not found." });
                    climberDTO = _climberService.AddNewClimber(aspNetIdentityId, userName);
                }
                var generatedId = _climbAttemptRepository.RecordClimbingAttempt(climberDTO.Id, climberDTO.UserName ?? userName, climbAttemptDTO.ClimbId, climbAttemptDTO.ClimbName, climbAttemptDTO.SuggestedGrade, climbAttemptDTO.EntryDate, climbAttemptDTO.Attempts, climbAttemptDTO.Rating);
                climbAttemptDTO.Id = generatedId; // Set the generated ID on the DTO
                return Ok(climbAttemptDTO);
            }
            else
            {
                _logger.LogInformation("User is not authenticated");
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        //view all climb attempts by climb id
        [HttpGet("log/view/climb/{climbId}")]
        public ActionResult<List<ClimbAttemptDTO>> ViewAllClimbingAttemptsByClimbId(string climbId)
        {
            var climbAttemptsList = _climbAttemptRepository.ViewAllClimbingAttemptsByClimbId(climbId);
            if (climbAttemptsList.IsNullOrEmpty())
            {
                return NotFound(new { Message = "No climb attempts logged or found so far." });
            }

            return Ok(climbAttemptsList);
        }

    }
}
