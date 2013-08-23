<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderDetails.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrderDetails" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrderDetails" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderDetails">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrder" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
    <telerik:RadGrid ID="rgPurchaseOrderDetails" runat="server" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderDetails">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderDetailId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderDetails">
            <Columns>
                <telerik:GridBoundColumn DataField="PurchaseOrderDetailId" DataType="System.Int32" FilterControlAltText="Filter PurchaseOrderDetailId column" HeaderText="PurchaseOrderDetailId" ReadOnly="True" SortExpression="PurchaseOrderDetailId" UniqueName="PurchaseOrderDetailId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                  <telerik:GridDropDownColumn UniqueName="DropDownPurchaseOrderListColumn" ListTextField="PurchaseOrderId" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="PurchaseOrderId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrder" HeaderText="PurchaseOrder"
                    DataField="PurchaseOrderId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>             
                  <telerik:GridDropDownColumn UniqueName="DropDownProductListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="ProductId" DataSourceID="OpenAccessLinqDataSourceProduct" HeaderText="Product"
                    DataField="ProductId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>             
           
                <telerik:GridNumericColumn DataField="OrderQuantity" DataType="System.Int32" FilterControlAltText="Filter OrderQuantity column" HeaderText="OrderQuantity" SortExpression="OrderQuantity" UniqueName="OrderQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="LineTotal" ReadOnly="true"  DataType="System.Decimal" FilterControlAltText="Filter LineTotal column" HeaderText="LineTotal" SortExpression="LineTotal" UniqueName="LineTotal">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="ReceivedQuantity" DataType="System.Int32" FilterControlAltText="Filter ReceivedQuantity column" HeaderText="ReceivedQuantity" SortExpression="ReceivedQuantity" UniqueName="ReceivedQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="ReturnedQuantity" DataType="System.Int32" FilterControlAltText="Filter ReturnedQuantity column" HeaderText="ReturnedQuantity" SortExpression="ReturnedQuantity" UniqueName="ReturnedQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="StockedQuantity" ReadOnly="true"  DataType="System.Int32" FilterControlAltText="Filter StockedQuantity column" HeaderText="StockedQuantity" SortExpression="StockedQuantity" UniqueName="StockedQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
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
