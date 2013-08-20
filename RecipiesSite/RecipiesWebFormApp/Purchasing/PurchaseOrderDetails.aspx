﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderDetails.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrderDetails" %>

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
                <telerik:GridBoundColumn DataField="ProductId" DataType="System.Int32" FilterControlAltText="Filter ProductId column" HeaderText="ProductId" SortExpression="ProductId" UniqueName="ProductId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DueDate" DataType="System.DateTime" FilterControlAltText="Filter DueDate column" HeaderText="DueDate" SortExpression="DueDate" UniqueName="DueDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OrderQuantity" DataType="System.Int32" FilterControlAltText="Filter OrderQuantity column" HeaderText="OrderQuantity" SortExpression="OrderQuantity" UniqueName="OrderQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LineTotal" DataType="System.Decimal" FilterControlAltText="Filter LineTotal column" HeaderText="LineTotal" SortExpression="LineTotal" UniqueName="LineTotal">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReceivedQuantity" DataType="System.Int32" FilterControlAltText="Filter ReceivedQuantity column" HeaderText="ReceivedQuantity" SortExpression="ReceivedQuantity" UniqueName="ReceivedQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RejectedQuantity" DataType="System.Int32" FilterControlAltText="Filter RejectedQuantity column" HeaderText="RejectedQuantity" SortExpression="RejectedQuantity" UniqueName="RejectedQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StockedQuantity" DataType="System.Int32" FilterControlAltText="Filter StockedQuantity column" HeaderText="StockedQuantity" SortExpression="StockedQuantity" UniqueName="StockedQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
