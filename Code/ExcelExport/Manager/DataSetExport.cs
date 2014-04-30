using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BattaTech.ExcelExport.Entities.DataSetSource;
using BattaTech.ExcelExport.Helper;

namespace BattaTech.ExcelExport.Manager
{
    public class DataSetExport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excel"></param>
        public static void Export(ExcelSheet excel)
        {
            if (excel != null && !string.IsNullOrEmpty(excel.fileName) && excel.grids != null && excel.grids.Count > 0 &&
                !string.IsNullOrEmpty(excel.author) && !string.IsNullOrEmpty(excel.company) && !string.IsNullOrEmpty(excel.version))
            {
                StringBuilder result = new StringBuilder();

                #region Info Section

                result.Append("<?xml version=\"1.0\"?>");
                result.Append("<?mso-application progid=\"Excel.Sheet\"?>");
                result.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                result.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                result.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                result.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                result.Append(" xmlns:html=\"http://www.w3.org/TR/REC-html40/\">");
                result.Append(" <DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
                result.Append(string.Concat("  <Author>", excel.author, "</Author>"));
                result.Append(string.Format("  <Created>{0}T{1}Z</Created>", DateTime.Now.ToString("yyyy-mm-dd"), DateTime.Now.ToString("HH:MM:SS")));
                result.Append(string.Concat("  <Company>", excel.company, "</Company>"));
                result.Append(string.Concat("  <Version>", excel.version, "</Version>"));
                result.Append(" </DocumentProperties>");
                result.Append("<OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">");
                result.Append("<AllowPNG/>");
                result.Append("</OfficeDocumentSettings>");
                result.Append(" <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                result.Append("  <WindowHeight>8955</WindowHeight>");
                result.Append("  <WindowWidth>11355</WindowWidth>");
                result.Append("  <WindowTopX>480</WindowTopX>");
                result.Append("  <WindowTopY>15</WindowTopY>");
                result.Append("  <ProtectStructure>False</ProtectStructure>");
                result.Append("  <ProtectWindows>False</ProtectWindows>");
                result.Append(" </ExcelWorkbook>");
                result.Append(" <Styles>");
                result.Append("  <Style ss:ID=\"Default\" ss:Name=\"Normal\">");
                result.Append("   <Alignment ss:Vertical=\"Bottom\"/>");
                result.Append("   <Borders/>");
                result.Append("   <Font ss:FontName=\"Arial\"/>");
                result.Append("   <Interior/>");
                result.Append("   <Protection/>");
                result.Append("  </Style>");

                #endregion Info Section

                #region Date & Format Styling Section

                int styleId = excel.defaultStyleId;
                List<ExcelStyle> alreadyAdded = new List<ExcelStyle>();

                result.Append("<Style ss:ID=\"s" + styleId.ToString() + "\">");
                result.Append("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                result.Append("</Style>");
                styleId++;

                foreach (Grid grid in excel.grids)
                {
                    result.Append(string.Concat("<Style ss:ID=\"s", styleId, "\">"));
                    result.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                    result.Append(string.Concat("<Font ss:FontName=\"Arial\" ss:Color=\"", grid.headerForeColor, "\" ss:Bold=\"1\"/>"));
                    result.Append(string.Concat("<Interior ss:Color=\"", grid.headerBackgroundColor, "\" ss:Pattern=\"Solid\"/>"));
                    result.Append("</Style>");

                    grid.headerStyleId = styleId;
                    styleId++;

                    if (grid.dataTable != null && grid.dataTable.Columns != null && grid.dataTable.Columns.Count > 0)
                    {
                        foreach (DataColumn column in grid.dataTable.Columns)
                        {
                            if (grid.columnsConfiguration != null && grid.columnsConfiguration.Count > 0)
                            {
                                ColumnModel columnConfig = grid.columnsConfiguration.Find(x => x.columnName == column.ColumnName);

                                if (columnConfig != null && !columnConfig.isHidden)
                                {
                                    var alreadyAddedStyle = alreadyAdded.Find(x => x.dataType == columnConfig.style.dataType && x.dataFormat == columnConfig.style.dataFormat);

                                    if (alreadyAddedStyle != null)
                                    {
                                        columnConfig.style.dataFormatStyleId = alreadyAddedStyle.dataFormatStyleId;
                                    }
                                    else
                                    {
                                        if (columnConfig.style.dataType == typeof(int))
                                        {
                                            result.Append("<Style ss:ID=\"s" + styleId.ToString() + "\">");
                                            result.Append("<NumberFormat ss:Format=\"0\"/>");
                                            result.Append("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                                            result.Append("</Style>");

                                            columnConfig.style.dataFormatStyleId = styleId;
                                            alreadyAdded.Add(columnConfig.style);

                                            styleId++;
                                        }
                                        else if (columnConfig.style.dataType == typeof(float) || columnConfig.style.dataType == typeof(double))
                                        {
                                            result.Append("<Style ss:ID=\"s" + styleId.ToString() + "\">");
                                            result.Append("<NumberFormat ss:Format=\"Fixed\"/>");
                                            result.Append("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                                            result.Append("</Style>");

                                            columnConfig.style.dataFormatStyleId = styleId;
                                            alreadyAdded.Add(columnConfig.style);

                                            styleId++;
                                        }
                                        else if (columnConfig.style.dataType == typeof(DateTime))
                                        {
                                            if (string.IsNullOrEmpty(columnConfig.style.dataFormat))
                                            {
                                                columnConfig.style.dataFormat = "dd-MMM-yyyy";
                                            }

                                            result.Append("<Style ss:ID=\"s" + styleId.ToString() + "\">");
                                            result.Append("<NumberFormat ss:Format=\"[ENG][$-409]" + GetExcelCompliantDataFormat(columnConfig.style.dataFormat) + ";@\"/>");
                                            result.Append("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                                            result.Append("</Style>");

                                            columnConfig.style.dataFormatStyleId = styleId;
                                            alreadyAdded.Add(columnConfig.style);

                                            styleId++;
                                        }
                                        else
                                        {
                                            columnConfig.style.dataFormatStyleId = excel.defaultStyleId;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion Date & Format Styling Section

                result.Append(string.Concat("  <Style ss:ID=\"s", styleId, "\">"));
                result.Append("   <Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                result.Append("  </Style>");
                result.Append(" </Styles>");

                styleId++;

                int workSheetNumber = 1;
                bool firstGrid = true;

                foreach (Grid grid in excel.grids)
                {
                    if (grid.dataTable != null && grid.dataTable.Rows != null && grid.dataTable.Rows.Count > 0)
                    {
                        #region Setting Sheet Name

                        if (!string.IsNullOrEmpty(grid.tableName))
                        {
                            result.Append(string.Concat(" <Worksheet ss:Name=\"", grid.tableName, "\">"));
                        }
                        else if (!string.IsNullOrEmpty(grid.dataTable.TableName))
                        {
                            result.Append(string.Concat(" <Worksheet ss:Name=\"", grid.dataTable.TableName, "\">"));
                        }
                        else
                        {
                            result.Append(string.Concat(" <Worksheet ss:Name=\"Sheet-", workSheetNumber.ToString(), "\">"));
                        }

                        #endregion Setting Sheet Name

                        result.Append(string.Format("  <Table ss:ExpandedColumnCount=\"{0}\" ss:ExpandedRowCount=\"{1}\" x:FullColumns=\"1\"", grid.dataTable.Columns.Count, (grid.dataTable.Rows.Count + 1)));
                        result.Append("     x:FullRows=\"1\">");

                        #region Setting Column Width

                        foreach (ColumnModel columnConfig in grid.columnsConfiguration)
                        {
                            if (!string.IsNullOrEmpty(columnConfig.columnName) && !columnConfig.isHidden)
                            {
                                if (grid.dataTable.Columns[columnConfig.columnName] != null)
                                {
                                    result.Append(string.Concat("<Column ss:AutoFitWidth=\"0\" ss:Width=\"", columnConfig.columnWidth, "\" />"));
                                }
                            }
                        }

                        #endregion Setting Column Width

                        #region Creating Header Row

                        result.Append("<Row>");

                        foreach (ColumnModel columnConfig in grid.columnsConfiguration)
                        {
                            if (!string.IsNullOrEmpty(columnConfig.columnName) && !columnConfig.isHidden)
                            {
                                DataColumn column = grid.dataTable.Columns[columnConfig.columnName];

                                if (column != null)
                                {
                                    result.Append(string.Concat("<Cell ss:StyleID=\"s", grid.headerStyleId, "\"><Data ss:Type=\"String\">", columnConfig.headerText, "</Data></Cell>"));
                                }
                            }
                        }

                        result.Append("</Row>");

                        #endregion Creating Header Row

                        #region Adding Rows

                        foreach (DataRow row in grid.dataTable.Rows)
                        {
                            result.Append("<Row>");

                            foreach (ColumnModel columnConfig in grid.columnsConfiguration)
                            {
                                if (!string.IsNullOrEmpty(columnConfig.columnName) && !columnConfig.isHidden)
                                {
                                    DataColumn column = grid.dataTable.Columns[columnConfig.columnName];

                                    if (column != null)
                                    {
                                        string data = Convert.ToString(row[column]);
                                        int dataFormatStyleId = 0;
                                        string type = string.Empty;

                                        if (columnConfig.style.dataFormatStyleId > 0)
                                        {
                                            dataFormatStyleId = columnConfig.style.dataFormatStyleId;

                                            if (columnConfig.style.dataType == typeof(int) || columnConfig.style.dataType == typeof(float) || columnConfig.style.dataType == typeof(double))
                                            {
                                                type = "Number";
                                            }
                                            else if (columnConfig.style.dataType == typeof(DateTime))
                                            {
                                                type = "DateTime";
                                                data = GetExcelCompliantDateFormat((DateTime)row[column]);
                                            }
                                            else
                                            {
                                                type = "String";
                                            }
                                        }

                                        result.Append(string.Concat("<Cell ss:StyleID=\"s", dataFormatStyleId, "\">"));
                                        result.Append(string.Concat("<Data ss:Type=\"", type, "\">", data, "</Data>"));
                                        result.Append("</Cell>");
                                    }
                                }
                            }

                            result.Append("</Row>");
                        }

                        #endregion Adding Rows

                        result.Append("  </Table>");
                        result.Append("  <WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");

                        if (firstGrid)
                        {
                            result.Append("<Print>");
                            result.Append("<ValidPrinterInfo/>");
                            result.Append("<HorizontalResolution>600</HorizontalResolution>");
                            result.Append("<VerticalResolution>600</VerticalResolution>");
                            result.Append("</Print>");
                            result.Append("<Selected/>");
                        }

                        result.Append("   <Panes>");
                        result.Append("    <Pane>");
                        result.Append("     <Number>3</Number>");
                        result.Append("     <ActiveRow>1</ActiveRow>");

                        if (firstGrid)
                        {
                            result.Append("     <ActiveCol>1</ActiveCol>");
                        }

                        result.Append("    </Pane>");
                        result.Append("   </Panes>");
                        result.Append("   <ProtectObjects>False</ProtectObjects>");
                        result.Append("   <ProtectScenarios>False</ProtectScenarios>");
                        result.Append("  </WorksheetOptions>");
                        result.Append(" </Worksheet>");
                    }

                    if (firstGrid)
                    {
                        firstGrid = false;
                    }

                    workSheetNumber++;
                }

                result.Append("</Workbook>");

                // Export to excel
                Utility.GenerateExcel(excel.fileName, result.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetExcelCompliantDataFormat(string input)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(input))
            {
                result = input.Replace("-", "\\-").Replace("/", "\\/").Replace(" ", "\\ ").Replace(",", "\\,");
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetExcelCompliantDateFormat(DateTime dateTime)
        {
            string result = string.Empty;

            string date = dateTime.ToString("yyyy-MM-dd");
            string time = dateTime.ToString("HH:mm:ss");
            result = date + "T" + time + ".000";

            return result;
        }
    }
}
