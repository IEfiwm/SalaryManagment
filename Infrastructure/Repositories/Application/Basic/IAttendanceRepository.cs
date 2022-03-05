using Application.Interfaces.Repositories.Base;
using Domain.Entities.Data;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IAttendanceRepository : IBaseIdentityRepository<Attendance, IdentityContext>
    {
    }
}
