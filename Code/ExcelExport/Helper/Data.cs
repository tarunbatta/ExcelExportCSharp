using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattaTech.ExcelExport.Helper
{
    public class Data
    {
        public static DataTable Users
        {
            get
            {
                DataTable table = new DataTable();

                table.Columns.Add("userId", typeof(int));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("age", typeof(int));
                table.Columns.Add("dob", typeof(DateTime));
                table.Columns.Add("salary", typeof(decimal));

                table.Rows.Add("1", "Tarun Batta", 30, DateTime.Now, 10.00);
                table.Rows.Add("2", "Mickey Mouse", 10, DateTime.Now.AddMonths(-1).AddYears(-1), 11.52);

                return table;
            }
        }

        public static DataTable Games
        {
            get
            {
                DataTable table = new DataTable();

                table.Columns.Add("gameId", typeof(int));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("isPopular", typeof(bool));

                table.Rows.Add("1", "LOL", true);
                table.Rows.Add("2", "Titan Fall", true);
                table.Rows.Add("3", "Mario Racing", false);

                return table;
            }
        }
    }
}
