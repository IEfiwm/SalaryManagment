using Infrastructure.Attribute;
using Domain.Entities.Base.Geography;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Geography
{
    [Base]
    internal class ProvinceMap : IdentityBaseEntityMap<Province>
    {
        public ProvinceMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Province> builder)
        {
            builder.Property(e => e.IsCapital).IsRequired();

            builder.Property(e => e.CountryRef).IsRequired();

            builder.Property(e => e.Title).IsRequired();
        }
    }
}
