using Application.Interfaces.Repositories;
using Domain.Entities.Data;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Application.Basic
{
    public class AttendanceRepository : BaseIdentityRepository<Attendance, IdentityContext>, IAttendanceRepository
    {
        public AttendanceRepository(IIdentityRepositoryAsync<Attendance, IdentityContext> repository) : base(repository)
        {
        }

       
    }
}