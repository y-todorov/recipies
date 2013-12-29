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
    public class PaymentTypeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<PaymentTypeViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PaymentTypes
                    .ToList().Select
                    (pod =>
                        PaymentTypeViewModel.ConvertFromPaymentTypeEntity(pod,
                            new PaymentTypeViewModel())).ToList();
            return Json(purchaseOrderHeaderViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PaymentTypeViewModel> paymentTypes)
        {
            if (paymentTypes != null && ModelState.IsValid)
            {
                foreach (PaymentTypeViewModel paymentTypeViewModel in paymentTypes)
                {
                    PaymentType newEntity =
                        PaymentTypeViewModel.ConvertToPaymentTypeEntity(paymentTypeViewModel,
                            new PaymentType());
                    ContextFactory.Current.PaymentTypes.Add(newEntity);
                    ContextFactory.Current.SaveChanges();

                    PaymentTypeViewModel.ConvertFromPaymentTypeEntity(newEntity, paymentTypeViewModel);
                }
            }

            return Json(paymentTypes.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PaymentTypeViewModel> paymentTypes)
        {
            if (paymentTypes != null && ModelState.IsValid)
            {
                foreach (PaymentTypeViewModel paymentType in paymentTypes)
                {
                    PaymentType entity =
                        ContextFactory.Current.PaymentTypes.FirstOrDefault(
                            c => c.PaymentTypeId == paymentType.PaymentTypeId);

                    PaymentTypeViewModel.ConvertToPaymentTypeEntity(paymentType, entity);

                    ContextFactory.Current.SaveChanges();

                    PaymentTypeViewModel.ConvertFromPaymentTypeEntity(entity, paymentType);
                }
            }

            return Json(paymentTypes.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PaymentTypeViewModel> paymentTypes)
        {
            foreach (PaymentTypeViewModel paymentType in paymentTypes)
            {
                PaymentType entity =
                    ContextFactory.Current.PaymentTypes.FirstOrDefault(
                        c => c.PaymentTypeId == paymentType.PaymentTypeId);
                ContextFactory.Current.PaymentTypes.Remove(entity);

                ContextFactory.Current.SaveChanges();
            }

            return Json(paymentTypes.ToDataSourceResult(request, ModelState));
        }
    }
}