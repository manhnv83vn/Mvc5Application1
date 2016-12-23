using Mvc5Application1.Areas.Administration.Models;
using Mvc5Application1.Areas.Administration.ViewModels;
using Mvc5Application1.Business.Contracts.Administration;
using Mvc5Application1.Business.Contracts.Exceptions;
using Mvc5Application1.Data.Model;
using Mvc5Application1.Extensions;
using Mvc5Application1.Framework;
using Mvc5Application1.Framework.Security.Authorization;
using Mvc5Application1.Helpers;
using Mvc5Application1.Infrastructure.Attribute;
using Mvc5Application1.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Application1.Areas.Administration.Controllers
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class EmployeeController : Controller
    {
        private readonly IAdministrationBusiness _administrationBusiness;

        public EmployeeController(IAdministrationBusiness administrationBusiness)
        {
            _administrationBusiness = administrationBusiness;
        }

        // GET: Administration/Employee/Index
        [HttpGet]
        [DisableCache]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new ImportNormViewModel();

            //var list = new List<Person>()
            //{
            //    new Person() {Name = "Nguyen Van Name1", Age = 10},
            //    new Person() {Name = "Le Van Name2", Age = 15},
            //    new Person() {Name = "Tran Van Name3", Age = 20}
            //};

            //var myList = list.Where(x => x != null && x.Age >= 15).Select(x => x.Name.Split(' ').Take(x.Name.Length - 1).Last());
            //var myList2 = list.Where(x => x != null && x.Age >= 15 && x.Name.StartsWith("Le")).Select(x => x.Name.Split(' ').Take(x.Name.Length - 1).Last());
            //var myList3 = list.Where(x => x != null && x.Age >= 15 && x.Name.EndsWith("Name3")).Select(x => x.Name.Split(' ').Take(x.Name.Length - 1).Last());

            //foreach (var firstName in myList)
            //{
            //    Debug.WriteLine(firstName);
            //}

            //foreach (var firstName in myList2)
            //{
            //    Debug.WriteLine(firstName);
            //}

            //foreach (var firstName in myList3)
            //{
            //    Debug.WriteLine(firstName);
            //}

            return View(model);
        }

        // POST: Administration/Employee/TestPost
        [HttpPost]
        [AllowAnonymous]
        public ActionResult TestPost(ImportNormViewModel model)
        {
            //TODO: Code for business here

            return Json(new
            {
                status = "OK",
                message = Message.IMPORT_Excel_File_Corrupt
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DownloadBlankTemplate()
        {
            try
            {
                const string filePath = "~/App_Data/ExportTemplates/ImportEmployeeTemplate.xlsx";
                var fileName = "EmployeesDataUpload.xlsx";
                var excel = new ExcelExporter(Server.MapPath(filePath));

                var folderPath = System.Web.HttpContext.Current.Server.MapPath("~\\Temp");
                var destinationFolder = string.Format("{0}\\{1}", folderPath, System.Web.HttpContext.Current.User.Identity.Name);
                CreateFolder(destinationFolder, fileName);

                const string sheet = "Employee";
                excel.SelectWorksheet(sheet);
                excel.SaveAs(Path.Combine(destinationFolder, fileName));

                return Json(new { fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                var errorMessage = Message.IMPORT_DRAWING_DownloadError;
                return Json(new { fileName = "", errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DownloadDataTemplate()
        {
            try
            {
                const string filePath = "~/App_Data/ExportTemplates/ImportEmployeeTemplate.xlsx";
                var fileName = "EmployeesDataUpload.xlsx";
                var excel = new ExcelExporter(Server.MapPath(filePath));

                var employees = _administrationBusiness.GetEmployeeses().OrderBy(t => t.PayrollNumber).ToList();
                SetDataForDownloadEmployees(excel, employees);
                var folderPath = System.Web.HttpContext.Current.Server.MapPath("~\\Temp");
                var destinationFolder = string.Format("{0}\\{1}", folderPath, System.Web.HttpContext.Current.User.Identity.Name);
                CreateFolder(destinationFolder, fileName);
                excel.SaveAs(Path.Combine(destinationFolder, fileName));
                return Json(new { fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                var errorMessage = Message.IMPORT_DRAWING_DownloadError;
                return Json(new { fileName = "", errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }

        [DisableCache]
        public ActionResult DownloadFileFromTemp(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return RedirectToAction("ImportEmployee");
            }

            var folderPath = System.Web.HttpContext.Current.Server.MapPath("~\\Temp");
            var destinationFolder = string.Format("{0}\\{1}", folderPath, System.Web.HttpContext.Current.User.Identity.Name);
            string fullPath = Path.Combine(destinationFolder, fileName);

            return File(fullPath, "application/vnd.ms-excel", fileName);
        }

        [HttpPost]
        [Mvc5Application1Permission(Function = "Add Employee")]
        public ActionResult ImportEmployeeSubmit(HttpPostedFileBase fileDrawingImport)
        {
            var file = Request.Files["fileDrawingImport"];

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentLength > 10485760)
                {
                    return Json(new { message = Message.IMPORT_FileSizeLimit }, JsonRequestBehavior.AllowGet);
                }

                var extension = Path.GetExtension(file.FileName);
                if (extension != ".xls" && extension != ".xlsx")
                {
                    return Json(new { message = Message.IMPORT_DRAWING_File_Corrupt }, JsonRequestBehavior.AllowGet);
                }
                var fileNameNoEx = Path.GetFileNameWithoutExtension(file.FileName);
                var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var fileName = string.Concat(fileNameNoEx, timeStamp, extension);

                var uploadPath = ConfigurationManager.AppSettings["ImportUploadFilePath"];
                var exists = Directory.Exists(Server.MapPath(uploadPath));
                if (!exists)
                    Directory.CreateDirectory(Server.MapPath(uploadPath));

                var path = Path.Combine(Server.MapPath(uploadPath), fileName);
                file.SaveAs(path);
                try
                {
                    var cont = ImportExportHelper.ReadExcelFile(path, true);

                    if (cont.Tables.Count > 0)
                    {
                        var sheet = cont.Tables[0];
                        var columnCount = sheet.Columns.Cast<DataColumn>().Select(x => x.ColumnName).Count(x => !string.IsNullOrEmpty(x));
                        if (columnCount < (typeof(ImportEmployeeTemplateHeaderEnum)).ToDictionary().Count())
                        {
                            return Json(new { message = Message.IMPORT_DRAWING_Invalid_Header_Row_Name }, JsonRequestBehavior.AllowGet);
                        }
                        if (sheet.Rows.Count > 0)
                        {
                            try
                            {
                                var resultSheet = sheet.Clone();
                                foreach (DataRow dr in sheet.Rows)
                                {
                                    resultSheet.Rows.Add(dr.ItemArray);
                                }
                                var errorList = new List<ExportDataErrorWrapper>();
                                var importModel = ExtractEmployeesList(ref sheet, ref errorList);
                                if (importModel.Any())
                                {
                                    if (!errorList.Any())
                                    {
                                        //Add New
                                        var addNewList = new List<Employees>();
                                        var updateList = new List<Employees>();

                                        foreach (var item in importModel)
                                        {
                                            var message = "";
                                            if (item.IsNew)
                                            {
                                                addNewList.Add(item.Destination);
                                                message = "Added";
                                            }
                                            else
                                            {
                                                if (item.IsSkip)
                                                {
                                                    message = "Skipped: employee data unchanged";
                                                }
                                                else
                                                {
                                                    updateList.Add(item.Destination);
                                                    message = "Updated";
                                                }
                                            }
                                            resultSheet.Rows[item.RowIndex][(int)ImportEmployeeTemplateHeaderEnum.ImportResult] = message;
                                        }
                                        //Execute Add/Update
                                        _administrationBusiness.AddRangeEmployeeNoSaveChange(addNewList);
                                        _administrationBusiness.UpdateRangeEmployeeNoSaveChange(updateList);

                                        if (addNewList.Any() || updateList.Any())
                                        {
                                            _administrationBusiness.EmployeesSaveChange();
                                        }

                                        var result = DownloadResultTemplate(resultSheet, Message.IMPORT_DRAWING_Import_Successfully);

                                        DeleteFile(path);
                                        return result;
                                    }
                                    else
                                    {
                                        foreach (var row in resultSheet.AsEnumerable())
                                        {
                                            row[(int)ImportEmployeeTemplateHeaderEnum.ImportResult] = "";
                                        }
                                        var result = DownloadResultTemplate(resultSheet, Message.IMPORT_DRAWING_Import_Failed, errorList);
                                        DeleteFile(path);
                                        return result;
                                    }
                                }
                                DeleteFile(path);
                                return Json(new { message = Message.IMPORT_Employee_NoEmployee_Imported }, JsonRequestBehavior.AllowGet);
                            }
                            catch (InvalidImportDataException ex)
                            {
                                DeleteFile(path);
                                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
                            }
                            catch (Exception ex)
                            {
                                DeleteFile(path);
                                return Json(new { message = "A problem occurred while implementing the function, please try again" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        DeleteFile(path);
                        return Json(new { message = Message.IMPORT_Employee_NoEmployee_Imported }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { message = Message.IMPORT_DRAWING_Invalid_Header_Row_Name }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    DeleteFile(path);
                    if (ex.Message.Contains("belongs to this DataTable"))
                    {
                        return Json(new { message = Message.IMPORT_DRAWING_Invalid_Header_Row_Name }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { message = Message.IMPORT_DRAWING_File_Corrupt }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { message = Message.IMPORT_DRAWING_File_Corrupt }, JsonRequestBehavior.AllowGet);
            return Json(new { message = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        private List<EmployeeImportViewModel> ExtractEmployeesList(ref DataTable sheet, ref List<ExportDataErrorWrapper> errors)
        {
            var templateCols = (typeof(ImportEmployeeTemplateHeaderEnum)).ToDictionary();
            var result = new List<EmployeeImportViewModel>();

            #region Check Template - Add index colum - Delete empty rows

            //Check Template

            if (sheet.Columns.Count < templateCols.Count())
                throw new InvalidImportDataException(Message.IMPORT_DRAWING_Invalid_Header_Row_Name);
            for (int i = 0; i < templateCols.Count(); i++)
            {
                if (templateCols[i] != sheet.Columns[i].ColumnName.Trim())
                {
                    throw new InvalidImportDataException(Message.IMPORT_DRAWING_Invalid_Header_Row_Name);
                }
            }

            //Add index column
            sheet.Columns.Add(new DataColumn("RowIndex"));
            for (var i = 0; i < sheet.Rows.Count; i++)
            {
                sheet.Rows[i]["RowIndex"] = i;
            }

            //Delete empty rows
            for (var i = 0; i < sheet.Rows.Count; i++)
            {
                var isEmpty = true;
                for (var j = 0; j < templateCols.Count(); j++)
                {
                    var header = templateCols[j];
                    if (sheet.Rows[i][header] != null && sheet.Rows[i][header].ToString().Trim() != "")
                    {
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty) continue;
                sheet.Rows[i].Delete();
                i = i - 1;
            }
            sheet.AcceptChanges();

            #endregion Check Template - Add index colum - Delete empty rows

            #region Check mandatory fields

            //Check mandatory fields
            var mandatoryColIndexs = new List<int>
            { (int)ImportEmployeeTemplateHeaderEnum.PayrollNumber,
                                                        (int)ImportEmployeeTemplateHeaderEnum.Surname,
                                                        (int)ImportEmployeeTemplateHeaderEnum.FirstName};

            foreach (var i in mandatoryColIndexs)
            {
                //var columnValues = sheet.AsEnumerable().Select(dr => dr[i]).ToList();
                var rowIndexsOfEmptyCells = sheet.AsEnumerable().Where(dr => dr[i] == null || dr[i].ToString().Trim() == "").Select(dr => Convert.ToInt32(dr["RowIndex"].ToString()) + 2).ToList();
                if (rowIndexsOfEmptyCells.Any())
                {
                    errors.AddRange(rowIndexsOfEmptyCells.Select(rowindex => new ExportDataErrorWrapper { ColumnIndex = i + 1, RowIndex = rowindex, Error = Message.IMPORT_Required_Cell }));
                }
            }

            #endregion Check mandatory fields

            #region Check maxlength

            //Check maxlength
            var maxLengthRule = new List<Tuple<ImportEmployeeTemplateHeaderEnum, int, string>>();
            maxLengthRule.Add(new Tuple<ImportEmployeeTemplateHeaderEnum, int, string>(ImportEmployeeTemplateHeaderEnum.PayrollNumber, 50, string.Format(Message.IMPORT_Employee_Max_Length, "Payroll Number")));
            maxLengthRule.Add(new Tuple<ImportEmployeeTemplateHeaderEnum, int, string>(ImportEmployeeTemplateHeaderEnum.Surname, 50, string.Format(Message.IMPORT_Employee_Max_Length, "Surname")));
            maxLengthRule.Add(new Tuple<ImportEmployeeTemplateHeaderEnum, int, string>(ImportEmployeeTemplateHeaderEnum.FirstName, 50, string.Format(Message.IMPORT_Employee_Max_Length, "First Name")));

            foreach (var rule in maxLengthRule)
            {
                var rule1 = rule;
                var columnValues = sheet.AsEnumerable().Where(a => a[(int)rule1.Item1] != null && a[(int)rule1.Item1].ToString().Trim().Length > rule1.Item2).ToList();
                if (columnValues.Any())
                {
                    errors.AddRange(from errorRow in columnValues let col = (int)rule.Item1 let row = Convert.ToInt32(errorRow["RowIndex"]) + 2 select new ExportDataErrorWrapper { ColumnIndex = col + 1, RowIndex = row, Error = rule.Item3 });
                }
            }

            #endregion Check maxlength

            #region Check Duplicate Payroll number

            var duplicateRows = sheet.AsEnumerable().Where(dr => dr[(int)ImportEmployeeTemplateHeaderEnum.PayrollNumber].ToString().Trim() != "")
                                                    .GroupBy(dr => dr[(int)ImportEmployeeTemplateHeaderEnum.PayrollNumber].ToString().Trim().ToLower())
                                                    .Where(i => i.Count() > 1)
                                                    .Select(i => i.ToList()).FirstOrDefault();
            if (duplicateRows != null)
            {
                var firstrow = Convert.ToInt32(duplicateRows[0]["RowIndex"]) + 2;
                var secondrow = Convert.ToInt32(duplicateRows[1]["RowIndex"]) + 2;

                const int col = (int)ImportEmployeeTemplateHeaderEnum.PayrollNumber;

                AppendErrorToExportErrorList(ref errors, Message.IMPORT_Employee_Duplicate_PayrollNumber, col + 1, firstrow);
                AppendErrorToExportErrorList(ref errors, Message.IMPORT_Employee_Duplicate_PayrollNumber, col + 1, secondrow);
            }

            #endregion Check Duplicate Payroll number

            #region Validate each row -> Add to return result

            var employeeExistInDb = _administrationBusiness.GetEmployeeses();
            Employees originalEmployees = null;
            foreach (var row in sheet.AsEnumerable())
            {
                var rowidx = Convert.ToInt32(row["RowIndex"]) + 2;
                var employeeImportModel = new EmployeeImportViewModel { RowIndex = rowidx - 2 };

                var employeeObj = new Employees
                {
                    PayrollNumber =
                        row[(int)ImportEmployeeTemplateHeaderEnum.PayrollNumber].ToString().Trim().ToUpper()
                };

                //Add existing drawing to result
                var existedEmployee = employeeExistInDb.FirstOrDefault(a => a.PayrollNumber.Trim().ToLower() == employeeObj.PayrollNumber.ToLower());

                if (existedEmployee == null)
                {
                    employeeImportModel.IsNew = true;
                }
                else
                {
                    employeeObj = existedEmployee;
                    originalEmployees = existedEmployee;
                }

                //Validate on each row
                const string patternPayrollRegex = @"^[a-zA-Z0-9]*$";

                //Payroll Number
                var payRollNo = row[(int)ImportEmployeeTemplateHeaderEnum.PayrollNumber];
                if (payRollNo != null && payRollNo.ToString().Trim() != "")
                {
                    if (!Regex.IsMatch(payRollNo.ToString().Trim(), patternPayrollRegex))
                    {
                        const int colidx = (int)ImportEmployeeTemplateHeaderEnum.PayrollNumber;
                        AppendErrorToExportErrorList(ref errors, Message.Import_Employee_InvalidPayrollNumberFormat, colidx + 1, rowidx);
                    }
                    else
                    {
                        employeeObj.PayrollNumber = payRollNo.ToString().ToUpper().Trim();
                    }
                }

                //Surname
                var surName = row[(int)ImportEmployeeTemplateHeaderEnum.Surname];
                if (surName != null && surName.ToString().Trim() != "")
                {
                    employeeObj.Surname = surName.ToString().Trim();
                }

                //First Name
                var firstName = row[(int)ImportEmployeeTemplateHeaderEnum.FirstName];
                if (firstName != null && firstName.ToString().Trim() != "")
                {
                    employeeObj.FirstName = firstName.ToString().Trim();
                }

                if (!employeeImportModel.IsNew)
                {
                    employeeImportModel.IsSkip = !IsDataChanged(employeeObj, originalEmployees);
                }

                employeeImportModel.Destination = employeeObj;
                result.Add(employeeImportModel);
            }

            #endregion Validate each row -> Add to return result

            return result;
        }

        public ActionResult DownloadResultTemplate(DataTable data, string errorMessage, List<ExportDataErrorWrapper> errorList = null)
        {
            const string filePath = "~/App_Data/ExportTemplates/ImportEmployeeTemplate.xlsx";
            var fileName = "ImportEmployeeResult.xlsx";
            var excel = new ExcelExporter(Server.MapPath(filePath));

            SetDataForDownloadDrawingResult(excel, data, errorList);

            var folderPath = System.Web.HttpContext.Current.Server.MapPath("~\\Temp");
            var destinationFolder = string.Format("{0}\\{1}", folderPath, System.Web.HttpContext.Current.User.Identity.Name);
            CreateFolder(destinationFolder, fileName);

            excel.SaveAs(Path.Combine(destinationFolder, fileName));
            return Json(new { message = errorMessage, fileName, status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        private bool IsDataChanged(Employees employee, Employees originalEmployees)
        {
            return employee.Surname != originalEmployees.Surname ||
                   employee.FirstName != originalEmployees.FirstName;
        }

        private void SetDataForDownloadEmployees(ExcelExporter excel, List<Employees> employeeses)
        {
            const string sheet = "Employee";
            excel.SelectWorksheet(sheet);
            excel.SetCellValues(2, 1, employeeses,
                x => x.PayrollNumber,
                x => x.Surname,
                x => x.FirstName);
        }

        private void SetDataForDownloadDrawingResult(ExcelExporter excel, DataTable data, List<ExportDataErrorWrapper> errorList = null)
        {
            const string sheet = "Employee";
            excel.SelectWorksheet(sheet);
            var temp = data.AsEnumerable().ToList();
            excel.SetCellValues(2, 1, temp,
                 x => x[(int)ImportEmployeeTemplateHeaderEnum.PayrollNumber],
                x => x[(int)ImportEmployeeTemplateHeaderEnum.Surname],
                x => x[(int)ImportEmployeeTemplateHeaderEnum.FirstName],
                x => x[(int)ImportEmployeeTemplateHeaderEnum.ImportResult]
                );
            if (errorList != null)
            {
                foreach (var item in errorList)
                {
                    excel.SetCommentAndHightlightCell(item.RowIndex, item.ColumnIndex, item.Error);
                }
            }
        }

        private void CreateFolder(string folderName, string fileName)
        {
            var exists = Directory.Exists(folderName);
            if (!exists)
                Directory.CreateDirectory(folderName);
            else
            {
                var files = Directory.GetFiles(folderName);
                foreach (var file in files.Where(file => file.Contains(fileName)))
                {
                    System.IO.File.Delete(file);
                }
            }
        }

        private void AppendErrorToExportErrorList(ref List<ExportDataErrorWrapper> errorList, string errorMessage, int col, int row)
        {
            var existed = errorList.FirstOrDefault(a => a.ColumnIndex == col && a.RowIndex == row);
            if (existed == null)
            {
                errorList.Add(new ExportDataErrorWrapper { ColumnIndex = col, RowIndex = row, Error = errorMessage });
            }
            else
            {
                existed.Error += "\n" + String.Format(errorMessage);
            }
        }

        private void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}