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
    public class ProjectBankAccountRepository : BaseIdentityRepository<ProjectBankAccount, IdentityContext>, IProjectBankAccountRepository
    {
        public ProjectBankAccountRepository(IIdentityRepositoryAsync<ProjectBankAccount, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<bool> CheckDuplication(long projectId, long bankId)
        {
            return await Model.AnyAsync(x => x.ProjectId == projectId && x.Bank_Account.BankId == bankId);
        }

        public async Task<int> RemoveAccountsFromProject(long projectId)
        {
            var bankAccounts = await Model.Where(x => x.ProjectId == projectId).ToListAsync();
            foreach (var bankAccProject in bankAccounts)
            {
                bankAccProject.IsDeleted = true;
                await this.UpdateAsync(bankAccProject);
            }

            var res = await this.SaveChangesAsync();

            return res;
        }

        public async Task<ProjectBankAccount> GetByAccountIdAndProjectId(long bank_AccountId, long projectId)
        {
            return await Model.FirstOrDefaultAsync(x => x.Bank_AccountId == bank_AccountId && x.ProjectId == projectId);
        }

        public async Task<int> SetAccountsToProject(List<long> bankAccounts, long projectId)
        {
            foreach (var acc in bankAccounts)
            {
                await this.InsertAsync(new ProjectBankAccount
                {
                    Bank_AccountId = acc,
                    ProjectId = projectId
                });
            }

            var res = await this.SaveChangesAsync();

            return res;
        }
    }
}
