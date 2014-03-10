﻿using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using Blitline.Net.Builders;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DevTrends.MvcDonutCaching;
using DevTrends.MvcDonutCaching.Annotations;
using Kendo.Mvc.UI;
using NPOI.HSSF.UserModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
using RecipiesWebFormApp.Helpers;
using Telerik.Reporting.Processing;
using Kendo.Mvc.Extensions;
using InventoryManagementMVC.Models;
using System.Data.Entity;
using System.Reflection;
using System.Net;
using System.Xml.Linq;


namespace InventoryManagementMVC.Controllers
{
    public class DownloadController : Controller
    {
        private string GetTableXml(string completeGridXml)
        {
            //string reportHtml = File.ReadAllText("ReportHtml.txt", Encoding.GetEncoding("Windows-1251"));

            //int startIndex = completeGridXml.IndexOf("<tbody>");
            //int endIndex = completeGridXml.LastIndexOf("</tbody>");

            int startIndex = completeGridXml.IndexOf("<table");
            int endIndex = completeGridXml.LastIndexOf("</table>");

            int len = endIndex - startIndex + "</table>".Length;

            string xml = completeGridXml.Substring(startIndex, len);
            xml = xml.Replace("&nbsp;", "");
            //xml = xml.Replace("<colgroup>", "");
            //xml = xml.Replace("</colgroup>", "");
            //xml = xml.Replace("<col>", "");
            //xml = xml.Replace("<col>", "");

            // replace script blocks
            while (xml.Contains("<colgroup>"))
            {
                xml = CutSubstringData(xml, "<colgroup>", "</colgroup>");
            }

            while (xml.Contains("<script>"))
            {
                xml = CutSubstringData(xml, "<script>", "</script>");
            }

            return xml;
        }

        private string CutSubstringData(string text, string startTag, string endTag)
        {
            int startIndex = text.IndexOf(startTag);
            int endIndex = text.IndexOf(endTag);
            int len = endIndex - startIndex + endTag.Length;

            string textToRemove = text.Substring(startIndex, len);
            string result = text.Replace(textToRemove, "");
                
            return result;
        }

        // make these two one monethod !
        private List<XElement> GetTrsWithDataOnly(string xml)
        {
            List<XElement> result = new List<XElement>();
            XDocument xdoc = XDocument.Parse(xml);

            List<XElement> trs = xdoc.Descendants("tr").ToList();

            foreach (XElement tr in trs)
            {
                List<XAttribute> attrs = tr.Attributes().ToList();

                if (attrs.Count == 0)
                {
                    result.Add(tr);
                }

                foreach (XAttribute attr in attrs)
                {
                    //if (attr.Name == "role" && attr.Value.Contains("row") ||
                    //    attr.Name == "class" && attr.Value.Contains("k-grouping-row") ||
                    //    attr.Name == "class" && attr.Value.Contains("k-group-footer") ||
                    //    attr.Name == "class" && attr.Value.Contains("k-grouping-row") 
                    //    )
                    if (!(attr.Name == "class" && attr.Value.Contains("k-footer-template")))
                    {
                        result.Add(tr);
                        break;
                    }
                }
            }
            return result;
        }

        private List<XElement> GetTrsFromGridFooterOnly(string xml)
        {
            List<XElement> result = new List<XElement>();
            XDocument xdoc = XDocument.Parse(xml);

            List<XElement> trs = xdoc.Descendants("tr").ToList();

            foreach (XElement tr in trs)
            {
                List<XAttribute> attrs = tr.Attributes().ToList();

                foreach (XAttribute attr in attrs)
                {
                    if (attr.Name == "class" && attr.Value.Contains("k-footer-template"))
                    {
                        result.Add(tr);
                        break;
                    }
                }
            }
            return result;
        }

        private List<XElement> GetTds(XElement row)
        {
            List<XElement> result = new List<XElement>();
            List<XElement> ths = row.Descendants("th").ToList();
            List<XElement> tds = row.Descendants("td").ToList();

            foreach (XElement th in ths)
            {
                //List<XAttribute> attrs = td.Attributes().ToList();
                //foreach (XAttribute attr in attrs)
                {
                    //if (attr.Name == "role" && attr.Value == "gridcell")
                    {
                        result.Add(th); // string valToInsertInExcel = td.Value;
                    }
                }
            }

            foreach (XElement td in tds)
            {
                //List<XAttribute> attrs = td.Attributes().ToList();
                //foreach (XAttribute attr in attrs)
                {
                    //if (attr.Name == "role" && attr.Value == "gridcell")
                    {
                        result.Add(td); // string valToInsertInExcel = td.Value;
                    }
                }
            }

            return result;
        }

        private List<XElement> GetThs(XElement row)
        {
            List<XElement> result = new List<XElement>();
            List<XElement> ths = row.Descendants("th").ToList();
        
            foreach (XElement th in ths)
            {
                //List<XAttribute> attrs = td.Attributes().ToList();
                //foreach (XAttribute attr in attrs)
                {
                    //if (attr.Name == "role" && attr.Value == "gridcell")
                    {
                        result.Add(th); // string valToInsertInExcel = td.Value;
                    }
                }
            }

            return result;
        }

        private string GetThValue(XElement row)
        {
            List<XElement> anchors = row.Descendants("a").ToList();
            foreach (XElement a in anchors)
            {
                List<XAttribute> attrs = a.Attributes().ToList();
                foreach (XAttribute attr in attrs)
                {
                    if (attr.Name == "class" && attr.Value == "k-link")
                    {
                        return a.Value;
                    }
                }
            }

            return string.Empty;
        }



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
            //var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            //headerRow.CreateCell(0).SetCellValue("Product ID");
            //headerRow.CreateCell(1).SetCellValue("Product Name");
            //headerRow.CreateCell(2).SetCellValue("Unit Price");
            //headerRow.CreateCell(3).SetCellValue("Quantity Per Unit");

            //for (int i = 0; i < props.Length; i++)
            //{
            //    headerRow.CreateCell(i).SetCellValue(props[i].Name);
            //}


            //(Optional) freeze the header row so it is not scrolled
            //sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 0;

            string tableXml = GetTableXml(html);
            List<XElement> trs = GetTrsWithDataOnly(tableXml);
            trs.AddRange(GetTrsFromGridFooterOnly(tableXml));
            foreach (XElement tr in trs)
            {
                var row = sheet.CreateRow(rowNumber++);

                List<XElement> ths = GetThs(tr);

                for (int i = 0; i < ths.Count; i++)
                {
                    string val = GetThValue(ths[i]);
                    row.CreateCell(i).SetCellValue(val);
                }

                List<XElement> tds = GetTds(tr);
                for (int i = 0; i < tds.Count; i++)
                {
                    string val = tds[i].Value;
                    if (val != null)
                    {
                        double dummyDouble;
                        DateTime dummyDateTime;
                        if (double.TryParse(val, System.Globalization.NumberStyles.Any, null, out dummyDouble))
                        {
                            row.CreateCell(i).SetCellValue(dummyDouble);
                        }
                        //else if (DateTime.TryParse(val, out dummyDateTime))
                        //{
                        //    row.CreateCell(i).SetCellValue(dummyDateTime); // fo not work well, should be tested more
                        //}
                        else
                        {
                            row.CreateCell(i).SetCellValue(val);
                        }
                    }
                    else
                    {
                        row.CreateCell(i).SetCellValue("");
                    }
                }
            }

            //    //Set values for the cells
            //    //row.CreateCell(0).SetCellValue(product.ProductId);
            //    //row.CreateCell(1).SetCellValue(product.Name);
            //    //row.CreateCell(2).SetCellValue(product.UnitPrice.ToString());
            //    //row.CreateCell(3).SetCellValue(product.UnitsInStock.GetValueOrDefault().ToString());
            //}

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            lastFileContentResult = File(output.ToArray(), //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "GridExcelExport.xls");
            //Suggested file name in the "Save as" dialog which will be displayed to the end user
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

            List<PurchaseOrderDetail> nonEmptyOrders =
                purchaseOrder.PurchaseOrderDetails.Where(pod => pod.OrderQuantity.GetValueOrDefault() != 0).ToList();

            salesOrderDetailsReport.DataSource = nonEmptyOrders;

            instanceReportSource.ReportDocument = salesOrderDetailsReport;

            //specify the output format of the produced image.
            System.Collections.Hashtable deviceInfo =
                new System.Collections.Hashtable();
            
            RenderingResult result = reportProcessor.RenderReport("Image", instanceReportSource, null);
            
            string tempFileName = Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFileName, result.DocumentBytes);

//            var uploadParams = new ImageUploadParams()
//{
//    File = new FileDescription(tempFileName),
//    PublicId= "test.tiff"
//};

//            Cloudinary cloudinary = CloudinaryHelper.GetInstance();
//            cloudinary.uploader .Upload(uploadParams);

//            var r = cloudinary.Api.UrlImgUp.BuildImageTag("test.pdf");

            



            string jpegFilePath = ImageHelper.ConvertTiffToJpeg(tempFileName).FirstOrDefault();


            //  http://www.telerik.com/community/forums/reporting/telerik-reporting/out-of-memory-in-azure-websites.aspx


            //string fileName = result.DocumentName + "." + result.Extension;
            // Until solving PDF problem extension will be .jpg
            string fileName = result.DocumentName + "." + "jpeg";

            byte[] documentBytes = System.IO.File.ReadAllBytes(jpegFilePath);

            return File(documentBytes, result.MimeType, fileName);
        }

    }
}