<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Insert.aspx.cs" Inherits="DynamicApplicationWebApplication.Employees.Insert" %>

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
							<td><asp:Label runat="server" Text="LastName" /></td>
							<td><asp:DynamicControl runat="server" DataField="LastName" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="FirstName" /></td>
							<td><asp:DynamicControl runat="server" DataField="FirstName" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Title" /></td>
							<td><asp:DynamicControl runat="server" DataField="Title" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="TitleOfCourtesy" /></td>
							<td><asp:DynamicControl runat="server" DataField="TitleOfCourtesy" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="BirthDate" /></td>
							<td><asp:DynamicControl runat="server" DataField="BirthDate" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="HireDate" /></td>
							<td><asp:DynamicControl runat="server" DataField="HireDate" OnInit="DynamicControl_Init" /></td>
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
							<td><asp:Label runat="server" Text="HomePhone" /></td>
							<td><asp:DynamicControl runat="server" DataField="HomePhone" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Extension" /></td>
							<td><asp:DynamicControl runat="server" DataField="Extension" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Photo" /></td>
							<td><asp:DynamicControl runat="server" DataField="Photo" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Notes" /></td>
							<td><asp:DynamicControl runat="server" DataField="Notes" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="PhotoPath" /></td>
							<td><asp:DynamicControl runat="server" DataField="PhotoPath" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Employee1" /></td>
							<td><asp:DynamicControl runat="server" DataField="Employee1" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Territories" /></td>
							<td><asp:DynamicControl runat="server" DataField="Territories" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Orders" /></td>
							<td><asp:DynamicControl runat="server" DataField="Orders" OnInit="DynamicControl_Init" /></td>
						</tr>
						<tr class="td">
							<td><asp:Label runat="server" Text="Employees" /></td>
							<td><asp:DynamicControl runat="server" DataField="Employees" OnInit="DynamicControl_Init" /></td>
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
	
