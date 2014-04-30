using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BattaTech.ExcelExport.Entities.DataSetSource
{
    public class Grid
    {
        public string tableName { get; set; }

        public DataTable dataTable { get; set; }

        public string headerForeColor { get; set; }

        public string headerBackgroundColor { get; set; }

        public int headerStyleId { get; set; }

        public List<ColumnModel> columnsConfiguration { get; set; }
    }
}
