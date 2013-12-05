﻿using System;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;
using System.Net;
using Kendo.Mvc;
using RecipiesModelNS;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using RecipiesWebFormApp.Caching;
using System.Reflection;
using System.Collections.Specialized;
using RecipiesWebFormApp.Controllers.Shared;

namespace RecipiesWebFormApp
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            if (!SiteMapManager.SiteMaps.ContainsKey("sitemap"))
            {
                SiteMapManager.SiteMaps.Register<XmlSiteMap>("sitemap", sitemap =>
                    sitemap.LoadFrom("~/sitemap.sitemap"));
            }

            // Code that runs on application startup
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HubConfiguration hubConfig = new HubConfiguration
            {
                EnableCrossDomain = true,
                EnableDetailedErrors = true
            };
            RouteTable.Routes.MapHubs(hubConfig);

            // Yordan, test that site is not asleep after 20 mins. of inactivity
            // By the way this works very well :)
            Timer timerRequestPage = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds); // 10 minutes
            timerRequestPage.Elapsed += timer_Elapsed;
            timerRequestPage.Start();

            DatabaseTableChangeWatcher.StartWathching(1);
            DatabaseTableChangeWatcher.DatabaseChange += DatabaseTableChangeWatcher_DatabaseChange;
        }

        void DatabaseTableChangeWatcher_DatabaseChange(object arg1, EventArgs arg2)
        {
            MyCacheManager.Instance.RemoveItems();
            var controllersType = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.ReflectedType != null &&
                t.ReflectedType.BaseType.Name == typeof(ControllerBase).Name).ToList();

            foreach (Type ct in controllersType)
            {
                string controllerFullName = ct.ReflectedType.Name;
                string controllerName = controllerFullName.Substring(0, controllerFullName.IndexOf("Controller"));

                var instance = Activator.CreateInstance(ct.ReflectedType);
                var res = ct.ReflectedType.GetMethod("Index").Invoke(instance, null);
            }

            //        //CookieAwareWebClient client = new CookieAwareWebClient();

            //        using (var client = new CookieAwareWebClient())
            //        {
            //            var values = new NameValueCollection
            //{
            //    { "username", "admin" },
            //    { "password", "admin1!!" },
            //};
            //            client.UploadValues("http://bluesystems.azurewebsites.net", values);

            //            // If the previous call succeeded we now have a valid authentication cookie
            //            // so we could download the protected page
            //            //string result = client.DownloadString("http://domain.loc/testpage.aspx");


            //            foreach (Type ct in controllersType)
            //            {
            //                string controllerFullName = ct.ReflectedType.Name;
            //                string controllerName = controllerFullName.Substring(0, controllerFullName.IndexOf("Controller"));
            //                string res = client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/" + controllerName)).Result;
            //                client.UploadValues("http://bluesystems.azurewebsites.net/" + controllerName, values);
            //                res = client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/" + controllerName)).Result;
            //            }
            //        }


        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                string res = client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/")).Result;
            }
            catch (Exception ex)
            {
                // no need to rethrow here
            }
        }

        private void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        private void Application_Error(object sender, EventArgs e)
        {
            //Exception ex = Server.GetLastError();
            //string exMessage = ex.Message;
            //string exStackTrace = ex.StackTrace;
            //Exception innerEx = ex.InnerException;
            //if (innerEx != null)
            //{
            //    string innerExMessage = innerEx.Message;
            //    string innerExstackTrace = innerEx.StackTrace;
            //    Debugger.Break();
            //}
            //Debugger.Break();

            var httpContext = ((HttpApplication)sender).Context;
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = Server.GetLastError();
            var controller = new ErrorController();
            var routeData = new RouteData();
            var action = "Index";

            if (ex is HttpException)
            {
                var httpEx = ex as HttpException;
                int exceptionHttpStatusCode = httpEx.GetHttpCode();
                switch (exceptionHttpStatusCode)
                {
                    case 404:
                        action = "NotFound";
                        break;
                    case 500:
                        action = "InternalServerError";
                        break;
                    // others if any
                }
            }


            //test

            if (IsAjaxRequest())
            {
                Response.Write("Your JSON here");
                //We clear the error
                httpContext.Response.StatusCode = 500;
                Server.ClearError();
                return;
            }


            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            //but this is importan. It helps the brousers to display a good error page
            httpContext.Response.StatusCode = 200;

            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));


        }

        //This method checks if we have an AJAX request or not
        private bool IsAjaxRequest()
        {
            //The easy way
            bool isAjaxRequest = (Request["X-Requested-With"] == "XMLHttpRequest")
            || ((Request.Headers != null)
            && (Request.Headers["X-Requested-With"] == "XMLHttpRequest"));

            //If we are not sure that we have an AJAX request or that we have to return JSON 
            //we fall back to Reflection
            if (!isAjaxRequest)
            {
                try
                {
                    //The controller and action
                    string controllerName = Request.RequestContext.
                                            RouteData.Values["controller"].ToString();
                    string actionName = Request.RequestContext.
                                        RouteData.Values["action"].ToString();

                    //We create a controller instance
                    DefaultControllerFactory controllerFactory = new DefaultControllerFactory();
                    Controller controller = controllerFactory.CreateController(
                    Request.RequestContext, controllerName) as Controller;

                    //We get the controller actions
                    ReflectedControllerDescriptor controllerDescriptor =
                    new ReflectedControllerDescriptor(controller.GetType());
                    ActionDescriptor[] controllerActions =
                    controllerDescriptor.GetCanonicalActions();

                    //We search for our action
                    foreach (ReflectedActionDescriptor actionDescriptor in controllerActions)
                    {
                        if (actionDescriptor.ActionName.ToUpper().Equals(actionName.ToUpper()))
                        {
                            //If the action returns JsonResult then we have an AJAX request
                            if (actionDescriptor.MethodInfo.ReturnType
                            .Equals(typeof(JsonResult)))
                                return true;
                        }
                    }
                }
                catch
                {

                }
            }

            return isAjaxRequest;
        }
   
    }
    
    public class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }
        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }
}