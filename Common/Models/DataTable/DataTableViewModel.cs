namespace Common.Models.DataTable
{
    public class DataTableViewModel<T>
    {
        public T ViewModel { get; set; }

        public long DataCount { get; set; }

        public long PageNumber { get; set; }

        public long PageCount { get; set; }

        public long PageSize { get; set; }
    }
}
