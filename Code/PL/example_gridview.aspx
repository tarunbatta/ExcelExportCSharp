<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="example_gridview.aspx.cs" Inherits="BattaTech.ExcelExport.PL.example_gridview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <asp:GridView ID="gvUsers" runat="server">
            </asp:GridView>
            <br />
            <asp:GridView ID="gvGames" runat="server">
            </asp:GridView>
        </div>
        <br />
        <div>
            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
        </div>
    </div>
</asp:Content>
