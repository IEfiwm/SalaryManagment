using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMenuRepository _menuRepository;
        private readonly IRole_MenuRepository _role_MenuRepository;

        public RoleController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMenuRepository menuRepository,
            IRole_MenuRepository role_MenuRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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

        //public async Task<IActionResult> OnGetCreate(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", new RoleViewModel()) });
        //    else
        //    {
        //        var role = await _roleManager.FindByIdAsync(id);
        //        if (role == null) _notify.Error("Unexpected Error. Role not found!");
        //        var roleviewModel = _mapper.Map<RoleViewModel>(role);
        //        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", roleviewModel) });
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> OnPostCreate(RoleViewModel role)
        //{
        //    if (ModelState.IsValid && role.Name != "SuperAdmin" && role.Name != "Basic")
        //    {
        //        if (string.IsNullOrEmpty(role.Id))
        //        {
        //            var resAdd = await _roleManager.CreateAsync(new IdentityRole(role.Name));
        //            if (resAdd.Errors?.Count() > 0)
        //            {
        //                _notify.Error("عملیات ثبت نقش با خطا مواجعه شد.");
        //                return RedirectToAction("Index");

        //            }
        //        }
        //        else
        //        {
        //            var existingRole = await _roleManager.FindByIdAsync(role.Id);
        //            existingRole.Name = role.Name;
        //            existingRole.NormalizedName = role.Name.ToUpper();
        //            await _roleManager.UpdateAsync(existingRole);
        //            _notify.Success("Role Updated");
        //        }

        //        var roles = await _roleManager.Roles.ToListAsync();
        //        var mappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        //        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", mappedRoles);
        //        return new JsonResult(new { isValid = true, html = html });
        //    }
        //    else
        //    {
        //        var html = await _viewRenderer.RenderViewToStringAsync<RoleViewModel>("_CreateOrEdit", role);
        //        return new JsonResult(new { isValid = false, html = html });
        //    }
        //}

        //public async Task<JsonResult> OnPostDelete(string id)
        //{
        //    var existingRole = await _roleManager.FindByIdAsync(id);
        //    if (existingRole.Name != "SuperAdmin" && existingRole.Name != "Basic")
        //    {
        //        //TODO Check if Any Users already uses this Role
        //        bool roleIsNotUsed = true;
        //        var allUsers = await _userManager.Users.ToListAsync();
        //        foreach (var user in allUsers)
        //        {
        //            if (await _userManager.IsInRoleAsync(user, existingRole.Name))
        //            {
        //                roleIsNotUsed = false;
        //            }
        //        }
        //        if (roleIsNotUsed)
        //        {
        //            await _roleManager.DeleteAsync(existingRole);
        //            _notify.Success($"Role {existingRole.Name} deleted.");
        //        }
        //        else
        //        {
        //            _notify.Error("Role is being Used by another User. Cannot Delete.");
        //        }
        //    }
        //    else
        //    {
        //        _notify.Error($"Not allowed to  delete {existingRole.Name} Role.");
        //    }
        //    var roles = await _roleManager.Roles.ToListAsync();
        //    var mappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        //    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", mappedRoles);
        //    return new JsonResult(new { isValid = true, html = html });
        //}
    }
}