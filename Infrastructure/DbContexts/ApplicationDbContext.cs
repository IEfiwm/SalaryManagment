using Application.Interfaces.Shared;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    //: AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //foreach (var property in builder.Model.GetEntityTypes()
            //.SelectMany(t => t.GetProperties())
            //.Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            //{
            //    property.SetColumnType("decimal(18,2)");
            //}
            base.OnModelCreating(builder);

            #region Add Mappings

            var implementedConfigTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract
                    && !t.IsGenericTypeDefinition
                    && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                        i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in implementedConfigTypes)
            {
                if (configType.GetCustomAttribute<ApplicationAttribute>() != null)
                {
                    dynamic config = Activator.CreateInstance(configType);

                    builder.ApplyConfiguration(config);
                }
            }
            #endregion
        }
    }
}