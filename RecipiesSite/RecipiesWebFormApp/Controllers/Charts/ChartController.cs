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
                List<PurchaseOrderDetail> pods =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderDetails.Where(
                            pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed)
                        .ToList();

                var grouping =
                    pods.OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate)
                        .GroupBy(pod => GetIso8601WeekOfYear(pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault()));


                List<Dictionary<string, double>> list = new List<Dictionary<string, double>>();
                List<Vendor> allVendors = ContextFactory.Current.Vendors.ToList();
                foreach (var item in grouping)
                {

                    Dictionary<string, double> entry = new Dictionary<string, double>();
                    entry.Add("Week", item.Key);

                    foreach (Vendor ven in allVendors)
                    {
                        entry.Add("EscapeStringYordan_" + ven.VendorId.ToString(), Math.Round(item.Where(pod => pod.PurchaseOrderHeader.VendorId == ven.VendorId).Sum(pod => pod.LineTotal), 3));
                    }
                    list.Add(entry);
                }

                var res = list.OrderBy(h => h["Week"]).ToList();
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