<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrders.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.PurchaseOrders" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrders" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceVendor" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Vendors" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceShipMethod" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ShipMethods" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceEmployee" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Employees" />
    <!-- PurchaseOrderId are case sensitives !!!!!! -->

       <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrder" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" OnSelected="OpenAccessLinqDataSourceProduct_Selected" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourcePurchaseOrderDetails" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="PurchaseOrderDetails" Where="PurchaseOrderId == @PurchaseOrderId" OnSelected="OpenAccessLinqDataSourcePurchaseOrderDetails_Selected" OnSelecting="OpenAccessLinqDataSourcePurchaseOrderDetails_Selecting" >
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="PurchaseOrderId" Type="Int32" />
        </WhereParameters>
    </telerik:OpenAccessLinqDataSource>
    <telerik:RadGrid ID="rgPurchaseOrders" runat="server" DataSourceID="OpenAccessLinqDataSourcePurchaseOrders" OnEditCommand="rgPurchaseOrders_EditCommand" OnItemCommand="rgPurchaseOrders_ItemCommand">
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
                <telerik:GridBoundColumn DataField="RevisionNumber" DataType="System.Int32" FilterControlAltText="Filter RevisionNumber column" HeaderText="RevisionNumber" SortExpression="RevisionNumber" UniqueName="RevisionNumber">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Status" DataType="System.Int32" FilterControlAltText="Filter Status column" HeaderText="Status" SortExpression="Status" UniqueName="Status">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
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
                <telerik:GridNumericColumn DataField="SubTotal" DataType="System.Decimal" FilterControlAltText="Filter SubTotal column" HeaderText="SubTotal" SortExpression="SubTotal" UniqueName="SubTotal">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="TaxAmt" DataType="System.Decimal" FilterControlAltText="Filter TaxAmt column" HeaderText="TaxAmt" SortExpression="TaxAmt" UniqueName="TaxAmt">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Freight" DataType="System.Decimal" FilterControlAltText="Filter Freight column" HeaderText="Freight" SortExpression="Freight" UniqueName="Freight">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="TotalDue" ReadOnly="true" DataType="System.Decimal" FilterControlAltText="Filter TotalDue column" HeaderText="TotalDue" SortExpression="TotalDue" UniqueName="TotalDue">
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
                <telerik:GridTemplateColumn InsertVisiblityMode="AlwaysVisible" Visible="false">

                    <EditItemTemplate>
                        <telerik:RadGrid ID="rgPurchaseOrderDetails" runat="server" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderDetails" OnPreRender="rgPurchaseOrderDetails_PreRender"  OnItemCommand="rgPurchaseOrderDetails_ItemCommand">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderDetailId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrderDetails" >
                                <Columns>
                                    <telerik:GridDropDownColumn UniqueName="DropDownPurchaseOrderListColumn" Visible="false" ListTextField="PurchaseOrderId" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                                        ListValueField="PurchaseOrderId" DataSourceID="OpenAccessLinqDataSourcePurchaseOrder"  ReadOnly="true" HeaderText="PurchaseOrder"
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
                                    <telerik:GridDateTimeColumn DataField="DueDate" DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" FilterControlAltText="Filter DueDate column" HeaderText="DueDate" SortExpression="DueDate" UniqueName="DueDate">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridNumericColumn DataField="OrderQuantity" DataType="System.Int32" FilterControlAltText="Filter OrderQuantity column" HeaderText="OrderQuantity" SortExpression="OrderQuantity" UniqueName="OrderQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="UnitPrice" DataType="System.Decimal" FilterControlAltText="Filter UnitPrice column" HeaderText="UnitPrice" SortExpression="UnitPrice" UniqueName="UnitPrice">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="LineTotal" ReadOnly="true" DataType="System.Decimal" FilterControlAltText="Filter LineTotal column" HeaderText="LineTotal" SortExpression="LineTotal" UniqueName="LineTotal">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="ReceivedQuantity" DataType="System.Int32" FilterControlAltText="Filter ReceivedQuantity column" HeaderText="ReceivedQuantity" SortExpression="ReceivedQuantity" UniqueName="ReceivedQuantity">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text="" />
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="RejectedQuantity" DataType="System.Int32" FilterControlAltText="Filter RejectedQuantity column" HeaderText="RejectedQuantity" SortExpression="RejectedQuantity" UniqueName="RejectedQuantity">
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
                        </telerik:RadGrid>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>          
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>
