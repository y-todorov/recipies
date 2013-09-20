using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using RecipiesWebFormApp;
using System.Timers;
using System.Net;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Text;
using RecipiesModelNS;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;
//using System.Threading;
using System.Globalization;
using System.Web.Caching;
using System.Reflection;
using System.Collections;
using Telerik.OpenAccess.Metadata;
using System.Threading.Tasks;
//using PubNubMessaging.Core;

namespace RecipiesWebFormApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
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
            Timer timerCheckDatabaseForChanges = new Timer(TimeSpan.FromSeconds(30).TotalMilliseconds);
            timerCheckDatabaseForChanges.Elapsed += timerCheckDatabaseForChanges_Elapsed;
            //timerCheckDatabaseForChanges.Start();

            int mnt = System.Threading.Thread.CurrentThread.ManagedThreadId;

        }

        void timerCheckDatabaseForChanges_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Factory.StartNew(() => CheckDatabaseForChanges());
            //CheckDatabaseForChanges();

        }

        private void CheckDatabaseForChanges()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            // It takse so fucking long for all types
            IList<MetaPersistentType> subTypes = ContextFactory.GetContextPerRequest().Metadata.PersistentTypes.Take(5).ToList();

            foreach (MetaPersistentType mpt in subTypes)
            {
                string tableName = string.Format("[{0}].[{1}]", mpt.Table.SchemaName, mpt.Table.Name);
                CheckTableForChanges(tableName, mpt.FullName);
            }


            s.Stop();
            var mills = s.ElapsedMilliseconds;
            var ticks = s.ElapsedTicks;
        }

        private async void CheckTableForChanges(string tableName, string persistentTypeFullName)
        {
            DateTime? lastModifiedDate = await Task.Factory.StartNew<DateTime?>(() => ContextFactory.GetContextPerRequest().ExecuteScalar<DateTime?>("SELECT [ModifiedDate] FROM " + tableName + " ORDER BY [ModifiedDate] DESC "));
            int lastProductCount = await Task.Factory.StartNew<int>(() => ContextFactory.GetContextPerRequest().ExecuteScalar<int>("SELECT COUNT(*) FROM  " + tableName)); //ContextFactory.GetContextPerRequest().Products.Count();
            string dateConstant = tableName + "Date";
            string countConstant = tableName + "Count";
            if (Application[dateConstant] == null)
            {
                Application.Add(dateConstant, lastModifiedDate);
            }
            else
            {
                DateTime? lastModifiedDateCacheValue = Application[dateConstant] as DateTime?;
                if (lastModifiedDateCacheValue.Value != lastModifiedDate.Value)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();
                    // Here we refresh only grids with ItemType product. We should do notification system on wathcing the sql database. SQL WATCH or something
                    context.Clients.Group(persistentTypeFullName).rebindRadGrid();
                    Application[dateConstant] = lastModifiedDate.Value;
                    Application[countConstant] = lastProductCount;
                    //return;
                }
                Application[dateConstant] = lastModifiedDate;
            }

            if (Application[countConstant] == null)
            {
                Application.Add(countConstant, lastProductCount);
            }
            else
            {
                int productCountCacheValue = (int)Application[countConstant];
                if (productCountCacheValue != lastProductCount)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<RebindHub>();

                    // Here we refresh only grids with ItemType product. We should do notification system on wathcing the sql database. SQL WATCH or something
                    context.Clients.Group(persistentTypeFullName).rebindRadGrid();
                    Application[dateConstant] = lastModifiedDate.Value;
                    Application[countConstant] = lastProductCount;
                }
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            string res = client.DownloadStringTaskAsync(new Uri("http://recipies.apphb.com/")).Result;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            Debugger.Break();
        }
    }
}
