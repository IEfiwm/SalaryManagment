using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Dashboard.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        [RegularExpression(@"^(?!(\d)\1{9})\d{10}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string NationalCode { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string IdentitySerialNumber { get; set; }

        [Required]
        [RegularExpression("^[0-9]{2,}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string IdentityNumber { get; set; }

        [Required]
        public string BirthPlace { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression(@"^(?!(\d)\1{9})\d{5,}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string InsuranceCode { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public byte NumberOfChildren { get; set; }

        [Required]
        [RegularExpression("^09[0-3][0-9]-?[0-9]{3}-?[0-9]{4}$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string PhoneNumber { get; set; }

        public List<AdditionalUserDataViewModel> AdditionalUserData
        {
            get;
            set;
        } = new List<AdditionalUserDataViewModel>();

        //[Required]
        //public MaritalStatus MaritalStatus { get; set; }

        //[Required]
        //public MilitaryService MilitaryService { get; set; }
    }
}