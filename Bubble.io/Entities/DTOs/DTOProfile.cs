using System.ComponentModel.DataAnnotations;

namespace Bubble.io.Entities.DTOs
{
    public class DTOProfile
    {
        public string id { get; set; }
        public string fistname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;
        public string email {  get; set; } = string.Empty;
        public string imageUrl {  get; set; } = string.Empty;
        public byte[] imageData { get; set; }
    }
}
