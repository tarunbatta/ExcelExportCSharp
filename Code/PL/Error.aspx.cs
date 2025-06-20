using System;
using System.Web;

namespace BattaTech.ExcelExport.PL
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string errorCode = Request.QueryString["code"];

                switch (errorCode)
                {
                    case "404":
                        errorCode.InnerText = "404";
                        errorMessage.InnerText = "Page Not Found";
                        errorDescription.InnerText = "The page you are looking for could not be found. " +
                                                   "Please check the URL and try again.";
                        break;
                    case "403":
                        errorCode.InnerText = "403";
                        errorMessage.InnerText = "Access Forbidden";
                        errorDescription.InnerText = "You don't have permission to access this resource. " +
                                                   "Please contact your administrator if you believe this is an error.";
                        break;
                    case "500":
                    default:
                        errorCode.InnerText = "500";
                        errorMessage.InnerText = "Internal Server Error";
                        errorDescription.InnerText = "An unexpected error occurred while processing your request. " +
                                                   "Please try again later or contact support if the problem persists.";
                        break;
                }
            }
        }
    }
}