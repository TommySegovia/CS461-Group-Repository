
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using System;


namespace PeakPals_Project.Services
{
    public interface IFitnessDataEntryService
    {
        public void RecordTestResult(int? climberId, int? testId, int? result, int? bodyWeight);
    }
}
