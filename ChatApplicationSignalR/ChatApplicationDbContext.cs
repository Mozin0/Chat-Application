using ChatApplicationSignalR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApplicationSignalR
{
    public class ChatApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Message> Messages { get; set; }
        public ChatApplicationDbContext(DbContextOptions<ChatApplicationDbContext> options) : base(options)
        {

        }

    }
}
