using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class PermissionMap : IdentityBaseEntityMap<Permission>
    {
        public PermissionMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Permission> builder)
        {
        }
    }
}
