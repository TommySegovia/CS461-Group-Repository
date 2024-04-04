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
                _logger.LogError($"Query string given is empty or null.");
                return BadRequest(new { Message = "The query parameter cannot be null or empty." });
            }

            var response = await _openBetaApiService.FindMatchingAreas(query);

            if (response is null) {
                _logger.LogError($"Failed to fetch from OpenBeta");
                return NotFound(new { Message = $"The api fetch from OpenBeta returned nothing. {response}" });
            }
            return Ok(response);
        }

        [HttpGet("search/climbs/{query}")]
        public async Task<ActionResult<OpenBetaQueryResult>> FindAllMatchingClimbs(string query)
        {
            if (string.IsNullOrEmpty(query)) {
                _logger.LogError($"Query string given is empty or null.");
                return BadRequest(new { Message = "The query parameter cannot be null or empty." });
            }

            var response = await _openBetaApiService.FindMatchingAreas(query, 200);

            if (response is null) {
                _logger.LogError($"Failed to fetch from OpenBeta");
                return NotFound(new { Message = $"The api fetch from OpenBeta returned nothing. {response}" });
            }
            return Ok(response);
        }

        [HttpGet("search/area/{id}")]
        public async Task<ActionResult<OBArea>> FindAreaById(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                _logger.LogError($"Id string given is empty or null.");
                return BadRequest(new { Message = "The query parameter cannot be null or empty." });
            }

            var response = await _openBetaApiService.FindAreaById(id);

            if (response is null) {
                _logger.LogError($"Failed to fetch from OpenBeta");
                return NotFound(new { Message = $"The api fetch from OpenBeta returned nothing. {response}" });
            }
            return Ok(response);
        }

        [HttpGet("search/area/ancestors/{id}")]
        public async Task<ActionResult<OBArea>> FindAncestorNameById(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                _logger.LogError($"Id string given is empty or null.");
                return BadRequest(new { Message = "The query parameter cannot be null or empty." });
            }

            var response = await _openBetaApiService.FindAncestorNameByAreaId(id);

            if (response is null) {
                _logger.LogError($"Failed to fetch from OpenBeta");
                return NotFound(new { Message = $"The api fetch from OpenBeta returned nothing. {response}" });
            }
            return Ok(response);
        }

        [HttpGet("climb/{id}")]
        public async Task<ActionResult<OBClimb>> FindClimbById(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                _logger.LogError($"Id string given is empty or null.");
                return BadRequest(new { Message = "The query parameter cannot be null or empty." });
            }

            var response = await _openBetaApiService.FindClimbById(id);

            if (response is null) {
                _logger.LogError($"Failed to fetch from OpenBeta");
                return NotFound(new { Message = $"The api fetch from OpenBeta returned nothing. {response}" });
            }
            return Ok(response);
        }

    }
}