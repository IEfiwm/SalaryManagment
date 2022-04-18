namespace Web.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string PersianName { get; set; }

        public string Root { get; set; }

        public string Icon { get; set; }

        public long? ParentId { get; set; }

        public int Order { get; set; } = 0;

        public bool Active { get; set; }

        public virtual MenuViewModel Parent { get; set; }
    }
}
