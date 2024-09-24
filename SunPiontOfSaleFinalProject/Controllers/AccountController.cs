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
                    Address = "",
                    EmailConfirmed = true
                 };
                    var result = await _userManager.CreateAsync(newuser, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newuser, "User");
                    await _signInManager.SignInAsync(newuser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                
                foreach(var item in result.Errors)
                    {
                    ModelState.AddModelError(string.Empty, item.Description);
                    }
                
            }
            return View(model);
        }

        public IActionResult Login(string? ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.UserName);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "User name not correct");
                        return View(model);
                    }
                   
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Rememberme, false);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                    ModelState.AddModelError(string.Empty, "Password not correct");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AccessDeniedTest()
        {
            return View();
        }
    }
}
