using PeakPals_Project.Models;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Abstract
{
    public interface IGroupListRepository : IRepository<GroupList>
    {
        void AddGroupToGroupList(int climberID, int communityGroupID);
        List<GroupList> GetGroupListByClimberIDAndGroupID(int climberID, int communityGroupID);
        int GetGroupMemberCountByGroupID(int communityGroupID);
        List<GroupList> GetGroupListByGroupID(int communityGroupID);
        List<GroupList> GetGroupListByClimberID(int climberID);
    }
}