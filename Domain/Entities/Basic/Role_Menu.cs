using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class Role_Menu : IdentityBaseEntity
    {
        public string RoleId { get; set; }

        public long MenuId { get; set; }

        public bool Disable { get; set; } = false;


        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role { get; set; }

    }
}
