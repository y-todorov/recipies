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
                List<PurchaseOrderDetail> pods =
                    ContextFactory.GetContextPerRequest()
                        .PurchaseOrderDetails.Where(
                            pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed)
                        .ToList();

                var grouping =
                    pods.OrderByDescending(pod => pod.PurchaseOrderHeader.ShipDate)
                        .GroupBy(pod => ChartController.GetIso8601WeekOfYear(pod.PurchaseOrderHeader.ShipDate.GetValueOrDefault()));

                List<Dictionary<string, object>> helpers = new List<Dictionary<string, object>>();

                foreach (var item in grouping)
                {

                    dynamic h = new Dictionary<string, object>();
                    h.Add("Week", item.Key);                  
                    helpers.Add(h);
                }

                var res = helpers.OrderBy(h => h["Week"]).ToList();
                ViewData.Add("WeekNumbers", res.Select(r => r["Week"].ToString()));
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