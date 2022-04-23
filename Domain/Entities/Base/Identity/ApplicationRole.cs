using Domain.Entities.Basic;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities.Base.Identity
{
    public partial class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string name, bool systemRole = false, bool upublic = true) : base(name)
        {
            Public = upublic;
            SystemRole = systemRole;
        }

        public ApplicationRole() : base()
        {
        }

        public bool Active { get; set; } = true;

        public bool SystemRole { get; set; } = false;

        public bool Public { get; set; } = true;

        public virtual ICollection<Role_Menu> Role_Menus { get; set; }

        public virtual ICollection<Role_Project_Permission> Role_Project_Permissions { get; set; }

    }
}