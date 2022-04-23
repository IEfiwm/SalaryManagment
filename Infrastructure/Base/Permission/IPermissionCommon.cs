using Domain.Entities.Base.Identity;
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

        Task<List<Domain.Entities.Basic.Project>> GetProjectsByPermission(string permissionName, ClaimsPrincipal userClaim);

       // Task<List<Domain.Entities.Basic.Project>> SetPermissionProjectsToUser(string permissionName, ClaimsPrincipal userClaim);

    }
}
