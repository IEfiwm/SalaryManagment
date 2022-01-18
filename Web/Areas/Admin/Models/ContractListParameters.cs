using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class ContractListParameters
    {
        public List<string> usernameList { get; set; }
        public long projectId { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
}
