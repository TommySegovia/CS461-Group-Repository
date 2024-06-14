
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using System;
#nullable enable


namespace PeakPals_Project.Services
{
    public interface IFitnessDataEntryService
    {
        public void RecordTestResult(int? climberId, int? testId, int? result, int? bodyWeight, int? age, string? gender, string? climbingExperience, int? climbingGrade);
        public void GenerateGraphsWithRecordHistory(List<FitnessDataEntryDTO> fitnessDataEntryListDTO, int testId);
        public void DeleteTestResult(int id, int testId, int climberId);
        public void GenerateRadarChart(List<FitnessDataEntryDTO> userTests, List<double> averageTests, int testId);
        public List<string> GetUsersStrongestStats(List<FitnessDataEntryDTO> userTests, List<double> averageTests, int testId);

    }
}
