using Common.Enums;
using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string BranchName { get; set; }

        public string Address { get; set; }

        public string OwnerName { get; set; }

        public string InsurancesName { get; set; }

        public string DisplayName { get; set; }

        public string DisplayPhoneNumber { get; set; }

        public string DisplayEmail { get; set; }

        public string DisplayPostalCode { get; set; }

        public string DisplayAddress { get; set; }
        
        public virtual ICollection<ApplicationUser> ProjectUsers { get; set; }

        public virtual ICollection<ProjectBankAccount>  ProjectBankAccounts { get; set; }

        public virtual ICollection<ProjectRule>  ProjectRules { get; set; }
    }
}
