using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IProjectBankAccountRepository : IBaseIdentityRepository<ProjectBankAccount, IdentityContext>
    {
        Task<bool> CheckDuplication(long projectId, long bankId);

        Task<int> SetAccountsToProject(List<long> bankAccounts, long projectId);

        Task<int> RemoveAccountsFromProject(long projectId);

        Task<ProjectBankAccount> GetByAccountIdAndProjectId(long bank_AccountId, long projectId);

    }
}
