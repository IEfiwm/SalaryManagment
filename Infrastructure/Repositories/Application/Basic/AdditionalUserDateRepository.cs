using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class AdditionalUserDateRepository : BaseIdentityRepository<AdditionalUserData, IdentityContext>, IAdditionalUserDateRepository
    {
        public AdditionalUserDateRepository(IIdentityRepositoryAsync<AdditionalUserData, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<bool> DeleteByUserId(string userId)
        {
            var list = Model.Where(x => x.ParentRef == userId).ToList();
            foreach (var data in list)
            {
                data.IsDeleted = true;
                await UpdateAsync(data);
            }
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateByUserId(List<AdditionalUserData> additionalUserDatas, string userId)
        {
            var pc = new PersianCalendar();

            await DeleteByUserId(userId);

            foreach (var data in additionalUserDatas)
            {
                if (data.Birthday != null)
                    data.Birthday = new DateTime(data.Birthday.Value.Year, data.Birthday.Value.Month, data.Birthday.Value.Day, pc);

                await InsertAsync(data);
            }
            await SaveChangesAsync();
            return true;

        }
    }
}