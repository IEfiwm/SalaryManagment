using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class ProjectMap : IdentityBaseEntityMap<Project>
    {
        public ProjectMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Project> builder)
        {
            //builder.HasMany(m => m.ProjectUsers)
            //    .WithOne(m => m.Project).HasForeignKey(m => m.ProjectRef);

            //builder.HasOne(e => e.CreatedByUser)
            //.WithMany(e => e.ProjectCreatedByUsers)
            //.HasForeignKey(e => e.CreatedByRef);

            //builder.HasOne(e => e.UpdatedByUser)
            //.WithMany(e => e.ProjectUpdatedByUsers)
            //.HasForeignKey(e => e.UpdatedByRef);
        }
    }
}
