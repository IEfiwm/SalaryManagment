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
    public class Role_MenuRepository : BaseIdentityRepository<Role_Menu, IdentityContext>, IRole_MenuRepository
    {
        public Role_MenuRepository(IIdentityRepositoryAsync<Role_Menu, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<List<Role_Menu>> GetByRoleId(string roleId)
        {
            return await Model.Include(x => x.Menu).Where(x => x.RoleId == roleId).ToListAsync();
        }
    }
}