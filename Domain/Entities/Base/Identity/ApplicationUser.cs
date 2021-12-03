using Common.Enums;
using Domain.Entities.Basic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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

        public string CallerRef { get; set; }

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




        public string FatherName { get; set; }

        public string IdentitySerialNumber { get; set; }

        public string IdentityNumber { get; set; }

        public string Nationality { get; set; }

        public string BirthPlace { get; set; }

        public string JobTitle { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string DegreeOfEducation { get; set; }

        public string InsuranceCode { get; set; }

        public byte NumberOfChildren { get; set; }

        public int MonthlyBaseYear { get; set; }

        public int MonthlySalary { get; set; }

        public int ChildrenRight { get; set; }

        public int WorkerRight { get; set; }

        public int FoodAndHouseRight { get; set; }

        public int DailyBaseYear { get; set; }

        public int DailySalary { get; set; }

        public int InsuranceHistory { get; set; }

        public int WorkExperience { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public MilitaryService MilitaryService { get; set; }

        public string JobCode { get; set; }

        //[ForeignKey("Bank")]
        public long? BankAccountRef { get; set; }

        //[ForeignKey("Project")]
        public long? ProjectRef { get; set; }

        public virtual BankAccount Bank { get; set; }

        public virtual Project Project { get; set; }
    }
}
