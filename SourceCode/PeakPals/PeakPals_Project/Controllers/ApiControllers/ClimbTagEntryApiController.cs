using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Models;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.ExtensionMethods;

namespace PeakPals_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimbTagEntryApiController : ControllerBase
    {
        private readonly PeakPalsContext _context;
        private readonly IClimbTagEntryRepository _climbTagEntryRepository;
        private readonly IClimbAttemptRepository _climbAttemptRepository;

        public ClimbTagEntryApiController(PeakPalsContext context, IClimbTagEntryRepository climbTagEntryRepository, IClimbAttemptRepository climbAttemptRepository)
        {
            _context = context;
            _climbTagEntryRepository = climbTagEntryRepository;
            _climbAttemptRepository = climbAttemptRepository;
        }

        // GET: api/ClimbTagEntryApi
        [HttpGet("log/view")]
        public ActionResult<List<ClimbTagEntryDTO>> GetClimbTagEntry()
        {
            return _climbTagEntryRepository.GetAll();
        }

        // POST: api/ClimbTagEntryApi
        [HttpPost("log/record")]
        public ActionResult<ClimbTagEntry> PostClimbTagEntry(ClimbTagEntryDTO climbTagEntryDTO)
        {
            _climbTagEntryRepository.AddClimbTagEntry(climbTagEntryDTO);
            return Ok(climbTagEntryDTO);
        }

        //get the tags for a specific climb attempt
        [HttpGet("log/view/{climbAttemptID}")]
        public ActionResult<List<string>> GetClimbTagEntryByClimbAttemptID(int climbAttemptID)
        {
            return _climbTagEntryRepository.GetClimbTagEntryByClimbAttemptID(climbAttemptID);
        }

        //search for climbs by tags
        [HttpGet("log/search/{tags}")]
        public ActionResult<List<ClimbAttemptDTO>> GetClimbTagEntryByTag(string tags)
        {
            // Split the tags parameter into an array of tags
            var tagArray = tags.Split(',');

            List<ClimbAttempt> climbAttempts = new List<ClimbAttempt>();

            // Iterate over each tag
            foreach (var tag in tagArray)
            {
                // Get the ids of the climbTagEntries that have the tag, then get the climbattempts using those ids
                List<int> climbTagEntryIds = _climbTagEntryRepository.GetClimbTagEntryIdByTag(tag);

                foreach (int climbAttemptID in climbTagEntryIds)
                {
                    climbAttempts.Add(_climbAttemptRepository.ViewClimbingAttemptByClimbAttemptID(climbAttemptID));
                }
            }

            // Remove duplicates
            climbAttempts = climbAttempts.Distinct().ToList();

            return climbAttempts.Select(f => f.ToDTO()).ToList();
        }


    }
}
