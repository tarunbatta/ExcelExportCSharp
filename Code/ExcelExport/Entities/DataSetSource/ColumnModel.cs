using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattaTech.ExcelExport.Entities.DataSetSource
{
    public class ColumnModel
    {
        public string headerText { get; set; }

        public string columnName { get; set; }

        public ExcelStyle style { get; set; }

        public int columnWidth { get; set; }

        public bool isHidden { get; set; }
    }
}
