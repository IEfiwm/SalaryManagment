using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class ProjectRuleMap : IdentityBaseEntityMap<ProjectRule>
    {
        public ProjectRuleMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<ProjectRule> builder)
        {
        }
    }
}
