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

//[ClassInitialize()]   Use ClassInitialize to run code before you run the first test in the class.
//[ClassCleanUp()]   Use ClassCleanup to run code after all tests in a class have run.
//[TestInitialize()]   Use TestInitialize to run code before you run each test.
//[TestCleanUp()]   Use TestCleanup to run code after each test has run.

        ProductController productController;
        DataSourceRequest request;


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
            List<ProductViewModel> products = new List<ProductViewModel>() {newProduct};
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

        public void UpdateTest()
        {
            //productController.Update()
        }
    }
}
