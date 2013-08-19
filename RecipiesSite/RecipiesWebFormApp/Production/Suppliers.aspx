<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="RecipiesWebFormApp.Production.xSuppliers" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="rgSuppliers" runat="server" DataSourceID="OpenAccessLinqDataSourceSupplier">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="SupplierId" DataSourceID="OpenAccessLinqDataSourceSupplier">
            <Columns>
                <telerik:GridBoundColumn DataField="SupplierId" DataType="System.Int32" FilterControlAltText="Filter SupplierId column" HeaderText="SupplierId" ReadOnly="True" SortExpression="SupplierId" UniqueName="SupplierId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column" HeaderText="CompanyName" SortExpression="CompanyName" UniqueName="CompanyName">
                    <ColumnValidationSettings>
                        <ModelErrorMessage />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ContactName" FilterControlAltText="Filter ContactName column" HeaderText="ContactName" SortExpression="ContactName" UniqueName="ContactName">
                    <ColumnValidationSettings>
                        <ModelErrorMessage />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="Address" UniqueName="Address">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column" HeaderText="City" SortExpression="City" UniqueName="City">
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
                <telerik:GridBoundColumn DataField="Fax" FilterControlAltText="Filter Fax column" HeaderText="Fax" SortExpression="Fax" UniqueName="Fax">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column" HeaderText="Email" SortExpression="Email" UniqueName="Email">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePage" FilterControlAltText="Filter HomePage column" HeaderText="HomePage" SortExpression="HomePage" UniqueName="HomePage">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true"  FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate" DataType="System.DateTime">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceSupplier" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Suppliers">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
