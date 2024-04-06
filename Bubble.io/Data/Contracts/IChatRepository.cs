using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IChatRepository
    {
        Task Add(ChatMessage chat);
        Task<IEnumerable<ChatMessage>> GetAll(string senderId, string receiverId);
        //Task<IEnumerable<ChatMessage>> Get(string senderId, string recieverId);
    }
}
