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

namespace InventoryManagementMVC.Controllers
{
    public class ChartController : ControllerBase
    {
        public ActionResult ProductsCountByCategory()
        {
            var pc = ContextFactory.Current.ProductCategories.Include(c => c.Products).ToList()
                .Select(
                    cat =>
                        new ProductsPerCategory
                        {
                            CategoryName = cat.Name.Substring(0, cat.Name.Length >= 10 ? 10 : cat.Name.Length),
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

                GpPerDay gh = new GpPerDay() { Days = date.ToString("dd/MM"), DayGp = dayGp };
                list.Add(gh);
            }

            return Json(list);
        }
    }
}