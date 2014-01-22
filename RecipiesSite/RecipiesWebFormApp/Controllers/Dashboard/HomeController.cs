using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecipiesModelNS;
using System;
using System.Diagnostics;
using InventoryManagementMVC.Helpers;


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
                List<PurchaseOrderDetail> pods =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderDetails.Where(
                            pod => pod.PurchaseOrderHeader.StatusId == (int) PurchaseOrderStatusEnum.Completed)
                        .ToList();

                var grouping =
                    pods.OrderBy(pod => pod.PurchaseOrderHeader.ShipDate)
                        .GroupBy(
                            pod => pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault().Year * 100 + int.Parse(
                                ControllerHelper.GetIso8601WeekOfYear(
                                    pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault())));



                List<Dictionary<string, int>> helpers = new List<Dictionary<string, int>>();

                foreach (var item in grouping)
                {
                    Dictionary<string, int> h = new Dictionary<string, int>();
                    int week = item.Key % 100;
                   // h.Add("Week", week.ToString());

                    h.Add("Week", week);

                    helpers.Add(h);
                }

                var res = helpers;//.OrderBy(h => h["Week"]).ToList();
                ViewData.Add("WeekNumbers", res.Select(r => r["Week"]));
            }
            catch (Exception ex)
            {
                throw ex;
                //Debugger.Break();
            }

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