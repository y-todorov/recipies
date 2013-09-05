<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RecipiesWebFormApp.Sales.Orders" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <yordan:YordanCustomRadGrid ID="rgSalesOrderHeaders" runat="server" DataSourceID="OpenAccessLinqDataSourceOrder" CellSpacing="0" GridLines="None">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderID" DataSourceID="OpenAccessLinqDataSourceOrder">
            <Columns>
                <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32" FilterControlAltText="Filter OrderID column" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" UniqueName="OrderID">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                 <telerik:GridDropDownColumn UniqueName="DropDownCustomerListColumn" ListTextField="CompanyName"
                    ListValueField="CustomerID" DataSourceID="OpenAccessLinqDataSourceCustomer" HeaderText="Customer"
                    DataField="CustomerID" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                     </telerik:GridDropDownColumn>
               <telerik:GridDropDownColumn UniqueName="DropDownEmployeeListColumn" ListTextField="FirstName"
                    ListValueField="EmployeeID" DataSourceID="OpenAccessLinqDataSourceEmployee" HeaderText="Employee"
                    DataField="EmployeeID" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                </telerik:GridDropDownColumn>
                  <telerik:GridDropDownColumn UniqueName="DropDownStatusListColumn" ListTextField="Name"
                    ListValueField="SalesOrderStatusId" DataSourceID="OpenAccessLinqDataSourceStatus" HeaderText="Status"
                    DataField="StatusId" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                </telerik:GridDropDownColumn>               
                <telerik:GridBoundColumn DataField="AccountName" FilterControlAltText="Filter AccountName column" HeaderText="AccountName" SortExpression="AccountName" UniqueName="AccountName">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="OrderDate" DataType="System.DateTime" FilterControlAltText="Filter OrderDate column" HeaderText="OrderDate" SortExpression="OrderDate" UniqueName="OrderDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                  <telerik:GridDateTimeColumn DataField="RequiredDate" FilterControlAltText="Filter RequiredDate column" HeaderText="RequiredDate" SortExpression="RequiredDate" UniqueName="RequiredDate" DataType="System.DateTime">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="ShippedDate" DataType="System.DateTime" FilterControlAltText="Filter ShippedDate column" HeaderText="ShippedDate" SortExpression="ShippedDate" UniqueName="ShippedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
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
                <telerik:GridDateTimeColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrder" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderHeaders">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCustomer" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Customers" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceEmployee" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Employees" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceStatus" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderStatus" />

</asp:Content>
