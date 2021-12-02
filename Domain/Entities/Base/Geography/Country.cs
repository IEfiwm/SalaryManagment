using Common.Enums;
using Domain.Base.Entity;
using NetTopologySuite.Geometries;

namespace Domain.Entities.Base.Geography
{
    public class Country : AuditBaseEntity
    {
        public string EnglishTitle { get; set; }

        public string PersianTitle { get; set; }

        public string ISOCode { get; set; }

        public string FlagPath { get; set; }

        public Languages Language { get; set; }

        public Geometry Area { get; set; }
    }
}
