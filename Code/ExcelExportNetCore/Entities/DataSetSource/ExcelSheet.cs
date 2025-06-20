using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattaTech.ExcelExport.Entities.DataSetSource
{
    public class ExcelSheet
    {
        public int defaultStyleId
        {
            get
            {
                return 70;
            }
        }

        public string? fileName { get; set; }

        public List<Grid>? grids { get; set; }

        public string? author { get; set; }

        public string? company { get; set; }

        public string? version { get; set; }
    }
}
