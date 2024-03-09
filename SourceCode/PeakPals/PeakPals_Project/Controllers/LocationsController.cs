using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PeakPals_Project.Models;

namespace PeakPals_Project.Controllers;

public class LocationsController : Controller
{
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(ILogger<LocationsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Search()
    {   
        return View();
    }

    public IActionResult Areas()
    {
        return View();
    }
}