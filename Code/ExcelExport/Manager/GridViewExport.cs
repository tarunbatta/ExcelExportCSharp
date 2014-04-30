using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BattaTech.ExcelExport.Entities.GridViewSource;
using BattaTech.ExcelExport.Helper;

namespace BattaTech.ExcelExport.Manager
{
    public class GridViewExport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excel"></param>
        public static void Export(ExcelSheet excel)
        {
            if (excel != null && !string.IsNullOrEmpty(excel.fileName) && excel.grids != null && excel.grids.Count > 0)
            {
                Table table = new Table();

                foreach (Grid grid in excel.grids)
                {
                    Table itemTable = GetTablularData(grid);

                    itemTable.BorderColor = System.Drawing.ColorTranslator.FromHtml(grid.borderColor);
                    itemTable.BorderWidth = Unit.Pixel(grid.borderWidthInPixel);

                    if (itemTable != null && itemTable.Rows != null && itemTable.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(grid.tableName))
                        {
                            TableRow tableNameRow = new TableRow();
                            TableCell tableNameCell = new TableCell();

                            tableNameCell.Controls.Add(new LiteralControl(grid.tableName));
                            tableNameCell.BackColor = System.Drawing.ColorTranslator.FromHtml(grid.headerBackgroundColor);
                            tableNameCell.BorderColor = System.Drawing.ColorTranslator.FromHtml(grid.borderColor);
                            tableNameCell.BorderWidth = Unit.Pixel(grid.borderWidthInPixel);
                            tableNameCell.ForeColor = System.Drawing.ColorTranslator.FromHtml(grid.headerForeColor);

                            tableNameRow.Cells.Add(tableNameCell);
                            table.Rows.Add(tableNameRow);
                        }

                        for (int i = 0; i < itemTable.Rows.Count; i++)
                        {
                            while (itemTable.Rows.Count > 0)
                            {
                                table.Rows.Add(itemTable.Rows[0]);
                            }
                        }

                        TableRow blankTableRow = new TableRow();
                        TableCell blankTableCell = new TableCell();

                        blankTableCell.Controls.Add(new LiteralControl("<br/>"));
                        blankTableRow.Cells.Add(blankTableCell);
                        table.Rows.Add(blankTableRow);
                    }
                }

                Utility.GenerateExcel(excel.fileName, GetStringData(table));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private static Table GetTablularData(Grid grid)
        {
            Table table = new Table();

            if (grid != null && grid.gridView != null && grid.gridView.Rows != null && grid.gridView.Rows.Count > 0)
            {
                if (grid.gridView.AllowPaging == true)
                {
                    grid.gridView.AllowPaging = false;
                    grid.gridView.DataBind();
                }

                int counter = 0;

                //  add the header row to the table
                if (grid.gridView.HeaderRow != null)
                {
                    GridViewExport.PrepareControlForExport(grid.gridView.HeaderRow);
                    GridViewRow gr = grid.gridView.HeaderRow;

                    if (gr != null && gr.Cells != null)
                    {
                        using (TableHeaderRow tr = new TableHeaderRow())
                        {
                            while (gr.Cells.Count > 0)
                            {
                                using (TableCell tc = gr.Cells[0])
                                {
                                    ColumnModel columnConfig = grid.columnsConfiguration[counter];

                                    if (columnConfig != null)
                                    {
                                        if (!columnConfig.isHidden)
                                        {
                                            if (!string.IsNullOrEmpty(columnConfig.headerText))
                                            {
                                                tc.Text = columnConfig.headerText;
                                            }

                                            tc.BorderColor = System.Drawing.ColorTranslator.FromHtml(grid.borderColor);
                                            tc.BorderWidth = Unit.Pixel(grid.borderWidthInPixel);
                                            tc.BackColor = System.Drawing.ColorTranslator.FromHtml(grid.headerBackgroundColor);
                                            tc.ForeColor = System.Drawing.ColorTranslator.FromHtml(grid.headerForeColor);

                                            tr.Cells.Add(tc);
                                        }
                                        else
                                        {
                                            // hack to get rid of the hidden column cell
                                            TableHeaderRow tempRow = new TableHeaderRow();
                                            tempRow.Cells.Add(tc);
                                        }
                                    }
                                }

                                table.Rows.Add(tr);
                                counter++;
                            }
                        }
                    }
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in grid.gridView.Rows)
                {
                    GridViewExport.PrepareControlForExport(row);
                    GridViewRow gr = row;

                    if (gr != null && gr.Cells != null)
                    {
                        using (TableHeaderRow tr = new TableHeaderRow())
                        {
                            counter = 0;

                            while (gr.Cells.Count > 0)
                            {
                                using (TableCell tc = gr.Cells[0])
                                {
                                    ColumnModel columnConfig = grid.columnsConfiguration[counter];

                                    if (columnConfig != null)
                                    {
                                        if (!columnConfig.isHidden)
                                        {
                                            tc.BorderColor = System.Drawing.ColorTranslator.FromHtml(grid.borderColor);
                                            tc.BorderWidth = Unit.Pixel(grid.borderWidthInPixel);

                                            tr.Cells.Add(tc);
                                        }
                                        else
                                        {
                                            // hack to get rid of the hidden column cell
                                            TableHeaderRow tempRow = new TableHeaderRow();
                                            tempRow.Cells.Add(tc);
                                        }
                                    }
                                }

                                table.Rows.Add(tr);
                                counter++;
                            }
                        }
                    }
                }

                //  add the footer row to the table
                if (grid.gridView.FooterRow != null)
                {
                    GridViewExport.PrepareControlForExport(grid.gridView.FooterRow);
                    GridViewRow gr = grid.gridView.FooterRow;

                    if (gr != null && gr.Cells != null)
                    {
                        using (TableHeaderRow tr = new TableHeaderRow())
                        {
                            counter = 0;

                            while (gr.Cells.Count > 0)
                            {
                                using (TableCell tc = gr.Cells[0])
                                {
                                    ColumnModel columnConfig = grid.columnsConfiguration[counter];

                                    if (columnConfig != null)
                                    {
                                        if (!columnConfig.isHidden)
                                        {
                                            tc.BorderColor = System.Drawing.ColorTranslator.FromHtml(grid.borderColor);
                                            tc.BorderWidth = Unit.Pixel(grid.borderWidthInPixel);
                                            tc.BackColor = System.Drawing.ColorTranslator.FromHtml(grid.footerBackgroundColor);
                                            tc.ForeColor = System.Drawing.ColorTranslator.FromHtml(grid.footerForeColor);

                                            tr.Cells.Add(tc);
                                        }
                                        else
                                        {
                                            // hack to get rid of the hidden column cell
                                            TableHeaderRow tempRow = new TableHeaderRow();
                                            tempRow.Cells.Add(tc);
                                        }
                                    }
                                }

                                table.Rows.Add(tr);
                                counter++;
                            }
                        }
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];

                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is Image)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as Image).AlternateText));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HtmlImage)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HtmlImage).Alt));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is HtmlAnchor)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HtmlAnchor).Title));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    GridViewExport.PrepareControlForExport(current);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static string GetStringData(Table table)
        {
            string result = string.Empty;

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        //  render the table into the htmlwriter
                        table.RenderControl(htw);

                        //  render the htmlwriter into the response
                        result = sw.ToString();
                    }
                }
            }

            return result;
        }
    }
}
