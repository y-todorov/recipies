using InventoryManagementMVC.Models.Purchasing;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using System.IO;
using Telerik.Reporting.Processing;
using Helpers;
using RestSharp;
using System.Net;

namespace InventoryManagementMVC.Controllers.Purchasing
{
    public class PurchaseOrderHeaderController : ControllerBase
    {
        public ActionResult Index()
        {
            List<PurchaseOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PurchaseOrderHeaders
                    .ToList().Select
                    (pod =>
                        PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod,
                            new PurchaseOrderHeaderViewModel())).ToList();
            return View(purchaseOrderHeaderViewModels);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PurchaseOrderHeaders
                    .ToList().Select
                    (pod =>
                        PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod,
                            new PurchaseOrderHeaderViewModel())).ToList();
            return Json(purchaseOrderHeaderViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderHeaderViewModel> purchaseOrderHeaders)
        {
            if (purchaseOrderHeaders != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderHeaderViewModel pohViewModel in purchaseOrderHeaders)
                {
                    PurchaseOrderHeader newPohEntity =
                        PurchaseOrderHeaderViewModel.ConvertToPurchaseOrderHeaderEntity(pohViewModel,
                            new PurchaseOrderHeader());
                    ContextFactory.Current.PurchaseOrderHeaders.Add(newPohEntity);
                    ContextFactory.Current.SaveChanges();
                    PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(newPohEntity, pohViewModel);
                }
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderHeaderViewModel> purchaseOrderHeaders)
        {
            if (purchaseOrderHeaders != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderHeaderViewModel pohViewModel in purchaseOrderHeaders)
                {
                    PurchaseOrderHeader pohEntity =
                        ContextFactory.Current.PurchaseOrderHeaders.FirstOrDefault(
                            c => c.PurchaseOrderId == pohViewModel.PurchaseOrderHeaderId);

                    PurchaseOrderHeaderViewModel.ConvertToPurchaseOrderHeaderEntity(pohViewModel, pohEntity);

                    ContextFactory.Current.SaveChanges();

                    PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pohEntity, pohViewModel);
                }
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderHeaderViewModel> purchaseOrderHeaders)
        {
            foreach (PurchaseOrderHeaderViewModel poh in purchaseOrderHeaders)
            {
                PurchaseOrderHeader pod =
                    ContextFactory.Current.PurchaseOrderHeaders.FirstOrDefault(
                        c => c.PurchaseOrderId == poh.PurchaseOrderHeaderId);
                ContextFactory.Current.PurchaseOrderHeaders.Remove(pod);

                ContextFactory.Current.SaveChanges();
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }
          
        public ActionResult Download(int? purchaseOrderHeaderId)
        {

            
            PurchaseOrderHeader purchaseOrder =
                ContextFactory.GetContextPerRequest()
                    .PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderHeaderId);

            ReportProcessor reportProcessor = new ReportProcessor();

            var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            RecipiesReports.PurchaseOrderDetailsReport salesOrderDetailsReport =
                new RecipiesReports.PurchaseOrderDetailsReport();
            salesOrderDetailsReport.DataSource = purchaseOrder.PurchaseOrderDetails;
            instanceReportSource.ReportDocument = salesOrderDetailsReport;

            //specify the output format of the produced image.
            System.Collections.Hashtable deviceInfo =
                new System.Collections.Hashtable();

            //deviceInfo["OutputFormat"] = "DOCX";

            RenderingResult result = reportProcessor.RenderReport("Image", instanceReportSource, null);

            string fileName = result.DocumentName + "." + result.Extension;

            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = fileName,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());


            return File(result.DocumentBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


            return File(result.DocumentBytes, result.MimeType);
        }

         public ActionResult SendEmail(int? purchaseOrderHeaderId)
         {
             //(Master as SiteMaster).MasterRadNotification.Show(
             //    "Sending Emails is temporary disabled. Will be enabled when the product is tested enough! ");
             //return;

             PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderHeaderId);

             ReportProcessor reportProcessor = new ReportProcessor();

             var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
             RecipiesReports.PurchaseOrderDetailsReport salesOrderDetailsReport = new RecipiesReports.PurchaseOrderDetailsReport();
             salesOrderDetailsReport.DataSource = purchaseOrder.PurchaseOrderDetails;
             instanceReportSource.ReportDocument = salesOrderDetailsReport;

             RenderingResult result = reportProcessor.RenderReport("Image", instanceReportSource, null);

             EmailTemplate defaultTemplate = ContextFactory.GetContextPerRequest().EmailTemplates.FirstOrDefault(et => et.IsDefault);
             if (defaultTemplate != null)
             {
                 RestResponse restResponse = EmailHelper.SendComplexMessage(defaultTemplate.From, purchaseOrder.Vendor.Email, defaultTemplate.Cc,
                     defaultTemplate.Bcc, defaultTemplate.Subject, defaultTemplate.TextBody, defaultTemplate.HtmlBody,
                     result.DocumentBytes, defaultTemplate.AttachmentName + "." + result.Extension);                
             }
             else
             {
             }

             return View("Index");
         }
        
    }
    
}