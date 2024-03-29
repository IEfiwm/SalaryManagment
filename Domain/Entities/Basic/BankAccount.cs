﻿using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class BankAccount : AuditBaseEntity
    {
        public string Title { get; set; }

        public string AccountNumber { get; set; }

        public string BranchName { get; set; }

        public string BranchCode { get; set; }

        public string iBan { get; set; }

        public string CardNumber { get; set; }

        [ForeignKey("Bank")]
        public long? BankId { get; set; }

        public Bank Bank { get; set; }

        public virtual ICollection<ApplicationUser> BankUsers { get; set; }
    }
}
