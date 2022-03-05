using Domain.Entities.Basic;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class ProjectBankAccountMap : IdentityBaseEntityMap<ProjectBankAccount>
    {
        public ProjectBankAccountMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<ProjectBankAccount> builder)
        {
        }
    }
}
