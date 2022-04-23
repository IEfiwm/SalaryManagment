using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class PermissionRepository : BaseIdentityRepository<Permission, IdentityContext>, IPermissionRepository
    {
        public PermissionRepository(IIdentityRepositoryAsync<Permission, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<Permission> GetByName(string name)
        {
            return await Model.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> IsParent(long parentId)
        {
           return await Model.AnyAsync(x => x.ParentId == parentId);
        }
    }
}