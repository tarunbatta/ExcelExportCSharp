using Microsoft.AspNetCore.Mvc;
using BattaTech.ExcelExport.Helper;
using BattaTech.ExcelExport.Entities.DataSetSource;
using BattaTech.ExcelExport.Manager;
using System.Data;

namespace ExcelExportWeb.Controllers
{
    public class ExcelController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportDataSet()
        {
            try
            {
                // Create sample data
                var dataTable = Data.Users;

                // Generate Excel file
                var stream = Utility.GenerateExcel("Users", dataTable);

                // Return file for download
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating Excel file: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult ExportGames()
        {
            try
            {
                // Create sample data
                var dataTable = Data.Games;

                // Generate Excel file
                var stream = Utility.GenerateExcel("Games", dataTable);

                // Return file for download
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Games.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating Excel file: {ex.Message}");
            }
        }
    }
}