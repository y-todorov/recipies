//using InventoryManagementMVC.Models;
//using Kendo.Mvc.UI;
//using RecipiesModelNS;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Kendo.Mvc.Extensions;
//using System.Data.Entity;

//namespace InventoryManagementMVC.Controllers
//{
//    public class ProductInventoryHeaderController : Controller
//    {
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
//        {
//            List<ProductInventoryHeaderViewModel> productInventoriesViewModels =
//                ContextFactory.Current.ProductInventoryHeaders.ToList().Select
//                    (pi =>
//                        ProductInventoryHeaderViewModel.ConvertFromProductInventoryHeaderEntity(pi,
//                            new ProductInventoryHeaderViewModel()))
//                    .ToList();
//            return Json(productInventoriesViewModels.ToDataSourceResult(request));
//        }

//        [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
//            [Bind(Prefix = "models")] IEnumerable<ProductInventoryHeaderViewModel> pihModels)
//        {
//            if (pihModels != null && ModelState.IsValid)
//            {
//                foreach (ProductInventoryHeaderViewModel pihModel in pihModels)
//                {
//                    ProductInventoryHeader pihEntity =
//                        ProductInventoryHeaderViewModel.ConvertToProductInventoryHeaderEntity(pihModel,
//                            new ProductInventoryHeader());


//                    ContextFactory.Current.ProductInventoryHeaders.Add(pihEntity);
//                    ContextFactory.Current.SaveChanges();

//                    List<Product> allProducts = ContextFactory.Current.Products.ToList();
//                    // Move this to the database project in ProductInventoryHeader
//                    foreach (Product product in allProducts)
//                    {
//                        if (product.ProductId == 224)
//                        {
//                        }
//                        ProductInventory pi = new ProductInventory();
//                        pi.ProductId = product.ProductId;
//                        //pi.ForDate = pihModel.ForDate;
//                        pi.AverageUnitPrice = product.UnitPrice;
//                        try
//                        {
//                            pi.QuantityByDocuments =
//                                product.GetQuantityByDocumentsForDate(pihModel.ForDate.GetValueOrDefault());
//                        }
//                        catch (Exception ex)
//                        {
//                            throw;
//                        }

//                        pi.ProductInventoryHeaderId = pihEntity.ProductInventoryHeaderId;

//                        ContextFactory.Current.Inventories.Add(pi);
//                        ContextFactory.Current.SaveChanges();

//                        //product.UnitPrice.GetValueOrDefault().ToString();
//                        //quantityByDocumentsRadNumericTextBox.Text =
//                        //    product.GetQuantityByDocumentsForDate(
//                        //        ForDateRadDateTimePicker.SelectedDate.GetValueOrDefault()).ToString();
//                    }

//                    //ContextFactory.Current.SaveChanges();

//                    ProductInventoryHeaderViewModel.ConvertFromProductInventoryHeaderEntity(pihEntity, pihModel);
//                }
//            }

//            return Json(pihModels.ToDataSourceResult(request, ModelState));
//        }

//        [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
//            [Bind(Prefix = "models")] IEnumerable<ProductInventoryHeaderViewModel> pihs)
//        {
//            if (pihs.Any())
//            {
//                foreach (ProductInventoryHeaderViewModel pihModel in pihs)
//                {
//                    ProductInventoryHeader inv =
//                        ContextFactory.Current.ProductInventoryHeaders.FirstOrDefault(
//                            p => p.ProductInventoryHeaderId == pihModel.ProductInventoryHeaderId);
//                    ContextFactory.Current.ProductInventoryHeaders.Remove(inv);

//                    //List<ProductInventory> pis = ContextFactory.Current.Inventories.OfType<ProductInventory>().Where(pi => pi.ProductInventoryHeaderId == pihModel.ProductInventoryHeaderId).ToList();

//                    //foreach (ProductInventory pInventory in pis)
//                    //{
//                    //    ContextFactory.Current.Inventories.Remove(pInventory);
//                    //}

//                    ContextFactory.Current.SaveChanges();
//                }
//            }

//            return Json(pihs.ToDataSourceResult(request, ModelState));
//        }
//    }
//}