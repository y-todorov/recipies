<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrders.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrders" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrders" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders">
    </telerik:OpenAccessLinqDataSource>
<telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="OpenAccessLinqDataSourcePurchaseOrders">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderID" DataSourceID="OpenAccessLinqDataSourcePurchaseOrders">
        <Columns>
            <telerik:GridBoundColumn DataField="PurchaseOrderID" DataType="System.Int32" FilterControlAltText="Filter PurchaseOrderID column" HeaderText="PurchaseOrderID" ReadOnly="True" SortExpression="PurchaseOrderID" UniqueName="PurchaseOrderID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="RevisionNumber" DataType="System.Int32" FilterControlAltText="Filter RevisionNumber column" HeaderText="RevisionNumber" SortExpression="RevisionNumber" UniqueName="RevisionNumber">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Status" DataType="System.Int32" FilterControlAltText="Filter Status column" HeaderText="Status" SortExpression="Status" UniqueName="Status">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Int32" FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" SortExpression="EmployeeID" UniqueName="EmployeeID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="VendorID" DataType="System.Int32" FilterControlAltText="Filter VendorID column" HeaderText="VendorID" SortExpression="VendorID" UniqueName="VendorID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipMethodID" DataType="System.Int32" FilterControlAltText="Filter ShipMethodID column" HeaderText="ShipMethodID" SortExpression="ShipMethodID" UniqueName="ShipMethodID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="OrderDate" DataType="System.DateTime" FilterControlAltText="Filter OrderDate column" HeaderText="OrderDate" SortExpression="OrderDate" UniqueName="OrderDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipDate" DataType="System.DateTime" FilterControlAltText="Filter ShipDate column" HeaderText="ShipDate" SortExpression="ShipDate" UniqueName="ShipDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="SubTotal" DataType="System.Decimal" FilterControlAltText="Filter SubTotal column" HeaderText="SubTotal" SortExpression="SubTotal" UniqueName="SubTotal">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TaxAmt" DataType="System.Decimal" FilterControlAltText="Filter TaxAmt column" HeaderText="TaxAmt" SortExpression="TaxAmt" UniqueName="TaxAmt">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Freight" DataType="System.Decimal" FilterControlAltText="Filter Freight column" HeaderText="Freight" SortExpression="Freight" UniqueName="Freight">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TotalDue" DataType="System.Decimal" FilterControlAltText="Filter TotalDue column" HeaderText="TotalDue" SortExpression="TotalDue" UniqueName="TotalDue">
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
