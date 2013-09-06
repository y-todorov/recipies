using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace RecipiesWebFormApp.Production
{
    public partial class Recipes : System.Web.UI.Page
    {
        public int RecipeId
        {
            get
            {
                int recipeId = (int)ViewState["RecipeId"];
                return recipeId;
            }
            set
            {
                ViewState["RecipeId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenAccessLinqDataSourceRecipeIngredients_InsertedUpdatedDeleted(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceStatusEventArgs e)
        {
            // So value per portion will be updated
            rgRecipes.Rebind();
        }

        protected void rgRecipes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                GridEditableItem editableItem = e.Item as GridEditableItem;
                if (editableItem != null)
                {
                    int recipeId = (int)editableItem.GetDataKeyValue(rgRecipes.MasterTableView.DataKeyNames[0]);
                    Recipe recipe = ContextFactory.GetContextPerRequest().Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
                    RecipeId = recipe.RecipeId;                   
                }
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RecipeId = 0;
            }
        }

        protected void OpenAccessLinqDataSourceRecipeIngredients_Inserting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceInsertEventArgs e)
        {
            RecipeIngredient newRecipeIngredient = e.NewObject as RecipeIngredient;
            if (newRecipeIngredient != null)
            {
                newRecipeIngredient.RecipeId = RecipeId;
            }
        }

        protected void OpenAccessLinqDataSourceRecipeIngredients_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
        {
            if (e.WhereParameters.ContainsKey("RecipeId"))
            {
                e.WhereParameters["RecipeId"] = RecipeId;
            }
        }

        protected void lblProductIngredients_PreRender(object sender, EventArgs e)
        {
            if (RecipeId > 0)
            {
                Label lblRecipeIngredients = (Label)sender;
                lblRecipeIngredients.Visible = false;
            }
        }

        protected void rgRecipeIngredients_PreRender(object sender, EventArgs e)
        {
            if (RecipeId <= 0)
            {
                RadGrid rgRecipeIngredients = (RadGrid)sender;
                rgRecipeIngredients.Visible = false;
            }
        }

      
    }
}