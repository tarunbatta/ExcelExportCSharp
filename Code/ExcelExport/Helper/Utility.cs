using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BattaTech.ExcelExport.Helper
{
    public class Utility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="table"></param>
        public static void GenerateExcel(string fileName, string table)
        {
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(table))
            {
                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
                HttpContext.Current.Response.ContentType = "application/ms-excel";

                HttpContext.Current.Response.Write(table);
                HttpContext.Current.Response.End();
            }
        }
    }
}
