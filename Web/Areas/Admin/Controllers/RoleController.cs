using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class RoleController : BaseController<RoleController>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMenuRepository _menuRepository;
        private readonly IRole_MenuRepository _role_MenuRepository;
        private readonly IUser_RoleRepository _user_RoleRepository;

        public RoleController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IMenuRepository menuRepository,
            IRole_MenuRepository role_MenuRepository,
            IUser_RoleRepository user_RoleRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _menuRepository = menuRepository;
            _role_MenuRepository = role_MenuRepository;
            _user_RoleRepository = user_RoleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var model = _mapper.Map<IEnumerable<RoleViewModel>>(roles);
            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> GetUsers(string roleId)
        {
            var user_role = await _user_RoleRepository.GetByRoleId(roleId);
            var model = user_role?.Select(x => x.UserId).ToList();

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["MenuList"] = _mapper.Map<List<MenuViewModel>>(await _menuRepository.GetListAsync());
            return View();
        }

        public async Task<IActionResult> Edit(string roleId)
        {
            var model = _mapper.Map<RoleViewModel>(await _roleManager.FindByIdAsync(roleId));

            var menus = (await _role_MenuRepository.GetByRoleId(roleId))?.Select(x => x.Menu);

            if (menus != null && menus.Count() > 0)
            {
                model.Menus = _mapper.Map<List<MenuViewModel>>(menus);
            }

            ViewData["MenuList"] = _mapper.Map<List<MenuViewModel>>(await _menuRepository.GetListAsync());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid && model.Name != "SuperAdmin" && model.Name != "Basic")
            {
                model.Id = Guid.NewGuid().ToString();
                var role = await _roleManager.CreateAsync(_mapper.Map<ApplicationRole>(model));

                if (string.IsNullOrEmpty(model.Id))
                {
                    _notify.Error("عملیات ثبت نقش با خطا مواجعه شد.");
                    return RedirectToAction("Index");
                }

                if (model.MenuIds.Count > 0)
                {
                    foreach (var menuId in model.MenuIds)
                    {
                        var resMenu = await _role_MenuRepository.InsertAndSaveAsync(new Role_Menu
                        {
                            Disable = false,
                            MenuId = menuId,
                            RoleId = model.Id
                        });

                        if (resMenu == 0)
                        {
                            _notify.Error("عملیات ثبت منو با خطا مواجعه شد.");
                            return RedirectToAction("Index");
                        }
                    }
                }

                _notify.Success("عملیات با موفقیت انجام شد.");
            }
            else
            {
                _notify.Error("عملیات ثبت منو با خطا مواجعه شد.");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid && model.Name != "SuperAdmin" && model.Name != "Basic")
            {
                var modelAcc = _mapper.Map(model, await _roleManager.FindByIdAsync(model.Id));

                modelAcc.Name = model.Name;
                modelAcc.NormalizedName = model.Name.ToUpper();

                await _roleManager.UpdateAsync(modelAcc);

                var role_menus = await _role_MenuRepository.GetByRoleId(model.Id);

                if (role_menus != null && role_menus.Count > 0)
                {
                    foreach (var roleMenu in role_menus)
                    {
                        await _role_MenuRepository.DeleteAsync(roleMenu);
                    }
                }

                if (model.MenuIds?.Count > 0)
                {
                    foreach (var menuId in model.MenuIds)
                    {
                        var resMenu = await _role_MenuRepository.InsertAndSaveAsync(new Role_Menu
                        {
                            Disable = false,
                            MenuId = menuId,
                            RoleId = model.Id
                        });

                        if (resMenu == 0)
                        {
                            _notify.Error("عملیات ثبت منو با خطا مواجعه شد.");
                            return RedirectToAction("Index");
                        }
                    }
                }

                _notify.Success("عملیات با موفقیت انجام شد.");
            }
            else
            {
                _notify.Error("عملیات ثبت منو با خطا مواجعه شد.");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string roleId)
        {
            var model = await _roleManager.FindByIdAsync(roleId);
            bool roleIsNotUsed = true;
            var allUsers = await _userManager.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, model.Name))
                {
                    roleIsNotUsed = false;
                }
            }
            if (roleIsNotUsed)
            {
                await _roleManager.DeleteAsync(model);
                _notify.Success("حذف با موفقیت انجام شد.");
            }
            else
            {
                _notify.Error("حذف امکان پذیر نیست، نقش مورد نظر در حال استفاده می باشد.");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserRole()
        {
            var users = await _userManager.Users.Where(m => m.UserType == Common.Enums.UserType.SystemUser).ToListAsync();

            ViewData["UserList"] = _mapper.Map<List<UserViewModel>>(users);
            ViewData["RoleList"] = _mapper.Map<List<RoleViewModel>>(await _roleManager.Roles.ToListAsync());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserRole(User_RoleViewModel model)
        {
            var user_role = await _user_RoleRepository.GetByRoleId(model.RoleId);

            if (user_role != null && user_role.Count > 0)
            {
                foreach (var roleMenu in user_role)
                {
                    await _user_RoleRepository.DeleteAsync(roleMenu);
                }
            }

            if (model.UserId?.Count > 0)
            {
                foreach (var userId in model.UserId)
                {
                    var resMenu = await _user_RoleRepository.InsertAndSaveAsync(new User_Role
                    {
                        UserId = userId,
                        RoleId = model.RoleId
                    });

                    if (resMenu == 0)
                    {
                        _notify.Error("عملیات ثبت کاربر با خطا مواجعه شد.");
                        return RedirectToAction("Index");
                    }
                }
            }

            _notify.Success("عملیات با موفقیت انجام شد.");

            return RedirectToAction("Index");
        }

    }
}