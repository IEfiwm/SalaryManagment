using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class BankAccountRepository : BaseAuditRepository<BankAccount, IdentityContext>, IBankAccountRepository
    {
        public BankAccountRepository(IAuditRepositoryAsync<BankAccount, IdentityContext> repository) : base(repository)
        {
        }
    }
}