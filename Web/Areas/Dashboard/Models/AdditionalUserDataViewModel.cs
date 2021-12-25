using Common.Enums;
using System;

namespace Web.Areas.Dashboard.Models
{
    public class AdditionalUserDataViewModel
    {
        public long Id { get; set; }

        public string ParentRef { get; set; } = null;

        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;

        public DateTime? Birthday { get; set; }

        public Gender? Gender { get; set; }

        public FamilyRole FamilyRole { get; set; }

        public string NationalCode { get; set; } = null;

        public string IdentityNumber { get; set; } = null;
    }
}
