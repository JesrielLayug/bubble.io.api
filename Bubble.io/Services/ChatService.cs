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
        private readonly IProfileRepository profileRepository;
        private readonly IHubContext<ChatHub> hubContext;

        public ChatService(IChatRepository chatRepository, IProfileRepository profileRepository, IHubContext<ChatHub> hubContext)
        {
            this.chatRepository = chatRepository;
            this.profileRepository = profileRepository;
            this.hubContext = hubContext;
        }

        public async Task<IEnumerable<DTOChatMessage?>> Get(string senderId, string recieverId)
        {
            var domainMessages = await chatRepository.GetAll(senderId, recieverId);
            var users = await profileRepository.GetAll();

            var chatMessages = domainMessages.convert(users);

            return chatMessages;
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
            await hubContext.Clients.All.SendAsync("RecieveMessage", chat.senderId, chat.receiverId, chat.content);
        }
    }
}
