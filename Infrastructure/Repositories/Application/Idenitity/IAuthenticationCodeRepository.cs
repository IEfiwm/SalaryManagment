using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public interface IAuthenticationCodeRepository : IBaseIdentityRepository<Project, IdentityContext>
    {
    }
}
