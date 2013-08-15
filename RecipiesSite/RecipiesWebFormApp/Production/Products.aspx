<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="RecipiesWebFormApp.Production.Product" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCategory" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Categories" />
    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="OpenAccessLinqDataSource1" CellSpacing="0" GridLines="None">
       
        <ValidationSettings ValidationGroup="ValGroup" EnableModelValidation="true" EnableValidation="true" />
       
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ProductID" DataSourceID="OpenAccessLinqDataSource1">

            <Columns>
                <telerik:GridBoundColumn DataField="ProductID" DataType="System.Int32" FilterControlAltText="Filter ProductID column" HeaderText="ProductID" ReadOnly="True" SortExpression="ProductID" UniqueName="ProductID">
<ColumnValidationSettings>
<ModelErrorMessage Text=""></ModelErrorMessage>
</ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter ProductName column" HeaderText="ProductName" SortExpression="ProductName" UniqueName="ProductName">
                  <%--  <ColumnValidationSettings EnableRequiredFieldValidation="true" EnableModelErrorMessageValidation="true">
                        <RequiredFieldValidator ForeColor="Red" ErrorMessage="This field is required"></RequiredFieldValidator>
                        <ModelErrorMessage BackColor="Red" />
                    </ColumnValidationSettings>--%>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierID" DataType="System.Int32" FilterControlAltText="Filter SupplierID column" HeaderText="SupplierID" SortExpression="SupplierID" UniqueName="SupplierID">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownCategoryListColumn" ListTextField="CategoryName"
                    ListValueField="CategoryID" DataSourceID="OpenAccessLinqDataSourceCategory" HeaderText="Category"
                    DataField="CategoryID" DropDownControlType="RadComboBox" >
<ColumnValidationSettings>
<ModelErrorMessage Text=""></ModelErrorMessage>
</ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridBoundColumn DataField="QuantityPerUnit" FilterControlAltText="Filter QuantityPerUnit column" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit" UniqueName="QuantityPerUnit">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UnitsInStock" DataType="System.Int16" FilterControlAltText="Filter UnitsInStock column" HeaderText="UnitsInStock" SortExpression="UnitsInStock" UniqueName="UnitsInStock">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UnitsOnOrder" DataType="System.Int16" FilterControlAltText="Filter UnitsOnOrder column" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" UniqueName="UnitsOnOrder">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReorderLevel" DataType="System.Int16" FilterControlAltText="Filter ReorderLevel column" HeaderText="ReorderLevel" SortExpression="ReorderLevel" UniqueName="ReorderLevel">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="Discontinued" DataType="System.Boolean" FilterControlAltText="Filter Discontinued column" HeaderText="Discontinued" SortExpression="Discontinued" UniqueName="Discontinued">
                </telerik:GridCheckBoxColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
