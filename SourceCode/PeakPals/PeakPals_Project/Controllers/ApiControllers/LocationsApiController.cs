using Microsoft.AspNetCore.Mvc;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Services;
using PeakPals_Project.Models;

namespace PeakPals_Project.Controllers
{

    [Route("api/locations")]
    [ApiController]
    public class LocationsApiController : ControllerBase
    {
        private readonly IOpenBetaApiService _openBetaApiService;
        private readonly ILogger<LocationsApiController> _logger;

        public LocationsApiController(IOpenBetaApiService openBetaApiService, ILogger<LocationsApiController> logger)
        {
            _openBetaApiService = openBetaApiService;
            _logger = logger;
        }

        [HttpGet("search/{query}")]
        public async Task<ActionResult<OpenBetaQueryResult>> FindAllMatchingAreas(string query)
        {
            if (string.IsNullOrEmpty(query)) {
                _logger.LogError($"Query string is empty or null.");
                return BadRequest(new { Message = "The query parameter cannot be null or empty." });
            }

            var response = await _openBetaApiService.FindMatchingAreas(query);

            if (response is null) {
                _logger.LogError($"Failed to fetch from OpenBeta");
                return NotFound(new { Message = $"The api fetch from OpenBeta returned nothing. {response}" });
            }
            else if (response is BadRequestObjectResult badRequest) {
                var errors = badRequest.Value;
                _logger.LogError(errors.ToString());
                return BadRequest(new { Message = "The api fetch from OpenBeta returned a bad request."});
            }
            else if (response is StatusCodeResult statusCodeResult && statusCodeResult.StatusCode == StatusCodes.Status408RequestTimeout) {
                return StatusCode(StatusCodes.Status408RequestTimeout, new { Message = "The API request timed out." });
            }
            else {
                return Ok(response);
            }

        }
    }
}