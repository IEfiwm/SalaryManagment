using Application.Enums;
using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "user",
                Email = "user@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            var tjUser = new ApplicationUser
            {
                UserName = "tjadmin",
                Email = "tjadmin@gmail.com",
                FirstName = "admin",
                LastName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }                
                
                user = await userManager.FindByEmailAsync(tjUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(tjUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(tjUser, Roles.Admin.ToString());
                }
            }
        }
    }
}