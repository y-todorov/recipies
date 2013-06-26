	<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Edit.aspx.cs" Inherits="DynamicApplicationWebApplication.Orders.Edit" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <h2 class="DDSubHeader"><%= this.Table.DisplayName %> Edit Form</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true" HeaderText="List of validation errors" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />
            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Edit"
                OnItemCommand="FormView1_ItemCommand" OnItemUpdated="FormView1_ItemUpdated" RenderOuterTable="false">
                <EditItemTemplate>
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
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="Update" />
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">
                        No such item.
                    </div>
                </EmptyDataTemplate>
            </asp:FormView>
			<asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>
            <telerik:OpenAccessLinqDataSource ID="DetailsDataSource" runat="server" EnableUpdate="True"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
	
