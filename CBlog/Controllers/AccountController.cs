using CBlog.Models;
using CBlog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CBlog.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
		{
            if(!ModelState.IsValid)
			{
                return View();
			}
            else
			{
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, DateOfBirth = model.DateOfBirth };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
		{
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
