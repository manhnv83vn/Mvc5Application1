namespace Mvc5Application1.Models
{
    public class ExportErrorWrapper
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string Error { get; set; }
        public string Color { get; set; }
    }
}