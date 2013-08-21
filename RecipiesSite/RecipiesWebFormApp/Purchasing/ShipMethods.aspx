<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShipMethods.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.ShipMethods" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register namespace="Telerik.Web.UI" tagprefix="telerik1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
        <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceShipMethod" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ShipMethods" />
   
</asp:Content>
