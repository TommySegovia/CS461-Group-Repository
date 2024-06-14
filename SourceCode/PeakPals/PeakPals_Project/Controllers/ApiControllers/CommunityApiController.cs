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
using Microsoft.AspNetCore.Http.HttpResults;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
#nullable enable
namespace PeakPals_Project.Controllers
{
    [Route("api/community")]
    [ApiController]
    public class CommunityApiController : ControllerBase
    {
        //private readonly IFitnessDataEntryService _fitnessDataEntryService;
        private readonly IClimberService _climberService;
        private readonly IClimberRepository _climberRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommunityGroupRepository _communityGroupRepository;
        private readonly IGroupListRepository _groupListRepository;
        private readonly ICommunityMessageRepository _communityMessageRepository;

        public CommunityApiController(IClimberService climberService, IClimberRepository climberRepository, UserManager<ApplicationUser> userManager
                                    , ICommunityGroupRepository communityGroupRepository, IGroupListRepository groupListRepository, ICommunityMessageRepository communityMessageRepository)
        {
            _climberService = climberService;
            _climberRepository = climberRepository;
            _userManager = userManager;
            _communityGroupRepository = communityGroupRepository;
            _groupListRepository = groupListRepository;
            _communityMessageRepository = communityMessageRepository;
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
            var users = await _userManager.Users.Where(u => u != null && u.UserName != null && u.UserName.Contains(username!)).ToListAsync();

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

        //get number of members in a group
        [HttpGet("members/group/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public ActionResult<int> GetGroupMemberCount(int groupID)
        {
            // Get the group list entries for the group
            var numberOfMembers = _groupListRepository.GetGroupMemberCountByGroupID(groupID);

            return Ok(numberOfMembers);
        }

        //get members of a group
        [HttpGet("members/group/{groupID}/list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Climber>))]
        public ActionResult<List<Climber>> GetGroupMembers(int groupID)
        {
            // Get the group list entries for the group
            var groupListEntries = _groupListRepository.GetGroupListByGroupID(groupID);

            if (groupListEntries == null || groupListEntries.Count == 0)
            {
                return Ok(new List<Climber>()); // Return an empty list if no member is found
            }

            var members = new List<Climber>();
            foreach (var entry in groupListEntries)
            {
                var climber = _climberRepository.GetClimberByClimberId(entry.ClimberID);
                if (climber != null)
                {
                    members.Add(climber);
                }
            }

            return Ok(members);
        }

        //remove from group
        [HttpPost("remove/group/{groupID}/member/{memberID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> RemoveGroupMember(int groupID, int memberID)
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

            // Check if the user is the owner of the group
            var group = await _communityGroupRepository.GetGroupById(groupID);

            if (group == null)
            {
                return NotFound(new { Message = "Group does not exist." });
            }

            if (group.OwnerID != climber.Id)
            {
                return Unauthorized(new { Message = "User is not the owner of this group." });
            }

            // Check if the member is a member of the group before removing
            var groupListEntry = _groupListRepository.GetGroupListByClimberIDAndGroupID(memberID, groupID);

            if (groupListEntry == null || groupListEntry.Count == 0)
            {
                return BadRequest(new { Message = "User is not a member of this group." });
            }

            // If the member is a member of the group, remove the group from the member's group list
            _groupListRepository.Delete(groupListEntry[0]);

            return Ok(true);

        }

        //get current user's climber ID
        [HttpGet("currentUserId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> GetCurrentUserClimberID()
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

            return Ok(climber.Id);

        }

        //set the new owner of the group
        [HttpPost("setOwner/group/{groupID}/user/{newOwnerID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> SetGroupOwner(int groupID, int newOwnerID)
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

            // Check if the user is the owner of the group
            var group = await _communityGroupRepository.GetGroupById(groupID);

            if (group == null)
            {
                return NotFound(new { Message = "Group does not exist." });
            }

            if (group.OwnerID != climber.Id)
            {
                return Unauthorized(new { Message = "User is not the owner of this group." });
            }

            // Check if the new owner is a member of the group
            var groupListEntry = _groupListRepository.GetGroupListByClimberIDAndGroupID(newOwnerID, groupID);

            if (groupListEntry == null || groupListEntry.Count == 0)
            {
                return BadRequest(new { Message = "New owner is not a member of this group." });
            }

            // Set the new owner of the group
            group.OwnerID = newOwnerID;
            _communityGroupRepository.AddOrUpdate(group);

            return Ok(true);

        }

        //delete group
        [HttpPost("delete/group/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> DeleteGroup(int groupID)
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

            // Check if the user is the owner of the group
            var group = await _communityGroupRepository.GetGroupById(groupID);

            if (group == null)
            {
                return NotFound(new { Message = "Group does not exist." });
            }

            if (group.OwnerID != climber.Id)
            {
                return Unauthorized(new { Message = "User is not the owner of this group." });
            }

            // Delete the group
            _communityGroupRepository.Delete(group);

            return Ok(true);

        }

        //get joined groups of the current user
        [HttpGet("joinedGroups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommunityGroup>))]
        public async Task<ActionResult<List<CommunityGroup>>> GetJoinedGroups()
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

            // Get the group list entries for the climber
            var groupListEntries = _groupListRepository.GetGroupListByClimberID(climber.Id);

            if (groupListEntries == null || groupListEntries.Count == 0)
            {
                return Ok(new List<CommunityGroup>()); // Return an empty list if no group is found
            }

            var groups = new List<CommunityGroup>();
            foreach (var entry in groupListEntries)
            {
                var group = await _communityGroupRepository.GetGroupById(entry.CommunityGroupID);
                if (group != null)
                {
                    groups.Add(group);
                }
            }

            return Ok(groups);

        }

        //get joined groups for a user by username
        [HttpGet("joinedGroups/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommunityGroup>))]
        public async Task<ActionResult<List<CommunityGroup>>> GetJoinedGroupsByUsername(string username)
        {
            // Get the climber associated with the username
            var climber = _climberRepository.GetClimberByUsername(username);

            if (climber == null)
            {
                return NotFound(new { Message = "Climber does not exist." });
            }

            // Get the group list entries for the climber
            var groupListEntries = _groupListRepository.GetGroupListByClimberID(climber.Id);

            if (groupListEntries == null || groupListEntries.Count == 0)
            {
                return Ok(new List<CommunityGroup>()); // Return an empty list if no group is found
            }

            var groups = new List<CommunityGroup>();
            foreach (var entry in groupListEntries)
            {
                var group = await _communityGroupRepository.GetGroupById(entry.CommunityGroupID);
                if (group != null)
                {
                    groups.Add(group);
                }
            }

            return Ok(groups);

        }

        // get all messages from a particular group
        [HttpGet("group/messages/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommunityMessage>))]
        public async Task<ActionResult<List<CommunityMessage>>> GetAllMessages(string groupId)
        {
            if (groupId == null)
            {
                return BadRequest(new { Message = "groupId is null"});
            }

            int.TryParse(groupId, out int id);
            var messages = await _communityMessageRepository.GetMessagesById(id);

            if (messages == null)
            {
                return NotFound(new { Message = "Group does not exist."});
            }

            return Ok(messages);
        }

        // post a message to a group
        [HttpPost("group/{groupId}/messages/{comment}")]
        public async Task<ActionResult> PostMessage(string comment, string groupId)
        {
            if (comment == null)
            {
                return BadRequest(new { Message = "Comment is null"});
            }

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

            int.TryParse(groupId, out int communityGroupId);

            await _communityMessageRepository.CreateMessage(climber.Id, communityGroupId, climber.UserName, comment);

            return Ok();
        }

        // 
    }
}