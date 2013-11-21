using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using System.Data.Entity; // .Include !!!!!!! THIS IS SO IMPROTANT

namespace InventoryManagementMVC.Controllers
{
    public class ProductInventoryController : Controller
    {
        public ActionResult Index()
        {
            List<ProductInventoryViewModel> productInventoriesViewModels =
                ContextFactory.Current.Inventories.OfType<ProductInventory>().ToList().Select
                    (pi =>
                        ProductInventoryViewModel.ConvertFromProductInventoryEntity(pi, new ProductInventoryViewModel()))
                    .ToList();
            return View(productInventoriesViewModels);
        }

        public ActionResult Read(int? productInventoryHeaderId, [DataSourceRequest] DataSourceRequest request)
        {

            List<ProductInventoryViewModel> productInventoriesViewModels =
                ContextFactory.Current.Inventories.OfType<ProductInventory>()
                .Include(pi => pi.Product.ProductCategory)
                .Include(pi => pi.Product.UnitMeasure)
                .Where(
                    pih => productInventoryHeaderId.HasValue ? pih.ProductInventoryHeaderId == productInventoryHeaderId.Value : true).ToList().Select
                    (pi =>
                        ProductInventoryViewModel.ConvertFromProductInventoryEntity(pi, new ProductInventoryViewModel()))
                    .ToList();
            return Json(productInventoriesViewModels.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int? productInventoryHeaderId, [DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryViewModel> pis)
        {
            if (pis != null && ModelState.IsValid)
            {
                foreach (ProductInventoryViewModel pi in pis)
                {
                    pi.ProductInventoryHeaderId = productInventoryHeaderId;
                    ProductInventory newPodEntity =
                        ProductInventoryViewModel.ConvertToProductInventoryEntity(pi, new ProductInventory());
                    ContextFactory.Current.Inventories.Add(newPodEntity);
                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    newPodEntity = ContextFactory.Current.Inventories.OfType<ProductInventory>()
                        .Include(pod => pod.Product.UnitMeasure)
                    .Include(pod => pod.Product.ProductCategory).FirstOrDefault(pod => pod.InventoryId == newPodEntity.InventoryId);
                    ProductInventoryViewModel.ConvertFromProductInventoryEntity(newPodEntity, pi);
                }
            }

            return Json(pis.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryViewModel> pis)
        {
            if (pis != null && ModelState.IsValid)
            {
                foreach (ProductInventoryViewModel pi in pis)
                {
                    ProductInventory piEntity =
                        ContextFactory.Current.Inventories.OfType<ProductInventory>().FirstOrDefault(
                            c => c.InventoryId == pi.InventoryId);

                    ProductInventoryViewModel.ConvertToProductInventoryEntity(pi, piEntity);

                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    piEntity = ContextFactory.Current.Inventories.OfType<ProductInventory>()
                        .Include(pod => pod.Product.UnitMeasure)
                   .Include(pod => pod.Product.ProductCategory).FirstOrDefault(pod => pod.InventoryId == piEntity.InventoryId);
                    ProductInventoryViewModel.ConvertFromProductInventoryEntity(piEntity, pi);
                }
            }

            return Json(pis.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductInventoryViewModel> pis)
        {
            foreach (ProductInventoryViewModel pi in pis)
            {
                ProductInventory piEntity =
                    ContextFactory.Current.Inventories.OfType<ProductInventory>().FirstOrDefault(
                        c => c.InventoryId == pi.InventoryId);
                ContextFactory.Current.Inventories.Remove(piEntity);

                ContextFactory.Current.SaveChanges();
            }

            return Json(pis.ToDataSourceResult(request, ModelState));
        }
    }
}