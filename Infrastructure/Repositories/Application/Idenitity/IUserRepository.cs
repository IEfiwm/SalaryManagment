using Common.Enums;
using Common.Models.DataTable;
using Domain.Entities.Base.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public interface IUserRepository
    {
        public IQueryable<ApplicationUser> Users { get; }

        public IQueryable<ApplicationUser> Model { get; }

        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);

        ApplicationUser GetUserById(string id);

        Task<ApplicationUser> GetUserByIdAsync(string id);

        List<ApplicationUser> GetUserList();

        Task<List<ApplicationUser>> GetUserListAsync();

        Task<List<ApplicationUser>> GetSysUserListAsync();

        Task<List<ApplicationUser>> GetUserListByProjectIdAsync(long projectId);

        Task<List<ApplicationUser>> GetUserListByProjectIdAsync(long projectId, int take, int page);

        Task<DataTableDTO<IEnumerable<ApplicationUser>>> GetUserListByProjectIdDataTableAsync(long projectId, string key, int pageSize, int pageNumber, EmployeeStatus? employeeStatus, Gender? gender, MilitaryService? militaryService, MaritalStatus? maritalStatus);

        Task<int> SaveChangesAsync();

        Task<string> GetLastPersonnelCode(long projecId);
    }
}