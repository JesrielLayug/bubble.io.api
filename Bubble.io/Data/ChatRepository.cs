using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bubble.io.Data
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext db;

        public ChatRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task Add(ChatMessage chat)
        {
            await db.ChatMessages.AddAsync(chat);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetAll(string senderId, string reciverId)
        {
            return await db.ChatMessages.Where(m => m.SenderId == senderId && m.ReceiverId == reciverId).ToListAsync();
        }


        //public async Task<IEnumerable<ChatMessage>> Get(string senderId, string recieverId)
        //{
        //    return await db.ChatMessages
        //        .Where(m => (m.SenderId == senderId && m.ReceiverId == recieverId)
        //                 || (m.SenderId == recieverId && m.ReceiverId == senderId))
        //        .OrderBy(m => m.Timestamp)
        //        .ToListAsync();
        //}
    }
}
