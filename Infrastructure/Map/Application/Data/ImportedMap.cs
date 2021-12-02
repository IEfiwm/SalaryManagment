using Domain.Entities.Data;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Data
{
    [Application]
    internal class ImportedMap : IdentityBaseEntityMap<Imported>
    {
        public ImportedMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Imported> builder)
        {
        }
    }
}
