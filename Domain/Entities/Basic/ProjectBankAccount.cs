using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class ProjectBankAccount : IdentityBaseEntity
    {

        [ForeignKey("Bank_Account")]
        public long Bank_AccountId { get; set; }

        [ForeignKey("Project")]
        public long ProjectId { get; set; }

        public Bank_Account Bank_Account { get; set; }

        public Project Project { get; set; }


    }
}
