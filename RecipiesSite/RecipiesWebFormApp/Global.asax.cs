using System;
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

                switch (httpEx.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;

                    // others if any
                }
            }

            //httpContext.ClearError();
            //httpContext.Response.Clear();
            //httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.StatusCode = 200;

            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));


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