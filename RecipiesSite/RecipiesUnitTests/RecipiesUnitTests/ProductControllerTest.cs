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
            Assert.IsTrue(areEqual, "Saved product field values are not equal to values that are to be saved!");
        }

        [TestMethod]
        public void ReadTest()
        {
            CrudTestsHelper.Read(productController, request, ContextFactory.Current.Products);
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
            CrudTestsHelper.Delete(productController, request);
        }


    }
}
