using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace RecipiesWebFormApp.Shared
{
    public static class LogentriesHelper
    {
        public static ILog ApplicationLog;

        static LogentriesHelper()
        {
            ApplicationLog = LogManager.GetLogger(typeof(HttpApplication));
        }
    }
}