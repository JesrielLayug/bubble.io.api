using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;
using System.Text.RegularExpressions;

namespace Bubble.io.Extensions
{
    public static class ChatExtension
    {
        public static DTOChatMessage Convert(
            this IEnumerable<ChatMessage> messages,
            IEnumerable<DTOProfileData> users
        )
        {
            if (messages == null || !messages.Any() || users == null || !users.Any())
                return null; 

            var firstMessage = messages.First();

            var senderProfile = users.FirstOrDefault(u => u.id == firstMessage.SenderId);
            var receiverProfile = users.FirstOrDefault(u => u.id == firstMessage.ReceiverId);

            if (senderProfile == null || receiverProfile == null)
                return null; 

            var chatContents = messages.Select(message => new DTOChatContent
            {
                content = message.Content,
                timeStamp = message.Timestamp
            }).OrderBy(chat => chat.timeStamp).ToList();

            return new DTOChatMessage
            {
                sender = new DTOProfileData
                {
                    id = senderProfile.id,
                    firstname = senderProfile.firstname,
                    lastname = senderProfile.lastname,
                    email = senderProfile.email,
                    imageData = senderProfile.imageData,
                    imageUrl = senderProfile.imageUrl
                },
                receiver = new DTOProfileData
                {
                    id = receiverProfile.id,
                    firstname = receiverProfile.firstname,
                    lastname = receiverProfile.lastname,
                    email = receiverProfile.email,
                    imageData = receiverProfile.imageData,
                    imageUrl = receiverProfile.imageUrl
                },
                chats = chatContents
            };
        }
    }
}
