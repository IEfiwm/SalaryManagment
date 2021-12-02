using Infrastructure.Attribute;
using Domain.Entities.Base.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Identity
{
    [Base]
    internal class AuthenticationCodeMap : IdentityBaseEntityMap<AuthenticationCode>
    {
        public AuthenticationCodeMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<AuthenticationCode> builder)
        {
            builder.Property(e => e.Code).IsRequired();

            builder.Property(e => e.PhoneNumber).IsRequired();
        }
    }
}
