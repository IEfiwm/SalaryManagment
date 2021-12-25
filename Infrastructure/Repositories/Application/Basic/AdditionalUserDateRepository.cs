using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class AdditionalUserDateRepository : BaseIdentityRepository<AdditionalUserData, IdentityContext>, IAdditionalUserDateRepository
    {
        public AdditionalUserDateRepository(IIdentityRepositoryAsync<AdditionalUserData, IdentityContext> repository) : base(repository)
        {
        }
    }
}