<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCategories.aspx.cs" Inherits="RecipiesWebFormApp.Production.xCategories" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="rgCategories" runat="server" DataSourceID="OpenAccessLinqDataSourceCategory">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="CategoryId" DataSourceID="OpenAccessLinqDataSourceCategory">
        <Columns>
            <telerik:GridBoundColumn DataField="CategoryId" DataType="System.Int32" FilterControlAltText="Filter CategoryId column" HeaderText="CategoryId" ReadOnly="True" SortExpression="CategoryId" UniqueName="CategoryId">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                <ColumnValidationSettings>
                        <ModelErrorMessage />
                    </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedDate" ReadOnly="true"  DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCategory" runat="server" ContextTypeName="DynamicApplicationModel.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ProductCategories">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
