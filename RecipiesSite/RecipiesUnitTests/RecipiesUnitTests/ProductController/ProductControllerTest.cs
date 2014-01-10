using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipiesModelNS;

namespace RecipiesUnitTests.ProductController
{
    [TestClass]
    public class ProductControllerTest
    {
        private InventoryManagementMVC.Controllers.ProductController productController;
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
            productController = new InventoryManagementMVC.Controllers.ProductController();
            request = new DataSourceRequest();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
        }

        [TestMethod]
        public void CreateProductTest()
        {
            CrudTestsHelper.Create(productController, request, new ProductViewModel());
        }

        [TestMethod]
        public void ReadProductTest()
        {
            CrudTestsHelper.Read(productController, request, ContextFactory.Current.Products);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            CrudTestsHelper.Update(productController, request, (new ProductViewModel()).ConvertFromEntity(ContextFactory.Current.Products.FirstOrDefault()));
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            CrudTestsHelper.Delete(productController, request);
        }
    }
}