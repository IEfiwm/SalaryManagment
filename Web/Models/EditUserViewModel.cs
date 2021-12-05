using Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EditUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        public string NationalCode { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string IdentitySerialNumber { get; set; }

        [Required]
        public string IdentityNumber { get; set; }

        [Required]
        public string BirthPlace { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string InsuranceCode { get; set; }

        [Required]
        public byte NumberOfChildren { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        [Required]
        public MilitaryService MilitaryService { get; set; }
    }
}