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

            // Yordan, test that site is not asleep after 20 mins. of inactivity
            // By the way this works very well :)
            Timer timerRequestPage = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds); // 10 minutes
            timerRequestPage.Elapsed += timer_Elapsed;
            timerRequestPage.Start();

            // Test for database refresh
            Timer timerCheckDatabaseForChanges = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            timerCheckDatabaseForChanges.Elapsed += timerCheckDatabaseForChanges_Elapsed;
            //timerCheckDatabaseForChanges.Start();

            //int mnt = System.Threading.Thread.CurrentThread.ManagedThreadId;

        }

        void timerCheckDatabaseForChanges_Elapsed(object sender, ElapsedEventArgs e)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            DateTime? lastModifiedDate = ContextFactory.GetContextPerRequest().Products.OrderByDescending(p => p.ModifiedDate).Select( p => p.ModifiedDate).FirstOrDefault();
            if (Application["ProductDate"] == null)
            {
                Application.Add("ProductDate", lastModifiedDate);
            }
            else
            {
                DateTime? lmd = Application["ProductDate"] as DateTime?;
                if (lmd.Value != lastModifiedDate.Value)
                {
                    //PubNubMessaging.Core.Pubnub.Instance.Publish("Products", "rebind", (t) => t.ToString(), (t) => t.ToString());
                }
                Application["ProductDate"] = lastModifiedDate;
            }


            s.Stop();
            var mills = s.ElapsedMilliseconds;
            var ticks = s.ElapsedTicks;
            //int mnt = System.Threading.Thread.CurrentThread.ManagedThreadId;

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
                      
        }
    }
}
