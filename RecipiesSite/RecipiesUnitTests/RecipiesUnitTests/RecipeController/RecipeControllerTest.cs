using System.Linq;
using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipiesModelNS;
using RecipiesWebFormApp.Quartz.Jobs;

namespace RecipiesUnitTests.RecipeController
{
    [TestClass]
    public class RecipeControllerTest
    {
        private InventoryManagementMVC.Controllers.RecipeController recipeController;
        private DataSourceRequest request;

        [TestInitialize]
        public void TestInitialize()
        {
            recipeController = new InventoryManagementMVC.Controllers.RecipeController();
            request = new DataSourceRequest();
        }

        [TestMethod]
        public void CreateTest()
        {
            CrudTestsHelper.Create(recipeController, request, new RecipeViewModel());
        }

        [TestMethod]
        public void ReadTest()
        {
            CrudTestsHelper.Read(recipeController, request, ContextFactory.Current.Recipes);
        }

        [TestMethod]
        public void UpdateTest()
        {
            CrudTestsHelper.Update(recipeController, request, (new RecipeViewModel()).ConvertFromEntity(ContextFactory.Current.Recipes.ToList().LastOrDefault()));
        }

        [TestMethod]
        public void DeleteTest()
        {
            CrudTestsHelper.Delete(recipeController, request);
        }

        [TestMethod]
        public void UpdateProductionValueOfAllRecipes()
        {
            CalculateRecipesProductionValuePerPortionJob job = new CalculateRecipesProductionValuePerPortionJob();
            job.Execute(null);
        }

    }
}