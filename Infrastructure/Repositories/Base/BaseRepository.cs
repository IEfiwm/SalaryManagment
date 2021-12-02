using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Base;
using Domain.Base.Entity;
using Infrastructure.CacheKeys.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class BaseRepository<T, TContext> : IBaseRepository<T, TContext>
        where T : IdentityBaseEntity
        where TContext : DbContext
    {
        private readonly IRepositoryAsync<T, TContext> _repository;

        //private readonly IDistributedCache _distributedCache;
        //private readonly BaseCacheKey<T> _baseCacheKey;

        public BaseRepository(
            //IDistributedCache distributedCache,
            IRepositoryAsync<T, TContext> repository
            //BaseCacheKey<T> baseCacheKey
            )
        {
            //_distributedCache = distributedCache;
            _repository = repository;
            //_baseCacheKey = baseCacheKey;
        }

        public IQueryable<T> Model => _repository.Entities;

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);

            //await _distributedCache.RemoveAsync(_baseCacheKey.ListKey);

            //await _distributedCache.RemoveAsync(_baseCacheKey.GetKey(entity.Id));
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<long> InsertAsync(T entity)
        {
            var model = await _repository.AddAsync(entity);

            //await _distributedCache.RemoveAsync(_baseCacheKey.ListKey);

            return model.Id;
        }

        public async Task<long> InsertAndSaveAsync(T entity)
        {
            var model = await _repository.AddAndSaveAsync(entity);

            return model.Id;
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            //await _distributedCache.RemoveAsync(_baseCacheKey.ListKey);

            //await _distributedCache.RemoveAsync(_baseCacheKey.GetKey(entity.Id));
        }
    }
}
