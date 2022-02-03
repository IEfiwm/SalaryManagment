using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class AdditionalUserDateRepository : BaseIdentityRepository<AdditionalUserData, IdentityContext>, IAdditionalUserDateRepository
    {
        private readonly IDocumentRepository _documentRepository;

        public AdditionalUserDateRepository(IIdentityRepositoryAsync<AdditionalUserData, IdentityContext> repository,
            IDocumentRepository documentRepository) : base(repository)
        {
            _documentRepository = documentRepository;

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
            //Delete old additional Users
            await DeleteByUserId(userId);

            foreach (var data in additionalUserDatas)
            {

                //Delete Documents for old additional Users
                if (data.Id != 0)
                {
                    await _documentRepository.DeleteByUserId(data.Id);
                    data.Id = 0;
                }

                var docs = new List<Document>();
                foreach (var doc in data.Documents)
                {
                    doc.Id = 0;
                    //reCreate documetns
                    if (doc.FileName != null)
                        docs.Add(doc);
                }
                data.Documents = docs;
                //reCreate additional Users
                await InsertAndSaveAsync(data);

            }
            return true;

        }

        public async Task<bool> HasDocumentsAsync(string userId)
        {
            var additional = await Model.FirstOrDefaultAsync(x => x.ParentRef == userId && x.FamilyRole == Common.Enums.FamilyRole.Me);

            if (additional is null)
                return false;

            var docs = await _documentRepository.GetByUserIdAsync(additional.Id);

            if (docs != null && docs.Count() > 0)
                return true;

            return false;
        }

        public Task<bool> HasAdditionalUsersAsync(string userId)
        {
            return Model.AnyAsync(x => x.ParentRef == userId && x.FamilyRole != Common.Enums.FamilyRole.Me);
        }

        public async Task<bool> HasAdditionalUserDocumentAsync(string userId)
        {
            var additionalList = await Model.Where(x => x.ParentRef == userId && x.FamilyRole != Common.Enums.FamilyRole.Me).ToListAsync();

            if (additionalList is null)
                return false;

            foreach (var additional in additionalList)
            {
                var docs = await _documentRepository.GetByUserIdAsync(additional.Id);

                if (docs != null && docs.Count() > 0)
                    return true;
            }

            return false;
        }

        public async Task<List<AdditionalUserData>> GetByUserIdAsync(string userId)
        {
            return await Model.Where(x => x.ParentRef == userId && x.FamilyRole != Common.Enums.FamilyRole.Me).ToListAsync();
        }

        public bool HasDocuments(string userId)
        {
            var additional = Model.FirstOrDefault(x => x.ParentRef == userId && x.FamilyRole == Common.Enums.FamilyRole.Me);

            if (additional is null)
                return false;

            var docs = _documentRepository.GetByUserId(additional.Id);

            if (docs != null && docs.Count() > 0)
                return true;

            return false;
        }

        public bool HasAdditionalUsers(string userId)
        {
            return Model.Any(x => x.ParentRef == userId && x.FamilyRole != Common.Enums.FamilyRole.Me);
        }

        public bool HasAdditionalUserDocument(string userId)
        {
            var additionalList = Model.Where(x => x.ParentRef == userId && x.FamilyRole != Common.Enums.FamilyRole.Me);
            if (additionalList is null)
                return false;
            foreach (var additional in additionalList)
            {
                var docs = _documentRepository.GetByUserId(additional.Id);
                if (docs != null && docs.Count() > 0)
                    return true;
            }
            return false;
        }

        public List<AdditionalUserData> GetByUserId(string userId)
        {
            return Model.Where(x => x.ParentRef == userId && x.FamilyRole != Common.Enums.FamilyRole.Me).ToList();
        }

        public AdditionalUserData GetUserAdditionalById(string userId)
        {
            return Model.FirstOrDefault(x => x.ParentRef == userId && x.FamilyRole == Common.Enums.FamilyRole.Me);
        }

        public async Task<bool> CreateByUserId(List<AdditionalUserData> additionalUserDatas, string userId)
        {
            foreach (var data in additionalUserDatas)
            {

                var docs = new List<Document>();
                foreach (var doc in data.Documents)
                {
                    doc.Id = 0;
                    //Create documetns
                    if (doc.FileName != null)
                        docs.Add(doc);
                }
                data.Documents = docs;
                //reCreate additional Users
                await InsertAndSaveAsync(data);

            }
            return true;
        }
    }
}