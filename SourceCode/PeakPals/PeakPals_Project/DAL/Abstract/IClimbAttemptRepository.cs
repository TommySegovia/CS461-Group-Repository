using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
#nullable enable

namespace PeakPals_Project.DAL.Abstract;

public interface IClimbAttemptRepository
{
    public List<ClimbAttemptDTO> ViewAllClimbingAttempts(int climberId);
    public ClimbAttempt? ViewClimbingAttempt(int climberId, string climbId);
    public int RecordClimbingAttempt(int climberId, string? climberName, string climbId, string? climbName, string? suggestedGrade, DateTime entryDate, int attempts, int rating);
    //public int RecordClimbingAttempt(int climberId, string climbId, string climbName, string? suggestedGrade, DateTime entryDate, int attempts, int rating);
    public ClimbAttempt? ViewClimbingAttemptByClimbAttemptID(int climbAttemptID);
    public List<ClimbAttemptDTO> ViewAllClimbingAttemptsByClimbId(string climbId);

    
}