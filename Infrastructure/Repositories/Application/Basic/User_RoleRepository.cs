using Application.Interfaces.Repositories;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public class User_RoleRepository : IUser_RoleRepository
    {
        private readonly IdentityContext _context;
        public User_RoleRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(IdentityUserRole<string> user_Role)
        {
            _context.UserRoles.Remove(user_Role);
            await _context.SaveChangesAsync();
        }

        public async Task<List<IdentityUserRole<string>>> GetByRoleId(string roleId)
        {
            return await _context.UserRoles.Where(x => x.RoleId == roleId).ToListAsync();
        }
        public async Task<List<IdentityUserRole<string>>> GetByUserId(string userId)
        {
            return await _context.UserRoles.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<int> InsertAndSaveAsync(IdentityUserRole<string> user_Role)
        {
            await _context.UserRoles.AddAsync(user_Role);
            return await _context.SaveChangesAsync();

        }
    }
}