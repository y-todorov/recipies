using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System.Web;
using System.Web.Mvc;
using System.Web.Security.AntiXss;
using System.Web.Util;
using System.Diagnostics;
using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;
using System.Collections;
using System.Collections.Generic;

namespace InventoryManagementMVC.Extensions
{
    public static class ChartBuilder
    {
        public static ChartBuilder<T> AddVendorsValuePerWeekOptions<T>(this ChartBuilder<T> builder) where T : class
        {
            List<Vendor> vendors = ContextFactory.Current.Vendors.ToList();

            Vendor fakeTotalVendor = new Vendor()
            {
                Name = "Total All Vendors",
                VendorId = 0
            };
            vendors.Insert(0, fakeTotalVendor);


            Stopwatch sw = new Stopwatch();
            sw.Start();

            builder.Series(series =>
            {
                bool isVisible = true;
                int counter = 0;
                foreach (Vendor vendor in vendors)
                {
                    series.Line("EscapeStringYordan_" + vendor.VendorId.ToString())
                        .Name(vendor.Name)
                        .Labels(l => l.Format("{0:C3}").Visible(true))
                        .Axis("Value")
                        .Visible(isVisible);

                        //.Tooltip(tp => tp.Template("Time interval:<label id=\"lblVendorPO\"></label> <script> document.getElementById(\"lblVendorPO\").innerHTML = getWeekString(#= category #)</script>Vendor: #= series.name # Value: #= value #"));
                    counter++;
                    if (counter >= 3)
                    {
                        isVisible = false;
                    }
                    // only the first 3 will be visible by default
                }
            });

            sw.Stop();
            long mils = sw.ElapsedMilliseconds;

            return builder;
        }
    }
}