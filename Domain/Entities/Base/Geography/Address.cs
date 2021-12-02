using Common.Enums;
using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base.Geography
{
    public class Address : AuditBaseEntity
    {
        public string UserRef { get; set; }

        public AddressType AddressType { get; set; }

        public string Region { get; set; }

        public int CityRef { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string ZipCode { get; set; }

        public string Plate { get; set; }

        public Point Location { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
