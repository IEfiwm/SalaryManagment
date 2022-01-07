using Application.Interfaces.Repositories;
using Domain.Entities.Data;
using Domain.Entities.Porc;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application
{
    public class ImportedRepository : BaseIdentityRepository<Imported, ApplicationDbContext>, IimportedRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImportedRepository(
            //IDistributedCache distributedCache,
            IIdentityRepositoryAsync<Imported, ApplicationDbContext> repository,
            //BaseCacheKey<Imported> baseCacheKey
            IUnitOfWork unitOfWork
            ) :
            base(
                //distributedCache, 
                repository
                //baseCacheKey
                )
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<bool> DeleteByIdAsync(long importedId)
        {
            var model = Model.FirstOrDefault(x => x.Id == importedId);
            if (model is null)
                return false;
            model.IsDeleted = true;
           await  SaveChangesAsync();
            return true;
        }
        public List<Attendance> GetUserAttendanceList()
        {
            return _unitOfWork.ExecuteStoreProcedure<Attendance>("[Basic].[SP_GetAttendances]");
        }
    }
}