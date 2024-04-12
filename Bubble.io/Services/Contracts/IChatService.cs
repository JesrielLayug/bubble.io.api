using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IChatService
    {
        Task Send(DTOChatMessageRequest chat);
        Task<List<DTOChatContent>> GetChatHistory(string senderId, string receiverId);
    }
}
