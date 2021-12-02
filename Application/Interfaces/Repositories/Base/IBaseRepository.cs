using Domain.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T, TContext>
        where T : IdentityBaseEntity
        where TContext : DbContext
    {
        IQueryable<T> Model { get; }

        Task<List<T>> GetListAsync();

        Task<T> GetByIdAsync(int id);

        Task<long> InsertAsync(T entity);

        Task<long> InsertAndSaveAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        public int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
