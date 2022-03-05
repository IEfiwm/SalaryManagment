using Application.Interfaces.Repositories;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class AttendanceRepository : BaseIdentityRepository<Attendance, IdentityContext>, IAttendanceRepository
    {
        public AttendanceRepository(IIdentityRepositoryAsync<Attendance, IdentityContext> repository) : base(repository)
        {
        }

        public async Task<List<Attendance>> GetByProjectId(int year, int month, long projectId)
        {
            return await Model.Where(x => x.ProjectRef == projectId && x.Month == month && x.Year == year).ToListAsync();
        }
    }
}