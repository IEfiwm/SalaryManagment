using Domain.Entities.Basic;
using Domain.Entities.Data;
using Infrastructure.Attribute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map.Application.Basic
{
    [Base]
    internal class AttendanceMap : IdentityBaseEntityMap<Attendance>
    {
        public AttendanceMap() : base()
        {
        }

        public override void Map(EntityTypeBuilder<Attendance> builder)
        {
        }
    }
}
