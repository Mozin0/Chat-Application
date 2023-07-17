using ChatApplicationSignalR.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplicationSignalR.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly RoleManager _roleManager;

        public RoleController(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _roleManager.CreateRole(roleName);
            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' created Successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
