using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IRole_Project_PermissionRepository : IBaseIdentityRepository<Role_Project_Permission, IdentityContext>
    {
    }
}
