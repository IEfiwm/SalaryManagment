using Infrastructure.Attribute;
using Domain.Entities.Base.Geography;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Geography
{
    [Base]
    internal class CountryMap : IdentityBaseEntityMap<Country>
    {
        public CountryMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Country> builder)
        {
            builder.Property(e => e.EnglishTitle).IsRequired();

            builder.Property(e => e.PersianTitle).IsRequired();

            builder.Property(e => e.ISOCode).IsRequired();
        }
    }
}
