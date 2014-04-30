using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BattaTech.ExcelExport.Helper;
using BattaTech.ExcelExport.Manager;
using BattaTech.ExcelExport.Entities.GridViewSource;

namespace BattaTech.ExcelExport.PL
{
    public partial class example_gridview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvUsers.DataSource = Data.Users;
            gvUsers.DataBind();

            gvGames.DataSource = Data.Games;
            gvGames.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExcelSheet excel = new ExcelSheet
            {
                fileName = "example_gridview",
                grids = new List<Grid>
                {
                    new Grid()
                    { 
                        tableName = "User Data", 
                        gridView = gvUsers,
                        headerForeColor =  "#333",
                        headerBackgroundColor = "#777",
                        borderColor = "#000000",
                        borderWidthInPixel = 1,
                        footerForeColor = "#777",
                        footerBackgroundColor = "#333",
                        columnsConfiguration = new List<ColumnModel>
                        {
                            new ColumnModel() { headerText = "User ID", isHidden = false },
                            new ColumnModel() { headerText = "User Name", isHidden = false },
                            new ColumnModel() { headerText = "Age", isHidden = true },
                            new ColumnModel() { headerText = "Date Of Birth", isHidden = false },
                            new ColumnModel() { headerText = "Salary ($)", isHidden = false }
                        }
                    }, 
                    new Grid()
                    { 
                        tableName = "Game Data", 
                        gridView = gvGames, 
                        headerForeColor =  "#FFFFFF",
                        headerBackgroundColor = "#000000",
                        borderColor = "#000000",
                        borderWidthInPixel = 1,
                        footerForeColor = "#FFFFFF",
                        footerBackgroundColor = "#000000",
                        columnsConfiguration = new List<ColumnModel>
                        {
                            new ColumnModel() { headerText = "Game ID", isHidden = false },
                            new ColumnModel() { headerText = "Game Name", isHidden = false },
                            new ColumnModel() { headerText = "Is Popular Game?", isHidden = true }
                        }
                    }
                }
            };

            if (excel != null)
            {
                GridViewExport.Export(excel);
            }
        }
    }
}