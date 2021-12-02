using Infrastructure.Attribute;
using Domain.Entities.Base.Geography;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Geography
{
    [Base]
    internal class AddressMap : AuditBaseEntityMap<Address>
    {
        public AddressMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Address> builder)
        {
            builder.Property(e => e.Address1).IsRequired();

            builder.Property(e => e.AddressType).IsRequired();

            builder.Property(e => e.CityRef).IsRequired();

            builder.Property(e => e.Region).IsRequired();
        }
    }
}
