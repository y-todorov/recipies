<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="RecipiesWebFormApp.Sales.OrderDetails" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <yordan:YordanCustomRadGrid ID="rgSalesOrderDetails" runat="server" DataSourceID="OpenAccessLinqDataSourceOrderDetail" CellSpacing="0" GridLines="None">

        <HeaderStyle />
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="SalesOrderDetailId" DataSourceID="OpenAccessLinqDataSourceOrderDetail">
            <Columns>
                <telerik:GridBoundColumn DataField="SalesOrderDetailId" DataType="System.Int32" FilterControlAltText="Filter SalesOrderDetailId column" HeaderText="SalesOrderDetailId" ReadOnly="True" SortExpression="SalesOrderDetailId" UniqueName="SalesOrderDetailId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SalesOrderId" DataType="System.Int32" FilterControlAltText="Filter SalesOrderId column" HeaderText="SalesOrderId" SortExpression="SalesOrderId" UniqueName="SalesOrderId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="RecipeId" FilterControlAltText="Filter RecipeId column" HeaderText="RecipeId" SortExpression="RecipeId" UniqueName="RecipeId" DataType="System.Int32">
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
                <telerik:GridBoundColumn DataField="UnitPriceDiscount" DataType="System.Double" FilterControlAltText="Filter UnitPriceDiscount column" HeaderText="UnitPriceDiscount" SortExpression="UnitPriceDiscount" UniqueName="UnitPriceDiscount">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LineTotal" DataType="System.Double" FilterControlAltText="Filter LineTotal column" HeaderText="LineTotal" SortExpression="LineTotal" UniqueName="LineTotal">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrderDetail" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderDetails">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrder" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Orders" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipe" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Recipes" />
</asp:Content>
