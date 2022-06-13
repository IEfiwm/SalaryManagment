using Domain.Entities.Base.Identity;
using Web.Abstractions;
using Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserRoleController : BaseController<UserRoleController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRoleController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var viewModel = new List<UserRoleViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            ViewData["Title"] = $"{user.UserName} - Roles";
            ViewData["Caption"] = $"Manage {user.Email}'s Roles.";
            foreach (var role in _roleManager.Roles)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.Selected = true;
                }
                else
                {
                    userRoleViewModel.Selected = false;
                }
                viewModel.Add(userRoleViewModel);
            }
            var model = new ManageUserRoleViewModel()
            {
                UserId = userId,
                UserRoles = viewModel
            };

            return View(model);
        }

        public async Task<IActionResult> Update(string id, ManageUserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            //var result = await _userManager.RemoveFromRolesAsync(user, roles);
            var result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected && !roles.Contains(x.RoleName)).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(_userManager, _roleManager);
            _notify.Success($"Updated Roles for User '{user.Email}'");
            return RedirectToAction("Index", new { userId = id });
        }
    }
}