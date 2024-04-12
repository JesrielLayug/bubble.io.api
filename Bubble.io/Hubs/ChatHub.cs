using Bubble.io.Data.Contracts;
using Bubble.io.Entities.DTOs;
using Bubble.io.Extensions;
using Bubble.io.Services;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bubble.io.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService chatService;
        private readonly ILogger<ChatHub> logger;
        private readonly IProfileService profileService;
        private readonly IChatRepository chatRepository;

        public ChatHub(IChatService chatService, IProfileService profileService, IChatRepository chatRepository, ILogger<ChatHub> logger)
        {
            this.chatService = chatService;
            this.logger = logger;
            this.profileService = profileService;
            this.chatRepository = chatRepository;
        }

        public async Task SendMessage(DTOChatMessageRequest request)
        {
            try
            {
                await chatService.Send(request);

                await Clients.All.SendAsync("ReceiveMessage", request);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while sending message.");
                throw;
            }
        }

        public async Task<List<DTOChatContent>> GetChatHistory(string senderId, string receiverId)
        {
            var chatHistory = await chatService.GetChatHistory(senderId, receiverId);

            await Clients.Caller.SendAsync("ReceiveChatHistory", chatHistory);

            return chatHistory;
        }
    }
}
