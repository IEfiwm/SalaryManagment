using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class BankAccountMap : IdentityBaseEntityMap<BankAccount>
    {
        public BankAccountMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<BankAccount> builder)
        {
            //        builder.HasMany(m => m.BankUsers)
            //.WithOne(m => m.Bank).HasForeignKey(m => m.BankAccountRef);

            //builder.HasOne(e => e.CreatedByUser)
            //.WithMany(e => e.BankCreatedByUsers)
            //.HasForeignKey(e => e.CreatedByRef);

            //builder.HasOne(e => e.UpdatedByUser)
            //.WithMany(e => e.BankUpdatedByUsers)
            //.HasForeignKey(e => e.UpdatedByRef);
        }
    }
}
