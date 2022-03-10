using Domain.Entities.Basic;
using Domain.Entities.Data;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class FieldMap : IdentityBaseEntityMap<Field>
    {
        public FieldMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Field> builder)
        {
        }
    }
}
