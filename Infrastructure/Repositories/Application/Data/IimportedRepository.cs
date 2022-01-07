﻿using Application.Interfaces.Repositories.Base;
using Domain.Entities.Data;
using Domain.Entities.Porc;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application
{
    public interface IimportedRepository : IBaseIdentityRepository<Imported, ApplicationDbContext>
    {
        List<Attendance> GetUserAttendanceList();
        Task<bool> DeleteByIdAsync(long importedId);
    }
}