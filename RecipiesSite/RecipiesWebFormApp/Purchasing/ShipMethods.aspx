<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShipMethods.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.ShipMethods" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="rgShipMethod" runat="server" DataSourceID="OpenAccessLinqDataSourceShipMethod">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="ShipMethodId" DataSourceID="OpenAccessLinqDataSourceShipMethod">
        <Columns>
            <telerik:GridBoundColumn DataField="ShipMethodId" DataType="System.Int32" FilterControlAltText="Filter ShipMethodId column" HeaderText="ShipMethodId" ReadOnly="True" SortExpression="ShipMethodId" UniqueName="ShipMethodId">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridNumericColumn DataField="ShipBase" DataType="System.Decimal" FilterControlAltText="Filter ShipBase column" HeaderText="ShipBase" SortExpression="ShipBase" UniqueName="ShipBase">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridNumericColumn>
            <telerik:GridNumericColumn DataField="ShipRate" DataType="System.Decimal" FilterControlAltText="Filter ShipRate column" HeaderText="ShipRate" SortExpression="ShipRate" UniqueName="ShipRate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridNumericColumn>
            <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceShipMethod" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ShipMethods">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
