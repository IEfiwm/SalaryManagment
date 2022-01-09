using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
    }
}