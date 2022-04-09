using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class Role_Project_Permission : IdentityBaseEntity
    {
        public string RoleId { get; set; }

        public long PermissionId { get; set; }

        public long ProjectId { get; set; }


        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

    }
}
