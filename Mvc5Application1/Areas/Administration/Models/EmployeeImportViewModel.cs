using Mvc5Application1.Data.Model;

namespace Mvc5Application1.Areas.Administration.Models
{
    public class EmployeeImportViewModel
    {
        public Employees Destination { get; set; }
        public bool IsNew { get; set; }
        public int RowIndex { get; set; }
        public bool IsSkip { get; set; }
    }
}