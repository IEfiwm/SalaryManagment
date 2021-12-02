using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using System.ComponentModel.DataAnnotations.Schema;

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

        public byte NumberOfChildren { get; set; }

        public int MonthlyBaseYear { get; set; }

        public int MonthlySalary { get; set; }

        public int ChildrenRight { get; set; }

        public int WorkerRight { get; set; }

        public int FoodAndHouseRight { get; set; }

        public int DailyBaseYear { get; set; }

        public int DailySalary { get; set; }

        //[ForeignKey("Bank")]
        public long? BankAccountRef { get; set; }

        //[ForeignKey("Project")]
        public long? ProjectRef { get; set; }

        public virtual BankAccount Bank { get; set; }

        public virtual Project Project { get; set; }
    }
}