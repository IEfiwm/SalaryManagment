using Common.Enums;
using Domain.Entities.Basic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base.Identity
{
    public partial class ApplicationUser : IdentityUser
    {
        public string PersonnelCode { get; set; }

        public string PrefixName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => PrefixName + " " + FirstName + " " + LastName;

        public string AvatarPath { get; set; }

        public DateTime? Birthday { get; set; }

        public Gender Gender { get; set; }

        public string About { get; set; }

        public string NationalCode { get; set; }

        public string Description { get; set; }

        public bool IsProfileCompleted { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsBlocked { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public UserType UserType { get; set; }

        [ForeignKey("Caller")]
        public string CallerRef { get; set; } = null;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        //public string CreatedByRef { get; set; }

        //public string UpdatedByRef { get; set; }

        //public virtual ApplicationUser CreatedBy { get; set; }

        //public virtual ApplicationUser UpdatedBy { get; set; }

        public virtual ApplicationUser Caller { get; set; }

        public virtual ICollection<ApplicationUser> CallerUsers { get; set; }

        public virtual ICollection<ApplicationUser> UpdatedByUsers { get; set; }

        public virtual ICollection<ApplicationUser> CreatedByUsers { get; set; }

        //public virtual ICollection<Project> ProjectUpdatedByUsers { get; set; }

        //public virtual ICollection<Project> ProjectCreatedByUsers { get; set; }

        public virtual ICollection<BankAccount> BankUpdatedByUsers { get; set; }

        public virtual ICollection<BankAccount> BankCreatedByUsers { get; set; }

    }
}
