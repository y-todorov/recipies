using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace RecipiesWebFormApp.Charts
{
    public partial class Vendors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetVendorPerWeek();
            }
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
            SetVendorPerWeek();
        }


        public void SetVendorPerWeek()
        {
            try
            {

                Vendor vendor;
                if (!string.IsNullOrEmpty(rcbVendor.SelectedValue))
                {
                    vendor = ContextFactory.GetContextPerRequest().Vendors.Where(v => v.VendorId.ToString() == rcbVendor.SelectedValue).FirstOrDefault();
                }
                else
                {
                    vendor = ContextFactory.GetContextPerRequest().Vendors.FirstOrDefault();
                }
                List<PurchaseOrderDetail> pods = ContextFactory.GetContextPerRequest().PurchaseOrderDetails.Where(pod => pod.PurchaseOrderHeader.StatusId == (int)PurchaseOrderStatusEnum.Completed).ToList();

                var grouping = pods.OrderByDescending(pod => pod.PurchaseOrderHeader.OrderDate).GroupBy(pod => GetIso8601WeekOfYear(pod.PurchaseOrderHeader.OrderDate.GetValueOrDefault()));

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
                rhcVendorsLastWeek.PlotArea.Series[0].Name = Server.HtmlEncode(vendor.Name);
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
            catch (Exception ex)
            {
                Debugger.Break();
                    
            }
        }
    }
}