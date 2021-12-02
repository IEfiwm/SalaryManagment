using Domain.Entities.Base.Identity;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Base.Identity
{
    [Base]
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(name: "Users", "Identity");

            builder.HasOne(e => e.Caller)
            .WithMany(e => e.CallerUsers)
            .HasForeignKey(e => e.CallerRef);

            //builder.HasOne(e => e.CreatedBy)
            //.WithMany(e => e.CreatedByUsers)
            //.HasForeignKey(e => e.CreatedByRef);

            //builder.HasMany(m => m.CreatedByUsers).WithOne(m => m.CreatedBy).HasForeignKey(m => m.CreatedByRef);

            //builder.HasOne(e => e.UpdatedBy)
            //.WithMany(e => e.UpdatedByUsers)
            //.HasForeignKey(e => e.UpdatedByRef);

            builder.Property(m => m.BankAccountRef).IsRequired(false);

            builder.Property(m => m.CallerRef).IsRequired(false);

            //builder.Property(m => m.CreatedBy).IsRequired(false);

            builder.Property(m => m.ProjectRef).IsRequired(false);

            builder
            .HasOne(s => s.Project)
            .WithMany(s => s.ProjectUsers)
            .HasForeignKey(p => p.ProjectRef);

            builder
            .HasOne(s => s.Bank)
            .WithMany(s => s.BankUsers)
            .HasForeignKey(p => p.BankAccountRef);
        }
    }
}
