using Application.Interfaces.Repositories;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application
{
    public class ImportedRepository : BaseIdentityRepository<Imported, ApplicationDbContext>, IimportedRepository
    {
        public ImportedRepository(
            //IDistributedCache distributedCache,
            IIdentityRepositoryAsync<Imported, ApplicationDbContext> repository
            //BaseCacheKey<Imported> baseCacheKey
            ) :
            base(
                //distributedCache, 
                repository
                //baseCacheKey
                )
        {
        }
    }
}