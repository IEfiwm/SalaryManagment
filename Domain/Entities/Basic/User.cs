using Common.Enums;
using Domain.Entities.Basic;
using System;

namespace Domain.Entities.Base.Identity
{
    public partial class ApplicationUser
    {
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

        public byte IncludedNumberOfChildren { get; set; }

        public byte NotIncludedNumberOfChildern { get; set; }

        public long MonthlyBaseYear { get; set; }

        public long MonthlySalary { get; set; }

        public long ChildrenRight { get; set; }

        public long WorkerRight { get; set; }

        public long FoodAndHouseRight { get; set; }

        public long DailyBaseYear { get; set; }

        public long DailySalary { get; set; }

        public long InsuranceHistory { get; set; }

        public long WorkExperience { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public MilitaryService MilitaryService { get; set; }

        public EmployeeStatus EmployeeStatus { get; set; }

        public string JobCode { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? StartWorkingDate { get; set; }

        public DateTime? EndWorkingDate { get; set; }

        public long InsuranceExemption { get; set; }

        public ExemptionReasons InsuranceExemptionType { get; set; }

        public long TaxExemption { get; set; }

        public ExemptionReasons TaxExemptionType { get; set; }

        //[ForeignKey("Bank")]
        public long? BankAccountRef { get; set; }

        //[ForeignKey("Project")]
        public long? ProjectRef { get; set; }

        public BankAccount Bank { get; set; }

        public Project Project { get; set; }
    }
}