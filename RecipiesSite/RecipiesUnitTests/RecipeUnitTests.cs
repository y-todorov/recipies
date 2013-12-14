using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipiesModelNS;
using System.Collections.Generic;


namespace RecipiesUnitTests
{
    [TestClass]
    public class RecipeUnitTests
    {
        [TestMethod]
        public void FindAllProductsWithQuantitiesInRecipe()
        {
            Recipe aRecipie = ContextFactory.Current.Recipes.FirstOrDefault();
            Dictionary<int, double> products = new Dictionary<int, double>();
            Recipe.GetProductsWithQuantities(aRecipie.RecipeId, products);
        }
    }
}
