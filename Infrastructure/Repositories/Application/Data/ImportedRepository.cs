using Application.Interfaces.Repositories;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application
{
    public class ImportedRepository : BaseRepository<Imported, ApplicationDbContext>, IimportedRepository
    {
        public ImportedRepository(
            //IDistributedCache distributedCache,
            IRepositoryAsync<Imported, ApplicationDbContext> repository
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