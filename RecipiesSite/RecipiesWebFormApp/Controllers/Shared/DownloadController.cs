using Autofac;
using DevTrends.MvcDonutCaching;
using DevTrends.MvcDonutCaching.Annotations;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using RecipiesWebFormApp.Caching;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Telerik.Reporting.Processing;

namespace InventoryManagementMVC.Controllers
{
    public class DownloadController : Controller
    {

        public ActionResult DownloadPurchaseOrder(int? purchaseOrderHeaderId)
        {
            PurchaseOrderHeader purchaseOrder =
                ContextFactory.GetContextPerRequest()
                    .PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderHeaderId);

            ReportProcessor reportProcessor = new ReportProcessor();

            var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            RecipiesReports.PurchaseOrderDetailsReport salesOrderDetailsReport =
                new RecipiesReports.PurchaseOrderDetailsReport();

            List<PurchaseOrderDetail> nonEmptyOrders = purchaseOrder.PurchaseOrderDetails.Where(pod => pod.OrderQuantity.GetValueOrDefault() != 0).ToList();

            salesOrderDetailsReport.DataSource = nonEmptyOrders;

            instanceReportSource.ReportDocument = salesOrderDetailsReport;

            //specify the output format of the produced image.
            System.Collections.Hashtable deviceInfo =
                new System.Collections.Hashtable();

            //deviceInfo["OutputFormat"] = "DOCX";

            //BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize()

            //JavaScriptSerializer s; s.Deserialize()

            RenderingResult result = reportProcessor.RenderReport("Image", instanceReportSource, null);
            //RenderingResult result = reportProcessor.RenderReport("pdf", instanceReportSource, null); // PROBLEMS
            //  http://www.telerik.com/community/forums/reporting/telerik-reporting/out-of-memory-in-azure-websites.aspx


            string fileName = result.DocumentName + "." + result.Extension;
           
            return File(result.DocumentBytes, result.MimeType, fileName);
        }
    }
}