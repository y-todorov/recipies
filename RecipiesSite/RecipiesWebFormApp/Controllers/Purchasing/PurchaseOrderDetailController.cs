﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using InventoryManagementMVC.Models.Purchasing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System.Data.Entity;
using DevTrends.MvcDonutCaching; // .Include !!!!!!! THIS IS SO IMPROTANT

namespace InventoryManagementMVC.Controllers
{
    public class PurchaseOrderDetailController : Controller
    {
        [DonutOutputCache(Duration = 24 * 3600)]
        public ActionResult Index()
        {
            List<PurchaseOrderDetailViewModel> purchaseOrderDetailViewModels =
                ContextFactory.Current.PurchaseOrderDetails
                    .Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory)
                    .ToList().Select
                    (pod =>
                        PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pod,
                            new PurchaseOrderDetailViewModel())).ToList();

            return View(purchaseOrderDetailViewModels);
        }

        [DonutOutputCache(Duration = 24 * 3600)]
        public ActionResult Read(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request)
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
        public ActionResult Create(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            if (purchaseOrderDetails != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderDetailViewModel podViewModel in purchaseOrderDetails)
                {
                    podViewModel.PurchaseOrderHeaderId = purchaseOrderHeaderId;
                    PurchaseOrderDetail newPodEntity =
                        PurchaseOrderDetailViewModel.ConvertToPurchaseOrderDetailEntity(podViewModel, new PurchaseOrderDetail());
                    ContextFactory.Current.PurchaseOrderDetails.Add(newPodEntity);
                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    newPodEntity = ContextFactory.Current.PurchaseOrderDetails.Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory).FirstOrDefault(pod => pod.PurchaseOrderDetailId == newPodEntity.PurchaseOrderDetailId);
                     PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(newPodEntity, podViewModel);
                }
            }

            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
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
                    pohEntity = ContextFactory.Current.PurchaseOrderDetails.Include(pod => pod.PurchaseOrderHeader.Vendor)
                   .Include(pod => pod.Product.ProductCategory).FirstOrDefault(pod => pod.PurchaseOrderDetailId == pohEntity.PurchaseOrderDetailId);
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pohEntity, podViewModel);
                }
            }

            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
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
        }
    }
}