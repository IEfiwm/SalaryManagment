using Infrastructure.Attribute;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Identity
{
    [Base]
    internal class ApplicationRolesMap : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable(name: "Roles","Identity");
        }
    }
}
