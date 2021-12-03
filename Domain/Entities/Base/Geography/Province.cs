using Domain.Base.Entity;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base.Geography
{
    public class Province : IdentityBaseEntity
    {
        public string Title { get; set; }

        public string Code { get; set; }

        [ForeignKey("Country")]
        public long CountryRef { get; set; }

        public bool IsCapital { get; set; } = false;

        public Geometry Area { get; set; }

        public virtual Country Country { get; set; }
    }
}
