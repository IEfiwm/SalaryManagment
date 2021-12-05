using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public class AuthenticationCodeRepository : BaseIdentityRepository<Project, IdentityContext>, IAuthenticationCodeRepository
    {
        public AuthenticationCodeRepository(IIdentityRepositoryAsync<Project, IdentityContext> repository) : base(repository)
        {
        }
    }
}