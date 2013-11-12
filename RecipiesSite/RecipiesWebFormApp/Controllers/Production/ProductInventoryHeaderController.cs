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
    }
}