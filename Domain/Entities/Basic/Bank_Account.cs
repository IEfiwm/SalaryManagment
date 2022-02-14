using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class Bank_Account : IdentityBaseEntity
    {
        public string AccountNumber { get; set; }
       
        public string BranchName { get; set; }

        public string BranchCode { get; set; }

        public string iBan { get; set; }

        public string CardNumber { get; set; }

        public bool Active { get; set; }
        
        [ForeignKey("Bank")]
        public long BankId { get; set; }

        public Bank Bank { get; set; }

        public virtual ICollection<ProjectBankAccount> ProjectBankAccounts { get; set; }


    }
}
