using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApplicationSignalR
{
    public class ChatApplicationDbContext : IdentityDbContext<User>
    {
        public ChatApplicationDbContext(DbContextOptions<ChatApplicationDbContext> options) : base(options)
        {

        }
    }
}
