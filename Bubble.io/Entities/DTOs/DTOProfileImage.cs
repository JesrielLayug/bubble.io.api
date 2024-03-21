using Microsoft.AspNetCore.Http;

namespace Bubble.io.Entities.DTOs
{
    public class DTOProfileImage
    {
        public IFormFile imageData { get; set; }
        public string imageUrl { get; set; } = string.Empty;
    }
}
