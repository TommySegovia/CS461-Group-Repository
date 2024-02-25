using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Authorization;
using PeakPals_Project.ExtensionMethods;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Services;

namespace PeakPals_Project.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IClimberRepository _climberRepository;
    private readonly IClimberService _climberService;

    public ProfileController(ILogger<ProfileController> logger, IClimberRepository climberRepository, IClimberService climberService)
    {
        _logger = logger;
        _climberRepository = climberRepository;
        _climberService = climberService;
    }


    [HttpGet("Profile/{username}")]
    public IActionResult GetProfile(string username)
    {
        var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var climberProfile = _climberRepository.GetClimberByUsername(username);
        
        if (climberProfile == null) {
            return NotFound();
        }

        if (currentUserID == climberProfile.AspnetIdentityId)
        {;
            // This is for when the user views their own profile.
            return View("UserProfile", climberProfile);
        }
        else
        {
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

        _climberService.UpdateClimber(currentUser);

        return View("UserProfile", currentUser);
    }
}