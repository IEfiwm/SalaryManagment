namespace Web.Areas.Admin.Models
{
    public class TransferPersonnelViewModel
    {
        public int OldProjectId { get; set; }

        public int NewProjectId { get; set; }

        public bool DisableOldProject { get; set; }
    }
}