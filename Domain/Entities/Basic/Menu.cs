using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class Menu : IdentityBaseEntity
    {
        public string Name { get; set; }

        public string PersianName { get; set; }

        public string Root { get; set; }

        public string Icon { get; set; }

        public long? ParentId { get; set; }

        public bool Active { get; set; }

        [ForeignKey("ParentId")]
        public virtual Menu Parent { get; set; }

        public virtual ICollection<Role_Menu> Role_Menus { get; set; }

    }
}
