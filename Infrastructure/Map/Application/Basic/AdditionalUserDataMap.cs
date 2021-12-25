using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class AdditionalUserDataMap : IdentityBaseEntityMap<AdditionalUserData>
    {
        public AdditionalUserDataMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<AdditionalUserData> builder)
        {
        }
    }
}