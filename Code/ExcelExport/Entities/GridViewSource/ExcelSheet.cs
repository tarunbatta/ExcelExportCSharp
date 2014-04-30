using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattaTech.ExcelExport.Entities.GridViewSource
{
    public class ExcelSheet
    {
        public string fileName { get; set; }

        public List<Grid> grids { get; set; }
    }
}
