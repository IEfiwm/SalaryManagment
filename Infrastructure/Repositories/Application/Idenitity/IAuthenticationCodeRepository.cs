using Application.Interfaces.Repositories.Base;
using Domain.Entities.Base.Identity;
using Infrastructure.DbContexts;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public interface IAuthenticationCodeRepository : IBaseIdentityRepository<AuthenticationCode, IdentityContext>
    {
        Task<string> GenerateNewCode(string phone);

        Task<bool> VerifyCode(string phone, string code);
    }
}