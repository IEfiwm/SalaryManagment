using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class BankAccountMap : AuditBaseEntityMap<BankAccount>
    {
        public BankAccountMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
            .WithMany(e => e.BankCreatedByUsers)
            .HasForeignKey(e => e.CreatedByRef);

            builder.HasOne(e => e.UpdatedByUser)
            .WithMany(e => e.BankUpdatedByUsers)
            .HasForeignKey(e => e.UpdatedByRef);
        }
    }
}
