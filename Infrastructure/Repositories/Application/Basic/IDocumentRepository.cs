using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IDocumentRepository : IBaseIdentityRepository<Document, IdentityContext>
    {
        Task<bool> DeleteByUserId(long userId);
    }
}