using ChatApplicationSignalR.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApplicationSignalR.Pages
{
    [Authorize]
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        public LogOutModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
          var isAuthenticated =  HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                await _signInManager.SignOutAsync();
                Response.Redirect("/home");
            }            
            return Page();
        }
    }
}
