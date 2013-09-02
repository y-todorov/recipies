<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Po2.aspx.cs" Inherits="RecipiesWebFormApp.Purchasing.Po2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <yordan:YordanCustomRadGrid ID="rgPurchaseOrders" runat="server" CellSpacing="0" GridLines="None" OnNeedDataSource="rgPurchaseOrders_NeedDataSource" DataSourceID="OpenAccessLinqDataSource1" OnItemCommand="rgPurchaseOrders_ItemCommand">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="PurchaseOrderId" DataSourceID="OpenAccessLinqDataSource1">
            <Columns>
                <telerik:GridTemplateColumn DataField="OrderDate" DataType="System.DateTime" FilterControlAltText="Filter OrderDate column" HeaderText="OrderDate" SortExpression="OrderDate" UniqueName="OrderDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="OrderDateTextBox" runat="server" Text='<%# Bind("OrderDate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Eval("OrderDate") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ShipDate" DataType="System.DateTime" FilterControlAltText="Filter ShipDate column" HeaderText="ShipDate" SortExpression="ShipDate" UniqueName="ShipDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="ShipDateTextBox" runat="server" Text='<%# Bind("ShipDate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ShipDateLabel" runat="server" Text='<%# Eval("ShipDate") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="SubTotal" DataType="System.Decimal" FilterControlAltText="Filter SubTotal column" HeaderText="SubTotal" SortExpression="SubTotal" UniqueName="SubTotal">
                    <EditItemTemplate>
                        <asp:TextBox ID="SubTotalTextBox" runat="server" Text='<%# Bind("SubTotal") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="SubTotalLabel" runat="server" Text='<%# Eval("SubTotal") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="Freight" DataType="System.Decimal" FilterControlAltText="Filter Freight column" HeaderText="Freight" SortExpression="Freight" UniqueName="Freight">
                    <EditItemTemplate>
                        <asp:TextBox ID="FreightTextBox" runat="server" Text='<%# Bind("Freight") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="FreightLabel" runat="server" Text='<%# Eval("Freight") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="TotalDue" DataType="System.Decimal" FilterControlAltText="Filter TotalDue column" HeaderText="TotalDue" SortExpression="TotalDue" UniqueName="TotalDue">
                    <EditItemTemplate>
                        <asp:TextBox ID="TotalDueTextBox" runat="server" Text='<%# Bind("TotalDue") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="TotalDueLabel" runat="server" Text='<%# Eval("TotalDue") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="ModifiedDateTextBox" runat="server" Text='<%# Bind("ModifiedDate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ModifiedDateLabel" runat="server" Text='<%# Eval("ModifiedDate") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="VendorId" DataType="System.Int32" FilterControlAltText="Filter VendorId column" HeaderText="VendorId" SortExpression="VendorId" UniqueName="VendorId">
                    <EditItemTemplate>
                        <asp:TextBox ID="VendorIdTextBox" runat="server" Text='<%# Bind("VendorId") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="VendorIdLabel" runat="server" Text='<%# Eval("VendorId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ShipMethodId" DataType="System.Int32" FilterControlAltText="Filter ShipMethodId column" HeaderText="ShipMethodId" SortExpression="ShipMethodId" UniqueName="ShipMethodId">
                    <EditItemTemplate>
                        <asp:TextBox ID="ShipMethodIdTextBox" runat="server" Text='<%# Bind("ShipMethodId") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ShipMethodIdLabel" runat="server" Text='<%# Eval("ShipMethodId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="PurchaseOrderId" DataType="System.Int32" FilterControlAltText="Filter PurchaseOrderId column" HeaderText="PurchaseOrderId" SortExpression="PurchaseOrderId" UniqueName="PurchaseOrderId">
                    <EditItemTemplate>
                        <asp:TextBox ID="PurchaseOrderIdTextBox" runat="server" Text='<%# Bind("PurchaseOrderId") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="PurchaseOrderIdLabel" runat="server" Text='<%# Eval("PurchaseOrderId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="EmployeeId" DataType="System.Int32" FilterControlAltText="Filter EmployeeId column" HeaderText="EmployeeId" SortExpression="EmployeeId" UniqueName="EmployeeId">
                    <EditItemTemplate>
                        <asp:TextBox ID="EmployeeIdTextBox" runat="server" Text='<%# Bind("EmployeeId") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="EmployeeIdLabel" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <EditItemTemplate>
                        <asp:TextBox ID="ModifiedByUserTextBox" runat="server" Text='<%# Bind("ModifiedByUser") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ModifiedByUserLabel" runat="server" Text='<%# Eval("ModifiedByUser") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="VAT" DataType="System.Decimal" FilterControlAltText="Filter VAT column" HeaderText="VAT" SortExpression="VAT" UniqueName="VAT">
                    <EditItemTemplate>
                        <asp:TextBox ID="VATTextBox" runat="server" Text='<%# Bind("VAT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="VATLabel" runat="server" Text='<%# Eval("VAT") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="StatusId" DataType="System.Int32" FilterControlAltText="Filter StatusId column" HeaderText="StatusId" SortExpression="StatusId" UniqueName="StatusId">
                    <EditItemTemplate>
                        <asp:TextBox ID="StatusIdTextBox" runat="server" Text='<%# Bind("StatusId") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="StatusIdLabel" runat="server" Text='<%# Eval("StatusId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EntityTypeName="" ResourceSetName="PurchaseOrderHeaders" />
</asp:Content>
