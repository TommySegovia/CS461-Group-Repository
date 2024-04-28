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

    }
}