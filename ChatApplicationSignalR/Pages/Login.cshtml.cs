using ChatApplicationSignalR.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace ChatApplicationSignalR.Pages
{
    [Route("/")]
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly UserManager _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly NavigationManager _navigationManager;
        [BindProperty]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        public string? Username { get; set; }
        [BindProperty]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        public string? ValidationMessage { get; set; }

        public LoginModel(UserManager userManager, SignInManager<User> signInManager, NavigationManager navigationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _navigationManager = navigationManager;
        }

        public void OnGet()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                Response.Redirect("/home");
            }
        }

        private async Task<bool> ValidateUserAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ValidationMessage = "Wrong Username or Password";
                return false;
            }

            var user = await _userManager.GetUserByUsernameAsync(Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, Password))
            {
                ValidationMessage = "Wrong Username or Password";
                return false;
            }

            return true;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await ValidateUserAsync())
                {
                    if (Username is not null)
                    {
                        var user = await _userManager.GetUserByUsernameAsync(Username);
                        if (user is not null)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                            if (result.Succeeded)
                            {
                                return Redirect("/home");
                            }
                            else
                            {
                                ValidationMessage = "Something went wrong with sign-in";
                            }
                        }
                    }
                }
            }
            return Page();
        }

    }
}
