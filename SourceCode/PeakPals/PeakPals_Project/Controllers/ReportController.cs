using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.Models;

namespace PeakPals_Project.Controllers;

public class ReportController : Controller
{
    private readonly ILogger<ReportController> _logger;

    public ReportController(ILogger<ReportController> logger)
    {
        _logger = logger;
    }

    public IActionResult Report()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
