<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecipeIngredients.aspx.cs" Inherits="RecipiesWebFormApp.Production.RecipeIngredients" %>
<%@ Register assembly="Telerik.OpenAccess.Web.40" namespace="Telerik.OpenAccess.Web" tagprefix="telerik" %>
<%@ Register assembly="YordanCustomControls" namespace="YordanCustomControls" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <cc1:YordanCustomRadGrid ID="YordanCustomRadGrid1" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourceRecipeIngredients" GridLines="None">
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="RecipeIngredientId" DataSourceID="OpenAccessLinqDataSourceRecipeIngredients">
        <Columns>
            <telerik:GridBoundColumn DataField="RecipeIngredientId" DataType="System.Int32" FilterControlAltText="Filter RecipeIngredientId column" HeaderText="RecipeIngredientId" ReadOnly="True" SortExpression="RecipeIngredientId" UniqueName="RecipeIngredientId">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
              <telerik:GridDropDownColumn UniqueName="DropDownRecipeListColumn" ListTextField="Name" 
                    ListValueField="RecipeId" DataSourceID="OpenAccessLinqDataSourceRecipe" HeaderText="Recipe"
                    DataField="RecipeId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>               
             <telerik:GridDropDownColumn UniqueName="DropDownProductListColumn" ListTextField="Name" 
                    ListValueField="ProductId" DataSourceID="OpenAccessLinqDataSourceProduct" HeaderText="Product"
                    DataField="ProductId" DropDownControlType="RadComboBox">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridDropDownColumn>             
            <telerik:GridNumericColumn DataField="QuantityPerPortion" DataType="System.Double" FilterControlAltText="Filter QuantityPerPortion column" HeaderText="QuantityPerPortion" SortExpression="QuantityPerPortion" UniqueName="QuantityPerPortion">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridNumericColumn>
            <telerik:GridNumericColumn DataField="Cost" DataType="System.Decimal" FilterControlAltText="Filter Cost column" HeaderText="Cost" SortExpression="Cost" UniqueName="Cost">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridNumericColumn>
            <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                <columnvalidationsettings>
                    <modelerrormessage text=""></modelerrormessage>
                </columnvalidationsettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</cc1:YordanCustomRadGrid>
<telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipeIngredients" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="RecipeIngredients" />

    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipe" Runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Recipes" />

</asp:Content>
