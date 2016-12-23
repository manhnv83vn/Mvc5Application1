namespace Mvc5Application1.Areas.Administration.Models
{
    public class ExportDataErrorWrapper
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string Error { get; set; }
        public string Color { get; set; }
    }
}