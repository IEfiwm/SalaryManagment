using Application.DTOs.Settings;
using Application.Interfaces.Shared;
using Domain.Entities.Base.Identity;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application.Idenitity;
using Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using Web.Services;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMultiLingualSupport(this IServiceCollection services)
        {
            #region Registering ResourcesPath

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #endregion Registering ResourcesPath

            services.AddMvc()
               .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization(options =>
               {
                   options.DataAnnotationLocalizerProvider = (type, factory) =>
                       factory.Create(typeof(SharedResource));
               });
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddHttpContextAccessor();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("ar"),
                    new CultureInfo("fr"),
                    new CultureInfo("fa"),
                    new CultureInfo("pt-BR"),
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
        }

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddPersistenceContexts(configuration);
            services.AddAuthenticationScheme(configuration);
        }

        private static void AddAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(o =>
            {
                //Add Authentication to all Controllers by default.
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>
            {

                options.AddPolicy("Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Admin");
                    });

                options.AddPolicy("Manager",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Manager");
                    });

                options.AddPolicy("SuperAdmin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("SuperAdmin");
                    });

                options.AddPolicy("User",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("User");
                    });

                options.AddPolicy("Administrators",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("SuperAdmin");
                        //authBuilder.RequireRole("Manager");
                        //authBuilder.RequireRole("Admin");
                    });

            });
        }

        private static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseInMemoryDatabase("IdentityDb"));
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
            }
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<IdentityContext>().AddDefaultUI().AddDefaultTokenProviders();
        }

        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.AddTransient<IDateTimeService, SystemDateTimeService>();
            services.AddTransient<IMailService, SMTPMailService>();
            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IAuthenticationCodeRepository, AuthenticationCodeRepository>();
        }
    }
}