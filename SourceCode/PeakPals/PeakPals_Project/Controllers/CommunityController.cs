using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Authorization;
using PeakPals_Project.ExtensionMethods;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeakPals_Project.Areas.Identity.Data;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.Controllers;

public class CommunityController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IClimberRepository _climberRepository;
    private readonly IClimberService _climberService;
    private readonly ICommunityGroupRepository _communityGroupRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public CommunityController(ILogger<ProfileController> logger, IClimberRepository climberRepository, IClimberService climberService
                            , ICommunityGroupRepository communityGroupRepository, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _climberRepository = climberRepository;
        _climberService = climberService;
        _communityGroupRepository = communityGroupRepository;
        _userManager = userManager;

    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Community/Group/{groupID}")]
    public async Task<IActionResult> GetGroup(int groupID)
    {
        if (User.Identity.IsAuthenticated)
        {
            // Fetch the group from the database
        var group = await _communityGroupRepository.GetGroupById(groupID);

        if (group == null)
        {
            // Handle the case where the group does not exist
            return NotFound();
        }

        // Get the current user
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            // Handle the case where the user is not authenticated
            return Unauthorized();
        }

        // Get the climber associated with the current user
        var climber = _climberRepository.GetClimberModelByAspNetIdentityId(currentUser.Id);
        if (climber == null)
        {
            // Handle the case where the climber does not exist
            ClimberDTO climberDTO = _climberService.AddNewClimber(currentUser.Id, currentUser.UserName);
            climber = climberDTO.ToModel();
            //return NotFound();
        }

        // Check if the current user's climber ID is the owner ID of the group
        if (group.OwnerID == climber.Id)
        {
            
            // Return the owner view if the current user's climber ID is the owner ID
            return View("CommunityGroupOwner", group);
        }
        else
        {
            
            // Return the regular view if the current user's climber ID is not the owner ID
            return View("CommunityGroup", group);
        }
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        
    }

    [HttpPost("Community/Group/{groupID}")]
    public IActionResult GetGroup(int groupID, string message)
    {
        if (User.Identity.IsAuthenticated)
        {
            return View();
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}