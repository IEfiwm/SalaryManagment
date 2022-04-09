using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class Role_Project_PermissionRepository : BaseIdentityRepository<Role_Project_Permission, IdentityContext>, IRole_Project_PermissionRepository
    {
        public Role_Project_PermissionRepository(IIdentityRepositoryAsync<Role_Project_Permission, IdentityContext> repository) : base(repository)
        {
        }

       
    }
}