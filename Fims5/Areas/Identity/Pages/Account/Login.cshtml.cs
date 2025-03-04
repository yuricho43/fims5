using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Fims5.Entities;
using System.ComponentModel.DataAnnotations;

namespace Fims5.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet()
        {
            ReturnUrl = Url.Content("~/home");
        }

        public async Task<ActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/home");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync
                    (Input.Name, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong Password or Username! Try again!");
                }
            }
            return Page();

        }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
    }
}
