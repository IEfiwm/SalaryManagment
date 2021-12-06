using Application.Interfaces.Repositories;
using Common.Helpers;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public class AuthenticationCodeRepository : BaseIdentityRepository<AuthenticationCode, IdentityContext>, IAuthenticationCodeRepository
    {
        public AuthenticationCodeRepository(IIdentityRepositoryAsync<AuthenticationCode, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<string> GenerateNewCode(string phone)
        {
            var model = await Model.Where(m => m.PhoneNumber == phone && m.IsActive && m.ExpireDate.Date == DateTime.Now.Date).ToListAsync();

            if (model.Count > 10)
            {
                return "";
            }
            else if (model.Any(m => m.ExpireDate >= DateTime.Now))
            {
                return "-1";
            }

            string code = CommonHelper.GenerateRandomOTP(PublicSettings.OTPCodeLenght);

            await InsertAsync(new AuthenticationCode
            {
                Code = code,
                ExpireDate = DateTime.Now.AddMinutes(PublicSettings.OTPExpireDate),
                IsActive = true,
                IsUsed = false,
                IsDeleted = false,
                PhoneNumber = phone,
                RequestedDate = DateTime.Now.Date
            });

            await SaveChangesAsync();

            return code;
        }

        public async Task<bool> VerifyCode(string phone, string code)
        {
            var model = await Model.Where(m => m.Code == code && m.PhoneNumber == phone).FirstOrDefaultAsync();

            return model.ExpireDate >= DateTime.Now;
        }
    }
}