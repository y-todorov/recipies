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
            Timer timer = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds); // 10 minutes
            timer.Elapsed += timer_Elapsed;
            timer.Start();
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
            // Code that runs when an unhandled error occurs           

            
        }
    }
}
