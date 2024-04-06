using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IChatService
    {
        Task Send(DTOChatMessageRequest chat);
        Task<IEnumerable<DTOChatMessage?>> Get(string senderId, string recieverId);
    }
}
