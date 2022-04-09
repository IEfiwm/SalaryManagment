using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class User_Role : IdentityBaseEntity
    {
        public string RoleId { get; set; }

        public string UserId { get; set; }


        [ForeignKey("MenuId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role { get; set; }

    }
}
