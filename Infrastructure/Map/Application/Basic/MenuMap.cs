using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class MenuMap : IdentityBaseEntityMap<Menu>
    {
        public MenuMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Menu> builder)
        {
        }
    }
}
