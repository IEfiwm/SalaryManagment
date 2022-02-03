namespace Common.Models.DataTable
{
    public class DataTableDTO<T>
    {
        public T Model { get; set; }

        public long DataCount { get; set; }

        public long PageNumber { get; set; }

        public long PageCount { get; set; }

        public long PageSize { get; set; }
    }
}
