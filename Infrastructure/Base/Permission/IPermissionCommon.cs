using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Permission
{
    public interface IPermissionCommon
    {
        Task<Domain.Entities.Basic.Permission> GetOrCreateByName(string name);

        Task<List<Project>> GetProjectsByPermission(string permissionName, ClaimsPrincipal userClaim);

        Task<bool> CheckProjectPermissionByProjectId(string permissionName, ClaimsPrincipal userClaim,long projectId);

        Task<bool> SetFullPermissionsProjectsToUser(Project project, ClaimsPrincipal userClaim);

        Task<List<Menu>> GetMenuOfUser(ApplicationUser user);

        void RefreshPermission(ApplicationUser user);
        void RefreshPermission();

    }
}
