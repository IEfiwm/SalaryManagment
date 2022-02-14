using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
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
    }
}
