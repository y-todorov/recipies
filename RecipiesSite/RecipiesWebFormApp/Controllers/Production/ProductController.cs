﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesWebFormApp;
using System.Web;
using System.Web.Caching;
using RecipiesWebFormApp.Caching;
using DevTrends.MvcDonutCaching;

namespace InventoryManagementMVC.Controllers
{
    public class ProductController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = ReadBase(request, typeof (ProductViewModel), typeof (Product),
                ContextFactory.Current.Products.ToList());
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var result = CreateBase(request, products, typeof (ProductViewModel), typeof (Product));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var result = UpdateBase(request, products, typeof (ProductViewModel), typeof (Product));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var result = DestroyBase(request, products, typeof (ProductViewModel), typeof (Product));
            return result;
        }

        public ActionResult ReadProductRecipies(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            var result = ReadBase(request, typeof (RecipeViewModel), typeof (Recipe),
                ContextFactory.Current.Recipes.Where(r => r.ProductIngredients.Any(pi => pi.ProductId == productId))
                    .ToList());
            return result;
        }

        public ActionResult ReadProductInventories(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<ProductInventory> productInventories =
                ContextFactory.Current.Inventories.OfType<ProductInventory>()
                    .Where(pi => pi.ProductId == productId)
                    .OrderByDescending(de => de.ProductInventoryHeader.ForDate)
                    .ToList();

            var result = ReadBase(request, typeof (ProductInventoryViewModel), typeof (ProductInventory),
                productInventories);
            return result;
        }

        public ActionResult ReadProductPurchaseOrders(int? productId, [DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderDetail> productPurchaseOrders =
                ContextFactory.Current.PurchaseOrderDetails.Where
                    (pi => pi.ProductId == productId)
                    .OrderByDescending(de => de.PurchaseOrderHeader.OrderDate)
                    .ToList();
            List<PurchaseOrderDetailViewModel> productPurchaseOrdersModels = productPurchaseOrders.Select
                (r =>
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(r,
                        new PurchaseOrderDetailViewModel()))
                .ToList();

            return Json(productPurchaseOrdersModels.ToDataSourceResult(request));
        }
    }
}