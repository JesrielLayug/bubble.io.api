using Microsoft.AspNetCore.Identity;

namespace Bubble.io.Entities
{
    public class User : IdentityUser
    {
        public string Fistname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
    }
}
