using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class ManageUserRoleViewModel
    {
        public string UserId { get; set; }
        public IList<UserRoleViewModel> UserRoles { get; set; }
    }

    public class UserRoleViewModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}