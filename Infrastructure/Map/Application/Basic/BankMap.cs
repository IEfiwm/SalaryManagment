using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class BankMap : IdentityBaseEntityMap<Bank>
    {
        public BankMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Bank> builder)
        {
        }
    }
}
