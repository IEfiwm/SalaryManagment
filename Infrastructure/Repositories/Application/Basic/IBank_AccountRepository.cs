using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IBank_AccountRepository : IBaseIdentityRepository<Bank_Account, IdentityContext>
    {
        Task<List<Bank_Account>> GetAllByBankId(long bankId);
        Task<Bank_Account> GetByAccountId(long accountId);
    }
}
