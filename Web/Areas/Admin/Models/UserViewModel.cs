using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Areas.Dashboard.Models;

namespace Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string JobCode { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string PhoneNumber { get; set; }

        public string PersonnelCode { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string NationalCode { get; set; }

        public string UserName { get; set; }

        public bool IsActive { get; set; } = true;

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public byte[] ProfilePicture { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FatherName { get; set; }

        public string IdentitySerialNumber { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string IdentityNumber { get; set; }

        public string Nationality { get; set; }

        public string BirthPlace { get; set; }

        public string JobTitle { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string DegreeOfEducation { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string InsuranceCode { get; set; }
      
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public byte NumberOfChildren { get; set; }

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

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string BankAccNumber { get; set; }

        public long BankRef { get; set; }

        public string ProjectName { get; set; }

        public long ProjectRef { get; set; }

        public string Id { get; set; }

        public DateTime? HireDate { get; set; }

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


        public long? BankId { get; set; }

    }
}