using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BattaTech.ExcelExport.Helper;
using BattaTech.ExcelExport.Manager;
using BattaTech.ExcelExport.Entities.DataSetSource;

namespace BattaTech.ExcelExport.PL
{
    public partial class example_dataset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExcelSheet excel = new ExcelSheet
            {
                fileName = "example_dataset",
                grids = new List<Grid>
                {
                    new Grid()
                    { 
                        tableName = "User Data", 
                        dataTable = Data.Users, 
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
                        dataTable = Data.Games, 
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
                DataSetExport.Export(excel);
            }
        }
    }
}