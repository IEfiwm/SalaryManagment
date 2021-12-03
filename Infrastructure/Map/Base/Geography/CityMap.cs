using Infrastructure.Attribute;
using Domain.Entities.Base.Geography;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.IO;

namespace Infrastructure.Map.Base.Geography
{
    [Base]
    internal class CityMap : IdentityBaseEntityMap<City>
    {
        public CityMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<City> builder)
        {
            builder.Property(e => e.Area).IsRequired();

            builder.Property(e => e.ProvinceRef).IsRequired();
        }
    }
}
