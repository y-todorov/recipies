<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderStatuses.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrderStatuses" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrderStatuses" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderStatus" />
<telerik:RadGrid ID="rgPurchaseOrderStatuses" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderStatuses" GridLines="None">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderStatusId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderStatuses">
        <Columns>
            <telerik:GridBoundColumn DataField="PurchaseOrderStatusId" DataType="System.Int32" FilterControlAltText="Filter PurchaseOrderStatusId column" HeaderText="PurchaseOrderStatusId" ReadOnly="True" SortExpression="PurchaseOrderStatusId" UniqueName="PurchaseOrderStatusId">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime"  ReadOnly="true" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedByUser" ReadOnly="true" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

</asp:Content>
