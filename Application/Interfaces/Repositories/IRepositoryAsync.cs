using Domain.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T, TContext>
        where T : IdentityBaseEntity
        where TContext : DbContext
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task<T> AddAndSaveAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        public int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}