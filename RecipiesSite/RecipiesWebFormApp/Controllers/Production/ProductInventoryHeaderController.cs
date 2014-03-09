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

        public ActionResult ReadProductInventoryPurchaseOrders(int? productInventoryHeaderId, [DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                ProductInventoryHeader pih =
                    ContextFactory.Current.ProductInventoryHeaders.FirstOrDefault(
                        p => p.ProductInventoryHeaderId == productInventoryHeaderId);

                List<PurchaseOrderDetail> purchaseOrders = new List<PurchaseOrderDetail>();

                if (pih != null)
                {
                    DateTime toDate = pih.ForDate.GetValueOrDefault().Date;

                    // Get last inventory before the current inventory

                    ProductInventoryHeader pihLast =
                        ContextFactory.Current.ProductInventoryHeaders.Where(
                            p => p.ForDate < toDate).OrderByDescending(p => p.ForDate).FirstOrDefault();

                    DateTime fromDate = DateTime.Now.AddYears(-100);

                    if (pihLast != null)
                    {
                        fromDate = pihLast.ForDate.GetValueOrDefault().Date;
                    }

                    List<Product> allProducts = ContextFactory.Current.Products.ToList();

                    foreach (Product product in allProducts)
                    {
                        // Get only purchase orders with lineTotal != 0;
                        purchaseOrders.AddRange(product.GetPurchaseOrderDetailsInPeriod(fromDate, toDate).Where(p => Math.Round(p.LineTotal, 4) != 0));
                    }

                }


                var result = ReadBase(request, typeof(PurchaseOrderDetailViewModel), typeof(PurchaseOrderDetail),
                 purchaseOrders);

                return result;
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }
            return null;
        }

        public ActionResult ReadProductInventoryWastes(int? productInventoryHeaderId, [DataSourceRequest] DataSourceRequest request)
        {
            List<ProductWaste> productWastes =
                ContextFactory.Current.Wastes.OfType<ProductWaste>().Where
                    (pi => pi.ProductId == productInventoryHeaderId).ToList().Where(w => w.Quantity.GetValueOrDefault() != 0)
                    .OrderByDescending(de => de.ProductWasteHeader.ForDate)
                    .ToList();
            var result = ReadBase(request, typeof(ProductWasteViewModel), typeof(ProductWaste),
                productWastes);


            return result;

            return null;
        }
    }
}