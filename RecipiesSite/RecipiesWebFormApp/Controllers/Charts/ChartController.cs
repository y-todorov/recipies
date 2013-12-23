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
using InventoryManagementMVC.Helpers;

namespace InventoryManagementMVC.Controllers
{
    public class ChartController : ControllerBase
    {
        int maxXLabelTextLenght = 10;

        public string GetWeekString(string weekAsInt)
        {
            int week = int.Parse(weekAsInt);
            string res = ControllerHelper.GetWeekStringFromWeekNumber(week);
            return res;
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
                        .GroupBy(pod => ControllerHelper.GetIso8601WeekOfYear(pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault()));


                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                List<Vendor> allVendors = ContextFactory.Current.Vendors.ToList();

                Vendor fakeTotalVendor = new Vendor()
                {
                    Name = "Total All Vendors",
                    VendorId = 0
                };

                foreach (var item in grouping)
                {

                    Dictionary<string, string> entry = new Dictionary<string, string>();
                    entry.Add("Week", item.Key);

                    entry.Add("EscapeStringYordan_" + fakeTotalVendor.VendorId, Math.Round(item.Sum(pod => pod.LineTotal), 3).ToString());

                    foreach (Vendor ven in allVendors)
                    {
                        entry.Add("EscapeStringYordan_" + ven.VendorId.ToString(),
                            Math.Round(item.Where(pod => pod.PurchaseOrderHeader.VendorId == ven.VendorId).Sum(pod => pod.LineTotal), 3).ToString());
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
            int lastNdays = 15;

            List<GpPerDay> list = new List<GpPerDay>();
            for (int i = lastNdays; i >= 0; i--)
            {
                DateTime fromDate = DateTime.Now.Date.AddDays(-i * 7);
                DateTime toDate = DateTime.Now.Date.AddDays((-i + 1) * 7);
                double sales =
                    SalesOrderHeader.GetSalesOrderHeadersInPeriod(fromDate, toDate, SalesOrderStatusEnum.Approved)
                        .Sum(soh => soh.SalesOrderDetails.Sum(sod => sod.LineTotal));
                double purchases =
                    (double)
                        PurchaseOrderHeader.GetPurchaseOrderDetailsInPeriod(fromDate, toDate,
                            PurchaseOrderStatusEnum.Completed, ProductCategory.GetCategoriesToExcludeFromGP()).Sum(poh => poh.LineTotal);
                double dayGp = sales - purchases;

                GpPerDay gh = new GpPerDay() { Days = ControllerHelper.GetIso8601WeekOfYear(fromDate), DayGp = Math.Round(dayGp, 3) };
                list.Add(gh);
            }

            return Json(list);
        }

        public ActionResult RecipeByGpDescending()
        {
            List<RecipeByGP> list = ContextFactory.Current.Recipes.OrderByDescending(r => r.GrossProfit).Select(r => new RecipeByGP() { RecipeName = r.Name, GP = r.GrossProfit }).ToList();

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

        public ActionResult TotalPosValuePerVendor()
        {
            List<PurchaseOrderHeader> allPos = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.ToList();
            List<Vendor> allVendors = ContextFactory.GetContextPerRequest().Vendors.ToList();
            List<TotalPoByVendor> list = allVendors
                    .Select(
                        vendor =>
                            new TotalPoByVendor
                            {
                                VendorName = vendor.Name.Substring(0, vendor.Name.Length >= maxXLabelTextLenght ? maxXLabelTextLenght : vendor.Name.Length),
                                PoTotalValue = allPos.Where(pod => pod.VendorId == vendor.VendorId)
                                .Sum(pod => pod.TotalDue)
                            }).ToList();

            list = list.OrderByDescending(l => l.PoTotalValue).ToList();
            return Json(list);
        }

    }
}