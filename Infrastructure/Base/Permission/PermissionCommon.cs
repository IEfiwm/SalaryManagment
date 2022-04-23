using Domain.Entities.Base.Identity;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public PermissionCommon(IPermissionRepository permissionRepository,
            IRole_Project_PermissionRepository role_Project_PermissionRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IProjectRepository projectRepository)
        {
            _permissionRepository = permissionRepository;
            _role_Project_PermissionRepository = role_Project_PermissionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _projectRepository = projectRepository;
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

        public async Task<List<Domain.Entities.Basic.Project>> GetProjectsByPermission(string permissionName, ClaimsPrincipal userClaim)
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
    }
}
