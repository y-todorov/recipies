<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recipes.aspx.cs" Inherits="RecipiesWebFormApp.Production.Recipes" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>
<%@ Register Assembly="YordanCustomControls" Namespace="YordanCustomControls" TagPrefix="yordan" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <yordan:YordanCustomRadGrid ID="rgRecipes" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourceRecipes" GridLines="None">
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="RecipeId" DataSourceID="OpenAccessLinqDataSourceRecipes">
            <Columns>
                <telerik:GridBoundColumn DataField="RecipeId" DataType="System.Int32" FilterControlAltText="Filter RecipeId column" HeaderText="RecipeId" ReadOnly="True" SortExpression="RecipeId" UniqueName="RecipeId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
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
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridHTMLEditorColumn DataField="Description" FilterControlAltText="Filter Description column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridHTMLEditorColumn>
                <telerik:GridNumericColumn DataField="ValuePerPortion" ReadOnly="true" DataType="System.Decimal" FilterControlAltText="Filter ValuePerPortion column" HeaderText="ValuePerPortion" SortExpression="ValuePerPortion" UniqueName="ValuePerPortion">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn InsertVisiblityMode="AlwaysVisible" Visible="false" HeaderText="Purchasing products">
                    <ItemTemplate />
                    <EditItemTemplate>
                        <asp:Label runat="server" ID="lblProductIngredients" Text="asdasd asd a"></asp:Label>
                        <yordan:YordanCustomRadGrid ID="YordanCustomRadGrid1" runat="server" CellSpacing="0" DataSourceID="OpenAccessLinqDataSourceRecipeIngredients" GridLines="None">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="RecipeIngredientId" DataSourceID="OpenAccessLinqDataSourceRecipeIngredients">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="RecipeIngredientId" Visible="false" DataType="System.Int32" FilterControlAltText="Filter RecipeIngredientId column" HeaderText="RecipeIngredientId" ReadOnly="True" SortExpression="RecipeIngredientId" UniqueName="RecipeIngredientId">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDropDownColumn UniqueName="DropDownRecipeListColumn" ListTextField="Name" Visible="false" ReadOnly="true"
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
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn DataField="Cost" DataType="System.Decimal" FilterControlAltText="Filter Cost column" HeaderText="Cost" SortExpression="Cost" UniqueName="Cost">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </yordan:YordanCustomRadGrid>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime" FilterControlAltText="Filter ModifiedDate column" HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModifiedByUser" FilterControlAltText="Filter ModifiedByUser column" HeaderText="ModifiedByUser" SortExpression="ModifiedByUser" UniqueName="ModifiedByUser">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text=""></ModelErrorMessage>
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </yordan:YordanCustomRadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceCategory" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="ProductCategories" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipes" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Recipes" />

    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipeIngredients" runat="server" OnUpdated="OpenAccessLinqDataSourceRecipeIngredients_InsertedUpdatedDeleted" OnInserted="OpenAccessLinqDataSourceRecipeIngredients_InsertedUpdatedDeleted" OnDeleted="OpenAccessLinqDataSourceRecipeIngredients_InsertedUpdatedDeleted" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="RecipeIngredients" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceProduct" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Products" />
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSourceRecipe" runat="server" ContextTypeName="RecipiesModelNS.RecipiesModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="Recipes" />


</asp:Content>
