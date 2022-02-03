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
    public class BaseIdentityRepository<T, TContext> : IBaseIdentityRepository<T, TContext>
        where T : IdentityBaseEntity
        where TContext : DbContext
    {
        private readonly IIdentityRepositoryAsync<T, TContext> _repository;

        //private readonly IDistributedCache _distributedCache;
        //private readonly BaseCacheKey<T> _baseCacheKey;

        public BaseIdentityRepository(
            //IDistributedCache distributedCache,
            IIdentityRepositoryAsync<T, TContext> repository
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

        public async Task<T> GetByIdAsync(long id)
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

    public class BaseAuditRepository<T, TContext> : IBaseAuditRepository<T, TContext>
        where T : AuditBaseEntity
        where TContext : DbContext
    {
        private readonly IAuditRepositoryAsync<T, TContext> _repository;

        //private readonly IDistributedCache _distributedCache;
        //private readonly BaseCacheKey<T> _baseCacheKey;

        public BaseAuditRepository(
            //IDistributedCache distributedCache,
            IAuditRepositoryAsync<T, TContext> repository
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

        public async Task SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;

            await _repository.UpdateAsync(entity);
        }

        public async Task SoftDeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return;

            await _repository.UpdateAsync(entity);
        }

        public async Task<T> GetByIdAsync(long id)
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
