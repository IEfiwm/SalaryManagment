using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Base;
using AutoMapper;
using Domain.Base.Entity;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IAuditRepositoryAsync<,>), typeof(AuditRepositoryAsync<,>));
            services.AddTransient(typeof(IIdentityRepositoryAsync<,>), typeof(IdentityRepositoryAsync<,>));
            services.AddTransient(typeof(IBaseIdentityRepository<,>), typeof(BaseIdentityRepository<,>));
            services.AddTransient(typeof(IBaseAuditRepository<,>), typeof(BaseAuditRepository<,>));
            services.AddScoped<IimportedRepository, ImportedRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IFileRepository, FileRepository>();

            #endregion Repositories
        }
    }
}