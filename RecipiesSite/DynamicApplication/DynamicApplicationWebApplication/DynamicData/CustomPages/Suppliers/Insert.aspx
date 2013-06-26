<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Insert.aspx.cs" Inherits="DynamicApplicationWebApplication.Suppliers.Insert" %>

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
							<td><asp:Label runat="server" Text="SupplierID" /></td>
							<td><asp:DynamicControl runat="server" DataField="SupplierID" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="CompanyName" /></td>
							<td><asp:DynamicControl runat="server" DataField="CompanyName" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ContactName" /></td>
							<td><asp:DynamicControl runat="server" DataField="ContactName" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="ContactTitle" /></td>
							<td><asp:DynamicControl runat="server" DataField="ContactTitle" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Address" /></td>
							<td><asp:DynamicControl runat="server" DataField="Address" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="City" /></td>
							<td><asp:DynamicControl runat="server" DataField="City" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Region" /></td>
							<td><asp:DynamicControl runat="server" DataField="Region" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="PostalCode" /></td>
							<td><asp:DynamicControl runat="server" DataField="PostalCode" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Country" /></td>
							<td><asp:DynamicControl runat="server" DataField="Country" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Phone" /></td>
							<td><asp:DynamicControl runat="server" DataField="Phone" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Fax" /></td>
							<td><asp:DynamicControl runat="server" DataField="Fax" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="HomePage" /></td>
							<td><asp:DynamicControl runat="server" DataField="HomePage" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Products" /></td>
							<td><asp:DynamicControl runat="server" DataField="Products" OnInit="DynamicControl_Init" /></td>
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
	
