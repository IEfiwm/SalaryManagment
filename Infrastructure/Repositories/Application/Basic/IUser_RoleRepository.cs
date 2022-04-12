using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IUser_RoleRepository : IBaseIdentityRepository<User_Role, IdentityContext>
    {
        Task<List<User_Role>> GetByRoleId(string roleId);
    }
}
