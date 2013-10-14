using System;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;
using System.Net;
using RecipiesModelNS;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;

//using System.Threading;

//using PubNubMessaging.Core;

namespace RecipiesWebFormApp
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
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

            // Test for database refresh
            Timer timerCheckDatabaseForChanges = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            timerCheckDatabaseForChanges.Elapsed += timerCheckDatabaseForChanges_Elapsed;
            //timerCheckDatabaseForChanges.Start();

            //int mnt = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        private void timerCheckDatabaseForChanges_Elapsed(object sender, ElapsedEventArgs e)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            //MethodInfo method = ContextFactory.GetContextPerRequest().GetType().GetMethod("GetAll");
            //MethodInfo generic = method.MakeGenericMethod(typeof(Product));
            //var res = generic.Invoke(ContextFactory.GetContextPerRequest(), null) as IEnumerable;

            DateTime? lastModifiedDate =
                ContextFactory.GetContextPerRequest()
                    .Products.OrderByDescending(p => p.ModifiedDate)
                    .Select(p => p.ModifiedDate)
                    .FirstOrDefault();
            if (Application["ProductDate"] == null)
            {
                Application.Add("ProductDate", lastModifiedDate);
            }
            else
            {
                DateTime? lastModifiedDateCacheValue = Application["ProductDate"] as DateTime?;
                if (lastModifiedDateCacheValue.Value != lastModifiedDate.Value)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();
                    // Here we refresh only grids with ItemType product. We should do notification system on wathcing the sql database. SQL WATCH or something
                    context.Clients.Group(typeof (Product).FullName).rebindRadGrid();
                    Application["ProductDate"] = lastModifiedDate.Value;
                    return;
                }
                Application["ProductDate"] = lastModifiedDate;
            }

            int lastProductCount = ContextFactory.GetContextPerRequest().Products.Count();
            if (Application["ProductCount"] == null)
            {
                Application.Add("ProductCount", lastProductCount);
            }
            else
            {
                int productCountCacheValue = (int) Application["ProductCount"];
                if (productCountCacheValue != lastProductCount)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();

                    // Here we refresh only grids with ItemType product. We should do notification system on wathcing the sql database. SQL WATCH or something
                    context.Clients.Group(typeof (Product).FullName).rebindRadGrid();
                    Application["ProductCount"] = lastProductCount;
                    return;
                }
            }


            s.Stop();
            var mills = s.ElapsedMilliseconds;
            var ticks = s.ElapsedTicks;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            string res = client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/")).Result;
            res = client.DownloadStringTaskAsync(new Uri("http://recipies.azurewebsites.net/")).Result;
        }

        private void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            string exMessage = ex.Message;
            string exStackTrace = ex.StackTrace;
            Exception innerEx = ex.InnerException;
            if (innerEx != null)
            {
                string innerExMessage = innerEx.Message;
                string innerExstackTrace = innerEx.StackTrace;
                Debugger.Break();
            }
            Debugger.Break();
        }
    }
}