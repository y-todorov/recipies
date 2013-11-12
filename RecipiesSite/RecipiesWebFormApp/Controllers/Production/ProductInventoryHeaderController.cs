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
            List<ProductInventory> inv1 = ContextFactory.Current.Inventories.OfType<ProductInventory>().Where(p => p.ForDate == new DateTime(2013, 9, 9)).ToList();
            foreach (var item in inv1)
            {
                item.ProductInventoryHeaderId = 1;
                ContextFactory.Current.SaveChanges();
            }

            List<ProductInventory>  inv2 = ContextFactory.Current.Inventories.OfType<ProductInventory>().Where(p => p.ForDate == new DateTime(2013, 9, 22)).ToList();
            foreach (var item in inv2)
            {
                item.ProductInventoryHeaderId = 2;
                ContextFactory.Current.SaveChanges();
            }

            List<ProductInventory>  inv3 = ContextFactory.Current.Inventories.OfType<ProductInventory>().Where(p => p.ForDate == new DateTime(2013, 10, 20)).ToList();
            foreach (var item in inv3)
            {
                item.ProductInventoryHeaderId = 3;
                ContextFactory.Current.SaveChanges();
            }

            List<ProductInventory> inv4 = ContextFactory.Current.Inventories.OfType<ProductInventory>().Where(p => p.ForDate == new DateTime(2013, 10, 21)).ToList();
            foreach (var item in inv4)
            {
                item.ProductInventoryHeaderId = 4;
                ContextFactory.Current.SaveChanges();
            }

            ContextFactory.Current.SaveChanges();

            //ContextFactory.Current.SaveChanges();


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
    }
}