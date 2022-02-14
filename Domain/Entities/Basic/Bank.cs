using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;

namespace Domain.Entities.Basic
{
    public class Bank : IdentityBaseEntity
    {
        public string Title { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Bank_Account> Bank_Accounts { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

    }
}
