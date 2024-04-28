using PeakPals_Project.Models;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Abstract
{
    public interface ICommunityGroupRepository : IRepository<CommunityGroup>
    {
        Task<List<CommunityGroup>> GetGroupsByName(string groupName);
        Task<CommunityGroup> GetGroupById(int groupID);

    }
}