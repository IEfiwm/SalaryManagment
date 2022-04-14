using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class Role_Project_PermissionViewModel
    {
        public long Id { get; set; }

        public string RoleId { get; set; }
       
        public long ProjectId { get; set; }

        public List<long> PermissionIds { get; set; }

        public RoleViewModel Role { get; set; }

        public PermissionsViewModel Permission { get; set; }

        public List<PermissionsViewModel> Permissions { get; set; }

    }
}
