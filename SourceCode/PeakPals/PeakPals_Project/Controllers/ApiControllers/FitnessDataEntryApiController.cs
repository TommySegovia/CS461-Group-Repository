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
using Microsoft.AspNetCore.Identity;
using PeakPals_Project.Areas.Identity.Data;
#nullable enable

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
        private readonly UserManager<ApplicationUser> _userManager;

        public FitnessDataEntryApiController(IFitnessDataEntryService fitnessDataEntryService, IClimberService climberService,
                                             IFitnessDataEntryRepository fitnessDataEntryRepository, IClimberRepository climberRepository,
                                             UserManager<ApplicationUser> userManager)
        {
            _fitnessDataEntryService = fitnessDataEntryService;
            _climberService = climberService;
            _fitnessDataEntryRepository = fitnessDataEntryRepository;
            _climberRepository = climberRepository;
            _userManager = userManager;
        }

        [HttpGet("Test/Results/{testId}")]
        public ActionResult<List<FitnessDataEntryDTO>> GetUserResultsWithTimesInChronologicalOrder(int testId)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {

                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null || _fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                var userResults = _fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(climberDTO.Id, testId);
                _fitnessDataEntryService.GenerateGraphsWithRecordHistory(userResults, testId);

                return Ok(userResults);
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        [HttpDelete("Test/Results/Delete/{id}/{testId}")]
        public ActionResult DeleteTestResult(int id, int testId)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null || _fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                _fitnessDataEntryService.DeleteTestResult(id, testId, climberDTO.Id);
                return Ok(new { Message = "Test Deleted" });
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }


        [HttpGet("Test/Results/MostRecent/{testId}")]
        public ActionResult<object> GetMostRecentUserTestValueAndBodyWeight(int testId)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null || _fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }

                var userResults = _fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(climberDTO.Id, testId);

                if (userResults.Any())
                {
                    var mostRecentResult = userResults.Last();
                    return Ok(new { Result = mostRecentResult.Result, BodyWeight = mostRecentResult.BodyWeight });
                }
                else
                {
                    // Handle the case where there are no recent results
                    return NotFound(new { Message = "No recent user value found" });
                }
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }


        [HttpGet("Test/Results/Average/All/PercentageOfBodyweight/{testId}/{minAge}/{maxAge}/{gender}/{climbingExperience}/{minimumClimbingGrade}/{maximumClimbingGrade}")]
        public ActionResult<double> GetAveragePercentageOfBodyweight(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                if (_fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                return Ok(_fitnessDataEntryRepository.GetAverageResultDividedByBodyweight(testId, minAge, maxAge, gender, climbingExperience, minimumClimbingGrade, maximumClimbingGrade));
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        [HttpGet("Test/Results/Average/All/{testId}/{minAge}/{maxAge}/{gender}/{climbingExperience}/{minimumClimbingGrade}/{maximumClimbingGrade}")]
        public ActionResult<double> GetAverageResult(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                if (_fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                return Ok(_fitnessDataEntryRepository.GetAverageResult(testId, minAge, maxAge, gender, climbingExperience, minimumClimbingGrade, maximumClimbingGrade));
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        [HttpGet("Test/Results/MostCommon/All/CampusBoard/{testId}/{minAge}/{maxAge}/{gender}/{climbingExperience}/{minimumClimbingGrade}/{maximumClimbingGrade}")]
        public ActionResult<double> GetMostCommonResultCampusBoard(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                if (_fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                return Ok(_fitnessDataEntryRepository.GetMostCommonResultCampusBoard(testId, minAge, maxAge, gender, climbingExperience, minimumClimbingGrade, maximumClimbingGrade));
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }



        [HttpPost("RecordTestResult")]
        public async Task<ActionResult> RecordTestResult(FitnessDataEntryDTO fitnessDataEntryDTO)
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                string? aspNetIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //pulls the age int column from the PeakPalsAuthDb identity, AspNetUsers table for the currently logged in user
                if (aspNetIdentityId == null)
                {
                    return BadRequest(new { Message = "User not authenticated" });
                }
                var user = await _userManager.FindByIdAsync(aspNetIdentityId);
                if (user == null)
                {
                    return BadRequest(new { Message = "User not authenticated" });
                }
                int? age = user.Age;
                string? gender = user.Gender;
                string? climbingExperience = user.ClimbingExperience;
                int? maxClimbGrade = user.MaxClimbGrade;

                int? testId = fitnessDataEntryDTO.TestId;

                //if the climber is not in the database, add them
                if (climberDTO == null)
                {
                    // placeholder userName that extracts first 3 characters of the UserName in Identity (which is just the email)
                    string? email = _userManager.GetEmailAsync(user).Result;
                    if (email == null || email.Contains("@") == false)
                    {
                        return BadRequest(new { Message = "User not authenticated or username is not in the form of a email" });
                    }
                    string userName = email.Split('@')[0];



                    climberDTO = _climberService.AddNewClimber(aspNetIdentityId, userName);

                    _fitnessDataEntryService.RecordTestResult(climberDTO.Id, testId, fitnessDataEntryDTO.Result, fitnessDataEntryDTO.BodyWeight, age, gender, climbingExperience, maxClimbGrade);
                    return Ok(new { Message = "Test Recorded" });
                }
                //if the climber is in the database, record the test result
                else
                {
                    _fitnessDataEntryService.RecordTestResult(climberDTO.Id, testId, fitnessDataEntryDTO.Result, fitnessDataEntryDTO.BodyWeight, age, gender, climbingExperience, maxClimbGrade);
                    return Ok(new { Message = "Test #" + testId + " Recorded" });
                }
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        [HttpGet("Test/Results/All/RadarChart")]
        public async Task<ActionResult> GenerateRadarChart()
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null || _fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                //get most recent test results for each test
                var userTests = new List<FitnessDataEntryDTO>();
                var averageTests = new List<double>();
                for (int i = 0; i <= 7; i++)
                {
                    var userResults = _fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(climberDTO.Id, i);
                    if (userResults.Any())
                    {
                        userTests.Add(userResults.Last());

                    }
                    else
                    {
                        userTests.Add(new FitnessDataEntryDTO { Result = 0 });
                    }
                    double? result;
                    

                    if (i <= 2) //strength tests
                    {
                        result = _fitnessDataEntryRepository.GetAverageResultDividedByBodyweight(i, 0, 100, "All", "All", 0, 100);
                    }
                    else if (i <= 6) //all tests except strength and campus board
                    {
                        result = _fitnessDataEntryRepository.GetAverageResult(i, 0, 100, "All", "All", 0, 100);
                    }
                    else //campus board test
                    {
                        result = _fitnessDataEntryRepository.GetMostCommonResultCampusBoard(i, 0, 100, "All", "All", 0, 100);
                    }

                    averageTests.Add(result ?? 0);
                }

                _fitnessDataEntryService.GenerateRadarChart(userTests, averageTests, climberDTO.Id);
                return await Task.FromResult(Ok(new { Message = "Radar Chart Generated" }));
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }

        //get users strongest stats
        [HttpGet("Test/Results/All/StrongestStats")]
        public ActionResult<object> GetUsersStrongestStats()
        {
            if (User.Identity == null)
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
            if (User.Identity.IsAuthenticated)
            {
                var climberDTO = _climberRepository.GetClimberByAspNetIdentityId(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (climberDTO == null || _fitnessDataEntryRepository == null)
                {
                    return NotFound();
                }
                //get most recent test results for each test
                var userTests = new List<FitnessDataEntryDTO>();
                var averageTests = new List<double>();
                for (int i = 0; i <= 7; i++)
                {
                    var userResults = _fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(climberDTO.Id, i);
                    if (userResults.Any())
                    {
                        userTests.Add(userResults.Last());

                    }
                    else
                    {
                        userTests.Add(new FitnessDataEntryDTO { Result = 0 });
                    }
                    double? result;

                    if (i <= 2) //strength tests
                    {
                        result = _fitnessDataEntryRepository.GetAverageResultDividedByBodyweight(i, 0, 100, "All", "All", 0, 100);
                    }
                    else if (i <= 6) //all tests except strength and campus board
                    {
                        result = _fitnessDataEntryRepository.GetAverageResult(i, 0, 100, "All", "All", 0, 100);
                    }
                    else //campus board test
                    {
                        result = _fitnessDataEntryRepository.GetMostCommonResultCampusBoard(i, 0, 100, "All", "All", 0, 100);
                    }

                    averageTests.Add(result ?? 0);
                }

                var strongestStats = _fitnessDataEntryService.GetUsersStrongestStats(userTests, averageTests, climberDTO.Id);
                return Ok(strongestStats);
            }
            else
            {
                return BadRequest(new { Message = "User not authenticated" });
            }
        }
    }
}
