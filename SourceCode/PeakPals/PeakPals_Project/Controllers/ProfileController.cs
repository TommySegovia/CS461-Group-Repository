using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Authorization;
using PeakPals_Project.ExtensionMethods;
using PeakPals_Project.DAL.Abstract;

namespace PeakPals_Project.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IClimberRepository _climberRepository;

    public ProfileController(ILogger<ProfileController> logger, IClimberRepository climberRepository)
    {
        _logger = logger;
        _climberRepository = climberRepository;
    }


    [HttpGet("profile/{username}")]
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

    [HttpGet("profile/edit")]
    [Authorize]
    public IActionResult EditProfile()
    {
        var currentUserDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (currentUserDTO == null)
        {
            return NotFound();
        }

        var currentUser = currentUserDTO.ToModel();

        return View("EditProfile", currentUser);
    }
}