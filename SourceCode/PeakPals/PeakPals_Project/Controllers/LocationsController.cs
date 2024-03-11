using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PeakPals_Project.Models;
using PeakPals_Project.Services;

namespace PeakPals_Project.Controllers;

public class LocationsController : Controller
{
    private readonly ILogger<LocationsController> _logger;
    private readonly IOpenBetaApiService _openBetaApiService;

    public LocationsController(ILogger<LocationsController> logger, IOpenBetaApiService openBetaApiService)
    {
        _logger = logger;
        _openBetaApiService = openBetaApiService;
    }

    public IActionResult Search()
    {   
        return View();
    }

    [HttpGet("Locations/Areas/{id}")]
    public IActionResult GetArea(string id)
    {
        // var area _openBetaApiService.FindAreaById(id);

        // Add exception handling.

        // return View("Areas", area);
        return View();
    }
}