<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="List.aspx.cs" Inherits="DynamicApplicationWebApplication.OrderDetails.List" %>

<%@ Register src="~/DynamicData/Content/GridViewPager.ascx" tagname="GridViewPager" tagprefix="asp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.OpenAccess.Web" Assembly="Telerik.OpenAccess.Web.40" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="dynamic" Namespace="Telerik.Web.UI.DynamicData" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head"/>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= this.Table.DisplayName %> List View</h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                    HeaderText="List of validation errors" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />

                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                        <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" /><br />
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>

			<dynamic:DynamicRadGrid ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                DataSourceID="GridDataSource" SelectedItemStyle-BackColor="LightBlue">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn>
							<ItemTemplate>
								<asp:DynamicHyperLink runat="server" Action="Edit" Text="Edit" Visible="True" />
									<asp:LinkButton runat="server" CommandName="Delete" Text="Delete" Visible="True"
										OnClientClick='return confirm("Are you sure you want to delete this item?");' />
								<asp:DynamicHyperLink runat="server" Text="Details" Visible="True" />
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="OrderID">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="OrderID"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="ProductID">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="ProductID"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="UnitPrice">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="UnitPrice"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="Quantity">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="Quantity"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="Discount">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="Discount"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="Order">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="Order"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
						<telerik:GridTemplateColumn HeaderText="Product">
							<ItemTemplate>
								<asp:DynamicControl runat="server" DataField="Product"/>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </dynamic:DynamicRadGrid>

			<telerik:OpenAccessLinqDataSource ID="GridDataSource" runat="server" EnableDelete="True" EnableInsert="True" EnableUpdate="True" />
			<asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
				<asp:DynamicFilterExpression ControlID="FilterRepeater" />
			</asp:QueryExtender>
			<br />
			<div class="DDBottomHyperLink">
            	<asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert"  Visible="True"><img runat="server" src="~/DynamicData/Content/Images/plus.gif" alt="Insert new item" />Insert new item</asp:DynamicHyperLink>
			</div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

