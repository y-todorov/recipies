using System.Linq;
using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipiesModelNS;

namespace RecipiesUnitTests.StoreController
{
    [TestClass]
    public class ProductInventoryControllerTest
    {
        private InventoryManagementMVC.Controllers.ProductInventoryHeaderController recipeController;
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
    }
}