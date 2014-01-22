using System.Diagnostics;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using RecipiesModelNS;
using System.Linq;

namespace RecipiesUnitTests.ChartController
{
    [TestClass]
    public class ChartControllerTest
    {
        private InventoryManagementMVC.Controllers.ChartController chartController;
        private DataSourceRequest request;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            chartController = new InventoryManagementMVC.Controllers.ChartController();
            request = new DataSourceRequest();
            var dymmy = ContextFactory.GetContextPerRequest().PurchaseOrderDetails.FirstOrDefault();
            

        }

        [TestCleanup]
        public void TestCleanUp()
        {
        }

        [TestMethod]
        public void VendorPurchasesByWeekTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            ActionResult ar = chartController.VendorPurchasesByWeek();
            Assert.IsNotNull(ar);
            s.Stop();
            long mils = s.ElapsedMilliseconds;
        }

        [TestMethod]
        public void GpPerDayLastDaysTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            ActionResult ar = chartController.GpPerDayLastDays();
            Assert.IsNotNull(ar);
            s.Stop();
            long mils = s.ElapsedMilliseconds;
        }

    }
}