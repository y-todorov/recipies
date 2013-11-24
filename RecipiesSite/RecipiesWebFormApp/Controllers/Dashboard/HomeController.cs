using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecipiesModelNS;
using System;
using System.Diagnostics;


namespace InventoryManagementMVC.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            // TEST MOVE THIS OUT OF HERE!!!!

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
                        .GroupBy(pod => ChartController.GetIso8601WeekOfYear(pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault()));

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

                //return Json(res);

            }
            catch (Exception ex)
            {
                Debugger.Break();
            }



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NavigateToOldSite()
        {
            //Server.Transfer("/Default.aspx"); works fine
            Response.Redirect("/Default.aspx", false);
            return new ContentResult();
        }

    }
}