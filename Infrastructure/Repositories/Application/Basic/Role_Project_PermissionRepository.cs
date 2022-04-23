using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class Role_Project_PermissionRepository : BaseIdentityRepository<Role_Project_Permission, IdentityContext>, IRole_Project_PermissionRepository
    {
        public Role_Project_PermissionRepository(IIdentityRepositoryAsync<Role_Project_Permission, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<List<Role_Project_Permission>> GetByRoleAndProjectId(string roleId, long projectId)
        {
            return await Model.Where(x => x.RoleId == roleId && x.ProjectId == projectId).ToListAsync();
        }
        public async Task<List<Role_Project_Permission>> GetByProjectId(long projectId)
        {
            return await Model
                .Include(x => x.Project)
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .Where(x => x.ProjectId == projectId)
                .Where(x => x.Role.Public && x.Role.Active)
                .ToListAsync();
        }

        public async Task<List<Role_Project_Permission>> GetByRoleListAndPermissionId(IEnumerable<string> rolesId, long permissionId)
        {
            return await Model
              .Include(x => x.Project)
              .Include(x => x.Role)
              .Include(x => x.Permission)
              .Where(x => rolesId.Contains(x.RoleId) && x.PermissionId == permissionId).ToListAsync();
        }
    }
}