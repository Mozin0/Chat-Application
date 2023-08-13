using Microsoft.EntityFrameworkCore;

namespace ChatApplicationSignalR.Factories
{
    public class DbContextFactory 
    {
        private IConfiguration _configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ChatApplicationDbContext CreateDbContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<ChatApplicationDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            return new ChatApplicationDbContext(optionBuilder.Options);
        }
    }
}
