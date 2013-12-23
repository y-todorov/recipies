using InventoryManagementMVC.Models.Purchasing;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using System.IO;
using Telerik.Reporting.Processing;
using Helpers;
using RestSharp;
using System.Net;
using System.Data.Entity;
using InventoryManagementMVC.Models;

namespace InventoryManagementMVC.Controllers.Purchasing
{
    public class SalesOrderHeaderController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SalesOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.SalesOrderHeaders
                    .ToList().Select
                    (pod =>
                        SalesOrderHeaderViewModel.ConvertFromSalesOrderHeaderEntity(pod,
                            new SalesOrderHeaderViewModel())).ToList();
            return Json(purchaseOrderHeaderViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SalesOrderHeaderViewModel> salesOrderHeaders)
        {
            if (salesOrderHeaders != null && ModelState.IsValid)
            {
                foreach (SalesOrderHeaderViewModel sohViewModel in salesOrderHeaders)
                {
                    SalesOrderHeader newPohEntity =
                        SalesOrderHeaderViewModel.ConvertToSalesOrderHeaderEntity(sohViewModel,
                            new SalesOrderHeader());
                    ContextFactory.Current.SalesOrderHeaders.Add(newPohEntity);
                    ContextFactory.Current.SaveChanges();

                    SalesOrderHeaderViewModel.ConvertFromSalesOrderHeaderEntity(newPohEntity, sohViewModel);
                }
            }

            return Json(salesOrderHeaders.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SalesOrderHeaderViewModel> sohs)
        {
            if (sohs != null && ModelState.IsValid)
            {
                foreach (SalesOrderHeaderViewModel sohViewModel in sohs)
                {
                    SalesOrderHeader pohEntity =
                        ContextFactory.Current.SalesOrderHeaders.FirstOrDefault(
                            c => c.SalesOrderHeaderId == sohViewModel.SalesOrderHeaderId);

                    SalesOrderHeaderViewModel.ConvertToSalesOrderHeaderEntity(sohViewModel, pohEntity);

                    ContextFactory.Current.SaveChanges();

                    SalesOrderHeaderViewModel.ConvertFromSalesOrderHeaderEntity(pohEntity, sohViewModel);
                }
            }

            return Json(sohs.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SalesOrderHeaderViewModel> sohs)
        {
            foreach (SalesOrderHeaderViewModel poh in sohs)
            {
                SalesOrderHeader pod =
                    ContextFactory.Current.SalesOrderHeaders.FirstOrDefault(
                        c => c.SalesOrderHeaderId == poh.SalesOrderHeaderId);
                ContextFactory.Current.SalesOrderHeaders.Remove(pod);

                ContextFactory.Current.SaveChanges();
            }

            return Json(sohs.ToDataSourceResult(request, ModelState));
        }


        public ActionResult ReadDetail(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderDetailViewModel> purchaseOrderDetailViewModels =
                ContextFactory.Current.PurchaseOrderDetails.Where(
                    pod => purchaseOrderHeaderId.HasValue ? pod.PurchaseOrderId == purchaseOrderHeaderId.Value : true)
                    .Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory)
                    .ToList().Select
                    (pod =>
                        PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pod,
                            new PurchaseOrderDetailViewModel())).ToList();
            return Json(purchaseOrderDetailViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateDetail(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            if (purchaseOrderDetails != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderDetailViewModel podViewModel in purchaseOrderDetails)
                {
                    podViewModel.PurchaseOrderHeaderId = purchaseOrderHeaderId;
                    PurchaseOrderDetail newPodEntity =
                        PurchaseOrderDetailViewModel.ConvertToPurchaseOrderDetailEntity(podViewModel,
                            new PurchaseOrderDetail());
                    ContextFactory.Current.PurchaseOrderDetails.Add(newPodEntity);
                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    newPodEntity = ContextFactory.Current.PurchaseOrderDetails.Include(
                        pod => pod.PurchaseOrderHeader.Vendor)
                        .Include(pod => pod.Product.ProductCategory)
                        .FirstOrDefault(pod => pod.PurchaseOrderDetailId == newPodEntity.PurchaseOrderDetailId);
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(newPodEntity, podViewModel);
                }
            }
            //return View("Index");
            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            if (purchaseOrderDetails != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderDetailViewModel podViewModel in purchaseOrderDetails)
                {
                    PurchaseOrderDetail pohEntity =
                        ContextFactory.Current.PurchaseOrderDetails.FirstOrDefault(
                            c => c.PurchaseOrderDetailId == podViewModel.PurchaseOrderDetailId);

                    PurchaseOrderDetailViewModel.ConvertToPurchaseOrderDetailEntity(podViewModel, pohEntity);

                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    pohEntity = ContextFactory.Current.PurchaseOrderDetails.Include(
                        pod => pod.PurchaseOrderHeader.Vendor)
                        .Include(pod => pod.Product.ProductCategory)
                        .FirstOrDefault(pod => pod.PurchaseOrderDetailId == pohEntity.PurchaseOrderDetailId);
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pohEntity, podViewModel);
                }
            }

            //return View("Index");

            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyDetail([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            foreach (PurchaseOrderDetailViewModel pod in purchaseOrderDetails)
            {
                PurchaseOrderDetail podEntity =
                    ContextFactory.Current.PurchaseOrderDetails.FirstOrDefault(
                        c => c.PurchaseOrderDetailId == pod.PurchaseOrderDetailId);
                ContextFactory.Current.PurchaseOrderDetails.Remove(podEntity);

                ContextFactory.Current.SaveChanges();
            }

            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
            //return View("Index");
        }
    }
}