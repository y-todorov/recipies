using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class RecipeIngredient : YordanBaseEntity
    {
        public override void OaldsDeleting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceDeleteEventArgs e)
        {
            recipeIdToUpdate = RecipeId;
            base.OaldsDeleting(sender, e);
        }

        private static int? recipeIdToUpdate;

        public override void OaldsDeleted(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceStatusEventArgs e)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(recipeIdToUpdate);
            base.OaldsDeleted(sender, e);
        }

        public override void OaldsUpdated(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceStatusEventArgs e)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId);
            base.OaldsUpdated(sender, e);
        }

        public override void OaldsInserted(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceStatusEventArgs e)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId);
            base.OaldsInserted(sender, e);
        }

        public override void Adding(RecipiesModel context, Telerik.OpenAccess.AddEventArgs e)
        {
            //UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId, context);
            base.Adding(context, e);
        }

        public override void Changing(RecipiesModel context, Telerik.OpenAccess.ChangeEventArgs e)
        {
            //UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId, context);
            base.Changing(context, e);
        }

        public override void Removing(RecipiesModel context, Telerik.OpenAccess.RemoveEventArgs e)
        {
            //UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId, context);
            base.Removing(context, e);
        }

        private void UpdateRecipesValuePerPortionFromIngredientsChange(int? recipeId)
        {
            if (recipeId.HasValue)
            {
                Recipe recipe = ContextFactory.GetContextPerRequest().Recipes.FirstOrDefault(re => re.RecipeId == recipeId.Value);
                if (recipe != null)
                {
                    decimal? valuePerPortion = 0;
                    foreach (RecipeIngredient ri in recipe.RecipeIngredients)
                    {
                        valuePerPortion += (decimal?)ri.TotalValue;
                    }
                    recipe.ProductionValuePerPortion = valuePerPortion;
                }
            }

        }
    }
}
