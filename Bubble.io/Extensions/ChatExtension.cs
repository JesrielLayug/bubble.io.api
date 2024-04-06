using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;

namespace Bubble.io.Extensions
{
    public static class ChatExtension
    {
        public static IEnumerable<DTOChatMessage>
            convert(
            this IEnumerable<ChatMessage> messages,
            IEnumerable<Profile> users
            )
        {
            return (from message in messages
                    join sender in users on message.SenderId equals sender.IdentityId
                    join receiver in users on message.ReceiverId equals receiver.IdentityId
                    orderby message.Timestamp
                    select new DTOChatMessage
                    {
                        senderId = sender.IdentityId,
                        senderName = sender.Firstname,
                        recieverId = receiver.IdentityId,
                        recieverName = receiver.Firstname,
                        content = message.Content,
                        timeStamp = message.Timestamp.ToString(),
                    }).ToList();
        }
    }
}
