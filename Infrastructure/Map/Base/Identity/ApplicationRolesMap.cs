using Domain.Entities.Base.Identity;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Identity
{
    [Base]
    internal class ApplicationRolesMap : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable(name: "Roles", "Identity");
        }
    }
}
