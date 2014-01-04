using System;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementMVC.Controllers;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using RecipiesModelNS;

namespace RecipiesUnitTests
{
    [TestClass]
    public class ProductControllerTest
    {
        ProductController productController;
        DataSourceRequest request;

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
            productController = new ProductController();
            request = new DataSourceRequest();
        }

        [TestCleanup]
        public void TestCleanUp()
        {

        }

        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            ProductViewModel newProduct = new ProductViewModel();
            UnitTestHelper.InitializeModelWithRandomValues(newProduct);
            List<ProductViewModel> products = new List<ProductViewModel>() { newProduct };
            JsonResult jr = productController.Create(request, products) as JsonResult;
            // Act
            ProductViewModel savedProduct = (jr.Data as DataSourceResult).Data.Cast<ProductViewModel>().FirstOrDefault();
            bool areEqual = UnitTestHelper.CheckIfTwoModelsAreEqual(newProduct, savedProduct);
            // Assert
            Assert.IsTrue(areEqual, "Saved product field values are not equalt to values that are to be saved!");
        }

        [TestMethod]
        public void ReadTest()
        {
            // Arrange
            JsonResult jr = productController.Read(request) as JsonResult;
            // Act
            List<ProductViewModel> resultProducts = (jr.Data as DataSourceResult).Data.Cast<ProductViewModel>().ToList();
            // Assert
            Assert.AreEqual(resultProducts.Count, ContextFactory.Current.Products.Count());
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Arrange
            JsonResult jrr = productController.Read(request) as JsonResult;

            // Act
            ProductViewModel existingProduct = (jrr.Data as DataSourceResult).Data.Cast<ProductViewModel>().FirstOrDefault();

            UnitTestHelper.InitializeModelWithRandomValues(existingProduct);

            List<ProductViewModel> productsForUpdate = new List<ProductViewModel>() { existingProduct };


            JsonResult jru = productController.Update(request, productsForUpdate) as JsonResult;
            ProductViewModel savedProduct = (jru.Data as DataSourceResult).Data.Cast<ProductViewModel>().FirstOrDefault();


            bool areEqual = UnitTestHelper.CheckIfTwoModelsAreEqual(existingProduct, savedProduct);
            areEqual &= existingProduct.ProductId == savedProduct.ProductId;

            // Assert
            Assert.IsTrue(areEqual, "Saved product field values are not equalt to values that are to be saved!");
        }

        [TestMethod]
        public void DeleteTest()
        {
            // Arrange
            JsonResult jrr = productController.Read(request) as JsonResult;

            List<ProductViewModel> allProducts = (jrr.Data as DataSourceResult).Data.Cast<ProductViewModel>().ToList();

            // Act
            ProductViewModel existingProduct = (jrr.Data as DataSourceResult).Data.Cast<ProductViewModel>().FirstOrDefault();

            List<ProductViewModel> productsForDelete = new List<ProductViewModel>() { existingProduct };

            JsonResult jru = productController.Destroy(request, productsForDelete) as JsonResult;
            ProductViewModel deletedProduct = (jru.Data as DataSourceResult).Data.Cast<ProductViewModel>().FirstOrDefault();

            JsonResult jrAfterDelete = productController.Read(request) as JsonResult;
            List<ProductViewModel> allProductsAfterDelete = (jrAfterDelete.Data as DataSourceResult).Data.Cast<ProductViewModel>().ToList();

            Assert.AreEqual(allProducts.Count - 1, allProductsAfterDelete.Count);
        }


    }
}
