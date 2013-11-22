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
using System.Data.Entity;
using InventoryManagementMVC.Models;
using RecipiesWebFormApp.Extensions;

namespace InventoryManagementMVC.Controllers.Purchasing
{
    public class PurchaseOrderHeaderController : ControllerBase // Do not use ControlllerBase becaouse Donut caching breaks downlod of the file ignore cache for actions that download files
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

                    List<ProductVendor> productsForVendor = ContextFactory.Current.ProductVendors.Where(pv => pv.VendorId == newPohEntity.VendorId && pv.ProductId != null).ToList();

                    foreach (ProductVendor pv in productsForVendor)
                    {
                        PurchaseOrderDetail pod = new PurchaseOrderDetail()
                        {
                            PurchaseOrderId = newPohEntity.PurchaseOrderId,
                            ProductId = pv.Product.ProductId,
                            UnitPrice = pv.Product.UnitPrice,
                            UnitMeasureId = pv.UnitMeasureId,
                        };

                        decimal coef = 1;
                        if (pv.UnitMeasure != null)
                        {
                            coef = (decimal)pv.Product.GetBaseUnitMeasureQuantityForProduct(1, pv.UnitMeasure);
                        }
                        pod.UnitPrice = coef * (decimal)pv.Product.GetAveragePriceLastDays(14);

                        ContextFactory.Current.PurchaseOrderDetails.Add(pod);
                    }

                    ContextFactory.Current.SaveChanges();
                    newPohEntity.ModifiedDate = DateTime.Now;
                    PurchaseOrderHeader.UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetails(newPohEntity.PurchaseOrderId);
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

        public ActionResult CorrectTotals()
        {
            List<PurchaseOrderHeader> headers = ContextFactory.Current.PurchaseOrderHeaders.ToList();

            foreach (PurchaseOrderHeader header in headers)
            {
                PurchaseOrderHeader.UpdatePurchaseOrderHeaderSubTotalFromPurchaseOrderDetails(header.PurchaseOrderId);
            }
            ContextFactory.Current.SaveChanges();

            return RedirectToAction("Index");

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

            List<PurchaseOrderDetail> nonEmptyOrders = purchaseOrder.PurchaseOrderDetails.Where(pod => pod.OrderQuantity.GetValueOrDefault() != 0).ToList();

            salesOrderDetailsReport.DataSource = nonEmptyOrders;

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
            //Response.AppendHeader("Content-Disposition", cd.ToString());



            //return File(result.DocumentBytes, result.MimeType, fileName);

            //FileStream MyFileStream;
            //long FileSize;

            //MyFileStream = new FileStream("sometext.txt", FileMode.Open);
            //FileSize = MyFileStream.Length;

            //byte[] Buffer = new byte[(int)FileSize];
            //MyFileStream.Read(Buffer, 0, (int)FileSize);
            //MyFileStream.Close();

            //Response.Write("<b>File Contents: </b>");
            //Response.BinaryWrite(result.DocumentBytes);

            //MyFileResult m = new MyFileResult(result);
            //return m;
            return File(result.DocumentBytes, result.MimeType, fileName);
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

            List<PurchaseOrderDetail> nonEmptyOrders = purchaseOrder.PurchaseOrderDetails.Where(pod => pod.OrderQuantity.GetValueOrDefault() != 0).ToList();

            salesOrderDetailsReport.DataSource = nonEmptyOrders;
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

            return RedirectToAction("Index");
        }

        public ActionResult ReadDetail(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderDetailViewModel> purchaseOrderDetailViewModels =
                ContextFactory.Current.PurchaseOrderDetails.Where(
                    pod => purchaseOrderHeaderId.HasValue ? pod.PurchaseOrderId == purchaseOrderHeaderId.Value : true)
                    .Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory)
                    .ToList().Select
                    (pod =>
                        PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pod,
                            new PurchaseOrderDetailViewModel())).ToList();
            return Json(purchaseOrderDetailViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateDetail(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            if (purchaseOrderDetails != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderDetailViewModel podViewModel in purchaseOrderDetails)
                {
                    podViewModel.PurchaseOrderHeaderId = purchaseOrderHeaderId;
                    PurchaseOrderDetail newPodEntity =
                        PurchaseOrderDetailViewModel.ConvertToPurchaseOrderDetailEntity(podViewModel, new PurchaseOrderDetail());
                    ContextFactory.Current.PurchaseOrderDetails.Add(newPodEntity);
                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    newPodEntity = ContextFactory.Current.PurchaseOrderDetails.Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory).FirstOrDefault(pod => pod.PurchaseOrderDetailId == newPodEntity.PurchaseOrderDetailId);
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(newPodEntity, podViewModel);
                }
            }
            //return View("Index");
            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            if (purchaseOrderDetails != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderDetailViewModel podViewModel in purchaseOrderDetails)
                {
                    PurchaseOrderDetail pohEntity =
                        ContextFactory.Current.PurchaseOrderDetails.FirstOrDefault(
                            c => c.PurchaseOrderDetailId == podViewModel.PurchaseOrderDetailId);

                    PurchaseOrderDetailViewModel.ConvertToPurchaseOrderDetailEntity(podViewModel, pohEntity);

                    ContextFactory.Current.SaveChanges();
                    // Prefetch Product and others ...
                    pohEntity = ContextFactory.Current.PurchaseOrderDetails.Include(pod => pod.PurchaseOrderHeader.Vendor)
                   .Include(pod => pod.Product.ProductCategory).FirstOrDefault(pod => pod.PurchaseOrderDetailId == pohEntity.PurchaseOrderDetailId);
                    PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pohEntity, podViewModel);
                }
            }

            //return View("Index");

            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyDetail([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderDetailViewModel> purchaseOrderDetails)
        {
            foreach (PurchaseOrderDetailViewModel pod in purchaseOrderDetails)
            {
                PurchaseOrderDetail podEntity =
                    ContextFactory.Current.PurchaseOrderDetails.FirstOrDefault(
                        c => c.PurchaseOrderDetailId == pod.PurchaseOrderDetailId);
                ContextFactory.Current.PurchaseOrderDetails.Remove(podEntity);

                ContextFactory.Current.SaveChanges();
            }

            return Json(purchaseOrderDetails.ToDataSourceResult(request, ModelState));
            //return View("Index");

        }

    }

}