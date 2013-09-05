<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recipes.aspx.cs" Inherits="RecipiesWebFormApp.Production.Recipes" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register assembly="YordanCustomControls" namespace="YordanCustomControls" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <cc1:YordanCustomRadGrid ID="YordanCustomRadGrid1" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourceRecipes" GridLines="None">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="RecipeId" DataSourceID="OpenAccessLinqDataSourceRecipes">
        <Columns>
            <telerik:GridBoundColumn DataField="RecipeId" DataType="System.Int32" FilterControlAltText="Filter RecipeId column" HeaderText="RecipeId" ReadOnly="True" SortExpression="RecipeId" UniqueName="RecipeId">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
             <telerik:GridDropDownColumn UniqueName="DropDownCategoryListColumn" ListTextField="Name"
                    ListValueField="CategoryID" DataSourceID="OpenAccessLinqDataSourceCategory" HeaderText="Category"
                    DataField="CategoryID" DropDownControlType="RadComboBox" AllowAutomaticLoadOnDemand="true"
                    AllowVirtualScrolling="true" ShowMoreResultsBox="true" ItemsPerRequest="10">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>  
            <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
            <telerik:GridHTMLEditorColumn DataField="Description" FilterControlAltText="Filter Description column" HeaderText="Description" SortExpression="Description" UniqueName="Description">                
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridHTMLEditorColumn>
            <telerik:GridNumericColumn DataField="ValuePerPortion" DataType="System.Decimal" FilterControlAltText="Filter ValuePerPortion column" HeaderText="ValuePerPortion" SortExpression="ValuePerPortion" UniqueName="ValuePerPortion">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridNumericColumn>
            <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedByUser"  FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</cc1:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCategory" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ProductCategories" />
<telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipes" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Recipes" />
</asp:Content>
