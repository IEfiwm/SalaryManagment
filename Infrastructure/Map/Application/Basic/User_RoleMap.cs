using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class User_RoleMap : IdentityBaseEntityMap<User_Role>
    {
        public User_RoleMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<User_Role> builder)
        {
        }
    }
}
