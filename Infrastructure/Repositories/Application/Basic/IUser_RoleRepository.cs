using Application.Interfaces.Repositories.Base;
using Domain.Entities.Basic;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Basic
{
    public interface IUser_RoleRepository
    {
        Task<List<IdentityUserRole<string>>> GetByRoleId(string roleId);

        Task<List<IdentityUserRole<string>>> GetByUserId(string userId);
       
        Task DeleteAsync(IdentityUserRole<string> user_Role);
        
        Task<int> InsertAndSaveAsync(IdentityUserRole<string> user_Role);
    }
}
