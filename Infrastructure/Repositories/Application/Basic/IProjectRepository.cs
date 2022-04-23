using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IProjectRepository : IBaseIdentityRepository<Project, IdentityContext>
    {
        Task<Project> GetProjectByName(string name);

        Task<Project> GetWithBankAccountsById(int projectId);
    }
}
