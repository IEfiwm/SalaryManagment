using Common.Enums;
using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Basic
{
    public class Project : IdentityBaseEntity
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public string WorkshopCode { get; set; }

        public string WorkshopName { get; set; }

        public string RowOfCovenant { get; set; }

        public string TaxAuthorityName { get; set; }

        public string TaxAuthorityCode { get; set; }

        public virtual ICollection<ApplicationUser> ProjectUsers { get; set; }
    }
}
