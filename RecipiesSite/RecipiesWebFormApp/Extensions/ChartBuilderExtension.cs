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

            builder.Series(series =>
                {
                    bool isVisible = true;
                    int counter = 0;
                    foreach (Vendor vendor in vendors)
                    {
                        series.Line("EscapeStringYordan_" + vendor.VendorId.ToString()).Name(vendor.Name).Labels(l => l.Format("{0:C3}")).Visible(isVisible).Axis("Value");
                        counter++;
                        if (counter >= 3)
                        {
                            isVisible = false;
                        }
                        // only the first 3 will be visible by default
                    }

                });


            return builder;
        }
    }
}