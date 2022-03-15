using Application.Interfaces.Repositories.Base;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IFieldRepository : IBaseIdentityRepository<Field, IdentityContext>
    {
        Task<IEnumerable<Field>> GetCalculateFields();
        Task<IEnumerable<Field>> GetCalculatedByFields();
    }
}
