using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class PayRollListViewModel
    {
        public int Year { get; set; }
     
        public int Month { get; set; }

        public long ProjectId { get; set; }

        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();


    }
}
