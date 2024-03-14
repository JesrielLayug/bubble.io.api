using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.io.Entities
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Fistname { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        public string Bio {  get; set; } = string.Empty;
        [Required]
        public string IdentityId { get; set; } = string.Empty;
    }
}
