using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class User_RoleRepository : BaseIdentityRepository<User_Role, IdentityContext>, IUser_RoleRepository
    {
        public User_RoleRepository(IIdentityRepositoryAsync<User_Role, IdentityContext> repository) : base(repository)
        {
        }

       
    }
}