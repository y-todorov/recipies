<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrders.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrders" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrders" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders" OnUpdating="OpenAccessLinqDataSourcePurchaseOrders_Updating">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceVendor" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Vendors" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceShipMethod" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ShipMethods" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceEmployee" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Employees" />
    <!-- PurchaseOrderId are case sensitives !!!!!! -->

    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrder" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" OnSelected="OpenAccessLinqDataSourceProduct_Selected" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrderDetails" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderDetails" Where="PurchaseOrderId == @PurchaseOrderId" OnSelected="OpenAccessLinqDataSourcePurchaseOrderDetails_Selected" OnSelecting="OpenAccessLinqDataSourcePurchaseOrderDetails_Selecting" OnInserting="OpenAccessLinqDataSourcePurchaseOrderDetails_Inserting" OnUpdating="OpenAccessLinqDataSourcePurchaseOrderDetails_Updating" OnDeleted="OpenAccessLinqDataSourcePurchaseOrderDetails_InsertedUpdatedDeleted" OnInserted="OpenAccessLinqDataSourcePurchaseOrderDetails_InsertedUpdatedDeleted" OnUpdated="OpenAccessLinqDataSourcePurchaseOrderDetails_InsertedUpdatedDeleted">
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="PurchaseOrderId" Type="Int32" />
        </WhereParameters>
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceUnit" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="UnitMeasures" />

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceStatus" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderStatus" Where="IsVisible == @IsVisible">
        <WhereParameters>
            <asp:Parameter DefaultValue="True" Name="IsVisible" Type="Boolean" />
        </WhereParameters>
    </telerik:OpenAccessLinqDataSource>
    <yordan:YordanCustomRadGrid ID="rgPurchaseOrders" runat="server" DataSourceID="OpenAccessLinqDataSourcePurchaseOrders" OnItemCommand="rgPurchaseOrders_ItemCommand" OnItemCreated="rgPurchaseOrders_ItemCreated">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrders">
            <Columns>
                <telerik:GridBoundColumn DataField="PurchaseOrderId" DataType="System.Int32" FilterControlAltText="Filter PurchaseOrderID column" HeaderText="PurchaseOrderID" ReadOnly="True" SortExpression="PurchaseOrderID" UniqueName="PurchaseOrderID">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownEmployeeListColumn" ListTextField="FirstName"
                    ListValueField="EmployeeID" DataSourceID="OpenAccessLinqDataSourceEmployee" HeaderText="Employee"
                    DataField="EmployeeID" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownVendorListVendor" ListTextField="Name"
                    ListValueField="VendorID" DataSourceID="OpenAccessLinqDataSourceVendor" HeaderText="Vendor"
                    DataField="VendorID" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownShipMethodListColumn" ListTextField="Name"
                    ListValueField="ShipMethodID" DataSourceID="OpenAccessLinqDataSourceShipMethod" HeaderText="ShipMethod"
                    DataField="ShipMethodID" DropDownControlType="RadComboBox" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridDropDownColumn UniqueName="DropDownStatusListColumn" ListTextField="Name"
                    ListValueField="PurchaseOrderStatusId" DataSourceID="OpenAccessLinqDataSourceStatus" HeaderText="Status"
                    DataField="StatusId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>
                <telerik:GridBoundColumn DataField="InvoiceNumber" FilterControlAltText="Filter InvoiceNumber column" HeaderText="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>

                <telerik:GridDateTimeColumn DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" FilterControlAltText="Filter OrderDate column" HeaderText="OrderDate" SortExpression="OrderDate" UniqueName="OrderDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="ShipDate" DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" FilterControlAltText="Filter ShipDate column" HeaderText="ShipDate" SortExpression="ShipDate" UniqueName="ShipDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridNumericColumn DataField="SubTotal" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter SubTotal column" HeaderText="SubTotal" SortExpression="SubTotal" UniqueName="SubTotal">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="VAT" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter VAT column" HeaderText="VAT" SortExpression="VAT" UniqueName="VAT">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Freight" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter Freight column" HeaderText="Freight" SortExpression="Freight" UniqueName="Freight">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="TotalDue" ReadOnly="true" DataFormatString="{0:C}" DataType="System.Decimal" FilterControlAltText="Filter TotalDue column" HeaderText="TotalDue" SortExpression="TotalDue" UniqueName="TotalDue">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
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
                <telerik:GridTemplateColumn InsertVisiblityMode="AlwaysVisible" Visible="false" HeaderText="Purchasing products">
                    <ItemTemplate />
                    <EditItemTemplate>
                        <asp:Label runat="server" ID="lblPurchaseOrderDetails" ForeColor="Blue" Text="Please save the purchase order so products can be added to it." OnPreRender="lblPurchaseOrderDetails_PreRender"></asp:Label>
                        <yordan:YordanCustomRadGrid ID="rgPurchaseOrderDetails" runat="server" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderDetails" OnItemCreated="rgPurchaseOrderDetails_ItemCreated" OnItemDataBound="rgPurchaseOrderDetails_ItemDataBound" OnCreateColumnEditor="rgPurchaseOrderDetails_CreateColumnEditor" OnPreRender="rgPurchaseOrderDetails_PreRender" OnItemCommand="rgPurchaseOrderDetails_ItemCommand">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderDetailId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderDetails">
                                <Columns>
                                    <telerik:GridDropDownColumn UniqueName="DropDownPurchaseOrderListColumn" Visible="false" ListTextField="PurchaseOrderId" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                                        ListValueField="PurchaseOrderId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrder" ReadOnly="true" HeaderText="PurchaseOrder"
                                        DataField="PurchaseOrderId" DropDownControlType="RadComboBox">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridBoundColumn DataField="PurchaseOrderId" DataType="System.Int32" Visible="false" FilterControlAltText="Filter PurchaseOrderId column" HeaderText="PurchaseOrderId" ReadOnly="true" SortExpression="PurchaseOrderId" UniqueName="PurchaseOrderId">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDropDownColumn UniqueName="DropDownProductListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                                        ListValueField="ProductId" DataSourceID="OpenAccessLinqDataSourceProduct" HeaderText="Product"
                                        DataField="ProductId" DropDownControlType="RadComboBox">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridBoundColumn ReadOnly="true" DataField="Product.Code" FilterControlAltText="Filter Product.Code column" HeaderText="Product.Code" SortExpression="Product.Code" UniqueName="Product.Code">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDropDownColumn UniqueName="DropDownUnitListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                                        ListValueField="UnitMeasureId" DataSourceID="OpenAccessLinqDataSourceUnit" HeaderText="UnitMeasure"
                                        DataField="UnitMeasureId" DropDownControlType="RadComboBox">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridNumericColumn DataField="OrderQuantity" DataType="System.Int32" FilterControlAltText="Filter OrderQuantity column" HeaderText="OrderQuantity" SortExpression="OrderQuantity" UniqueName="OrderQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="UnitPrice" DataFormatString="{0:C}" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="LineTotal" ReadOnly="true" DataFormatString="{0:C}" DataType="System.Decimal" FilterControlAltText="Filter LineTotal column" HeaderText="LineTotal" SortExpression="LineTotal" UniqueName="LineTotal">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="ReceivedQuantity" DataType="System.Int32" FilterControlAltText="Filter ReceivedQuantity column" HeaderText="ReceivedQuantity" SortExpression="ReceivedQuantity" UniqueName="ReceivedQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="ReturnedQuantity" DataType="System.Int32" FilterControlAltText="Filter ReturnedQuantity column" HeaderText="ReturnedQuantity" SortExpression="ReturnedQuantity" UniqueName="ReturnedQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="StockedQuantity" ReadOnly="true" DataType="System.Int32" FilterControlAltText="Filter StockedQuantity column" HeaderText="StockedQuantity" SortExpression="StockedQuantity" UniqueName="StockedQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
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
                        </yordan:YordanCustomRadGrid>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ButtonType="LinkButton" Text="Download PO" UniqueName="Download" CommandName="GeneratePurchaseOrderReport"></telerik:GridButtonColumn>
                <telerik:GridButtonColumn ButtonType="LinkButton" Text="Send mail" CommandName="SendMail"></telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>

</asp:Content>
