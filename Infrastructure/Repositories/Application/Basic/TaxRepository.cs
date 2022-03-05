using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class TaxRepository : BaseIdentityRepository<Tax, IdentityContext>, ITaxRepository
    {
        public TaxRepository(IIdentityRepositoryAsync<Tax, IdentityContext> repository) : base(repository)
        {
        }

       
    }
}