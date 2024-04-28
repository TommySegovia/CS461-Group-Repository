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

namespace PeakPals_Project.Controllers
{
    [Route("api/community")]
    [ApiController]
    public class CommunityApiController : ControllerBase
    {
        private readonly IFitnessDataEntryService _fitnessDataEntryService;
        private readonly IClimberService _climberService;
        private readonly IClimberRepository _climberRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommunityGroupRepository _communityGroupRepository;

        public CommunityApiController(IClimberService climberService, IClimberRepository climberRepository, UserManager<ApplicationUser> userManager, ICommunityGroupRepository communityGroupRepository)
        {
            _climberService = climberService;
            _climberRepository = climberRepository;
            _userManager = userManager;
            _communityGroupRepository = communityGroupRepository;
        }

        [HttpGet("search/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClimberDTO>))]
        public async Task<ActionResult<List<ClimberDTO>>> GetUserResults(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { Message = "Username field cannot be empty." });
            }

            // Fetch users with usernames containing the search username
            var users = await _userManager.Users.Where(u => u.UserName.Contains(username)).ToListAsync();

            if (users == null || users.Count == 0)
            {
                return Ok(new List<ClimberDTO>()); // Return an empty list if no user is found
            }

            var climberDTOs = new List<ClimberDTO>();
            foreach (var user in users)
            {
                var climber = _climberRepository.GetClimberModelByAspNetIdentityId(user.Id);
                if (climber != null)
                {
                    var climberDTO = climber.ToDTO();
                    climberDTOs.Add(climberDTO);
                }
            }

            return Ok(climberDTOs);
        }

        [HttpGet("search/group/{groupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommunityGroup>))]
        public async Task<ActionResult<List<CommunityGroup>>> GetGroupResults(string? groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest(new { Message = "Group name field cannot be empty." });
            }

            // Fetch groups with names containing the search group name
            var groups = await _communityGroupRepository.GetGroupsByName(groupName);

            if (groups == null || groups.Count == 0)
            {
                return Ok(new List<CommunityGroup>()); // Return an empty list if no group is found
            }

            return Ok(groups);
        }


    }
}