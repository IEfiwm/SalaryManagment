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
    public class DocumentRepository : BaseIdentityRepository<Document, IdentityContext>, IDocumentRepository
    {
        public DocumentRepository(IIdentityRepositoryAsync<Document, IdentityContext> repository) : base(repository)
        {

        }
        public async Task<bool> DeleteByUserId(long userId)
        {
            var list = Model.Where(x => x.AdditionalRef == userId).ToList();
            foreach (var data in list)
            {
                data.IsDeleted = true;
                await UpdateAsync(data);
            }
            await SaveChangesAsync();
            return true;
        }

        public IEnumerable<Document> GetByUserId(long userId)
        {
            return Model.Where(x => x.AdditionalRef == userId).ToList();
        }

        public async Task<IEnumerable<Document>> GetByUserIdAsync(long userId)
        {
            return await Model.Where(x => x.AdditionalRef == userId).ToListAsync();
        }
    }
}