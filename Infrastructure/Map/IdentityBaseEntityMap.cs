using Domain.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace Infrastructure.Map
{
    public abstract class IdentityBaseEntityMap<TEntityType> : IEntityTypeConfiguration<TEntityType> where TEntityType : IdentityBaseEntity
    {
        protected IdentityBaseEntityMap(string schema = null, string tableName = null, bool usePrefixTableName = true)
        {
            SchemaName = schema;

            TableName = tableName;

            UsePrefixTableName = usePrefixTableName;
        }

        protected static string SchemaName;

        protected static string TableName;

        protected static bool UsePrefixTableName;

        protected static string EntityName => typeof(TEntityType).Name;

        protected static string PrefixTableName => "Tb";

        private static string ConfigureTableName(string tableName, bool usePrefixTableName)
        {
            tableName = tableName.Trim();

            if (string.IsNullOrWhiteSpace(tableName))
            {
                tableName = EntityName;
            }

            if (usePrefixTableName)
            {
                if (!string.IsNullOrWhiteSpace(PrefixTableName))
                {
                    tableName = PrefixTableName.Trim() + tableName;
                }
            }

            return tableName;
        }

        public virtual void Configure(EntityTypeBuilder<TEntityType> builder)
        {
            TableName ??= typeof(TEntityType).Name;

            SchemaName ??= typeof(TEntityType).Namespace.Split(".").Last();

            if (UsePrefixTableName)
            {
                TableName = PrefixTableName + TableName;
            }

            builder.ToTable(TableName, SchemaName);

            builder.HasKey(e => e.Id).IsClustered();

            builder.Property(e => e.IsDeleted).IsRequired();

            Map(builder);
        }

        public virtual void Map(EntityTypeBuilder<TEntityType> builder)
        {
        }
    }
}
