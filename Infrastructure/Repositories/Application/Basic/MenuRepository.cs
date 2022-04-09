using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class MenuRepository : BaseIdentityRepository<Menu, IdentityContext>, IMenuRepository
    {
        public MenuRepository(IIdentityRepositoryAsync<Menu, IdentityContext> repository) : base(repository)
        {
        }

       
    }
}