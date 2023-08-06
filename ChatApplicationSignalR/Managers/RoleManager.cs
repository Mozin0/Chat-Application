using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplicationSignalR.Managers
{
    public class RoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManager(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoles(string[] roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    if (await CreateRole(role) is { Succeeded: true } result) { }
                }
            }
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                if (await _roleManager.CreateAsync(role) is { Succeeded: true} result) { }
            }            
            return IdentityResult.Success;
        }
    }
}
