using BattaTech.ExcelExport.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BattaTech.ExcelExport.PL
{
    public partial class index : System.Web.UI.Page
    {
        public DataTable Users
        {
            get
            {
                DataTable table = new DataTable();

                table.Columns.Add("userId", typeof(int));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("age", typeof(int));
                table.Columns.Add("dob", typeof(DateTime));
                table.Columns.Add("salary", typeof(decimal));

                table.Rows.Add("1", "Tarun Batta", 30, DateTime.Now, 10.00);
                table.Rows.Add("2", "Mickey Mouse", 10, DateTime.Now.AddMonths(-1).AddYears(-1), 11.52);

                return table;
            }
        }

        public DataTable Games
        {
            get
            {
                DataTable table = new DataTable();

                table.Columns.Add("gameId", typeof(int));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("isPopular", typeof(bool));

                table.Rows.Add("1", "LOL", true);
                table.Rows.Add("2", "Titan Fall", true);
                table.Rows.Add("3", "Mario Racing", false);

                return table;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExcelSheet excel = new ExcelSheet
            {
                fileName = "test_download",
                grids = new List<Grid>
                {
                    new Grid()
                    { 
                        tableName = "User Data", 
                        dataTable = this.Users, 
                        headerForeColor =  "#333",
                        headerBackgroundColor = "#777",
                        columnsConfiguration = new List<ColumnModel>
                        {
                            new ColumnModel() { headerText = "User ID", columnName = "userId", style = new ExcelStyle() { dataType = typeof(int), dataFormat = string.Empty }, columnWidth= 50, isHidden = false },
                            new ColumnModel() { headerText = "User Name", columnName = "name", style = new ExcelStyle() { dataType = typeof(string), dataFormat = string.Empty }, columnWidth= 100, isHidden = false },
                            new ColumnModel() { headerText = "Age", columnName = "age", style = new ExcelStyle() { dataType = typeof(int), dataFormat = string.Empty }, columnWidth= 50, isHidden = false },
                            new ColumnModel() { headerText = "Date Of Birth", columnName = "dob", style = new ExcelStyle() { dataType = typeof(DateTime), dataFormat = string.Empty }, columnWidth= 150, isHidden = false },
                            new ColumnModel() { headerText = "Salary ($)", columnName = "salary", style = new ExcelStyle() { dataType = typeof(float), dataFormat = string.Empty }, columnWidth= 50, isHidden = false }
                        }
                    }, 
                    new Grid()
                    { 
                        tableName = "Game Data", 
                        dataTable = this.Games, 
                        headerForeColor =  "#FFFFFF",
                        headerBackgroundColor = "#000000",
                        columnsConfiguration = new List<ColumnModel>
                        {
                            new ColumnModel() { headerText = "Game ID", columnName = "gameId", style = new ExcelStyle() { dataType = typeof(int), dataFormat = string.Empty }, columnWidth= 50, isHidden = false },
                            new ColumnModel() { headerText = "Game Name", columnName = "name", style = new ExcelStyle() { dataType = typeof(string), dataFormat = string.Empty }, columnWidth= 100, isHidden = false },
                            new ColumnModel() { headerText = "Is Popular Game?", columnName = "isPopular", style = new ExcelStyle() { dataType = typeof(bool), dataFormat = string.Empty }, columnWidth= 150, isHidden = true }
                        }
                    }
                },
                author = "Tarun Batta",
                company = "Batta Tech Private Limited",
                version = "1.0.0"
            };

            if (excel != null)
            {
                GridExport.Export(excel);
            }
        }
    }
}