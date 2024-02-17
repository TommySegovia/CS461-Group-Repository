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

namespace PeakPals_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessDataEntryApiController : ControllerBase
    {
        private readonly IFitnessDataEntryService _fitnessDataEntryService;
        private readonly IFitnessDataEntryRepository _fitnessDataEntryRepository;

        public FitnessDataEntryApiController(IFitnessDataEntryService fitnessDataEntryService, IFitnessDataEntryRepository fitnessDataEntryRepository)
        {
            _fitnessDataEntryService = fitnessDataEntryService;
            _fitnessDataEntryRepository = fitnessDataEntryRepository;
        }

        [HttpGet]
        public ActionResult<List<FitnessDataEntry>> GetFitnessDataEntry()
        {
            if (_fitnessDataEntryRepository == null)
            {
                return NotFound();
            }
            return Ok(_fitnessDataEntryRepository.GetAll());
        }

        [HttpGet("HangTest/{climberId}")]
        public ActionResult<List<FitnessDataEntryDTO>> GetUserResultsWithTimesInChronologicalOrder(int climberId)
        {
            int testId = 0; // 0 is the id for the hang test
            if (_fitnessDataEntryRepository == null)
            {
                return NotFound();
            }
            return Ok(_fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(climberId, testId));
        }

        [HttpPost("RecordHangTestResult")]
        public ActionResult RecordHangTestResult(FitnessDataEntryDTO fitnessDataEntryDTO)
        {
            _fitnessDataEntryService.RecordTestResult(fitnessDataEntryDTO.ClimberId, 0, fitnessDataEntryDTO.Result, fitnessDataEntryDTO.BodyWeight);
            return Ok(new { Message = "Hang Test Recorded" });
        }


        /*

        [HttpPost("RecordHangTestResult/{climberId}/{result}/{bodyWeight}")]
        public ActionResult RecordHangTestResult(int climberId, int result, int bodyWeight)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _fitnessDataEntryService.RecordTestResult(climberId, 0, result, bodyWeight);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        */

    }
}
