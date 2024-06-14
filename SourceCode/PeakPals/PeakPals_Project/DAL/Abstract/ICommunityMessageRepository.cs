using PeakPals_Project.Models;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Abstract
{
    public interface ICommunityMessageRepository : IRepository<CommunityMessage>
    {
        Task<List<CommunityMessage>> GetMessagesById(int groupId);
        Task CreateMessage(int ClimberId, int CommunityGroupId, string DisplayName, string Message);

    }
}