using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IRole_Project_PermissionRepository : IBaseIdentityRepository<Role_Project_Permission, IdentityContext>
    {
        Task<List<Role_Project_Permission>> GetByRoleAndProjectId(string roleId, long projectId);

        Task<List<Role_Project_Permission>> GetByProjectId(long projectId);

        Task<List<Role_Project_Permission>> GetByRoleListAndPermissionId(IEnumerable<string> rolesId, long permissionId);
    }
}
