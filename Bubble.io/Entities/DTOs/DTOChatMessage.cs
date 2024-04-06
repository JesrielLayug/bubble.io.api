namespace Bubble.io.Entities.DTOs
{
    public class DTOChatMessage
    {
        public string senderId {  get; set; }
        public string senderName { get; set; }
        public string recieverId { get; set; }
        public string recieverName { get;set; }
        public string content { get; set; }
        public string timeStamp { get; set; }
    }
}
