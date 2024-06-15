using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Concrete
{
    public class GroupListRepository : Repository<GroupList>, IGroupListRepository
    {
        private DbSet<GroupList> _groupList;
        public GroupListRepository(PeakPalsContext context) : base(context)
        {
            _groupList = context.GroupList;
        }

        public void AddGroupToGroupList(int climberID, int communityGroupID)
        {
            // Create a new GroupList object
            var groupList = new GroupList
            {
                ClimberID = climberID,
                CommunityGroupID = communityGroupID
            };

            // Add the new GroupList object to the GroupList table
            _groupList.Add(groupList);
        }
        
        public List<GroupList> GetGroupListByClimberIDAndGroupID(int climberID, int communityGroupID)
        {
            // Search the GroupList table for GroupList objects with the specified climber ID and community group ID
            // If there are any, return the list; otherwise, return an empty list
            var groupList = _groupList.Where(c => c.ClimberID == climberID && c.CommunityGroupID == communityGroupID).ToList();

            if (groupList != null)
            {
                return groupList;
            }
            else
            {
                return new List<GroupList>();
            }
        }

        public int GetGroupMemberCountByGroupID(int communityGroupID)
        {
            // Search the GroupList table for GroupList objects with the specified community group ID
            // Return the count of the GroupList objects
            return  _groupList.Where(c => c.CommunityGroupID == communityGroupID).Count();
        }

        public List<GroupList> GetGroupListByGroupID(int communityGroupID)
        {
            // Search the GroupList table for GroupList objects with the specified community group ID
            // If there are any, return the list; otherwise, return an empty list
            var groupList = _groupList.Where(c => c.CommunityGroupID == communityGroupID).ToList();

            if (groupList != null)
            {
                return groupList;
            }
            else
            {
                return new List<GroupList>();
            }
        }

        public List<GroupList> GetGroupListByClimberID(int climberID)
        {
            // Search the GroupList table for GroupList objects with the specified climber ID
            // If there are any, return the list; otherwise, return an empty list
            var groupList = _groupList.Where(c => c.ClimberID == climberID).ToList();

            if (groupList != null)
            {
                return groupList;
            }
            else
            {
                return new List<GroupList>();
            }
        }
    }
}