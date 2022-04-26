namespace Web.Areas.Admin.Models
{
    public class PermissionsViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string PersianName { get; set; }
        
        public PermissionsViewModel Parent { get; set; }

        public long? ParentId { get; set; }
    }
}
