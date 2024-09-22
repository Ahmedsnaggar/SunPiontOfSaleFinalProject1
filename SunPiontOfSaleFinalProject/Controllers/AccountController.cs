using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunPiontOfSaleFinalProject.Repositories.Context;
using SunPiontOfSaleFinalProject.Repositories.ViewModels;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newuser = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = ""
                 };
                    var result = await _userManager.CreateAsync(newuser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                foreach(var item in result.Errors)
                    {
                    ModelState.AddModelError(string.Empty, item.Description);
                    }
                
            }
            return View(model);
        }
    }
}
