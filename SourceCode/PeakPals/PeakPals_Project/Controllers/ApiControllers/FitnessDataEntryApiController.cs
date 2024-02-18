using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Data;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.ExtensionMethods;
using PeakPals_Project.Services;
using System.Security.Claims;

namespace PeakPals_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessDataEntryApiController : ControllerBase
    {
        private readonly IFitnessDataEntryService _fitnessDataEntryService;
        private readonly IClimberService _climberService;
        private readonly IFitnessDataEntryRepository _fitnessDataEntryRepository;
        private readonly IClimberRepository _climberRepository;

        public FitnessDataEntryApiController(IFitnessDataEntryService fitnessDataEntryService, IClimberService climberService,
                                             IFitnessDataEntryRepository fitnessDataEntryRepository, IClimberRepository climberRepository)
        {
            _fitnessDataEntryService = fitnessDataEntryService;
            _climberService = climberService;
            _fitnessDataEntryRepository = fitnessDataEntryRepository;
            _climberRepository = climberRepository;
        }

        [HttpGet("HangTest/Results")]
        public ActionResult<List<FitnessDataEntryDTO>> GetUserResultsWithTimesInChronologicalOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                int testId = 0; // 0 is the id for the hang test

                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null || _fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                return Ok(_fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(climberDTO.Id, testId));
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }


        [HttpPost("RecordHangTestResult")]
        public ActionResult RecordHangTestResult(FitnessDataEntryDTO fitnessDataEntryDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                string? aspNetIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //placeholder names until we make a user profile page
                string firstName = "John";
                string lastName = "Doe";

                //if the climber is not in the database, add them
                if (climberDTO == null)
                {
                    climberDTO = _climberService.AddNewClimber(aspNetIdentityId, firstName, lastName);

                    _fitnessDataEntryService.RecordTestResult(climberDTO.Id, 0, fitnessDataEntryDTO.Result, fitnessDataEntryDTO.BodyWeight);
                    return Ok(new { Message = "Hang Test Recorded" });
                }
                //if the climber is in the database, record the test result
                else
                {
                    _fitnessDataEntryService.RecordTestResult(climberDTO.Id, 0, fitnessDataEntryDTO.Result, fitnessDataEntryDTO.BodyWeight);
                    return Ok(new { Message = "Hang Test Recorded" });
                }
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }
    }
}
