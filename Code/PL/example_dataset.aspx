<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="example_dataset.aspx.cs" Inherits="BattaTech.ExcelExport.PL.example_dataset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
    </div>
</asp:Content>
