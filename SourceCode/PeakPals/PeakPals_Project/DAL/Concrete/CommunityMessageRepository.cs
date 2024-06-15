using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;
using System.Collections.Generic;


namespace PeakPals_Project.DAL.Concrete
{
    public class CommunityMessageRepository : Repository<CommunityMessage>, ICommunityMessageRepository
    {
        private readonly DbSet<CommunityMessage> _communityMessage;
        private readonly PeakPalsContext _context;
        public CommunityMessageRepository(PeakPalsContext context) : base(context)
        {
            _communityMessage = context.CommunityMessage;
            _context = context;
        }

        // get all messages by group id
        public async Task<List<CommunityMessage>> GetMessagesById(int groupID)
        {
            // Search all community messages that belong to a certain group by their id
            // and return them as a list.

            var messages = await _communityMessage.Where(c => c.CommunityGroupId == groupID).ToListAsync();

            return messages;
        }

        // create a new message
        public async Task CreateMessage(int ClimberId, int CommunityGroupId, string DisplayName, string Message)
        {
            // Add a new message to the community message table
            // and return the message.
            CommunityMessage message = new CommunityMessage
            {
                ClimberId = ClimberId,
                CommunityGroupId = CommunityGroupId,
                DisplayName = DisplayName,
                Message = Message
            };


            _communityMessage.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}