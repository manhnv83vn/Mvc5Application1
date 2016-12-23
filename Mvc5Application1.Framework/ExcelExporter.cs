using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using Color = System.Drawing.Color;

namespace Mvc5Application1.Framework
{
    public class ExcelExporter
    {
        private readonly SLDocument _excelDocument;

        /// <summary>
        /// Select an existing worksheet. If the given name doesn't match an existing worksheet, the previously selected worksheet is used.
        ///
        /// </summary>
        /// <param name="worksheetName">The name of an existing worksheet.</param>
        /// <returns>
        /// True if there's an existing worksheet with that name and that worksheet is successfully selected. False otherwise.
        /// </returns>
        public bool SelectWorksheet(string worksheetName)
        {
            return _excelDocument.SelectWorksheet(worksheetName);
        }

        public void InsertRow(int startRowIndex, int numberOfRows)
        {
            _excelDocument.InsertRow(startRowIndex, numberOfRows);
        }

        public void SetCellValue(int rowIndex, int colIndex, long data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(int rowIndex, int colIndex, int data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(int rowIndex, int colIndex, decimal data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(int rowIndex, int colIndex, double data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(int rowIndex, int colIndex, float data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetBackGroundColorToCell(int rowIndex, int colIndex, Color foregrColor, Color bgColor)
        {
            var style = _excelDocument.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, foregrColor, bgColor);
            _excelDocument.SetCellStyle(rowIndex, colIndex, style);
        }

        public void SetCellValue(int rowIndex, int colIndex, string data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(int rowIndex, int colIndex, DateTime data)
        {
            if (!_excelDocument.SetCellValue(rowIndex, colIndex, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, long data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, int data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, decimal data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, double data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, float data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, string data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCellValue(string cell, DateTime data)
        {
            if (!_excelDocument.SetCellValue(cell, data))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void CopyCellStyle(int fromRowIndex, int fromColumnIndex, int toRowIndex, int toColumnIndex)
        {
            var style = _excelDocument.GetCellStyle(fromRowIndex, fromColumnIndex);
            _excelDocument.SetCellStyle(toRowIndex, toColumnIndex, style);
        }

        public void SetCellValues<T>(int rowIndex, int columnIndex, IEnumerable<T> items,
            Action<int, int> callback, params Func<T, object>[] valueAccessors)
        {
            if (items == null) return;
            var currentRow = rowIndex;
            foreach (var item in items)
            {
                var currentColumn = columnIndex;
                foreach (var valueAccessor in valueAccessors)
                {
                    var value = valueAccessor(item);
                    if (value != null)
                    {
                        _excelDocument.SetCellValue(currentRow, currentColumn, value.ToString());
                    }
                    if (callback != null)
                    {
                        callback(currentRow, currentColumn);
                    }

                    currentColumn++;
                }
                currentRow++;
            }
        }

        public void AddPageBreak(int rowIndex, int colIndex)
        {
            _excelDocument.InsertPageBreak(rowIndex, colIndex);
        }

        public void SetCellValues<T>(int rowIndex, int columnIndex, IEnumerable<T> items, params Func<T, object>[] valueAccessors)
        {
            SetCellValues(rowIndex, columnIndex, items, null, valueAccessors);
        }

        public void SaveAs(string fileName)
        {
            _excelDocument.SaveAs(fileName);
        }

        public void SaveAs(Stream outputStream)
        {
            _excelDocument.SaveAs(outputStream);
        }

        public ExcelExporter(string templateFile)
        {
            _excelDocument = new SLDocument(templateFile);
        }

        public void SetPrintArea(string startCell, string endCell)
        {
            _excelDocument.SetPrintArea(startCell, endCell);
        }

        public void SetComment(int rowIndex, int columnIndex, string comment)
        {
            var comm = _excelDocument.CreateComment();
            comm.SetText(comment);
            _excelDocument.InsertComment(rowIndex, columnIndex, comm);
            if (!_excelDocument.InsertComment(rowIndex, columnIndex, comm))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetCommentAndHightlightCell(int rowIndex, int columnIndex, string comment)
        {
            var comm = _excelDocument.CreateComment();
            comm.Height = 100;
            comm.Width = 200;
            comm.SetText(comment);
            var style = _excelDocument.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, Color.Orange, Color.DarkSalmon);

            if (!_excelDocument.SetCellStyle(rowIndex, columnIndex, style) || !_excelDocument.InsertComment(rowIndex, columnIndex, comm))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetFormatCellHorizontalAlignRight(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.SetHorizontalAlignment(HorizontalAlignmentValues.Right);
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetFormatCellHorizontalAlignCenter(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetBoldAndUnderLineCell(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.SetFontBold(true);
            style.SetFontUnderline(UnderlineValues.Single);
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetItalicAndCenterCell(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            style.SetFontItalic(true);
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetBorderBottomCell(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetFormatCellHorizontalAlignLeft(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
            style.SetFontBold(false);
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetBoldAndItalicAndCenterCell(int rowIndex, int columnIndex)
        {
            var style = _excelDocument.CreateStyle();
            style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            style.SetFontItalic(true);
            style.SetFontBold(true);
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void ConvertCellValueToNumber(int rowIndex, int columnIndex)
        {
            var value = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
            if (value != "")
                _excelDocument.SetCellValueNumeric(rowIndex, columnIndex, value);
        }

        //public void SetBackGroundColorToColumn(int colIndex, System.Drawing.Color foregrColor, System.Drawing.Color bgColor)
        //{
        //    var style = _excelDocument.CreateStyle();
        //    style.Fill.SetPattern(PatternValues.Solid, foregrColor, bgColor);
        //    _excelDocument.SetColumnStyle(colIndex, style);
        //}
        //public void SetBackGroundColorToRow(int rowIndex, System.Drawing.Color foregrColor, System.Drawing.Color bgColor)
        //{
        //    var style = _excelDocument.CreateStyle();
        //    style.Fill.SetPattern(PatternValues.Solid, foregrColor, bgColor);
        //    _excelDocument.SetRowStyle(rowIndex, style);
        //}

        public void SetCellBackGroundColor(int rowIndex, int colIndex, Color foreColor, Color bgColor)
        {
            var style = _excelDocument.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, foreColor, bgColor);
            _excelDocument.SetCellStyle(rowIndex, colIndex, style);
        }

        public void SetRowBackGroundColor(int rowIndex, int startCol, int endCol, Color foreColor, Color bgColor)
        {
            for (int colIndex = startCol; colIndex <= endCol; colIndex++)
            {
                SetCellBackGroundColor(rowIndex, colIndex, foreColor, bgColor);
            }
        }

        public void FormatExcel(int startRow, int startCol, int endCol, int numberOfItem)
        {
            var endRow = startRow + numberOfItem - 1;
            //Increase the excel rows
            if (numberOfItem <= 1) return;
            //Increase the excel rows
            InsertRow(startRow + 1, numberOfItem - 1);
            //Copy the style from the first row to the new rows
            for (var rowIndex = startRow; rowIndex < endRow; rowIndex++)
            {
                for (var columnIndex = startCol; columnIndex <= endCol; columnIndex++)
                {
                    CopyCellStyle(rowIndex, columnIndex, rowIndex + 1, columnIndex);
                }
            }
        }
    }
}