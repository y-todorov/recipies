<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RecipiesWebFormApp.Sales.Orders" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="OpenAccessLinqDataSource1">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderID" DataSourceID="OpenAccessLinqDataSource1">
        <Columns>
            <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32" FilterControlAltText="Filter OrderID column" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" UniqueName="OrderID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CustomerID" FilterControlAltText="Filter CustomerID column" HeaderText="CustomerID" SortExpression="CustomerID" UniqueName="CustomerID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="EmployeeID" DataType="System.Int32" FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" SortExpression="EmployeeID" UniqueName="EmployeeID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridDateTimeColumn DataField="OrderDate" DataType="System.DateTime" FilterControlAltText="Filter OrderDate column" HeaderText="OrderDate" SortExpression="OrderDate" UniqueName="OrderDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridDateTimeColumn>
            <telerik:GridBoundColumn DataField="RequiredDate" DataType="System.DateTime" FilterControlAltText="Filter RequiredDate column" HeaderText="RequiredDate" SortExpression="RequiredDate" UniqueName="RequiredDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShippedDate" DataType="System.DateTime" FilterControlAltText="Filter ShippedDate column" HeaderText="ShippedDate" SortExpression="ShippedDate" UniqueName="ShippedDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipVia" DataType="System.Int32" FilterControlAltText="Filter ShipVia column" HeaderText="ShipVia" SortExpression="ShipVia" UniqueName="ShipVia">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Freight" DataType="System.Decimal" FilterControlAltText="Filter Freight column" HeaderText="Freight" SortExpression="Freight" UniqueName="Freight">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipName" FilterControlAltText="Filter ShipName column" HeaderText="ShipName" SortExpression="ShipName" UniqueName="ShipName">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipAddress" FilterControlAltText="Filter ShipAddress column" HeaderText="ShipAddress" SortExpression="ShipAddress" UniqueName="ShipAddress">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipCity" FilterControlAltText="Filter ShipCity column" HeaderText="ShipCity" SortExpression="ShipCity" UniqueName="ShipCity">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipRegion" FilterControlAltText="Filter ShipRegion column" HeaderText="ShipRegion" SortExpression="ShipRegion" UniqueName="ShipRegion">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipPostalCode" FilterControlAltText="Filter ShipPostalCode column" HeaderText="ShipPostalCode" SortExpression="ShipPostalCode" UniqueName="ShipPostalCode">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ShipCountry" FilterControlAltText="Filter ShipCountry column" HeaderText="ShipCountry" SortExpression="ShipCountry" UniqueName="ShipCountry">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Orders">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
