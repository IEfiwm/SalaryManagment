using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class ProjectRuleRepository : BaseIdentityRepository<ProjectRule, IdentityContext>, IProjectRuleRepository
    {
        public ProjectRuleRepository(IIdentityRepositoryAsync<ProjectRule, IdentityContext> repository) : base(repository)
        {
        }

       
    }
}