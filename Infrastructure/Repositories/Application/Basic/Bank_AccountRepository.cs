using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class Bank_AccountRepository : BaseIdentityRepository<Bank_Account, IdentityContext>, IBank_AccountRepository
    {
        public Bank_AccountRepository(IIdentityRepositoryAsync<Bank_Account, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<List<Bank_Account>> GetAllByBankId(long bankId)
        {
            return await Model.Where(x => x.BankId == bankId).ToListAsync();
        }

        public async Task<Bank_Account> GetByAccountId(long accountId)
        {
            return await Model.FirstOrDefaultAsync(x => x.Id == accountId);
        }

    }
}
