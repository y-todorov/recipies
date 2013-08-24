<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReport.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrderReport" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.1.13.802, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:ReportViewer ID="ReportViewer1" runat="server"></telerik:ReportViewer>

</asp:Content>
