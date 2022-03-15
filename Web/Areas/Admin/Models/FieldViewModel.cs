namespace Web.Areas.Admin.Models
{
    public class FieldViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsCalculate { get; set; } = false;

        public bool IsCalculatedBy { get; set; } = false;
    }
}
