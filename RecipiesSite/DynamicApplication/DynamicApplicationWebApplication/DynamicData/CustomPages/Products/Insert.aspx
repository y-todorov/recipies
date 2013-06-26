<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Insert.aspx.cs" Inherits="DynamicApplicationWebApplication.Products.Insert" %>

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
							<td><asp:Label runat="server" Text="ProductID" /></td>
							<td><asp:DynamicControl runat="server" DataField="ProductID" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ProductName" /></td>
							<td><asp:DynamicControl runat="server" DataField="ProductName" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="QuantityPerUnit" /></td>
							<td><asp:DynamicControl runat="server" DataField="QuantityPerUnit" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="UnitPrice" /></td>
							<td><asp:DynamicControl runat="server" DataField="UnitPrice" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="UnitsInStock" /></td>
							<td><asp:DynamicControl runat="server" DataField="UnitsInStock" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="UnitsOnOrder" /></td>
							<td><asp:DynamicControl runat="server" DataField="UnitsOnOrder" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ReorderLevel" /></td>
							<td><asp:DynamicControl runat="server" DataField="ReorderLevel" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Discontinued" /></td>
							<td><asp:DynamicControl runat="server" DataField="Discontinued" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Category" /></td>
							<td><asp:DynamicControl runat="server" DataField="Category" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Supplier" /></td>
							<td><asp:DynamicControl runat="server" DataField="Supplier" OnInit="DynamicControl_Init" /></td>
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
	
