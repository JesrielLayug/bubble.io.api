using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IChatRepository
    {
        Task Add(ChatMessage chat);
        Task<IEnumerable<ChatMessage>> GetChatHistory(string senderId, string receiverId);
    }
}
