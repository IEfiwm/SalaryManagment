using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class ContractListViewModel
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public long ProjectId { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
