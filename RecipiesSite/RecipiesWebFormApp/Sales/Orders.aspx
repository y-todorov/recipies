<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RecipiesWebFormApp.Sales.Orders" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <yordan:YordanCustomRadGrid ID="rgSalesOrderHeaders" runat="server" ItemType="RecipiesModelNS.SalesOrderHeader" DataSourceID="OpenAccessLinqDataSourceOrder" CellSpacing="0" GridLines="None" OnItemCommand="rgSalesOrderHeaders_ItemCommand">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="SalesOrderHeaderId" DataSourceID="OpenAccessLinqDataSourceOrder">
            <Columns>
                <telerik:GridBoundColumn DataField="SalesOrderHeaderId" DataType="System.Int32" FilterControlAltText="Filter SalesOrderHeaderId column" HeaderText="SalesOrderHeaderId" ReadOnly="True" SortExpression="SalesOrderHeaderId" UniqueName="SalesOrderHeaderId">
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
                    <telerik:GridDropDownColumn UniqueName="DropDownPaymentTypeColumn" ListTextField="Name"
                    ListValueField="PaymentTypeId" DataSourceID="OpenAccessLinqDataSourcePaymentType" HeaderText="Payment Type"
                    DataField="PaymentTypeId" DropDownControlType="RadComboBox">
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
                <telerik:GridTemplateColumn>
                    <ItemStyle />
                    <EditItemTemplate>
                        <asp:Label runat="server" ID="lblSalesOrderDetails" ForeColor="Blue" Text="Please save the sales order so recipies can be added to it." OnPreRender="lblSalesOrderDetails_PreRender"></asp:Label>
                        <yordan:YordanCustomRadGrid ID="rgSalesOrderDetails" runat="server" ItemType="RecipiesModelNS.SalesOrderDetail" DataSourceID="OpenAccessLinqDataSourceOrderDetail" CellSpacing="0" GridLines="None"  OnPreRender="rgSalesOrderDetails_PreRender">
                            <HeaderStyle />
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="SalesOrderDetailId" DataSourceID="OpenAccessLinqDataSourceOrderDetail">
                                <Columns>                                                                   
                                    <telerik:GridDropDownColumn UniqueName="DropDownRecipeListColumn" ListTextField="Name"
                                        ListValueField="RecipeId" DataSourceID="OpenAccessLinqDataSourceRecipe" HeaderText="Recipe"
                                        DataField="RecipeId" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridNumericColumn DataField="OrderQuantity" DataType="System.Double" FilterControlAltText="Filter OrderQuantity column" HeaderText="OrderQuantity" SortExpression="OrderQuantity" UniqueName="OrderQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="UnitPriceDiscount" DataType="System.Double" FilterControlAltText="Filter UnitPriceDiscount column" HeaderText="UnitPriceDiscount" SortExpression="UnitPriceDiscount" UniqueName="UnitPriceDiscount">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="LineTotal" ReadOnly="true" DataType="System.Decimal" FilterControlAltText="Filter LineTotal column" HeaderText="LineTotal" SortExpression="LineTotal" UniqueName="LineTotal">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
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
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrder" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderHeaders" OnUpdating="OpenAccessLinqDataSourceOrder_Updating">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCustomer" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Customers" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceEmployee" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Employees" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceStatus" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderStatus" Where="IsVisible == @IsVisible" >
        <WhereParameters>
            <asp:Parameter DefaultValue="True" Name="IsVisible" Type="Boolean" />
        </WhereParameters>
    </telerik:OpenAccessLinqDataSource>

    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceOrderDetail" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderDetails" OnInserting="OpenAccessLinqDataSourceOrderDetail_Inserting" OnSelecting="OpenAccessLinqDataSourceOrderDetail_Selecting" Where="SalesOrderHeaderId == @SalesOrderHeaderId" >
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="SalesOrderHeaderId" Type="Int32" />
        </WhereParameters>
</telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePaymentType" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PaymentTypes" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceSalesOrderHeader" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="SalesOrderHeaders" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipe" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Recipes" />

</asp:Content>
