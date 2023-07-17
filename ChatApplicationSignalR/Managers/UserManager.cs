using Microsoft.AspNetCore.Identity;

namespace ChatApplicationSignalR.Managers
{
    public class UserManager
    {
        private readonly UserManager<User> _userManager;

        public UserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUser(string username, string password)
        {
            var user = new User
            {
                UserName = username
            };

            var result = await _userManager.CreateAsync(user,password);
            return result;
        }
    }
}
