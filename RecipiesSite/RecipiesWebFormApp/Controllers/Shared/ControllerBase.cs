using System.Data.Entity;
using Autofac;
using DevTrends.MvcDonutCaching;
using DevTrends.MvcDonutCaching.Annotations;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesWebFormApp.Caching;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Collections;

namespace InventoryManagementMVC.Controllers
{
    [DonutOutputCache(Duration = 24*3600,
        Options = OutputCacheOptions.IgnoreFormData | OutputCacheOptions.NoCacheLookupForPosts)]
    public class ControllerBase : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            JsonResult jr = base.Json(data, contentType, contentEncoding, behavior);
            jr.MaxJsonLength = int.MaxValue;
            return jr;
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            JsonResult jr = base.Json(data, contentType, contentEncoding);
            jr.MaxJsonLength = int.MaxValue;
            return jr;
        }

        public ActionResult ReadBase([DataSourceRequest] DataSourceRequest request, Type modelType, Type entityType)
        {
            DbSet dbset = ContextFactory.Current.Set(entityType);
            dbset.Load();
            var en = dbset.Local.GetEnumerator();

            List<object> result = new List<object>();
            while (en.MoveNext())
            {
                dynamic newModel = Activator.CreateInstance(modelType);
                dynamic newEntity = Activator.CreateInstance(entityType);
                newEntity = en.Current;
                var modelToAdd = newModel.ConvertFromEntity(newEntity);
                result.Add(modelToAdd);
            }
            return Json(result.ToDataSourceResult(request));
        }

    }
}