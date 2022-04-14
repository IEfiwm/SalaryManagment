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
    public class User_RoleRepository : BaseIdentityRepository<User_Role, IdentityContext>, IUser_RoleRepository
    {
        public User_RoleRepository(IIdentityRepositoryAsync<User_Role, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<List<User_Role>> GetByRoleId(string roleId)
        {
            return await Model.Where(x => x.RoleId == roleId).ToListAsync();
        }
        public async Task<List<User_Role>> GetByUserId(string userId)
        {
            return await Model.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}