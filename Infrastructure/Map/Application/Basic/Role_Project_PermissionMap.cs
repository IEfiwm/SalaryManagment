using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class Role_Project_PermissionMap : IdentityBaseEntityMap<Role_Project_Permission>
    {
        public Role_Project_PermissionMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Role_Project_Permission> builder)
        {
        }
    }
}
