using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class ProjectRepository : BaseIdentityRepository<Project, IdentityContext>, IProjectRepository
    {
        public ProjectRepository(IIdentityRepositoryAsync<Project, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<Project> GetProjectByName(string name)
        {
            return await Model.Where(m => m.Title == name).FirstOrDefaultAsync();
        }

        public async Task<Project> GetWithBankAccountsById(int projectId)
        {
            return await Model
                .Include(x=>x.ProjectBankAccounts).ThenInclude(x=>x.Bank_Account)
                .FirstOrDefaultAsync(m => m.Id == projectId);
        }
    }
}