using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel;

namespace ChatApplicationSignalR.Managers
{
    public class UserManager
    {
        private readonly UserManager<User> _userManager;

        public UserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
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
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return string.Join(", ", roles);
            }

            return string.Empty; 
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
           return await _userManager.CheckPasswordAsync(user, password);
        }

    }
}
