using Quartz;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipiesWebFormApp.Quartz.Jobs
{
    public class RefreshWebsiteJob : IJob
    {
        public void Execute(IJobExecutionContext context)
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
    }
}