using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;
using Bubble.io.Extensions;
using Bubble.io.Hubs;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace Bubble.io.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public async Task<List<DTOChatContent>> GetChatHistory(string senderId, string receiverId)
        {
            var chatMessages = await chatRepository.GetChatHistory(senderId, receiverId);

            var chatContents = chatMessages.Select(msg => new DTOChatContent
            {
                id = msg.Id,
                senderId = senderId,
                receiverId = receiverId,
                content = msg.Content,
                timeStamp = msg.Timestamp
            }).ToList();

            return chatContents;
        }

        public async Task Send(DTOChatMessageRequest chat)
        {
            var message = new ChatMessage
            {
                SenderId = chat.senderId,
                ReceiverId = chat.receiverId,
                Content = chat.content,
                Timestamp = DateTime.UtcNow,
            };

            await chatRepository.Add(message);
        }
    }
}
