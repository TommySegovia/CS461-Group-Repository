using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Concrete
{
    public class CommunityGroupRepository : Repository<CommunityGroup>, ICommunityGroupRepository
    {
        private DbSet<CommunityGroup> _communityGroup;
        public CommunityGroupRepository(PeakPalsContext context) : base(context)
        {
            _communityGroup = context.CommunityGroup;
        }

        public async Task<List<CommunityGroup>> GetGroupsByName(string groupName)
        {
            // Search the community group table for groups with names containing the search group name
            // If there are any, return the list; otherwise, return an empty list

            var groups = await _communityGroup.Where(c => c.Name.Contains(groupName)).ToListAsync();

            if (groups != null)
            {
                return groups;
            }
            else
            {
                return new List<CommunityGroup>();
            }
        }

        public async Task<CommunityGroup> GetGroupById(int groupID)
        {
            // Search the community group table for a group with the specified group ID
            // If the group exists, return it; otherwise, return null

            var group = await _communityGroup.FirstOrDefaultAsync(c => c.Id == groupID);

            return group;
        }

    }
}