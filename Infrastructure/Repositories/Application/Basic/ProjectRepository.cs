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

        public Task<Project> GetProjectByName(string name)
        {
            return Model.Where(m => m.Title == name).FirstOrDefaultAsync();
        }
    }
}