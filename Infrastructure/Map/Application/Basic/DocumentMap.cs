using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class DocumentMap : IdentityBaseEntityMap<Document>
    {
        public DocumentMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Document> builder)
        {
        }
    }
}