using Common.Enums;
using System;

namespace Web.Areas.Admin.Models
{
    public class ProjectViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public string WorkshopCode { get; set; }

        public string WorkshopName { get; set; }

        public string RowOfCovenant { get; set; }

        public string TaxAuthorityName { get; set; }

        public string TaxAuthorityCode { get; set; }
    }
}
