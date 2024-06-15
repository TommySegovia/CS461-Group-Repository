using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PeakPals_Project.Models;
using PeakPals_Project.Services;
using GraphQL.Client.Http;

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
        if (User.Identity.IsAuthenticated)
        {
            return View();
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }

    [HttpGet("Locations/Areas/{id}")]
    public IActionResult GetArea(string id)
    {
        if (User.Identity.IsAuthenticated)
        {
            Task<OBArea> area = _openBetaApiService.FindAreaById(id);
            if (area == null)
            {
                return NotFound();
            }
            return View("Areas", area.Result);
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }

    [HttpGet("Locations/Climbs/{id}")]
    public async Task<IActionResult> GetClimb(string id)
    {
        if (User.Identity.IsAuthenticated)
        {
            try
            {
                OBClimb climb = await _openBetaApiService.FindClimbById(id);
                if (climb == null)
                {
                    return NotFound();
                }

                return View("Climbs", climb);
            }
            catch (GraphQLHttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching climb with id {Id}", id);
                return StatusCode(502); // Bad Gateway
            }
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

    }
}