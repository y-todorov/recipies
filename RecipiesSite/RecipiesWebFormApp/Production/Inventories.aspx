<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventories.aspx.cs" Inherits="RecipiesWebFormApp.Production.Inventory" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register assembly="YordanCustomControls" namespace="YordanCustomControls" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <yordan:YordanCustomOpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
    <cc1:YordanCustomRadGrid ID="YordanCustomRadGridInventory" runat="server" CellSpacing="0" ItemType="RecipiesModelNS.Inventory" DataSourceID="OpenAccessLinqDataSourceInventory" GridLines="None" OnItemCreated="YordanCustomRadGridInventory_ItemCreated" >
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="InventoryId" DataSourceID="OpenAccessLinqDataSourceInventory">
            <Columns>
                <telerik:GridBoundColumn DataField="InventoryId" DataType="System.Int32" FilterControlAltText="Filter InventoryId column" HeaderText="InventoryId" ReadOnly="True" SortExpression="InventoryId" UniqueName="InventoryId">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridBoundColumn>
               <telerik:GridDropDownColumn UniqueName="DropDownProductListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="ProductId" DataSourceID="OpenAccessLinqDataSourceProduct" HeaderText="Product"
                    DataField="ProductId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>    
                <telerik:GridDateTimeColumn DataField="ForDate" DataType="System.DateTime" FilterControlAltText="Filter ForDate column" HeaderText="ForDate" SortExpression="ForDate" UniqueName="ForDate">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridNumericColumn DataField="AverageUnitPrice" DataType="System.Decimal" FilterControlAltText="Filter AverageUnitPrice column" HeaderText="AverageUnitPrice" SortExpression="AverageUnitPrice" UniqueName="AverageUnitPrice">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="QuantityByDocuments" DataType="System.Double" FilterControlAltText="Filter QuantityByDocuments column" HeaderText="QuantityByDocuments" SortExpression="QuantityByDocuments" UniqueName="QuantityByDocuments">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="ValueByDocuments" ReadOnly="true"  DataType="System.Decimal" FilterControlAltText="Filter ValueByDocuments column" HeaderText="ValueByDocuments" SortExpression="ValueByDocuments" UniqueName="ValueByDocuments">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="StocktakeQuantity" DataType="System.Double" FilterControlAltText="Filter StocktakeQuantity column" HeaderText="StocktakeQuantity" SortExpression="StocktakeQuantity" UniqueName="StocktakeQuantity">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn DataField="StocktakeValue" DataType="System.Decimal" FilterControlAltText="Filter StocktakeValue column" HeaderText="StocktakeValue" SortExpression="StocktakeValue" UniqueName="StocktakeValue">
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="StocktakeValueRadNumericTextBox" runat="server" DbValue='<%# Bind("StocktakeValue") %>' Type="Number">
                        </telerik:RadNumericTextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="StocktakeValueLabel" runat="server" Text='<%# Eval("StocktakeValue") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn DataField="DeficiencyQuantity" ReadOnly="true" DataType="System.Double" FilterControlAltText="Filter DeficiencyQuantity column" HeaderText="DeficiencyQuantity" SortExpression="DeficiencyQuantity" UniqueName="DeficiencyQuantity">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DeficiencyValue" ReadOnly="true"  DataType="System.Decimal" FilterControlAltText="Filter DeficiencyValue column" HeaderText="DeficiencyValue" SortExpression="DeficiencyValue" UniqueName="DeficiencyValue">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="SurplusQuantity" ReadOnly="true" DataType="System.Double" FilterControlAltText="Filter SurplusQuantity column" HeaderText="SurplusQuantity" SortExpression="SurplusQuantity" UniqueName="SurplusQuantity">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="SurplusValue" ReadOnly="true"  DataType="System.Decimal" FilterControlAltText="Filter SurplusValue column" HeaderText="SurplusValue" SortExpression="SurplusValue" UniqueName="SurplusValue">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridNumericColumn>
                <telerik:GridDateTimeColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <columnvalidationsettings>
                        <modelerrormessage text=""></modelerrormessage>
                    </columnvalidationsettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </cc1:YordanCustomRadGrid>
    <yordan:YordanCustomOpenAccessLinqDataSource ID="OpenAccessLinqDataSourceInventory" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Inventories" />
</asp:Content>
