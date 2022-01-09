﻿using Common.Enums;
using System;

namespace Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string JobCode { get; set; }

        public string PhoneNumber { get; set; }

        public string PersonnelCode { get; set; }

        public string NationalCode { get; set; }

        public string UserName { get; set; }

        public bool IsActive { get; set; } = true;

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public byte[] ProfilePicture { get; set; }

        public bool EmailConfirmed { get; set; }

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

        public string BankName { get; set; }

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

        public bool HasDocument { get; set; } = false;

        public bool HasAdditionalUser { get; set; } = false;

        public bool HasAdditionalUserDocument { get; set; } = false;

    }
}