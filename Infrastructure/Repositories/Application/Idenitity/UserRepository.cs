using Domain.Entities.Base.Identity;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _identityContext.SaveChangesAsync();
        }
    }
}
