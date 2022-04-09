using Domain.Entities.Basic;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities.Base.Identity
{
    public partial class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string name) : base(name)
        {

        }

        public ApplicationRole() : base()
        {
        }

        public bool Active { get; set; }

        public virtual ICollection<Role_Menu> Role_Menus { get; set; }

        public virtual ICollection<User_Role> User_Roles { get; set; }

        public virtual ICollection<Role_Project_Permission> Role_Project_Permissions { get; set; }

    }
}