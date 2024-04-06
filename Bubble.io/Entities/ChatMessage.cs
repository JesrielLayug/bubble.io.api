using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bubble.io.Entities
{
    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {  get; set; }
        public string SenderId { get; set; }
        public string ReceiverId {  get; set; }
        public string Content {  get; set; }
        public DateTime Timestamp { get; set; }
    }
}
