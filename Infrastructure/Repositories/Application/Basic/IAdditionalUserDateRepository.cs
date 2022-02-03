using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IAdditionalUserDateRepository : IBaseIdentityRepository<AdditionalUserData, IdentityContext>
    {
        Task<bool> DeleteByUserId(string userId);

        Task<bool> UpdateByUserId(List<AdditionalUserData> additionalUserDatas, string userId);

        bool HasDocuments(string userId);

        bool HasAdditionalUsers(string userId);

        bool HasAdditionalUserDocument(string userId);

        List<AdditionalUserData> GetByUserId(string userId);

        AdditionalUserData GetUserAdditionalById(string userId);

        Task<bool> CreateByUserId(List<AdditionalUserData> additionalUserDatas, string userId);

        Task<bool> HasDocumentsAsync(string userId);

        Task<bool> HasAdditionalUsersAsync(string userId);

        Task<bool> HasAdditionalUserDocumentAsync(string userId);

        Task<List<AdditionalUserData>> GetByUserIdAsync(string userId);
    }
}