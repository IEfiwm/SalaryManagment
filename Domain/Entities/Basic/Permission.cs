using Domain.Base.Entity;
using Domain.Entities.Base.Identity;
using System.Collections.Generic;

namespace Domain.Entities.Basic
{
    public class Permission : IdentityBaseEntity
    {
        public string Name { get; set; }

        public string PersianName { get; set; }

        public long? ParentId { get; set; }

        public virtual Permission Parent { get; set; }

        public virtual ICollection<Role_Project_Permission> Role_Project_Permissions { get; set; }

    }
}
