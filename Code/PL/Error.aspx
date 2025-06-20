<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BattaTech.ExcelExport.PL.Error" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error - Excel Export</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }
        .error-container {
            max-width: 600px;
            margin: 50px auto;
            background: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            text-align: center;
        }
        .error-code {
            font-size: 72px;
            color: #e74c3c;
            margin: 0;
            font-weight: bold;
        }
        .error-message {
            font-size: 24px;
            color: #2c3e50;
            margin: 20px 0;
        }
        .error-description {
            color: #7f8c8d;
            margin: 20px 0;
            line-height: 1.6;
        }
        .back-link {
            display: inline-block;
            margin-top: 20px;
            padding: 10px 20px;
            background-color: #3498db;
            color: white;
            text-decoration: none;
            border-radius: 4px;
            transition: background-color 0.3s;
        }
        .back-link:hover {
            background-color: #2980b9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="error-container">
            <h1 class="error-code" id="errorCode" runat="server">500</h1>
            <h2 class="error-message" id="errorMessage" runat="server">Internal Server Error</h2>
            <p class="error-description" id="errorDescription" runat="server">
                An unexpected error occurred while processing your request. 
                Please try again later or contact support if the problem persists.
            </p>
            <a href="~/index.aspx" class="back-link" runat="server">Return to Home</a>
        </div>
    </form>
</body>
</html> 