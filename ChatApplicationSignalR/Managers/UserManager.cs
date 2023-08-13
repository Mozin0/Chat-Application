using ChatApplicationSignalR.Factories;
using ChatApplicationSignalR.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;

namespace ChatApplicationSignalR.Managers
{
    public class UserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly DbContextFactory _dbContextFactory;

        public UserManager(UserManager<User> userManager, DbContextFactory dbContextFactory )
        {
            _userManager = userManager;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IdentityResult> AddUserAsync(string username, string password)
        {
            var user = new User
            {
                UserName = username
            };

            var result = await _userManager.CreateAsync(user,password);
            return result;
        }

        public async Task<IdentityResult> AssignRoleToUserAsync(string username, string roleName)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null) throw new InvalidOperationException($"User: {username} not found.");
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<string> GetRoleFromUserAsync(string username)
        {
            using var _dbContext = _dbContextFactory.CreateDbContext();
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                var roles = await _dbContext.UserRoles
                    .Where(userRole => userRole.UserId == user.Id)
                    .Join(_dbContext.Roles, userRole => userRole.RoleId, role => role.Id, (userRole, role) => role.Name)
                    .ToListAsync();
                return string.Join(", ", roles);
            }
            return string.Empty; 
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            using var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
           return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Users.ToListAsync();
        }

        public async Task DeleteUser(string username)
        {
            using var dbcontext = _dbContextFactory.CreateDbContext();
            var user = await GetUserByUsernameAsync(username);
            if (user is not null)
            {
                dbcontext.Users.Remove(user);
                await dbcontext.SaveChangesAsync();
            }
        }

    }
}
