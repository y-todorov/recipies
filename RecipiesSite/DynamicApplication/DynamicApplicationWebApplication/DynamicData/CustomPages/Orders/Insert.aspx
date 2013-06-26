<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Insert.aspx.cs" Inherits="DynamicApplicationWebApplication.Orders.Insert" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server" />

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= this.Table.DisplayName %> Insert Form</h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="List of validation errors" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Insert"
                OnItemCommand="FormView1_ItemCommand" OnItemInserted="FormView1_ItemInserted" RenderOuterTable="false">
                <InsertItemTemplate>
                    <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
						<tr class="td">
							<td><asp:Label runat="server" Text="OrderID" /></td>
							<td><asp:DynamicControl runat="server" DataField="OrderID" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="OrderDate" /></td>
							<td><asp:DynamicControl runat="server" DataField="OrderDate" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="RequiredDate" /></td>
							<td><asp:DynamicControl runat="server" DataField="RequiredDate" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShippedDate" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShippedDate" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Freight" /></td>
							<td><asp:DynamicControl runat="server" DataField="Freight" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShipName" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShipName" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShipAddress" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShipAddress" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShipCity" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShipCity" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShipRegion" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShipRegion" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShipPostalCode" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShipPostalCode" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ShipCountry" /></td>
							<td><asp:DynamicControl runat="server" DataField="ShipCountry" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Customer" /></td>
							<td><asp:DynamicControl runat="server" DataField="Customer" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Employee" /></td>
							<td><asp:DynamicControl runat="server" DataField="Employee" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Shipper" /></td>
							<td><asp:DynamicControl runat="server" DataField="Shipper" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="OrderDetails" /></td>
							<td><asp:DynamicControl runat="server" DataField="OrderDetails" OnInit="DynamicControl_Init" /></td>
						</tr>
                        <tr class="td">
                            <td colspan="2">
                                <asp:LinkButton runat="server" CommandName="Insert" Text="Insert" />
                                <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>

            <telerik:OpenAccessLinqDataSource ID="DetailsDataSource" runat="server" EnableInsert="true" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
	
