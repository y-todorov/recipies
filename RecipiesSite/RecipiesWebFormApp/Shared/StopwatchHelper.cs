using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RecipiesWebFormApp.Shared
{
    public static class StopwatchHelper
    {
        private static Stopwatch stopwatch;

        public static void StartNewMeasurement()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public static long StopLastMeasurement()
        {
            stopwatch.Stop();
            long milliseconds = stopwatch.ElapsedMilliseconds;
            return milliseconds;
        }
    }
}
