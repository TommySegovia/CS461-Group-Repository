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
        private readonly IGroupListRepository _groupListRepository;

        public CommunityApiController(IClimberService climberService, IClimberRepository climberRepository, UserManager<ApplicationUser> userManager
                                    , ICommunityGroupRepository communityGroupRepository, IGroupListRepository groupListRepository)
        {
            _climberService = climberService;
            _climberRepository = climberRepository;
            _userManager = userManager;
            _communityGroupRepository = communityGroupRepository;
            _groupListRepository = groupListRepository;
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

        [HttpPost("create/group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommunityGroup))]
        public async Task<ActionResult<CommunityGroup>> CreateGroup([FromBody] CommunityGroup group)
        {
            if (group == null)
            {
                return BadRequest(new { Message = "Group object cannot be null." });
            }

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            // Get the climber associated with the current user
            var climber = _climberRepository.GetClimberModelByAspNetIdentityId(currentUser.Id);

            if (climber == null)
            {
                return NotFound(new { Message = "Climber does not exist." });
            }

            // Set the owner ID of the group to the current user's climber ID
            group.OwnerID = climber.Id;

            // Add the group to the database
            _communityGroupRepository.AddOrUpdate(group);

            // Add the group to the climber's group list

            _groupListRepository.AddOrUpdate(new GroupList
            {
                ClimberID = climber.Id,
                CommunityGroupID = group.Id
            });

            return Ok(group);

        }

        //check if the user is a member of the group
        [HttpGet("check/group/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> CheckGroupMembership(int groupID)
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            // Get the climber associated with the current user
            var climber = _climberRepository.GetClimberModelByAspNetIdentityId(currentUser.Id);

            if (climber == null)
            {
                return NotFound(new { Message = "Climber does not exist." });
            }

            // Get the group list entry for the current user and group
            var groupListEntry = _groupListRepository.GetGroupListByClimberIDAndGroupID(climber.Id, groupID);

            if (groupListEntry == null || groupListEntry.Count == 0)
            {
                return Ok(false); // Return false if the user is not a member of the group
            }

            return Ok(true);

        }

        [HttpPost("join/group/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> JoinGroup(int groupID)
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            // Get the climber associated with the current user
            var climber = _climberRepository.GetClimberModelByAspNetIdentityId(currentUser.Id);

            if (climber == null)
            {
                return NotFound(new { Message = "Climber does not exist." });
            }

            // Check if the user is already a member of the group
            var existingGroupList = _groupListRepository.GetGroupListByClimberIDAndGroupID(climber.Id, groupID);
            if (existingGroupList != null && existingGroupList.Any())
            {
                return BadRequest(new { Message = "User is already a member of this group." });
            }

            // Add the group to the climber's group list
            _groupListRepository.AddOrUpdate(new GroupList
            {
                ClimberID = climber.Id,
                CommunityGroupID = groupID
            });

            return Ok(true);
        }

        [HttpPost("leave/group/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> LeaveGroup(int groupID)
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            // Get the climber associated with the current user
            var climber = _climberRepository.GetClimberModelByAspNetIdentityId(currentUser.Id);

            if (climber == null)
            {
                return NotFound(new { Message = "Climber does not exist." });
            }

            // Check if the user is a member of the group before removing
            var groupListEntry = _groupListRepository.GetGroupListByClimberIDAndGroupID(climber.Id, groupID);

            if (groupListEntry == null || groupListEntry.Count == 0)
            {
                return BadRequest(new { Message = "User is not a member of this group." });
            }

            // If the user is a member of the group, remove the group from the climber's group list
            _groupListRepository.Delete(groupListEntry[0]);

            return Ok(true);
        }

    }
}