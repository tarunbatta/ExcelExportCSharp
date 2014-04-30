<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BattaTech.ExcelExport.PL.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Button ID="btnDataSet" runat="server" Text="DataSet Example" OnClick="btnDataSet_Click" />
        <br />
        <asp:Button ID="btnGridView" runat="server" Text="GridView Example" OnClick="btnGridView_Click" />
    </div>
</asp:Content>
