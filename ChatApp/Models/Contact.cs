using ChatApp.Data;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class Contact
    {

        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public string ContactUserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
        public ApplicationUser ContactUser { get; set; } = null!;
        
    }
}
