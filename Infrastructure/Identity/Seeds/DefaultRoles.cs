using Application.Enums;
using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            var test = new ApplicationRole("Test");
            await roleManager.CreateAsync(test);
            await roleManager.CreateAsync(new ApplicationRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Roles.Manager.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Roles.User.ToString()));
        }
    }
}