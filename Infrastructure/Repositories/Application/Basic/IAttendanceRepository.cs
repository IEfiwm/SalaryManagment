using Application.Interfaces.Repositories.Base;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IAttendanceRepository : IBaseIdentityRepository<Attendance, IdentityContext>
    {
        Task<List<Attendance>> GetByProjectId(int year, int month, long projectId);
    }
}
