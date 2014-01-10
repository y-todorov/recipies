using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipiesModelNS;
using System.Linq;

namespace RecipiesUnitTests.CategoryController
{
    [TestClass]
    public class CategoryControllerTest
    {
        private InventoryManagementMVC.Controllers.CategoryController categoryController;
        private DataSourceRequest request;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            categoryController = new InventoryManagementMVC.Controllers.CategoryController();
            request = new DataSourceRequest();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
        }

        [TestMethod]
        public void CreateCategoryTest()
        {
            CrudTestsHelper.Create(categoryController, request, new CategoryViewModel());
        }

        [TestMethod]
        public void ReadCategoryTest()
        {
            CrudTestsHelper.Read(categoryController, request, ContextFactory.Current.ProductCategories);
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            CrudTestsHelper.Update(categoryController, request, (new CategoryViewModel()).ConvertFromEntity(ContextFactory.Current.ProductCategories.ToList().LastOrDefault()));
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            CrudTestsHelper.Delete(categoryController, request);
        }
    }
}