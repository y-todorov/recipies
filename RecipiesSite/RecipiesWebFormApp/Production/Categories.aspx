<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="RecipiesWebFormApp.Production.Category" %><%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="OpenAccessLinqDataSource1">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="CategoryID" DataSourceID="OpenAccessLinqDataSource1">
        <Columns>
            <telerik:GridBoundColumn DataField="CategoryID" DataType="System.Int32" FilterControlAltText="Filter CategoryID column" HeaderText="CategoryID" ReadOnly="True" SortExpression="CategoryID" UniqueName="CategoryID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CategoryName" FilterControlAltText="Filter CategoryName column" HeaderText="CategoryName" SortExpression="CategoryName" UniqueName="CategoryName">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter Description column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Categories" />
</asp:Content>
