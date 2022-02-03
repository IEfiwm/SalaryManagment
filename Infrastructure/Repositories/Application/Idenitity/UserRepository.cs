using Common.Models.DataTable;
using Domain.Entities.Base.Identity;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IdentityContext _identityContext;

        public UserRepository(IdentityContext identityContext,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        public IQueryable<ApplicationUser> Users { get => _userManager.Users; }

        public IQueryable<ApplicationUser> Model { get => _identityContext.Users; }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _identityContext.Users
                   .Include(e => e.Project)
                   .Include(e => e.Bank)
                   .Where(m => m.Id == id)
                   .FirstOrDefault();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _identityContext.Users
                  .Include(e => e.Project)
                  .Include(e => e.Bank)
                  .Where(m => m.Id == id)
                  .FirstOrDefaultAsync();
        }

        public List<ApplicationUser> GetUserList()
        {
            return _identityContext.Users
                .Include(e => e.Project)
                .Include(e => e.Bank)
                .ToList();
        }

        public async Task<List<ApplicationUser>> GetUserListAsync()
        {
            return await _identityContext.Users
                .Include(e => e.Project)
                .Include(e => e.Bank)
                //.Where(e=>e.PersonnelCode.Contains("777777"))
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUserListByProjectIdAsync(long projectId)
        {
            return await _identityContext.Users
                .Include(e => e.Project)
                .Where(x => x.ProjectRef == projectId && x.Email == null && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUserListByProjectIdAsync(long projectId, int take, int page)
        {
            return await _identityContext.Users
                .Include(e => e.Project)
                .Where(x => x.ProjectRef == projectId && x.Email == null && !x.IsDeleted)
                .Skip(take * page)
                .Take(take)
                .ToListAsync();
        }

        public async Task<DataTableDTO<IEnumerable<ApplicationUser>>> GetUserListByProjectIdDataTableAsync(long projectId, int pageSize, int pageNumber)
        {
            var result = new DataTableDTO<IEnumerable<ApplicationUser>>();

            var data = await _identityContext.Users
                .Include(e => e.Project)
                .Where(x => x.ProjectRef == projectId && x.Email == null && !x.IsDeleted)
                .OrderByDescending(m => m.PersonnelCode)
                .ToListAsync();

            result.DataCount = data.Count;

            result.PageSize = pageSize;

            result.PageNumber = pageNumber;

            result.PageCount = data.Count / pageSize;

            result.Model = data
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToList();

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _identityContext.SaveChangesAsync();
        }
    }
}
