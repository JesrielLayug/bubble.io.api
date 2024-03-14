using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.io.Entities
{
    public class ProfileImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string IdentityId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public byte[] ImageData { get; set; }
    }
}
