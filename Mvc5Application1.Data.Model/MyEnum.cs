using System.ComponentModel;

namespace Mvc5Application1.Data.Model
{
    public static class CommonString
    {
        public static string DateFormat = "dd-MMM-yyyy";
    }

    public enum MessageEnum
    {
        Empty,
        AddSuccess,
        AddFail,
        UpdateSuccess,
        UpdateFail,
        DeleteSuccess,
        DeleteFail,
        SaveSuccess,
        SaveFail
    }

    public enum ImportEmployeeTemplateHeaderEnum
    {
        [Description("Payroll Number")]
        PayrollNumber = 0,

        [Description("Surname")]
        Surname = 1,

        [Description("First Name")]
        FirstName = 2,

        [Description("(Import Result)")]
        ImportResult = 3
    }
}