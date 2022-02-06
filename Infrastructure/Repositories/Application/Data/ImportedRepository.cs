using Application.Interfaces.Repositories;
using Common.Models.DataTable;
using Domain.Entities.Data;
using Domain.Entities.Porc;
using Infrastructure.Dapper;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application
{
    public class ImportedRepository : BaseIdentityRepository<Imported, ApplicationDbContext>, IimportedRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IApplicationReadDbConnection _readDbConnection;

        public ImportedRepository(
            IIdentityRepositoryAsync<Imported, ApplicationDbContext> repository,
            IApplicationReadDbConnection readDbConnection,
            IUnitOfWork unitOfWork
            ) :
            base(
                //distributedCache, 
                repository
                //baseCacheKey
                )
        {
            _unitOfWork = unitOfWork;
            _readDbConnection = readDbConnection;
        }

        public async Task<bool> DeleteByIdAsync(long importedId)
        {
            var model = Model.FirstOrDefault(x => x.Id == importedId);
            if (model is null)
                return false;
            model.IsDeleted = true;
            await SaveChangesAsync();
            return true;
        }

        public async Task<DataTableDTO<IEnumerable<Attendance>>> GetUserAttendanceListAsync(int year, int month, string key, int pageSize, int pageNumber)
        {
            var result = new DataTableDTO<IEnumerable<Attendance>>();

            var data = await _readDbConnection.QueryAsync<Attendance>($"EXEC  [Basic].[SP_GetAttendancesSearch]  {year},{month},'{key}',{pageSize},{pageNumber}");

            var count = await _readDbConnection.QueryFirstOrDefaultAsync<long>($"EXEC  [Basic].[SP_GetAttendancesCount]  {year},{month},'{key}'");

            result.Model = data;

            result.DataCount = count;

            result.PageSize = pageSize;

            result.PageNumber = pageNumber;

            result.PageCount = count / pageSize;

            return result;
        }

        public List<Imported> GetUserAttendanceListByUserList(string year, string month, List<string> userlist)
        {
            return Model.Where(x => x.Year == year && x.Month == month && userlist.Contains(x.NationalCode)).ToList();
        }
    }
}