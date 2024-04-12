using System.Collections;

namespace Bubble.io.Entities.DTOs
{
    public class DTOChatMessage
    {
        public DTOProfileData sender {  get; set; }
        public DTOProfileData receiver { get; set; }
        public List<DTOChatContent> chats {  get; set; }
    }
}
