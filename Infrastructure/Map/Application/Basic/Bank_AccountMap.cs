using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class Bank_AccountMap : IdentityBaseEntityMap<Bank_Account>
    {
        public Bank_AccountMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Bank_Account> builder)
        {
        }
    }
}
