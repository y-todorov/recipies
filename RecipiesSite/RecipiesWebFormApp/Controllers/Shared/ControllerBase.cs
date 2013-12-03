﻿using Autofac;
using DevTrends.MvcDonutCaching;
using DevTrends.MvcDonutCaching.Annotations;
using Kendo.Mvc.UI;
using RecipiesWebFormApp.Caching;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementMVC.Controllers
{
    [DonutOutputCache(Duration = 24 * 3600, Options= OutputCacheOptions.IgnoreFormData)]
    public class ControllerBase : Controller
    {  
        public long ActionMilliseconds { get; set; }

        public long ResultMilliseconds { get; set; }

        private Stopwatch stopwatch = new Stopwatch();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //stopwatch.Start();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
            //stopwatch.Stop();
           // ActionMilliseconds = stopwatch.ElapsedMilliseconds;
            //ViewData.Add("ActionMilliseconds", ActionMilliseconds);
            stopwatch.Reset();
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
           // stopwatch.Start();
            base.OnResultExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //base.OnResultExecuted(filterContext);
            //stopwatch.Stop();
            //ResultMilliseconds = stopwatch.ElapsedMilliseconds;
            //ViewData.Add("ResultMilliseconds", ResultMilliseconds);
            stopwatch.Reset();
        }
    }
}