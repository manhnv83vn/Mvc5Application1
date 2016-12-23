using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel;
using Mvc5Application1.Business.Contracts.Exceptions;
using Mvc5Application1.Models;
using Mvc5Application1.Resources;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mvc5Application1.Helpers
{
    public static class ImportExportHelper
    {
        private static string PROVIDER_XLSX = ConfigurationManager.AppSettings["ProviderXLSX"];
        private static string PROVIDER_XLS = ConfigurationManager.AppSettings["ProviderXLS"];
        private static string EXTENDED_PROPERTIES_XLSX = ConfigurationManager.AppSettings["ExtendedPropertiesXLSX"];
        private static string EXTENDED_PROPERTIES_XLS = ConfigurationManager.AppSettings["ExtendedPropertiesXLS"];

        private static string GetConnectionString(string filePath)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            var extension = Path.GetExtension(filePath);

            // XLSX - Excel 2007, 2010, 2012, 2013
            if (extension == ".xlsx")
            {
                props["Provider"] = PROVIDER_XLSX;
                props["Extended Properties"] = EXTENDED_PROPERTIES_XLSX;
            }
            else
            {
                props["Provider"] = PROVIDER_XLS;
                props["Extended Properties"] = EXTENDED_PROPERTIES_XLS;
            }

            props["Data Source"] = filePath;

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }

        public static DataSet ReadExcelFile(string filePath, bool readByOpenXmlDocument, string sheetName = null)
        {
            if (readByOpenXmlDocument)
            {
                return ReadExcelFileSLDocument(filePath, sheetName);
            }
            return ReadExcelFileSLDocument(filePath, sheetName);
        }

        public static DataSet ReadExcelFileOleDb(string filePath)
        {
            DataSet ds = new DataSet();

            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                // Get all Sheets in Excel File
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data
                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();

                    //if (!sheetName.EndsWith("$"))
                    //    continue;

                    // Get all rows from the Sheet
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    ds.Tables.Add(dt);
                }

                cmd = null;
                conn.Close();
            }

            return ds;
        }

        public static DataSet ReadExcelFileExDataReader(string filePath, params string[] sheetNames)
        {
            IExcelDataReader excelReader = null;
            FileStream stream = null;
            DataSet excelDataSet = new DataSet();
            DataSet resultDataSet = new DataSet();
            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if (filePath.ToLower().EndsWith(".xls"))
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream, true);
                }
                else if (filePath.ToLower().EndsWith(".xlsx"))
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                // DataSet – Create column names from first row
                excelReader.IsFirstRowAsColumnNames = false;

                excelDataSet = excelReader.AsDataSet(true);

                foreach (DataTable t in excelDataSet.Tables)
                {
                    if (t.Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        var firstRow = t.Rows[0];
                        foreach (var c in firstRow.ItemArray)
                        {
                            if (dt.Columns.Contains(c.ToString()))
                            {
                                throw new InvalidImportDataException(Message.IMPORT_Common_Invalid_Header_Row_Name);
                            }
                            dt.Columns.Add(c.ToString());
                        }
                        if (t.Rows.Count > 1)
                        {
                            for (int i = 1; i < t.Rows.Count; i++)
                            {
                                var dr = dt.NewRow();
                                dr.ItemArray = t.Rows[i].ItemArray.Clone() as object[];
                                dt.Rows.Add(dr);
                            }
                        }
                        resultDataSet.Tables.Add(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //errorMessage = FileUpload1.FileName + ” does not seem to be in Excel (*.xls) format. Error creating or binding dataset from excelReader : “ + ex.Message;
            }
            finally
            {
                if (excelReader != null) excelReader.Close();
                if (stream != null) stream.Close();
                File.Delete(filePath);
            }

            return resultDataSet;
        }

        private static DataSet ReadExcelFileSLDocument(string filePath, string sheetName = null)
        {
            var ds = new DataSet();
            DataTable dtStrongTyping = new DataTable("Sheet1");
            using (SLDocument sl = sheetName == null ? new SLDocument(filePath) : new SLDocument(filePath, sheetName))
            {
                SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                int iStartColumnIndex = stats.StartColumnIndex;

                //Get headerrow
                for (var colIndex = stats.StartColumnIndex; colIndex <= stats.EndColumnIndex; colIndex++)
                {
                    dtStrongTyping.Columns.Add(new DataColumn(sl.GetCellValueAsString(1, colIndex), typeof(string)));
                }

                for (var row = stats.StartRowIndex + 1; row <= stats.EndRowIndex; ++row)
                {
                    DataRow dr = dtStrongTyping.NewRow();
                    for (var colIndex = stats.StartColumnIndex; colIndex <= stats.EndColumnIndex; colIndex++)
                    {
                        //var fieldType = GetDataType(sl.GetCellStyle(row, colIndex).FormatCode);

                        if (IsDateTimeType(sl.GetCellStyle(row, colIndex).FormatCode))
                        {
                            //DateTime date;
                            //DateTime.TryParse(sl.GetCellValueAsString(row, colIndex), out date);
                            try
                            {
                                var dateNumber = Convert.ToInt32(sl.GetCellValueAsString(row, colIndex));
                                dr[colIndex - stats.StartColumnIndex] = FromExcelSerialDate(dateNumber).ToString("dd-MMM-yyyy");
                            }
                            catch (FormatException)
                            {
                                dr[colIndex - stats.StartColumnIndex] = sl.GetCellValueAsString(row, colIndex);
                            }
                        }
                        else
                        {
                            dr[colIndex - stats.StartColumnIndex] = sl.GetCellValueAsString(row, colIndex);
                        }
                    }
                    dtStrongTyping.Rows.Add(dr);
                }
            }
            ds.Tables.Add(dtStrongTyping);
            return ds;
        }

        public static void AppendErrorToExportErrorList(ref List<ExportErrorWrapper> errorList, string errorMessage, int col, int row)
        {
            var existed = errorList.FirstOrDefault(a => a.ColumnIndex == col && a.RowIndex == row);
            if (existed == null)
            {
                errorList.Add(new ExportErrorWrapper { ColumnIndex = col, RowIndex = row, Error = errorMessage });
            }
            else
            {
                if (existed.Error.Trim() != errorMessage.Trim())
                    existed.Error += "\n" + String.Format(errorMessage);
            }
        }

        public static void CreateFolder(string folderName, string fileName)
        {
            var exists = Directory.Exists(folderName);
            if (!exists)
                Directory.CreateDirectory(folderName);
            else
            {
                var files = Directory.GetFiles(folderName);
                foreach (var file in files)
                {
                    if (file.Contains(fileName))
                        File.Delete(file);
                }
            }
        }

        private static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }

        private static bool IsDateTimeType(string formatCode)
        {
            if (formatCode.Contains("[$-409]") || formatCode.Contains("[$-F800]") || formatCode.Contains("m/d") || formatCode.Contains("d-m") || formatCode.Contains("m-y") || formatCode.Contains("m yy"))
            {
                return true;
            }
            return false;
        }

        private static string GetDataType(string formatCode)
        {
            if (formatCode.Contains("h:mm") || formatCode.Contains("mm:ss"))
            {
                return "Time";
            }
            if (formatCode.Contains("[$-409]") || formatCode.Contains("[$-F800]") || formatCode.Contains("m/d") || formatCode.Contains("d-m") || formatCode.Contains("m-y") || formatCode.Contains("m y"))
            {
                return "Date";
            }
            if (formatCode.Contains("#,##0.0"))
            {
                return "Currency";
            }
            if (formatCode.Last() == '%')
            {
                return "Percentage";
            }
            if (formatCode.IndexOf("0") == 0)
            {
                return "Numeric";
            }
            return "String";
        }

        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        private static DataSet RealExcelFileOpenXML(string filePath)
        {
            var ds = new DataSet();
            var dt = new DataTable("Sheet1");

            //Open the Excel file in Read Mode using OpenXml.
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
            {
                //Read the first Sheet from Excel file.
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                //Get the Worksheet instance.
                var worksheetPart = doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart;
                if (worksheetPart != null)
                {
                    Worksheet worksheet = worksheetPart.Worksheet;
                    //Fetch all the rows present in the Worksheet.

                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                    //Loop through the Worksheet rows.
                    var currentRowIndex = 0;
                    foreach (var row in rows)
                    {
                        currentRowIndex++;
                        //Use the first row to add columns to DataTable.
                        if (row.RowIndex.Value == 1)
                        {
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dt.Columns.Add(GetValue(doc, cell));
                            }
                        }
                        else
                        {
                            if (currentRowIndex < row.RowIndex.Value)
                            {
                                for (int i = currentRowIndex; i < row.RowIndex.Value; i++)
                                {
                                    dt.Rows.Add();
                                }
                                currentRowIndex = Convert.ToInt32(row.RowIndex.Value);
                            }
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            var columnIndex = 0;
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                // Gets the column index of the cell with data
                                var columnIndexFromName = GetColumnIndexFromName(GetColumnName(cell.CellReference));
                                {
                                    var cellColumnIndex = columnIndexFromName;

                                    while (columnIndex < cellColumnIndex)
                                    {
                                        dt.Rows[dt.Rows.Count - 1][columnIndex] = string.Empty;
                                        columnIndex++;
                                    }
                                }

                                var numberingFormats = doc.WorkbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats;
                                dt.Rows[dt.Rows.Count - 1][columnIndex] = GetValue(doc, cell, numberingFormats);
                                columnIndex++;
                            }
                        }
                    }
                }
            }
            ds.Tables.Add(dt);
            return ds;
        }

        private static string GetValue(SpreadsheetDocument spreadsheetdocument, Cell cell, NumberingFormats formats = null)
        {
            // Get value in Cell
            var sharedString = spreadsheetdocument.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return string.Empty;
            }

            var cellValue = cell.CellValue.InnerText;

            // The condition that the Cell DataType is SharedString
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return sharedString.SharedStringTable.ChildElements[int.Parse(cellValue)].InnerText;
            }
            if (cell.StyleIndex != null)
            {
                CellFormat cf = spreadsheetdocument.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[int.Parse(cell.StyleIndex.InnerText)] as CellFormat;
                if ((cf.NumberFormatId >= 14 && cf.NumberFormatId <= 22) ||
                    (cf.NumberFormatId >= 165 && cf.NumberFormatId <= 180) ||
                    cf.NumberFormatId == 278 || cf.NumberFormatId == 185 || cf.NumberFormatId == 196 ||
                    cf.NumberFormatId == 217 || cf.NumberFormatId == 326 || cf.NumberFormatId == 323)
                {
                    return DateTime.FromOADate(Convert.ToDouble(cell.CellValue.Text)).ToShortDateString();
                }
                if (formats != null)
                {
                    var numberingFormat = formats.Cast<NumberingFormat>()
                        .SingleOrDefault(f => f.NumberFormatId.Value == cf.NumberFormatId);
                    if (numberingFormat != null && numberingFormat.FormatCode.HasValue && IsDateTimeType(numberingFormat.FormatCode.Value))
                    {
                        return DateTime.FromOADate(Convert.ToDouble(cell.CellValue.Text)).ToShortDateString();
                    }
                }
            }
            return cellValue;
        }

        private static List<char> Letters = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };

        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }

        /// <summary>
        /// Given just the column name (no row index), it will return the zero based column index.
        /// Note: This method will only handle columns with a length of up to two (ie. A to Z and AA to ZZ).
        /// A length of three can be implemented when needed.
        /// </summary>
        /// <param name="columnName">Column Name (ie. A or AB)</param>
        /// <returns>Zero based index if the conversion was successful; otherwise null</returns>
        public static int GetColumnIndexFromName(string columnName)
        {
            int number = 0;
            int pow = 1;
            for (int i = columnName.Length - 1; i >= 0; i--)
            {
                number += (columnName[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return number - 1;
        }
    }
}