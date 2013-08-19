<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="RecipiesWebFormApp.Sales.OrderDetails" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="OpenAccessLinqDataSourceOrderDetail" CellSpacing="0" GridLines="None">

        <HeaderStyle />
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderDetailID" DataSourceID="OpenAccessLinqDataSourceOrderDetail">
            <Columns>
                <telerik:GridBoundColumn DataField="OrderDetailID" DataType="System.Int32" FilterControlAltText="Filter OrderDetailID column" HeaderText="OrderDetailID" ReadOnly="True" SortExpression="OrderDetailID" UniqueName="OrderDetailID">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownOrderListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="OrderID" DataSourceID="OpenAccessLinqDataSourceOrder" HeaderText="Order"
                    DataField="OrderID" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <RequiredFieldValidator ForeColor="Red" ErrorMessage="This field is required!"></RequiredFieldValidator>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownProductListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="ProductID" DataSourceID="OpenAccessLinqDataSourceProduct" HeaderText="Product"
                    DataField="ProductID" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <RequiredFieldValidator ForeColor="Red" ErrorMessage="This field is required!"></RequiredFieldValidator>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridNumericColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Quantity" DataType="System.Int32" FilterControlAltText="Filter Quantity column" HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Discount" DataType="System.Double" FilterControlAltText="Filter Discount column" HeaderText="Discount" SortExpression="Discount" UniqueName="Discount">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrderDetail" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="OrderDetails">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrder" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Orders" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
</asp:Content>
