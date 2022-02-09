﻿using Application.Interfaces.Repositories.Base;
using Common.Models.DataTable;
using Domain.Entities.Data;
using Domain.Entities.Porc;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application
{
    public interface IimportedRepository : IBaseIdentityRepository<Imported, ApplicationDbContext>
    {
        List<Imported> GetUserAttendanceListByUserList(string year, string month, List<string> userlist);

        Task<bool> CheckDuplicateAttendance(string nationalCode, string year, string month);

        Task<DataTableDTO<IEnumerable<Attendance>>> GetUserAttendanceListAsync(int year, int month, string key, int pageSize, int pageNumber);

        Task<bool> DeleteByIdAsync(long importedId);
    }
}