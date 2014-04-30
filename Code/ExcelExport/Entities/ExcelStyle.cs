using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattaTech.ExcelExport.Entities
{
    public class ExcelStyle
    {
        public Type dataType { get; set; }

        public string dataFormat { get; set; }

        public int dataFormatStyleId { get; set; }
    }
}
