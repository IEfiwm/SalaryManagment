﻿using AspNetCoreHero.Extensions.Logging;
using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                var logger = loggerFactory.CreateLogger("app");

                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                    await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);

                    await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(userManager, roleManager);

                    await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);

                    logger.LogInformation("Finished Seeding Default Data");

                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60); });
                    webBuilder.UseStartup<Startup>();
                });
    }
}