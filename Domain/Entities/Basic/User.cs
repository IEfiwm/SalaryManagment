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

        public EmployeeStatus EmployeeStatus { get; set; }

        public string JobCode { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? StartWorkingDate { get; set; }

        public DateTime? EndWorkingDate { get; set; }

        //[ForeignKey("Bank")]
        public long? BankAccountRef { get; set; }

        //[ForeignKey("Project")]
        public long? ProjectRef { get; set; }

        public BankAccount Bank { get; set; }

        public Project Project { get; set; }
    }
}