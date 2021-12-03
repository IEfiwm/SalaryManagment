using Domain.Entities.Base.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Domain.Base.Entity
{
    public abstract class AuditBaseEntity<TKey> : IdentityBaseEntity<TKey>
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedByRef { get; set; }

        public string UpdatedByRef { get; set; } = null;

        [ForeignKey("UpdatedByRef")]
        public virtual ApplicationUser UpdatedByUser { get; set; }

        [ForeignKey("CreatedByRef")]
        public virtual ApplicationUser CreatedByUser { get; set; }
    }

    public abstract class AuditBaseEntity : AuditBaseEntity<long>
    {
    }
}
