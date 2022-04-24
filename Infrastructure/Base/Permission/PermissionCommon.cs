using Alachisoft.NCache.Client;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public async Task<List<Menu>> GetMenuOfUser(ApplicationUser user)
        {
            var menuHeader = new List<Menu>();

            var key = string.Format("menu_{0}", user.UserName);

            var menus = _memoryCache.Get<List<Menu>>(key);

            //if (menuHeader == null)
            //{
            List<ApplicationRole> roles = new List<ApplicationRole>();
            var roleNames = await _userManager.GetRolesAsync(user);
            roles = _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToList();

            foreach (var role in roles)
            {
                var role_menu = await _role_MenuRepository.GetByRoleId(role.Id);

                if (role_menu is not null)
                    menuHeader.AddRange(role_menu.Select(x => x.Menu));

                menuHeader.Distinct();
            }

            //if (menuHeader != null)
            //{
            //    if (_appSettings.CacheDbResults && _cache != null)
            //    {
            //        var cacheItem = new CacheItem(menuHeader);
            //        cacheItem.SlidingExpiration = TimeSpan.AddMinutes(10);

            //        // Add CacheItem to cache
            //        _cache.Insert(cacheKey, cacheItem);
            //    }
            //}
            //}
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

                var permission = await GetOrCreateByName("Show");

                var role_Project_Permission = await _role_Project_PermissionRepository.GetByRoleListAndPermissionId(roles.Select(x => x.Id), permission.Id);

                var model = role_Project_Permission.Select(x => x.Project).ToList();
                return model;
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
            foreach (var item in new List<string> { "Show", "Create", "Update", "Delete" })
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
