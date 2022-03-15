using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class ProjectRuleRepository : BaseIdentityRepository<ProjectRule, IdentityContext>, IProjectRuleRepository
    {
        public ProjectRuleRepository(IIdentityRepositoryAsync<ProjectRule, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<bool> GetByFieldtId(long fieldId, long projectId, long id)
        {
            return await Model.AnyAsync(x => x.FieldId == fieldId && x.ProjectId == projectId && x.Id != id);
        }

        public async Task<bool> GetByFieldtId(long fieldId, long projectId)
        {
            return await Model.AnyAsync(x => x.FieldId == fieldId && x.ProjectId == projectId);
        }
    }
}