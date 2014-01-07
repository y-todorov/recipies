using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipiesModelNS;
using System.Collections;

namespace RecipiesUnitTests
{
    public static class CrudTestsHelper
    {
        public static void Read(dynamic controller, DataSourceRequest request, IEnumerable entities)
        {
            // Arrange
            JsonResult jr = controller.Read(request) as JsonResult;
            // Act
            List<object> resultProducts = (jr.Data as DataSourceResult).Data.Cast<object>().ToList();
            // Assert
            Assert.AreEqual(resultProducts.Count, entities.Cast<object>().Count());
        }

        public static void Delete(dynamic controller, DataSourceRequest request)
        {
            // Arrange
            JsonResult jrr = controller.Read(request) as JsonResult;

            List<object> allProducts = (jrr.Data as DataSourceResult).Data.Cast<object>().ToList();

            Type modelType = allProducts.First().GetType();

            // Act
            IEnumerable modelForDelete = (jrr.Data as DataSourceResult).Data;

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(modelType);

            dynamic listOfModelsInstance = Activator.CreateInstance(constructedListType);

            IEnumerator en = modelForDelete.GetEnumerator();
            en.MoveNext();

            dynamic correctInstance = Activator.CreateInstance(modelType);
            correctInstance = en.Current;

            listOfModelsInstance.Add(correctInstance);

            JsonResult jru = controller.Destroy(request, listOfModelsInstance) as JsonResult;
            object deletedProduct = (jru.Data as DataSourceResult).Data.Cast<object>().FirstOrDefault();

            JsonResult jrAfterDelete = controller.Read(request) as JsonResult;
            List<object> allProductsAfterDelete = (jrAfterDelete.Data as DataSourceResult).Data.Cast<object>().ToList();

            Assert.AreEqual(allProducts.Count - 1, allProductsAfterDelete.Count);
        }
    }
}
