using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IRole_MenuRepository : IBaseIdentityRepository<Role_Menu, IdentityContext>
    {
        Task<List<Role_Menu>> GetByRoleId(string roleId);
    }
}
