using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BattaTech.ExcelExport.Entities.GridViewSource
{
    public class Grid
    {
        public string tableName { get; set; }

        public GridView gridView { get; set; }

        public string headerForeColor { get; set; }

        public string headerBackgroundColor { get; set; }

        public string borderColor { get; set; }

        public int borderWidthInPixel { get; set; }

        public string footerForeColor { get; set; }

        public string footerBackgroundColor { get; set; }

        public List<ColumnModel> columnsConfiguration { get; set; }
    }
}
