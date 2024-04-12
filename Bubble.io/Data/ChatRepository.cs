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

        public async Task<IEnumerable<ChatMessage>> GetChatHistory(string senderId, string receiverId)
        {
            return await db.ChatMessages
                .Where(m =>
                    (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                    (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
