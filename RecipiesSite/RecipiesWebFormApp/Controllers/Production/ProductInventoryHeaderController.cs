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
            List<ProductInventoryHeaderViewModel> productInventoriesViewModels =
                ContextFactory.Current.ProductInventoryHeaders.ToList().Select
                    (pi =>
                        ProductInventoryHeaderViewModel.ConvertFromProductInventoryHeaderEntity(pi, new ProductInventoryHeaderViewModel()))
                    .ToList();
            return View(productInventoriesViewModels);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ProductInventoryHeaderViewModel> productInventoriesViewModels =
                ContextFactory.Current.ProductInventoryHeaders.ToList().Select
                    (pi =>
                        ProductInventoryHeaderViewModel.ConvertFromProductInventoryHeaderEntity(pi, new ProductInventoryHeaderViewModel()))
                    .ToList();
            return Json(productInventoriesViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryHeaderViewModel> pihModels)
        {
            if (pihModels != null && ModelState.IsValid)
            {
                foreach (ProductInventoryHeaderViewModel pihModel in pihModels)
                {
                    ProductInventoryHeader pihEntity =
                        ProductInventoryHeaderViewModel.ConvertToProductInventoryHeaderEntity(pihModel,
                            new ProductInventoryHeader());




                    ContextFactory.Current.ProductInventoryHeaders.Add(pihEntity);
                    ContextFactory.Current.SaveChanges();

                    List<Product> allProducts = ContextFactory.Current.Products.ToList();

                    foreach (Product product in allProducts)
                    {
                        ProductInventory pi = new ProductInventory();
                        pi.ForDate = pihModel.ForDate;
                        pi.AverageUnitPrice = product.UnitPrice;
                        pi.QuantityByDocuments = product.GetQuantityByDocumentsForDate(pihModel.ForDate.GetValueOrDefault());

                        pi.ProductInventoryHeaderId = pihEntity.ProductInventoryHeaderId;

                        ContextFactory.Current.Inventories.Add(pi);
                        ContextFactory.Current.SaveChanges();

                        //product.UnitPrice.GetValueOrDefault().ToString();
                        //quantityByDocumentsRadNumericTextBox.Text =
                        //    product.GetQuantityByDocumentsForDate(
                        //        ForDateRadDateTimePicker.SelectedDate.GetValueOrDefault()).ToString();
                    }

                    //ContextFactory.Current.SaveChanges();

                    ProductInventoryHeaderViewModel.ConvertFromProductInventoryHeaderEntity(pihEntity, pihModel);
                }
            }

            return Json(pihModels.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryHeaderViewModel> products)
        {
            if (products.Any())
            {
                foreach (ProductInventoryHeaderViewModel pihModel in products)
                {
                    ProductInventoryHeader inv =
                        ContextFactory.Current.ProductInventoryHeaders.FirstOrDefault(p => p.ProductInventoryHeaderId == pihModel.ProductInventoryHeaderId);
                    ContextFactory.Current.ProductInventoryHeaders.Remove(inv);

                    List<ProductInventory> pis = ContextFactory.Current.Inventories.OfType<ProductInventory>().Where(pi => pi.ProductInventoryHeaderId == pihModel.ProductInventoryHeaderId).ToList();

                    foreach (ProductInventory pInventory in pis)
                    {
                        ContextFactory.Current.Inventories.Remove(pInventory);
                    }

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }
    }
}