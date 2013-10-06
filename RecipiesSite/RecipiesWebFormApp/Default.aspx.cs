using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess;
using Telerik.Web.UI;

namespace RecipiesWebFormApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllCharts();
             
            }
        }

        private void LoadAllCharts()
        {

            rhcLast10ModifiedProducts.DataSource = ContextFactory.GetContextPerRequest().Products.OrderByDescending(pr => pr.ModifiedDate).Take(10);

            rhcProductsCountByCategory.DataSource = ContextFactory.GetContextPerRequest().ProductCategories.Select(cat => new { CategoryName = cat.Name, ProductCount = cat.Products.Count }).OrderByDescending(res => res.ProductCount);

            rhcProductsForReorder.DataSource = ContextFactory.GetContextPerRequest().Products.Where(product => product.UnitsInStock <= product.ReorderLevel).OrderByDescending(product => product.ReorderLevel).Take(10);

            rhcMostExpensiveProducts.DataSource = ContextFactory.GetContextPerRequest().Products.OrderByDescending(product => product.UnitPrice).Take(10);

            Vendor vendor;
            if (!string.IsNullOrEmpty(rcbVendor.SelectedValue))
            {
                vendor = ContextFactory.GetContextPerRequest().Vendors.Where(v => v.VendorId.ToString() == rcbVendor.SelectedValue).FirstOrDefault();
            }
            else
            {
                vendor = ContextFactory.GetContextPerRequest().Vendors.FirstOrDefault();
            }

            SetVendorPerWeek(vendor);
        }

        public void SetVendorPerWeek(Vendor vendor)
        {
            List<PurchaseOrderDetail> pods = ContextFactory.GetContextPerRequest().PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

            var grouping = pods.OrderByDescending(pod => pod.PurchaseOrderHeader.OrderDate).GroupBy(pod => GetIso8601WeekOfYear(pod.PurchaseOrderHeader.OrderDate.Value));

            if (!string.IsNullOrEmpty(rcbVendor.SelectedValue))
            {
                vendor = ContextFactory.GetContextPerRequest().Vendors.Where(v => v.VendorId.ToString() == rcbVendor.SelectedValue).FirstOrDefault();
            }
            else
            {
                vendor = ContextFactory.GetContextPerRequest().Vendors.FirstOrDefault();
            }

            if (vendor == null)
            {
                return;
            }

            List<HelperClass> helpers = new List<HelperClass>();
            rhcVendorsLastWeek.PlotArea.Series[0].Name = vendor.Name;
            //  <telerik:LineSeries DataFieldY="VendorValue" Name="Euro">
            //</telerik:LineSeries>
            foreach (var item in grouping)
            {
                //foreach (Vendor vendor in vendors)
                {
                    HelperClass h = new HelperClass();
                    h.Week = item.Key;
                    h.VendorValue = item.Where(pod => pod.PurchaseOrderHeader.VendorId == vendor.VendorId).Sum(pod => pod.LineTotal);
                    helpers.Add(h);
                    //rhcVendorsLastWeek.PlotArea.Series[0].Name = vendor.Name;
                    //break;

                    //rhcVendorsLastWeek.PlotArea.Series.Add(new LineSeries() { DataFieldY = "VendorValue", Name = vendor.Name });
                }
                //break;
            }

            rhcVendorsLastWeek.DataSource = helpers.OrderBy(h => h.Week);
        }

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
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public class HelperClass
        {
            public int Week { get; set; }
            public double VendorValue { get; set; }
        }

        protected void rcbVendor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadAllCharts();
        }
    }
}