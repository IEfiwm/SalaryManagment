using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models
{
    public class ProjectViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string WorkshopCode { get; set; }

        public string WorkshopName { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string RowOfCovenant { get; set; }

        public string TaxAuthorityName { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string TaxAuthorityCode { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public string OwnerName { get; set; }

        public string InsurancesName { get; set; }

        public string DisplayName { get; set; }
    
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید.")]
        public string DisplayPhoneNumber { get; set; }

        public string DisplayEmail { get; set; }

        public string DisplayPostalCode { get; set; }

        public string DisplayAddress { get; set; }

        public List<long> BankAccounts { get; set; }
        public List<ProjectBankAccountViewModel>  projectBanks { get; set; }


    }
}
