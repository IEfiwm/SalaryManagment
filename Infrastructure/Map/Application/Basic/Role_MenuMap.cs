using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class Role_MenuMap : IdentityBaseEntityMap<Role_Menu>
    {
        public Role_MenuMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Role_Menu> builder)
        {
        }
    }
}
