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

namespace RecipiesWebFormApp.Extensions
{
    public static class ChartBuilder
    {
        public static ChartBuilder<T> AddVendorsValuePerWeekOptions<T>(this ChartBuilder<T> builder) where T : class
        {
            List<Vendor> vendors = ContextFactory.Current.Vendors.ToList();


            builder.Series(series =>
                
               series.Line("VendorValue").Name("SUPERDAWN FRESH").Labels(l => l.Format("{0:C3}").Visible(true)).Axis("Value"));
                

            return builder;
        }
    }
}