using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class Bank_AccountRepository : BaseIdentityRepository<Bank_Account, IdentityContext>, IBank_AccountRepository
    {
        public Bank_AccountRepository(IIdentityRepositoryAsync<Bank_Account, IdentityContext> repository) : base(repository)
        {
        }
    }
}
