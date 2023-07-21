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
        public string? ValidationMessage { get; set;}

        public LoginModel(UserManager userManager, SignInManager<User> signInManager, NavigationManager navigationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _navigationManager = navigationManager;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/home");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Username != null)
                {
                    var user = await _userManager.GetUserByUsernameAsync(Username);
                    var checkPassword = await _userManager.CheckPasswordAsync(user, Password);

                    if (user != null && checkPassword)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                        if (result.Succeeded)
                        {
                            Response.Redirect("/home");
                        }
                        else
                        {
                            ValidationMessage = "Wrong Username or Password";
                        }
                    }
                    else
                    {
                        ValidationMessage = "Wrong Username or Password";
                    }
                }
                else
                {
                    ValidationMessage = "Wrong Username or Password";
                }
            }
            return Page();
        }
    }
}
