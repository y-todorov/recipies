using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using System.Data.Entity;

namespace InventoryManagementMVC.Controllers
{
    public class ProductInventoryHeaderController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = ReadBase(request, typeof(ProductInventoryHeaderViewModel), typeof(ProductInventoryHeader), ContextFactory.Current.ProductInventoryHeaders.ToList());
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryHeaderViewModel> pihModels)
        {
            if (pihModels != null && ModelState.IsValid)
            {
                foreach (ProductInventoryHeaderViewModel pihModel in pihModels)
                {
                    ProductInventoryHeader pihEntity = pihModel.ConvertToEntity(new ProductInventoryHeader());


                    ContextFactory.Current.ProductInventoryHeaders.Add(pihEntity);
                    ContextFactory.Current.SaveChanges();

                    List<Product> allProducts = ContextFactory.Current.Products.ToList();
                    allProducts = allProducts.OrderBy(p => p.CategoryId).ThenBy(p => p.Name).ToList();
                    // Move this to the database project in ProductInventoryHeader
                    foreach (Product product in allProducts)
                    {
                        if (product.ProductId == 224)
                        {
                        }
                        ProductInventory pi = new ProductInventory();
                        pi.ProductId = product.ProductId;
                        //pi.ForDate = pihModel.ForDate;
                        pi.AverageUnitPrice = product.UnitPrice;
                        try
                        {
                            pi.QuantityByDocuments =
                                product.GetQuantityByDocumentsForDate(pihModel.ForDate.GetValueOrDefault());
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        pi.ProductInventoryHeaderId = pihEntity.ProductInventoryHeaderId;

                        ContextFactory.Current.Inventories.Add(pi);
                        ContextFactory.Current.SaveChanges();

                    }

                    pihModel.ConvertFromEntity(pihEntity);
                }
            }

            return Json(pihModels.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryHeaderViewModel> pihs)
        {
            if (pihs.Any())
            {
                foreach (ProductInventoryHeaderViewModel pihModel in pihs)
                {
                    ProductInventoryHeader inv =
                        ContextFactory.Current.ProductInventoryHeaders.FirstOrDefault(
                            p => p.ProductInventoryHeaderId == pihModel.ProductInventoryHeaderId);
                    ContextFactory.Current.ProductInventoryHeaders.Remove(inv);
                    
                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(pihs.ToDataSourceResult(request, ModelState));
        }
    }
}