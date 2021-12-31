using Common.Enums;
using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class AdditionalUserData : IdentityBaseEntity
    {
        [ForeignKey("Parent")]
        public string ParentRef { get; set; } = null;

        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;

        public DateTime? Birthday { get; set; }

        public Gender? Gender { get; set; }

        public FamilyRole FamilyRole { get; set; }

        public string NationalCode { get; set; } = null;

        public string IdentityNumber { get; set; } = null;

        public virtual ApplicationUser Parent { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}
