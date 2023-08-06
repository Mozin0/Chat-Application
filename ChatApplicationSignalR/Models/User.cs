using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace ChatApplicationSignalR.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Conversation> Conversations { get; set; }

        public User()
        {
            Conversations = new();  
        }
    }
}
