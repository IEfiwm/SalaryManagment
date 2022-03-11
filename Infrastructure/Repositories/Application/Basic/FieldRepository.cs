using Application.Interfaces.Repositories;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class FieldRepository : BaseIdentityRepository<Field, IdentityContext>, IFieldRepository
    {
        public FieldRepository(IIdentityRepositoryAsync<Field, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<IEnumerable<Field>> GetCalculatedByFields()
        {
            return await Model.Where(x => x.IsCalculatedBy).ToListAsync();
        }

        public async Task<IEnumerable<Field>> GetCalculateFields()
        {
            return await Model.Where(x => x.IsCalculate).ToListAsync();
        }
    }
}