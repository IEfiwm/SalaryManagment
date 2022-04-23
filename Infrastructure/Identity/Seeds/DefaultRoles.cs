using Application.Enums;
using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var superadmin = roleManager.Roles.FirstOrDefault(x => x.Name == Roles.SuperAdmin.ToString());
            var admin = roleManager.Roles.FirstOrDefault(x => x.Name == Roles.Admin.ToString());
            var manager = roleManager.Roles.FirstOrDefault(x => x.Name == Roles.Manager.ToString());
            var user = roleManager.Roles.FirstOrDefault(x => x.Name == Roles.User.ToString());
            if (superadmin == null)
                await roleManager.CreateAsync(new ApplicationRole(Roles.SuperAdmin.ToString(), true, false));
            if (admin == null)
                await roleManager.CreateAsync(new ApplicationRole(Roles.Admin.ToString(), true));
            if (manager == null)
                await roleManager.CreateAsync(new ApplicationRole(Roles.Manager.ToString(), true));
            if (user == null)
                await roleManager.CreateAsync(new ApplicationRole(Roles.User.ToString(), true));
        }
    }
}