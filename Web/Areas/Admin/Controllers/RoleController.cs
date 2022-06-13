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
    [Authorize]
    public class RoleController : BaseController<RoleController>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMenuRepository _menuRepository;
        private readonly IRole_MenuRepository _role_MenuRepository;
        private readonly IUser_RoleRepository _user_RoleRepository;
        private readonly IRole_Project_PermissionRepository _role_Project_PermissionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IMenuRepository menuRepository,
            IRole_MenuRepository role_MenuRepository,
            IUser_RoleRepository user_RoleRepository,
            IRole_Project_PermissionRepository role_Project_PermissionRepository,
            IProjectRepository projectRepository,
            IPermissionRepository permissionRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _menuRepository = menuRepository;
            _role_MenuRepository = role_MenuRepository;
            _user_RoleRepository = user_RoleRepository;
            _role_Project_PermissionRepository = role_Project_PermissionRepository;
            _projectRepository = projectRepository;
            _permissionRepository = permissionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var roles = await _roleManager.Roles.Where(x => x.Active && x.Public && !x.SystemRole).ToListAsync();

            var model = _mapper.Map<IEnumerable<RoleViewModel>>(roles);

            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> GetUsers(string roleId)
        {
            var user_role = await _user_RoleRepository.GetByRoleId(roleId);
            var model = user_role?.Select(x => x.UserId).ToList();

            return Json(model);
        }

        public async Task<IActionResult> GetPermissions(string roleId, long projectId)
        {
            var role_Project_Permissions = await _role_Project_PermissionRepository.GetByRoleAndProjectId(roleId, projectId);
            var model = role_Project_Permissions?.Select(x => x.PermissionId).ToList();

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

                       _permissionCommon.RefreshPermission();

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

        public async Task<IActionResult> RoleProjectPermissionList(long projectId)
        {
            if (projectId == 0)
            {
                _notify.Error("بروز مشکل در انتخاب پروژه");

                return RedirectToAction("Index", "Project");
            }

            var role_Project_Permissions = await _role_Project_PermissionRepository.GetByProjectId(projectId);

            var model = _mapper.Map<List<Role_Project_PermissionViewModel>>(role_Project_Permissions);

            model = model.GroupBy(x => new { x.ProjectId, x.RoleId }).Select(x =>

               new Role_Project_PermissionViewModel
               {
                   Role = x.Select(y => y.Role).FirstOrDefault(),
                   ProjectId = x.Key.ProjectId,
                   Permissions = x.Select(y => y.Permission).ToList(),
               }).ToList();

            return View(model);
        }

        public async Task<IActionResult> RoleProjectPermission(long projectId, string? roleId = null)
        {
            if (projectId == 0)
            {
                _notify.Error("بروز مشکل در انتخاب پروژه");

                return RedirectToAction("Index", "Project");
            }

            ViewData["PermissionList"] = _mapper.Map<List<PermissionsViewModel>>(await _permissionRepository.GetListAsync());

            ViewData["RoleList"] = _mapper.Map<List<RoleViewModel>>(await _roleManager.Roles.Where(x => x.Active && x.Public).ToListAsync());

            var model = new Role_Project_PermissionViewModel { ProjectId = projectId };

            if (roleId != null)
            {
                model.RoleId = roleId;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRoleProjectPermission(Role_Project_PermissionViewModel model)
        {
            var role_Project_Permissions = await _role_Project_PermissionRepository.GetByRoleAndProjectId(model.RoleId, model.ProjectId);

            if (role_Project_Permissions != null && role_Project_Permissions.Count > 0)
            {
                foreach (var item in role_Project_Permissions)
                {
                    await _role_Project_PermissionRepository.DeleteAsync(item);
                }
            }

            if (model.PermissionIds?.Count > 0)
            {
                foreach (var permissionId in model.PermissionIds)
                {
                    var resMenu = await _role_Project_PermissionRepository.InsertAndSaveAsync(new Role_Project_Permission
                    {
                        PermissionId = permissionId,
                        RoleId = model.RoleId,
                        ProjectId = model.ProjectId
                    });

                    if (resMenu == 0)
                    {
                        _notify.Error("عملیات ثبت سطح دسترسی با خطا مواجعه شد.");
                        return RedirectToAction("Index");
                    }
                }
            }

            _notify.Success("عملیات با موفقیت انجام شد.");

            return RedirectToAction("RoleProjectPermissionList", new { projectId = model.ProjectId });
        }

        public async Task<IActionResult> UserRole()
        {
            var users = await _userManager.Users.Where(m => m.UserType == Common.Enums.UserType.SystemUser).ToListAsync();

            ViewData["UserList"] = _mapper.Map<List<UserViewModel>>(users);

            ViewData["RoleList"] = _mapper.Map<List<RoleViewModel>>(await _roleManager.Roles.Where(x => x.Active && x.Public).ToListAsync());

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
                    var resMenu = await _user_RoleRepository.InsertAndSaveAsync(new IdentityUserRole<string>
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

        public async Task<IActionResult> DeleteRoleProjectPermission(long id)
        {
            var role_Project_Permission = await _role_Project_PermissionRepository.GetByIdAsync(id);
            if (role_Project_Permission is not null)
            {
                await _role_Project_PermissionRepository.DeleteAsync(role_Project_Permission);
                var role_Project_PermissionsRes = await _role_Project_PermissionRepository.SaveChangesAsync();

                if (role_Project_PermissionsRes > 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}