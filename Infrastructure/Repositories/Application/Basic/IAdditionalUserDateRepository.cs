using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IAdditionalUserDateRepository : IBaseIdentityRepository<AdditionalUserData, IdentityContext>
    {
    }
}