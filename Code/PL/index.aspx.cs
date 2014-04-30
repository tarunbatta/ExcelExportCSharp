using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BattaTech.ExcelExport.PL
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDataSet_Click(object sender, EventArgs e)
        {
            Response.Redirect("example_dataset.aspx");
        }

        protected void btnGridView_Click(object sender, EventArgs e)
        {
            Response.Redirect("example_gridview.aspx");
        }
    }
}