using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Models;

namespace PeakPals_Project.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly ClimberRepository _climberRepository;

    public ProfileController(ILogger<ProfileController> logger, ClimberRepository climberRepository)
    {
        _logger = logger;
        _climberRepository = climberRepository;
    }

    public IActionResult GetProfile(string username)
    {
        if (User.Identity.IsAuthenticated)
        {
            
        }

        var user = User;

        return View();
    }
}