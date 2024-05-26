
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using System;


namespace PeakPals_Project.Services
{
    public interface IFitnessDataEntryService
    {
        public void RecordTestResult(int? climberId, int? testId, int? result, int? bodyWeight, int? age, string? gender, string? climbingExperience, int? climbingGrade);
        public void GenerateGraphsWithRecordHistory(List<FitnessDataEntryDTO> fitnessDataEntryListDTO, int testId);
        public void DeleteTestResult(int id, int testId, int climberId);
        public double? GenerateRadarChart(List<FitnessDataEntryDTO> userTests, List<double> averageTests, int testId);

    }
}
