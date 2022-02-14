using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class BankRepository : BaseIdentityRepository<Bank, IdentityContext>, IBankRepository
    {
        public BankRepository(IIdentityRepositoryAsync<Bank, IdentityContext> repository) : base(repository)
        {
        }
    }
}
