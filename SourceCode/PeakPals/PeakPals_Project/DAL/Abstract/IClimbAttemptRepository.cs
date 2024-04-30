using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract;

public interface IClimbAttemptRepository
{
    public List<ClimbAttemptDTO> ViewAllClimbingAttempts(int climberId);
    public ClimbAttempt ViewClimbingAttempt(int climberId, string climbId);
    public void RecordClimbingAttempt(int climberId, string climbId, string climbName, string? suggestedGrade, DateTime entryDate, int attempts, int rating);
    
}