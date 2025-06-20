using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattaTech.ExcelExport.Entities.DataSetSource
{
    public class Grid
    {
        public string? tableName { get; set; }

        public DataTable? dataTable { get; set; }

        public string? headerForeColor { get; set; }

        public string? headerBackgroundColor { get; set; }

        public int headerStyleId { get; set; }

        public int defaultStyleId { get; set; }

        public List<ColumnModel>? columnsConfiguration { get; set; }
    }
}
