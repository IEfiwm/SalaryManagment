using Domain.Base.Entity;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base.Geography
{
    public class City : IdentityBaseEntity
    {
        public string Title { get; set; }

        public string Code { get; set; }

        [ForeignKey("Province")]
        public long ProvinceRef { get; set; }

        public Geometry Area { get; set; }

        public virtual Province Province { get; set; }
    }
}
