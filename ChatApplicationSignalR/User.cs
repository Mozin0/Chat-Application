using Microsoft.AspNetCore.Identity;

namespace ChatApplicationSignalR
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
