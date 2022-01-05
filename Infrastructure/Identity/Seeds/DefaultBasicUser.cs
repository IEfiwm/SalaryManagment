using Application.Enums;
using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User

            var adminUser = new ApplicationUser
            {
                UserName = "user",
                Email = "user@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
                }
            }

            var userList = new List<ApplicationUser>();

            userList.Add(new ApplicationUser
            {
                UserName = "tjadmin",
                Email = "tjadmin@gmail.com",
                FirstName = "admin",
                LastName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            });

            userList.Add(new ApplicationUser
            {
                UserName = "bashiri",
                Email = "bashiri@gmail.com",
                FirstName = "",
                LastName = "Bashiri",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            });

            userList.Add(new ApplicationUser
            {
                UserName = "eftekhari",
                Email = "eftekhari@gmail.com",
                FirstName = "",
                LastName = "Eftekhari",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            });

            userList.Add(new ApplicationUser
            {
                UserName = "khorasani",
                Email = "khorasani@gmail.com",
                FirstName = "",
                LastName = "Khorasani",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            });

            userList.Add(new ApplicationUser
            {
                UserName = "jabarzadeh",
                Email = "jabarzadeh@gmail.com",
                FirstName = "",
                LastName = "JabarZadeh",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            });

            userList.Add(new ApplicationUser
            {
                UserName = "alinaghi",
                Email = "alinaghi@gmail.com",
                FirstName = "",
                LastName = "Alinaghi",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            });

            foreach (var defaultUser in userList)
            {
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);

                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                        await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                    }
                }
            }
        }
    }
}