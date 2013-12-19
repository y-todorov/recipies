﻿using Autofac;
using DevTrends.MvcDonutCaching;
using DevTrends.MvcDonutCaching.Annotations;
using Kendo.Mvc.UI;
using NPOI.HSSF.UserModel;
using RecipiesModelNS;
using RecipiesWebFormApp.Caching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Telerik.Reporting.Processing;
using Kendo.Mvc.Extensions;
using InventoryManagementMVC.Models;
using System.Data.Entity;
using System.Reflection;
using System.Net; 


namespace InventoryManagementMVC.Controllers
{
    

    public class DownloadController : Controller
    {
        private static FileContentResult lastFileContentResult;

        public ActionResult DownloadExport()
        {
            if (lastFileContentResult != null)
            {
                return lastFileContentResult;
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK); 
            }
        }

        public void ExportWithOpenXML(string typeName, string html, [DataSourceRequest] DataSourceRequest request)
        {

            var allPis = ContextFactory.Current.Inventories.OfType<ProductInventory>()
                .Include(pi => pi.Product.ProductCategory)
                .Include(pi => pi.Product.UnitMeasure)
                .ToList();

            List<ProductInventoryViewModel> productInventoriesViewModels =
               allPis.Select
                    (pi =>
                        ProductInventoryViewModel.ConvertFromProductInventoryEntity(pi, new ProductInventoryViewModel()))
                    .ToList();
            //return Json(productInventoriesViewModels.ToDataSourceResult(request));



            //Get the data representing the current grid state - page, sort and filter
            DataSourceResult res = ContextFactory.Current.Products.ToDataSourceResult(request);
            //IEnumerable products = res.Data;

            IEnumerable productInventories = productInventoriesViewModels;

            PropertyInfo[] props = typeof(ProductInventoryViewModel).GetProperties();


            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            //sheet.SetColumnWidth(0, 10 * 256);
            //sheet.SetColumnWidth(1, 50 * 256);
            //sheet.SetColumnWidth(2, 50 * 256);
            //sheet.SetColumnWidth(3, 50 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            //headerRow.CreateCell(0).SetCellValue("Product ID");
            //headerRow.CreateCell(1).SetCellValue("Product Name");
            //headerRow.CreateCell(2).SetCellValue("Unit Price");
            //headerRow.CreateCell(3).SetCellValue("Quantity Per Unit");

            for (int i = 0; i < props.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(props[i].Name);
            }


            //(Optional) freeze the header row so it is not scrolled
            //sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (ProductInventoryViewModel product in productInventories)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                for (int i = 0; i < props.Length; i++)
                {
                    
                    object val = props[i].GetValue(product);

                    // SPECIFIC
                    if (props[i].Name == "ProductId")
                    {
                        if (val != null)
                        {
                            var prod = ContextFactory.Current.Products.FirstOrDefault(p => p.ProductId == (int)val);
                            if (prod != null)
                            {
                                val = prod.Name;
                            }
                        }
                    }

                    if (val != null)
                    {
                        row.CreateCell(i).SetCellValue(val.ToString());
                    }
                    else
                    {
                        row.CreateCell(i).SetCellValue("");

                    }
                }
                //Set values for the cells
                //row.CreateCell(0).SetCellValue(product.ProductId);
                //row.CreateCell(1).SetCellValue(product.Name);
                //row.CreateCell(2).SetCellValue(product.UnitPrice.ToString());
                //row.CreateCell(3).SetCellValue(product.UnitsInStock.GetValueOrDefault().ToString());
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            lastFileContentResult = File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "GridExcelExport.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            //return View("DownloadExport");
            //return new HttpStatusCodeResult(HttpStatusCode.OK); 
            //return File(new byte[1], "xls");
        }

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

            //XmlSerializer ser = new XmlSerializer(typeof(Telerik.Reporting.InstanceReportSource));
            //MemoryStream ms = new MemoryStream();
            //ser.Serialize(ms, instanceReportSource);
            //ms.Position = 0;
            //instanceReportSource.ReportDocument.
            //byte[] arr = ms.ToArray();
            
            RenderingResult result = reportProcessor.RenderReport("Image", instanceReportSource, null);
            //RenderingResult result = reportProcessor.RenderReport("pdf", instanceReportSource, null); // PROBLEMS
            //  http://www.telerik.com/community/forums/reporting/telerik-reporting/out-of-memory-in-azure-websites.aspx


            string fileName = result.DocumentName + "." + result.Extension;
           
            return File(result.DocumentBytes, result.MimeType, fileName);
        }
    }
}