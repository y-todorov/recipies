using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipiesModelNS
{
    public partial class RecipeIngredient : YordanBaseEntity
    {
        public override void Adding(RecipiesModel context, Telerik.OpenAccess.AddEventArgs e)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId, context);
            base.Adding(context, e);
        }

        public override void Changing(RecipiesModel context, Telerik.OpenAccess.ChangeEventArgs e)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId, context);
            base.Changing(context, e);
        }

        public override void Removing(RecipiesModel context, Telerik.OpenAccess.RemoveEventArgs e)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId, context);
            base.Removing(context, e);
        }

        private void UpdateRecipesValuePerPortionFromIngredientsChange(int? recipeId, RecipiesModel context)
        {
            if (recipeId.HasValue)
            {
                Recipe recipe = context.Recipes.FirstOrDefault(re => re.RecipeId == recipeId.Value);
                if (recipe != null)
                {
                    decimal? valuePerPortion = 0;
                    foreach (RecipeIngredient ri in recipe.RecipeIngredients)
                    {
                        valuePerPortion += ri.Cost;
                    }
                    recipe.ValuePerPortion = valuePerPortion;
                }
            }

        }
    }
}
