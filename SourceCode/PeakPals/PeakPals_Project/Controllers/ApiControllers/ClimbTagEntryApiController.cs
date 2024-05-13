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

namespace PeakPals_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimbTagEntryApiController : ControllerBase
    {
        private readonly PeakPalsContext _context;
        private readonly IClimbTagEntryRepository _climbTagEntryRepository;

        public ClimbTagEntryApiController(PeakPalsContext context, IClimbTagEntryRepository climbTagEntryRepository)
        {
            _context = context;
            _climbTagEntryRepository = climbTagEntryRepository;
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
        
    }
}
