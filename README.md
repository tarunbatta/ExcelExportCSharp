# Server Side Excel Export using C&#35;

## Introduction

This library is written in C#, and enables the developer to provide server side excel export feature with ease.

## Features

1. Easy to use.
1. DataSet library features,
    1. Export multiple DataSet(s).
    1. Specify excel file name.
    1. Specify author name in excel sheet.
    1. Specify company name in excel sheet.
    1. Specify version of excel sheet.
    1. Specify header Fore Color and Background Color.
    1. Specify a different header text or any column.
    1. Hide any column.
    1. Custom width for a column.
    1. Specify Data type and format for a column.
1. GridView library features,
    1. Export multiple GridView(s).
    1. Specify excel file name.
    1. Specify custom table header name.
    1. Specify header Fore Color and Background Color.
    1. Specify Footer Fore Color and Background Color.
    1. Specify Border Color and Width.

## Example

```csharp
ExcelSheet excel = new ExcelSheet
{
    fileName = "example_dataset",
    grids = new List
    {
        new Grid()
        { 
            tableName = "User Data", 
            dataTable = Data.Users, 
            headerForeColor =  "#333",
            headerBackgroundColor = "#777",
            columnsConfiguration = new List
            {
                new ColumnModel() { headerText = "User ID", columnName = "userId", style = new ExcelStyle() { dataType = typeof(int), dataFormat = string.Empty }, columnWidth= 50, isHidden = false },
                new ColumnModel() { headerText = "User Name", columnName = "name", style = new ExcelStyle() { dataType = typeof(string), dataFormat = string.Empty }, columnWidth= 100, isHidden = false },
                new ColumnModel() { headerText = "Age", columnName = "age", style = new ExcelStyle() { dataType = typeof(int), dataFormat = string.Empty }, columnWidth= 50, isHidden = false },
                new ColumnModel() { headerText = "Date Of Birth", columnName = "dob", style = new ExcelStyle() { dataType = typeof(DateTime), dataFormat = string.Empty }, columnWidth= 150, isHidden = false },
                new ColumnModel() { headerText = "Salary ($)", columnName = "salary", style = new ExcelStyle() { dataType = typeof(float), dataFormat = string.Empty }, columnWidth= 50, isHidden = false }
            }
        }, 
        new Grid()
        { 
            tableName = "Game Data", 
            dataTable = Data.Games, 
            headerForeColor =  "#FFFFFF",
            headerBackgroundColor = "#000000",
            columnsConfiguration = new List
            {
                new ColumnModel() { headerText = "Game ID", columnName = "gameId", style = new ExcelStyle() { dataType = typeof(int), dataFormat = string.Empty }, columnWidth= 50, isHidden = false },
                new ColumnModel() { headerText = "Game Name", columnName = "name", style = new ExcelStyle() { dataType = typeof(string), dataFormat = string.Empty }, columnWidth= 100, isHidden = false },
                new ColumnModel() { headerText = "Is Popular Game?", columnName = "isPopular", style = new ExcelStyle() { dataType = typeof(bool), dataFormat = string.Empty }, columnWidth= 150, isHidden = true }
            }
        }
    },
    author = "Tarun Batta",
    company = "Batta Tech Private Limited",
    version = "1.0.0"
};
 
if (excel != null)
{
    DataSetExport.Export(excel);
}
```