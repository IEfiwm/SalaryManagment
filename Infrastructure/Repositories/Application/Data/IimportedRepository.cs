using Application.Interfaces.Repositories.Base;
using Domain.Entities.Data;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application
{
    public interface IimportedRepository : IBaseRepository<Imported, ApplicationDbContext>
    {
    }
}