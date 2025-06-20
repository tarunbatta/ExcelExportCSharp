using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Data;

namespace BattaTech.ExcelExport.Helper
{
    public class Utility
    {
        /// <summary>
        /// Cross-platform Excel export method using EPPlus.
        /// </summary>
        /// <param name="fileName">The name of the Excel file to generate</param>
        /// <param name="dataTable">The DataTable containing the data to export</param>
        /// <returns>MemoryStream containing the Excel file</returns>
        public static MemoryStream GenerateExcel(string fileName, DataTable dataTable)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            // Add headers
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            }

            // Add data
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                }
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Legacy method signature for backward compatibility.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="table"></param>
        public static void GenerateExcel(string fileName, string table)
        {
            // TODO: Implement cross-platform Excel file generation and download logic.
            throw new NotImplementedException("Excel export is not implemented for .NET Core. Implement file generation and return as a stream or file result in your web framework.");
        }
    }
}
