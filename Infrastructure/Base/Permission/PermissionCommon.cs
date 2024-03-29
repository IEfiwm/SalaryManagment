﻿using Alachisoft.NCache.Client;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Base.Permission
{
    public class PermissionCommon : IPermissionCommon
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRole_Project_PermissionRepository _role_Project_PermissionRepository;
        private readonly IRole_MenuRepository _role_MenuRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _memoryCache;
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();

        public PermissionCommon(IPermissionRepository permissionRepository,
            IRole_Project_PermissionRepository role_Project_PermissionRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IProjectRepository projectRepository,
            SignInManager<ApplicationUser> signInManager,
            IRole_MenuRepository role_MenuRepository,
            IMemoryCache memoryCache)
        {
            _permissionRepository = permissionRepository;
            _role_Project_PermissionRepository = role_Project_PermissionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _projectRepository = projectRepository;
            _signInManager = signInManager;
            _role_MenuRepository = role_MenuRepository;
            _memoryCache = memoryCache;
        }

        public async Task<bool> CheckProjectPermissionByProjectId(string permissionName, ClaimsPrincipal userClaim, long projectId)
        {
            if (!userClaim.IsInRole("SuperAdmin"))
            {
                if (projectId == 0)
                {
                    return true;
                }

                var user = await _userManager.GetUserAsync(userClaim);

                var roleNames = await _userManager.GetRolesAsync(user);

                var roles = _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToList()?.Select(x => x.Id);

                var permission = await GetOrCreateByName(permissionName);

                var role_Project_Permission = await _role_Project_PermissionRepository.GetByProjectId(projectId);

                if (role_Project_Permission.Any(x => x.PermissionId == permission.Id && roles.Contains(x.RoleId)))
                {
                    return true;
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        public void RefreshPermission()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }

        public void RefreshPermission(ApplicationUser user)
        {
            var key = string.Format("menu_{0}", user.UserName);

            _memoryCache.Remove(key);

        }

        public async Task<List<Menu>> GetMenuOfUser(ApplicationUser user)
        {
            var key = string.Format("menu_{0}", user.UserName);

            var menu = _memoryCache.Get<List<Menu>>(key);

            if (menu is not null && menu.Count > 0)
            {
                return menu;
            }

            List<ApplicationRole> roles = new List<ApplicationRole>();

            var roleNames = await _userManager.GetRolesAsync(user);

            roles = _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToList();

            var menuHeader = new List<Menu>();

            foreach (var role in roles)
            {
                var role_menu = await _role_MenuRepository.GetByRoleId(role.Id);

                if (role_menu is not null)
                    menuHeader.AddRange(role_menu.Select(x => x.Menu));

                menuHeader.Distinct();

            }

            if (menuHeader != null)
            {
                var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);
                options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                _memoryCache.Set<List<Menu>>(key, menuHeader, options);

            }



            return menuHeader;
        }

        public async Task<Domain.Entities.Basic.Permission> GetOrCreateByName(string name)
        {
            var permission = await _permissionRepository.GetByName(name);

            if (permission == null)
            {
                permission = new Domain.Entities.Basic.Permission
                {
                    Name = name
                };

                var permissionId = await _permissionRepository.InsertAndSaveAsync(permission);
                permission.Id = permissionId;
            }

            return permission;
        }

        public async Task<List<Project>> GetProjectsByPermission(string permissionName, ClaimsPrincipal userClaim)
        {
            if (!userClaim.IsInRole("SuperAdmin"))
            {
                var user = await _userManager.GetUserAsync(userClaim);

                var roleNames = await _userManager.GetRolesAsync(user);

                var roles = _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToList();

                var permission = await GetOrCreateByName(permissionName);

                var role_Project_Permission = await _role_Project_PermissionRepository.GetByRoleListAndPermissionId(roles.Select(x => x.Id), permission.Id);

                var model = role_Project_Permission.Select(x => x.Project).ToList();

                return model
                    .Where(m => m.ProjectStatus == Common.Enums.ProjectStatus.Started)
                    .ToList();
            }
            else
            {
                return await _projectRepository.GetListAsync();
            }
        }

        public async Task<bool> SetFullPermissionsProjectsToUser(Project project, ClaimsPrincipal userClaim)
        {
            /// add role with project name
            /// 
            var role = new ApplicationRole
            {
                Active = true,
                Name = " ادمین " + project.Title,
                Id = Guid.NewGuid().ToString(),
                SystemRole = false,

            };

            var res = await _roleManager.CreateAsync(role);
            if (!res.Succeeded)
                return false;
            /// 
            /// add role to user
            /// 
            var user = await _userManager.GetUserAsync(userClaim);

            var result = await _userManager.AddToRolesAsync(user, new List<string> { role.Name });
            if (!result.Succeeded)
                return false;
            var currentUser = await _userManager.GetUserAsync(userClaim);
            await _signInManager.RefreshSignInAsync(currentUser);
            await Identity.Seeds.DefaultSuperAdminUser.SeedAsync(_userManager, _roleManager);

            /// 
            /// add 4 permission with role to project
            /// 

            foreach (var item in new List<string> { "Show", "ExportBank", "ExportInsuranceList", "CreateProjectRule", "ShowDoshboard", "PersonnelList", "CreatePersonnel", "ShowProjectRule", "ImportAttendance", "DeleteAttendance", "ShowMonthlyAttendance", "DeleteMonthlyAttendance", "ShowContractList", "ShowAttendance", "ShowPayRollList", "ExportInsuranceSummary", "ExportTaxList", "ExportTaxSummary", "EditProjectRule", "EditPersonnel", "EditProject", "Create", "Update", "Delete", "DeleteProject" })
            {
                var permission = await GetOrCreateByName(item);

                var resPermission = await _role_Project_PermissionRepository.InsertAndSaveAsync(new Role_Project_Permission
                {
                    PermissionId = permission.Id,
                    RoleId = role.Id,
                    ProjectId = project.Id
                });

                if (resPermission == 0)
                    return false;

            }
            /// 
            return true;
        }
    }
}