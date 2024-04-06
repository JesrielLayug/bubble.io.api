using Microsoft.AspNetCore.SignalR;

namespace Bubble.io.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string recieverId, string content)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderId, recieverId, content);
        }
    }
}
