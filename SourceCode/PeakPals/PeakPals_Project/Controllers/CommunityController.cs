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

public class CommunityController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IClimberRepository _climberRepository;
    private readonly IClimberService _climberService;

    public CommunityController(ILogger<ProfileController> logger, IClimberRepository climberRepository, IClimberService climberService)
    {
        _logger = logger;
        _climberRepository = climberRepository;
        _climberService = climberService;
    }

    public IActionResult Index()
    {
        return View();
    }
}