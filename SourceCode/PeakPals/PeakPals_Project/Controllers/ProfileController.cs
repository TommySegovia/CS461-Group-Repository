using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Authorization;
using PeakPals_Project.ExtensionMethods;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Services;
using PeakPals_Project.Areas.Identity.Data;

namespace PeakPals_Project.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IClimberRepository _climberRepository;
    private readonly IClimberService _climberService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(ILogger<ProfileController> logger, IClimberRepository climberRepository, IClimberService climberService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _climberRepository = climberRepository;
        _climberService = climberService;
        _userManager = userManager;
    }


    [HttpGet("Profile/{username}")]
    public async Task<IActionResult> GetProfile(string username)
    {
        var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //find the user by username in the aspnetusers table
        var user = await _userManager.FindByNameAsync(username);
        _logger.LogInformation("Username: " + username);
        _logger.LogInformation("User: " + user);
        //find the climber by aspnetidentityid in the climber repository with a matching aspnetidentityid
        if (user == null)
        {
            return NotFound();
        }
        var climberProfile = _climberRepository.GetClimberModelByAspNetIdentityId(user.Id);

        if (currentUserID == user.Id)
        {
            if (climberProfile == null) {
                _climberService.AddNewClimber(currentUserID, username);
                var newClimberProfile = _climberRepository.GetClimberModelByAspNetIdentityId(user.Id);
                return View("UserProfile", newClimberProfile);
            }
            // This is for when the user views their own profile.
            return View("UserProfile", climberProfile);
        }
        else
        {
            if (climberProfile == null) {
                return NotFound();
            }
            // This is for when the user views another user's profile.
            return View("OtherProfile", climberProfile);
        }
    }

    [HttpGet("Profile/Edit")]
    [Authorize]
    public IActionResult EditProfile()
    {
        var currentUser = _climberRepository.GetClimberModelByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (currentUser == null)
        {
            return NotFound();
        }

        return View("EditProfile", currentUser);
    }

    [HttpPost("Profile/Edit")]
    [Authorize]
    public IActionResult PostProfile(Climber model)
    {
        var currentUser = _climberRepository.GetClimberModelByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (currentUser == null)
        {
            return NotFound();
        }
        

        currentUser.DisplayName = model.DisplayName;
        currentUser.ImageLink = model.ImageLink;
        currentUser.Bio = model.Bio;
        currentUser.CustomLink = model.CustomLink;
        currentUser.LinkText = model.LinkText;
        currentUser.City = model.City;
        currentUser.State = model.State;
        currentUser.Age = model.Age;
        currentUser.FirstName = model.FirstName;
        currentUser.LastName = model.LastName;

        _climberService.UpdateClimber(currentUser);

        return View("UserProfile", currentUser);
    }
}