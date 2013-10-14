using System.Linq;

namespace RecipiesModelNS
{
    public partial class RecipeIngredient : YordanBaseEntity
    {
        private static int? recipeIdToUpdate;

        public override void Added(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId);
            base.Added(e);
        }

        public override void Changed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(RecipeId);
            base.Changed(e);
        }

        public override void Removing(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            recipeIdToUpdate = RecipeId;
            base.Removing(e);
        }

        public override void Removed(System.Data.Entity.Infrastructure.DbEntityEntry e = null)
        {
            UpdateRecipesValuePerPortionFromIngredientsChange(recipeIdToUpdate);
            base.Removed(e);
        }


        private void UpdateRecipesValuePerPortionFromIngredientsChange(int? recipeId)
        {
            if (recipeId.HasValue)
            {
                Recipe recipe =
                    ContextFactory.GetContextPerRequest().Recipes.FirstOrDefault(re => re.RecipeId == recipeId.Value);
                if (recipe != null)
                {
                    decimal? valuePerPortion = 0;
                    foreach (RecipeIngredient ri in recipe.RecipeIngredients)
                    {
                        valuePerPortion += (decimal?) ri.TotalValue;
                    }
                    recipe.ProductionValuePerPortion = valuePerPortion;
                    ContextFactory.GetContextPerRequest().SaveChanges();
                }
            }
        }
    }
}