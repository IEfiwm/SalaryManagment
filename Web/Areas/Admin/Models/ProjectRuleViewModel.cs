using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class ProjectRuleViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Rule { get; set; }

        public List<string> RuleList { get; set; }

        public long ProjectId { get; set; }

    }
}
