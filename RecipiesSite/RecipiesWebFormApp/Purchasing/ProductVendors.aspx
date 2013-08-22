<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductVendors.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.ProductVendors" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="rgProductVendors" runat="server" DataSourceID="OpenAccessLinqDataSourceProductVendors">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ProductVendorId" DataSourceID="OpenAccessLinqDataSourceProductVendors">
            <Columns>
                <telerik:GridBoundColumn DataField="ProductVendorId" DataType="System.Int32" FilterControlAltText="Filter ProductVendorId column" HeaderText="ProductVendorId" ReadOnly="True" SortExpression="ProductVendorId" UniqueName="ProductVendorId">
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
                       <telerik:GridDropDownColumn UniqueName="DropDownVendorListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="VendorId" DataSourceID="OpenAccessLinqDataSourceVendor" HeaderText="Vendor"
                    DataField="VendorId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>  
                              <telerik:GridDropDownColumn UniqueName="DropDownUnitMeasureListColumn" ListTextField="Name" EmptyListItemText="" EnableEmptyListItem="true" EmptyListItemValue="" ConvertEmptyStringToNull="true"
                    ListValueField="UnitMeasureId" DataSourceID="OpenAccessLinqDataSourceUnitMeasure" HeaderText="UnitMeasure"
                    DataField="UnitMeasureId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>                               
                <telerik:GridNumericColumn DataField="AverageLeadTime" DataType="System.Int32" FilterControlAltText="Filter AverageLeadTime column" HeaderText="AverageLeadTime" SortExpression="AverageLeadTime" UniqueName="AverageLeadTime">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="StandardPrice" DataType="System.Decimal" FilterControlAltText="Filter StandardPrice column" HeaderText="StandardPrice" SortExpression="StandardPrice" UniqueName="StandardPrice">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="LastReceiptCost" DataType="System.Decimal" FilterControlAltText="Filter LastReceiptCost column" HeaderText="LastReceiptCost" SortExpression="LastReceiptCost" UniqueName="LastReceiptCost">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridDateTimeColumn DataField="LastReceiptDate" DataType="System.DateTime" FilterControlAltText="Filter LastReceiptDate column" HeaderText="LastReceiptDate" SortExpression="LastReceiptDate" UniqueName="LastReceiptDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="MinOrderQuantity" DataType="System.Int32" FilterControlAltText="Filter MinOrderQuantity column" HeaderText="MinOrderQuantity" SortExpression="MinOrderQuantity" UniqueName="MinOrderQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="MaxOrderQuantity" DataType="System.Int32" FilterControlAltText="Filter MaxOrderQuantity column" HeaderText="MaxOrderQuantity" SortExpression="MaxOrderQuantity" UniqueName="MaxOrderQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="OnOrderQuantity" DataType="System.Int32" FilterControlAltText="Filter OnOrderQuantity column" HeaderText="OnOrderQuantity" SortExpression="OnOrderQuantity" UniqueName="OnOrderQuantity">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                     <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
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
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProductVendors" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ProductVendors">
    </telerik:OpenAccessLinqDataSource>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceVendor" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Vendors" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceUnitMeasure" Runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="UnitMeasures" />
</asp:Content>
