using PeakPals_Project.Models;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Abstract
{
    public interface IGroupListRepository : IRepository<GroupList>
    {
        void AddGroupToGroupList(int climberID, int communityGroupID);
    }
}