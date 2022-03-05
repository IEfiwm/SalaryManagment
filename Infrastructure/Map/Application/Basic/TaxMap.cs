using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class TaxMap : IdentityBaseEntityMap<Tax>
    {
        public TaxMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Tax> builder)
        {
        }
    }
}
