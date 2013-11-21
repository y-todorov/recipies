using System;
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
    public class SalesOrderDetailController : ControllerBase
    {
        public ActionResult Read(int? salesOrderHeaderId, [DataSourceRequest] DataSourceRequest request)
        {
            List<SalesOrderDetailViewModel> salesOrderDetailViewModels =
                ContextFactory.Current.SalesOrderDetails.Where(
                    pod => salesOrderHeaderId.HasValue ? pod.SalesOrderHeaderId == salesOrderHeaderId.Value : true)
                    .ToList().Select
                    (pod =>
                        SalesOrderDetailViewModel.ConvertFromSalesOrderDetailEntity(pod,
                            new SalesOrderDetailViewModel())).ToList();
            return Json(salesOrderDetailViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int? salesOrderHeaderId, [DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SalesOrderDetailViewModel> salesOrderDetails)
        {
            if (salesOrderDetails != null && ModelState.IsValid)
            {
                foreach (SalesOrderDetailViewModel sodViewModel in salesOrderDetails)
                {
                    sodViewModel.SalesOrderHeaderId = salesOrderHeaderId;
                    SalesOrderDetail newPodEntity =
                        SalesOrderDetailViewModel.ConvertToSalesOrderDetailEntity(sodViewModel, new SalesOrderDetail());
                    ContextFactory.Current.SalesOrderDetails.Add(newPodEntity);
                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    SalesOrderDetailViewModel.ConvertFromSalesOrderDetailEntity(newPodEntity, sodViewModel);
                }
            }

            return Json(salesOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SalesOrderDetailViewModel> salesOrderDetails)
        {
            if (salesOrderDetails != null && ModelState.IsValid)
            {
                foreach (SalesOrderDetailViewModel sodViewModel in salesOrderDetails)
                {
                    SalesOrderDetail pohEntity =
                        ContextFactory.Current.SalesOrderDetails.FirstOrDefault(
                            c => c.SalesOrderDetailId == sodViewModel.SalesOrderDetailId);

                    SalesOrderDetailViewModel.ConvertToSalesOrderDetailEntity(sodViewModel, pohEntity);

                    ContextFactory.Current.SaveChanges();
                    SalesOrderDetailViewModel.ConvertFromSalesOrderDetailEntity(pohEntity, sodViewModel);
                }
            }

            return Json(salesOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SalesOrderDetailViewModel> salesOrderDetails)
        {
            foreach (SalesOrderDetailViewModel sod in salesOrderDetails)
            {
                SalesOrderDetail podEntity =
                    ContextFactory.Current.SalesOrderDetails.FirstOrDefault(
                        c => c.SalesOrderDetailId == sod.SalesOrderDetailId);
                ContextFactory.Current.SalesOrderDetails.Remove(podEntity);

                ContextFactory.Current.SaveChanges();
            }

            return Json(salesOrderDetails.ToDataSourceResult(request, ModelState));
        }
    }
}