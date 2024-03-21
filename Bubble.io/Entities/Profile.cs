using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.io.Entities
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Fistname { get; set; } = string.Empty;
        public string? Lastname { get; set; } = string.Empty;
        public string? Bio {  get; set; } = string.Empty;
        public string? ImageUrl {  get; set; } = string.Empty;
        public string? IdentityId { get; set; } = string.Empty;
    }
}
