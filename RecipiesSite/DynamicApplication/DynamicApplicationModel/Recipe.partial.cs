using System;
using System.Collections.Generic;
using System.Linq;


namespace RecipiesModelNS
{
    public partial class Recipe : YordanBaseEntity
    {
        public static void GetProductsWithQuantities(int? recipeId, Dictionary<int, double> productsWithQuantities)
        {
            Recipe recipe = ContextFactory.Current.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
            if (recipe != null)
            {
                foreach (ProductIngredient pi in recipe.ProductIngredients)
                {
                    if (!pi.ProductId.HasValue)
                    {
                        throw new ApplicationException(
                            string.Format("Product ingredient with id {0} does not have a Product! Please select one!",
                                pi.ProductIngredientId));
                    }
                    if (productsWithQuantities.ContainsKey(pi.ProductId.Value))
                    {
                        productsWithQuantities[pi.ProductId.Value] += pi.QuantityPerPortion.GetValueOrDefault();
                    }
                    else
                    {
                        productsWithQuantities.Add(pi.ProductId.Value, pi.QuantityPerPortion.GetValueOrDefault());
                    }
                }

                foreach (RecipeIngredient ri in recipe.RecipeIngredients1)
                {
                    if (!ri.IngredientRecipeId.HasValue)
                    {
                        throw new ApplicationException(
                            string.Format("Recipe ingredient with id {0} does not have a Product! Please select one!",
                                ri.RecipeIngredientId));
                    }

                    GetProductsWithQuantities(ri.IngredientRecipeId, productsWithQuantities);
                }
            }
        }
    }
}