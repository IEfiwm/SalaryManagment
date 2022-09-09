using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Areas.Dashboard.Models;

namespace Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string JobCode { get; set; }

        [Required]
        [RegularExpression("^09[0-9][0-9]-?[0-9]{3}-?[0-9]{4}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string PersonnelCode { get; set; }

        [Required]
        [RegularExpression(@"^(?!(\d)\1{9})\d{10}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string NationalCode { get; set; }

        public string UserName { get; set; }

        public bool IsActive { get; set; } = true;

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public byte[] ProfilePicture { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        public string FatherName { get; set; }

        public string IdentitySerialNumber { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string IdentityNumber { get; set; }

        public string Nationality { get; set; }

        public string BirthPlace { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string DegreeOfEducation { get; set; }

        [Required]
        [RegularExpression(@"^(?!(\d)\1{9})\d{5,}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string InsuranceCode { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public byte NotIncludedNumberOfChildren { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public byte IncludedNumberOfChildren { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int MonthlyBaseYear { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int MonthlySalary { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int ChildrenRight { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int WorkerRight { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int FoodAndHouseRight { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int DailyBaseYear { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int DailySalary { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int InsuranceHistory { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int WorkExperience { get; set; }

        public string BankName { get; set; }

        [Required]
        [RegularExpression(@"^(?!(\d)\1{9})\d{5,}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string BankAccNumber { get; set; }

        [Required]
        public string ShebaNumber { get; set; }

        public long BankRef { get; set; }

        public string ProjectName { get; set; }

        public long ProjectRef { get; set; }

        public string Id { get; set; }

        public DateTime? HireDate { get; set; }

        [Required]
        public DateTime? StartWorkingDate { get; set; }

        public DateTime? EndWorkingDate { get; set; }

        public DateTime? Birthday { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public MilitaryService MilitaryService { get; set; }

        public Gender Gender { get; set; }

        public EmployeeStatus EmployeeStatus { get; set; }

        public bool HasDocument { get; set; } = false;

        public bool HasAdditionalUser { get; set; } = false;

        public bool HasAdditionalUserDocument { get; set; } = false;

        public List<AdditionalUserDataViewModel> AdditionalUserData
        {
            get;
            set;
        } = new List<AdditionalUserDataViewModel>();

        [Required]
        public long? BankId { get; set; }

        public List<string> RoleIds { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public long InsuranceExemption { get; set; }

        public ExemptionReasons InsuranceExemptionType { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public long TaxExemption { get; set; }

        public ExemptionReasons TaxExemptionType { get; set; }

        public PersonnelType PersonnelType { get; set; }
    }
}