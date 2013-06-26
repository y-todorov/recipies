<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Details.aspx.cs" Inherits="DynamicApplicationWebApplication.OrderDetails.Details" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server" />

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= this.Table.DisplayName %> Details View</h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="List of validation errors" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false">
                <ItemTemplate>
                    <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
						<tr class="td">
							<td><asp:Label runat="server" Text="OrderID" /></td>
							<td><asp:DynamicControl runat="server" DataField="OrderID" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ProductID" /></td>
							<td><asp:DynamicControl runat="server" DataField="ProductID" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="UnitPrice" /></td>
							<td><asp:DynamicControl runat="server" DataField="UnitPrice" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Quantity" /></td>
							<td><asp:DynamicControl runat="server" DataField="Quantity" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Discount" /></td>
							<td><asp:DynamicControl runat="server" DataField="Discount" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Order" /></td>
							<td><asp:DynamicControl runat="server" DataField="Order" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Product" /></td>
							<td><asp:DynamicControl runat="server" DataField="Product" OnInit="DynamicControl_Init" /></td>
						</tr>
  
                        <tr class="td">
                            <td colspan="2">
                                <asp:DynamicHyperLink runat="server" Action="Edit" Text="Edit" Visible="True" />
                                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" Visible="True"
                                    OnClientClick='return confirm("Are you sure you want to delete this item?");' />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">No such item.</div>
                </EmptyDataTemplate>
            </asp:FormView>

            <telerik:OpenAccessLinqDataSource ID="DetailsDataSource" runat="server" EnableDelete="True" />

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>

            <br />

            <div class="DDBottomHyperLink">
                <asp:DynamicHyperLink ID="ListHyperLink" runat="server" Action="List" Visible="True">Show all items</asp:DynamicHyperLink>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>		
	
