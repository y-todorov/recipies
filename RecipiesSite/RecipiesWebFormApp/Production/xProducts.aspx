<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="xProducts.aspx.cs" Inherits="RecipiesWebFormApp.Production.xProducts" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="OpenAccessLinqDataSource1">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ProductId" DataSourceID="OpenAccessLinqDataSource1">
            <Columns>
                <telerik:GridBoundColumn DataField="ProductId" DataType="System.Int32" FilterControlAltText="Filter ProductId column" HeaderText="ProductId" ReadOnly="True" SortExpression="ProductId" UniqueName="ProductId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UnitId" DataType="System.Int32" FilterControlAltText="Filter UnitId column" HeaderText="UnitId" SortExpression="UnitId" UniqueName="UnitId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CategoryId" DataType="System.Int32" FilterControlAltText="Filter CategoryId column" HeaderText="CategoryId" SortExpression="CategoryId" UniqueName="CategoryId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierID" DataType="System.Int32" FilterControlAltText="Filter SupplierID column" HeaderText="SupplierID" SortExpression="SupplierID" UniqueName="SupplierID">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                    <ColumnValidationSettings EnableRequiredFieldValidation="true" EnableModelErrorMessageValidation="true">
                        <RequiredFieldValidator ForeColor="Red" ErrorMessage="This field is required!"></RequiredFieldValidator>
                        <ModelErrorMessage BackColor="Red" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="UnitsInStock" DataType="System.Int32" FilterControlAltText="Filter UnitsInStock column" HeaderText="UnitsInStock" SortExpression="UnitsInStock" UniqueName="UnitsInStock">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="UnitsOnOrder" DataType="System.Int32" FilterControlAltText="Filter UnitsOnOrder column" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" UniqueName="UnitsOnOrder">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridDateTimeColumn DataField="ModifiedDate" ReadOnly="true" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Store" FilterControlAltText="Filter Store column" HeaderText="Store" SortExpression="Store" UniqueName="Store">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="ReorderLevel" DataType="System.Int32" FilterControlAltText="Filter ReorderLevel column" HeaderText="ReorderLevel" SortExpression="ReorderLevel" UniqueName="ReorderLevel">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="XProducts">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
