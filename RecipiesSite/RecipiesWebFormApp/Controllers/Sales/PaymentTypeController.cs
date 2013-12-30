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
            var result = base.ReadBase(request, typeof(PaymentTypeViewModel), typeof(PaymentType));
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PaymentTypeViewModel> paymentTypes)
        {
            
            if (paymentTypes != null && ModelState.IsValid)
            {
                foreach (PaymentTypeViewModel paymentTypeViewModel in paymentTypes)
                {
                    PaymentType newEntity = paymentTypeViewModel.ConvertToEntity(new PaymentType());
                      
                    ContextFactory.Current.PaymentTypes.Add(newEntity);
                    ContextFactory.Current.SaveChanges();

                    paymentTypeViewModel.ConvertFromEntity(newEntity);
                    //PaymentTypeViewModel.ConvertFromPaymentTypeEntity(newEntity, paymentTypeViewModel);
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

                    paymentType.ConvertToEntity(entity);
                    //PaymentTypeViewModel.ConvertToPaymentTypeEntity(paymentType, entity);

                    ContextFactory.Current.SaveChanges();

                    paymentType.ConvertFromEntity(entity);
                    //PaymentTypeViewModel.ConvertFromPaymentTypeEntity(entity, paymentType);
                }
            }

            return Json(paymentTypes.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PaymentTypeViewModel> paymentTypes)
        {
            var result = base.DestroyBase(request, paymentTypes, typeof(PaymentTypeViewModel), typeof(PaymentType));
            return result;

            //Type modelType = typeof(PaymentTypeViewModel);
            // Type entityType = typeof(PaymentType);

            //DbSet dbset = ContextFactory.Current.Set(entityType);
            //dbset.Load();
            //var en = dbset.Local.GetEnumerator();

            ////dbset.Remove()

            //List<object> result = new List<object>();
            //while (en.MoveNext())
            //{
            //    dynamic newModel = Activator.CreateInstance(modelType);
            //    dynamic newEntity = Activator.CreateInstance(entityType);
            //    newEntity = en.Current;
            //    var modelToAdd = newModel.ConvertFromEntity(newEntity);
            //    if (paymentTypes.Any(m => m == modelToAdd))
            //    {
            //        result.Add(en.Current);
            //    }
            //}

            //foreach (var o in result)
            //{
            //    dbset.Remove(o);
            //    ContextFactory.Current.SaveChanges();
            //}



            //foreach (PaymentTypeViewModel paymentType in paymentTypes)
            //{
            //    foreach (object pt in result)
            //    {
            //        if (paymentType.ConvertToEntity((PaymentType)pt) == pt)
            //        {

            //            dbset.Remove(pt);
            //        }
            //    }
                

            //    //PaymentType entity =
            //    //    ContextFactory.Current.PaymentTypes.FirstOrDefault(
            //    //        c => c.PaymentTypeId == paymentType.PaymentTypeId);
            //    //ContextFactory.Current.PaymentTypes.Remove(entity);

            //    ContextFactory.Current.SaveChanges();
            //}

            return Json(paymentTypes.ToDataSourceResult(request, ModelState));
        }
    }
}