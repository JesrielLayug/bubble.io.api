namespace Bubble.io.Entities.DTOs
{
    public class DTOChatContent
    {
        public string id {  get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public string content { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
