using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IProjectRuleRepository : IBaseIdentityRepository<ProjectRule, IdentityContext>
    {
        Task<bool> GetByFieldtId(long fieldId,long projectId, long id);

        Task<bool> GetByFieldtId(long fieldId, long projectId);
    }
}
