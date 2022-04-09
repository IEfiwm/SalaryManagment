using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public bool Active { get; set; }

        public List<long> MenuIds { get; set; }

        public List<MenuViewModel> Menus { get; set; }


    }
}