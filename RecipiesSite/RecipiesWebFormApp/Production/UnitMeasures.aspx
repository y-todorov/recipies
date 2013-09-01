<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnitMeasures.aspx.cs" Inherits="RecipiesWebFormApp.Production.xUnits" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadGrid ID="rgUnits" runat="server" DataSourceID="OpenAccessLinqDataSourceUnit">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="UnitMeasureId" DataSourceID="OpenAccessLinqDataSourceUnit">
            <Columns>
                <telerik:GridBoundColumn DataField="UnitMeasureId" DataType="System.Int32" FilterControlAltText="Filter UnitId column" HeaderText="UnitId" ReadOnly="True" SortExpression="UnitMeasureId" UniqueName="UnitMeasureId">
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
                  <telerik:GridBoundColumn DataField="ModifiedByUser" ReadOnly="true" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceUnit" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="UnitMeasures">
    </telerik:OpenAccessLinqDataSource>
</asp:Content>
