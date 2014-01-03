using System;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementMVC.Controllers;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace RecipiesUnitTests
{
    [TestClass]
    public class ProductControllerTest
    {

//[ClassInitialize()]   Use ClassInitialize to run code before you run the first test in the class.
//[ClassCleanUp()]   Use ClassCleanup to run code after all tests in a class have run.
//[TestInitialize()]   Use TestInitialize to run code before you run each test.
//[TestCleanUp()]   Use TestCleanup to run code after each test has run.
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestCleanup]
        public void TestCleanUp()
        {

        }

        [TestMethod]
        public void CreateTest()
        {
            ProductController pc = new ProductController();
            DataSourceRequest request = new DataSourceRequest();

            ProductViewModel pvm = new ProductViewModel();
            pvm.CategoryId = null;
            pvm.Code = "TestCode";
            List<ProductViewModel> products = new List<ProductViewModel>() {pvm};

            JsonResult jr = pc.Create(request, products) as JsonResult;

            ProductViewModel result = (jr.Data as DataSourceResult).Data.Cast<ProductViewModel>().FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(pvm.Code, result.Code);


        }
        
        

        [TestMethod]
        public void ReadTest()
        {
            ProductController pc = new ProductController();
            DataSourceRequest request = new DataSourceRequest();
            JsonResult jr = pc.Read(request) as JsonResult;
        }
    }
}
