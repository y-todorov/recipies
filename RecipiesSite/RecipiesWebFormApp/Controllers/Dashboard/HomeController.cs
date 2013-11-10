using System.Web.Mvc;
using Kendo.Mvc;
using DevTrends.MvcDonutCaching;

namespace InventoryManagementMVC.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

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