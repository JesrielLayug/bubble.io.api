namespace Bubble.io.Entities.DTOs
{
    public class DTOChatMessageRequest
    {
        public string senderId {  get; set; }
        public string receiverId { get; set; }
        public string content { get; set; }
    }
}
