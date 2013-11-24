using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models.Chart;
using RecipiesModelNS;
using System.Data.Entity;
using DevTrends.MvcDonutCaching;
using InventoryManagementMVC.Models.Chart;
using System.Globalization;
using System.Diagnostics;
using System.Dynamic;
using System.Data;

namespace InventoryManagementMVC.Controllers
{
    public class ChartController : ControllerBase
    {
        int maxXLabelTextLenght = 10;

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public ActionResult VendorPurchasesByWeek()
        {
            try
            {
                Vendor vendor;
                if (!string.IsNullOrEmpty("4"))
                {
                    int vendorId = int.Parse("4");
                    vendor =
                        ContextFactory.GetContextPerRequest()
                            .Vendors.Where(v => v.VendorId == vendorId)
                            .FirstOrDefault();
                }
                else
                {
                    vendor = ContextFactory.GetContextPerRequest().Vendors.FirstOrDefault();
                }
                List<PurchaseOrderDetail> pods =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderDetails.Where(
                            pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed)
                        .ToList();

                var grouping =
                    pods.OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate)
                        .GroupBy(pod => GetIso8601WeekOfYear(pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault()));

                if (!string.IsNullOrEmpty("4"))
                {
                    int vendorId = int.Parse("4");
                    vendor =
                        ContextFactory.GetContextPerRequest()
                            .Vendors.Where(v => v.VendorId == vendorId)
                            .FirstOrDefault();
                }
                else
                {
                    vendor = ContextFactory.GetContextPerRequest().Vendors.FirstOrDefault();
                }

                if (vendor == null)
                {
                    //return;
                }

                //List<VendorPurchasesByWeek> helpers = new List<VendorPurchasesByWeek>();
                //List<dynamic> helpers = new List<dynamic>();
                List<Dictionary<string, object>> helpers = new List<Dictionary<string, object>>();

                foreach (var item in grouping)
                {
                    //dynamic h = new ExpandoObject();// = new dynamic();// = new VendorPurchasesByWeek();

                    //dynamic h = new VendorPurchasesByWeek();
                    //h.Week = item.Key;
                    //h.VendorValue =
                    //    Math.Round(
                    //    item.Where(pod => pod.PurchaseOrderHeader.VendorId == vendor.VendorId).Sum(pod => pod.LineTotal), 3);
                    //h.VendorValue2 =
                    //   Math.Round(
                    //   item.Where(pod => pod.PurchaseOrderHeader.VendorId == vendor.VendorId).Average(pod => pod.LineTotal), 3);
                    //helpers.Add(h);
                    dynamic h = new Dictionary<string, object>();
                    h.Add("Week", item.Key);
                    h.Add("VendorValue", Math.Round(item.Where(pod => pod.PurchaseOrderHeader.VendorId == vendor.VendorId).Sum(pod => pod.LineTotal), 3));
                    h.Add("VendorValue2", Math.Round(item.Where(pod => pod.PurchaseOrderHeader.VendorId == vendor.VendorId).Average(pod => pod.LineTotal), 3));
                    helpers.Add(h);
                }

                var res = helpers.OrderBy(h => h["Week"]).ToList();
                ViewData.Add("WeekNumbers", res.Select(r => r["Week"].ToString()));
                
                //DataTable table = new DataTable("test");
                //table.Columns.Add(new DataColumn("VendorValue", typeof(double)));
                //table.Rows.Add(12);

                //return Json(table);
                
                return Json(res);

            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
            throw new ApplicationException();

            
            
        }


        public ActionResult ProductsCountByCategory()
        {
            var pc = ContextFactory.Current.ProductCategories.Include(c => c.Products).ToList()
                .Select(
                    cat =>
                        new ProductsPerCategory
                        {
                            CategoryName = cat.Name.Substring(0, cat.Name.Length >= maxXLabelTextLenght ? maxXLabelTextLenght : cat.Name.Length),
                            //CategoryName = cat.Name,
                            ProductCount = cat.Products.Count,
                            ProductValue =
                                (decimal)
                                    (cat.Products.Count != 0 ? Math.Round(cat.Products.Sum(p => p.StockValue), 3) : 0)
                        })
                .OrderByDescending(res => res.ProductCount).Where(ppc => ppc.ProductCount != 0)
                .ToList();


            return Json(pc);
        }

        public ActionResult GpPerDayLastDays()
        {
            int lastNdays = 30;

            List<GpPerDay> list = new List<GpPerDay>();
            for (int i = 29; i >= 0; i--)
            {
                DateTime date = DateTime.Now.Date.AddDays(-i);
                double sales =
                    SalesOrderHeader.GetSalesOrderHeadersInPeriod(date, date, SalesOrderStatusEnum.Approved)
                        .Sum(soh => soh.SalesOrderDetails.Sum(sod => sod.LineTotal));
                double purchases =
                    (double)
                        PurchaseOrderHeader.GetPurchaseOrderHeadersInPeriod(date, date,
                            PurchaseOrderStatusEnum.Completed).Sum(poh => poh.TotalDue).GetValueOrDefault();
                double dayGp = sales - purchases;

                GpPerDay gh = new GpPerDay() { Days = date.ToString("dd/MM"), DayGp = Math.Round(dayGp, 3) };
                list.Add(gh);
            }

            return Json(list);
        }

        public ActionResult LowProducts()
        {
            var list = ContextFactory.GetContextPerRequest().Products
                    .Where(product => product.UnitsInStock <= product.ReorderLevel)
                    .OrderByDescending(product => product.ReorderLevel)
                    .Select(
                        p =>
                            new LowProduct
                            {
                                UnitsInStock = p.UnitsInStock,
                                UnitsOnOrder = p.UnitsOnOrder,
                                ReorderLevel = p.ReorderLevel,
                                ProductName = p.Name.Substring(0, maxXLabelTextLenght)
                            }).Take(10).ToList();

            return Json(list);
        }
    }
}