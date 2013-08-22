<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="RecipiesWebFormApp.Sales.Customer" %><%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCustomer" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Customers" />
<telerik:RadGrid ID="rgCustomers" runat="server" DataSourceID="OpenAccessLinqDataSourceCustomer">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="CustomerID" DataSourceID="OpenAccessLinqDataSourceCustomer">
        <Columns>
            <telerik:GridBoundColumn DataField="CustomerID" DataType="System.Int32" FilterControlAltText="Filter CustomerID column" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" UniqueName="CustomerID">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column" HeaderText="CompanyName" SortExpression="CompanyName" UniqueName="CompanyName">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ContactName" FilterControlAltText="Filter ContactName column" HeaderText="ContactName" SortExpression="ContactName" UniqueName="ContactName">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="Address" UniqueName="Address">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Country" FilterControlAltText="Filter Country column" HeaderText="Country" SortExpression="Country" UniqueName="Country">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter Phone column" HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column" HeaderText="Email" SortExpression="Email" UniqueName="Email">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
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
