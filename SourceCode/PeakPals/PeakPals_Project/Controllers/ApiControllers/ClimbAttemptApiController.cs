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
        private readonly UserManager<ApplicationUser> _userManager;

        public ClimbAttemptApiController(IClimbAttemptRepository climbAttemptRepository, IClimberRepository climberRepository, UserManager<ApplicationUser> userManager)
        {
            _climbAttemptRepository = climbAttemptRepository;
            _climberRepository = climberRepository;
            _userManager = userManager;
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

        [HttpPost("log/record")]
        public ActionResult RecordClimbAttempt(ClimbAttemptDTO climbAttemptDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null)
                {
                    return NotFound(new { Message = "Climber not found." });
                }
                var generatedId = _climbAttemptRepository.RecordClimbingAttempt(climberDTO.Id, climbAttemptDTO.ClimbId, climbAttemptDTO.ClimbName, climbAttemptDTO.SuggestedGrade, climbAttemptDTO.EntryDate, climbAttemptDTO.Attempts, climbAttemptDTO.Rating);
                climbAttemptDTO.Id = generatedId; // Set the generated ID on the DTO
                return Ok(climbAttemptDTO);
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }



    }
}
