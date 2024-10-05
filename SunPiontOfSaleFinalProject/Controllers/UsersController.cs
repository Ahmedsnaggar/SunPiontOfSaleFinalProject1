using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SunPiontOfSaleFinalProject.Repositories.Context;
using SunPiontOfSaleFinalProject.Repositories.ViewModels;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IHttpContextAccessor _contextAccessor;
        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
        }
        #region Users
        public async Task<IActionResult> UsersList()
        {
           
            var users =  await _userManager.Users.Select(c=> new UserViewModel()
            {
                Id = c.Id,
                UserName = c.UserName,
                Email = c.Email,
                Roles = _userManager.GetRolesAsync(c).Result
            }).ToListAsync();
            
            return View(users);
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return RedirectToAction("index", "home");
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,


                Roles = roles.Select(r => new RolesCheckedViewModel()
                {
                    RoleName = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return RedirectToAction("UsersList");

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r=> r.IsSelected== true).Select(rn=> rn.RoleName));
            return RedirectToAction("UsersList");
        }
        [AllowAnonymous]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string Username = _contextAccessor.HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(Username);
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion



        #region Roles

        public async Task<IActionResult> RolesList()
        {
            var roles = await _roleManager.Roles.Select(r=> new RolesViewModel()
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToListAsync();
            return View(roles);
        }

        public IActionResult CreateRole()
        {
            var role = new RolesViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Id = model.Id,
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        #endregion
    }
}
