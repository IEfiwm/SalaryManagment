using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IBank_AccountRepository : IBaseIdentityRepository<Bank_Account, IdentityContext>
    {

    }
}
