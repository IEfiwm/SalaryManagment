﻿using Application.Interfaces.Repositories;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
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
            var pc = new PersianCalendar();

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
    }
}