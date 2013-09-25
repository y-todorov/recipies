﻿using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Web;
using Telerik.ReportViewer.WebForms;
using Telerik.Reporting.Processing;
using RecipiesReports;
using Helpers;
using System.Web.Security;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace RecipiesWebFormApp.Purchasing
{
    public partial class PurchaseOrders : System.Web.UI.Page
    {
        public int PurchaseOrderId
        {
            get
            {
                int poId = (int)ViewState["PurchaseOrderId"];
                return poId;
            }
            set
            {
                ViewState["PurchaseOrderId"] = value;
            }
        }

        public int? VendorId
        {
            get
            {
                int? vendorId = (int)ViewState["VendorId"];
                return vendorId;
            }
            set
            {
                ViewState["VendorId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Selecting(object sender, OpenAccessLinqDataSourceSelectEventArgs e)
        {
            if (e.WhereParameters.ContainsKey("PurchaseOrderId"))
            {
                e.WhereParameters["PurchaseOrderId"] = PurchaseOrderId;
            }
        }

        protected void rgPurchaseOrders_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                GridEditableItem editableItem = e.Item as GridEditableItem;
                if (editableItem != null)
                {
                    int purchaseOrderId = (int)editableItem.GetDataKeyValue(rgPurchaseOrders.MasterTableView.DataKeyNames[0]);
                    PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);
                    VendorId = purchaseOrder.VendorId;
                    PurchaseOrderId = purchaseOrderId;
                }
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                PurchaseOrderId = 0;
            }
            if (e.CommandName == "GeneratePurchaseOrderReport")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                if (dataItem != null)
                {
                    int purchaseOrderId = (int)dataItem.GetDataKeyValue(rgPurchaseOrders.MasterTableView.DataKeyNames[0]);
                    PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);

                    ReportProcessor reportProcessor = new ReportProcessor();

                    var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
                    RecipiesReports.PurchaseOrderDetailsReport salesOrderDetailsReport = new RecipiesReports.PurchaseOrderDetailsReport();
                    salesOrderDetailsReport.DataSource = purchaseOrder.PurchaseOrderDetails;
                    instanceReportSource.ReportDocument = salesOrderDetailsReport;

                    //PurchaseOrderHeaderReport salesOrderHeaderReport = new PurchaseOrderHeaderReport();
                    //salesOrderHeaderReport.DataSource = purchaseOrder;
                    //instanceReportSource.ReportDocument = salesOrderHeaderReport;
                   
                    RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);

                    string fileName = result.DocumentName + "." + result.Extension;

                    Response.Clear();
                    Response.ContentType = result.MimeType;
                    Response.Cache.SetCacheability(HttpCacheability.Private);
                    Response.Expires = -1;
                    Response.Buffer = true;

                    Response.AddHeader("Content-Disposition",
                                       string.Format("{0};FileName=\"{1}\"",
                                                     "attachment",
                                                     fileName));

                    Response.BinaryWrite(result.DocumentBytes);
                    Response.End();
                }
            }
            if (e.CommandName.Equals("SendMail"))
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                if (dataItem != null)
                {
                    (Master as SiteMaster).MasterRadNotification.Show("Sending Emails is temporary disabled. Will be enabled when the product is tested enough! ");
                    return;

                    //int purchaseOrderId = (int)dataItem.GetDataKeyValue(rgPurchaseOrders.MasterTableView.DataKeyNames[0]);
                    //PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);

                    //ReportProcessor reportProcessor = new ReportProcessor();

                    //var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
                    //RecipiesReports.PurchaseOrderDetailsReport salesOrderDetailsReport = new RecipiesReports.PurchaseOrderDetailsReport();
                    //salesOrderDetailsReport.DataSource = purchaseOrder.PurchaseOrderDetails;
                    //instanceReportSource.ReportDocument = salesOrderDetailsReport;

                    //RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);

                    //EmailTemplate defaultTemplate = ContextFactory.GetContextPerRequest().EmailTemplates.FirstOrDefault(et => et.IsDefault);
                    //if (defaultTemplate != null)
                    //{
                    //    RestResponse restResponse = EmailHelper.SendComplexMessage(defaultTemplate.From, purchaseOrder.Vendor.Email, defaultTemplate.Cc,
                    //        defaultTemplate.Bcc, defaultTemplate.Subject, defaultTemplate.TextBody, defaultTemplate.HtmlBody,
                    //        result.DocumentBytes, defaultTemplate.AttachmentName + "." + result.Extension);
                    //    if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    //    {
                    //        (Master as SiteMaster).MasterRadNotification.Show("An Email has been successfully sent to address " + purchaseOrder.Vendor.Email);
                    //    }
                    //    else
                    //    {
                    //        (Master as SiteMaster).MasterRadNotification.Show("Error sending Email! ResponseStatus: " + restResponse.ResponseStatus.ToString() + ", StatusCode: " + restResponse.StatusCode.ToString()  + 
                    //            ", Content: " + HttpUtility.JavaScriptStringEncode(restResponse.Content));
                    //    }
                    //}
                    //else
                    //{
                    //    (Master as SiteMaster).MasterRadNotification.Show("Error sending Email! There is no default email template. Please add email templates and configure one of them as a default!");
                    //}
                }
            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {

            }
        }

        protected void rgPurchaseOrderDetails_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                // set here default values to show 
                // We should get the default product price ot item changed of the products. Thats tough.
                if (VendorId.HasValue)
                {
                    Vendor vendor = ContextFactory.GetContextPerRequest().Vendors.FirstOrDefault(v => v.VendorId == VendorId);
                    if (vendor != null)
                    {
                        PurchaseOrderDetail newPod = new PurchaseOrderDetail() { UnitPrice = null };
                        e.Item.OwnerTableView.InsertItem(newPod);
                    }
                }
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {              
                 //this works fine
                PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
                if (purchaseOrder != null && purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
                {
                    (Master as SiteMaster).MasterRadWindowManager.RadAlert("You can not delete products from a purchase order with completed status!", 300, 200, "Can not delete!", "");
                    e.Canceled = true;
                }
            }
        }



        protected void OpenAccessLinqDataSourceProduct_Selected(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            List<Product> products = e.Result as List<Product>;
            products.Clear();
            List<Product> filteredProducts = ContextFactory.GetContextPerRequest().ProductVendors.
                Where(pv => pv.VendorId == VendorId && pv.Product != null).Select(pv => pv.Product).ToList();
            products.AddRange(filteredProducts);
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Selected(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {

        }
                
        protected void rgPurchaseOrderDetails_PreRender(object sender, EventArgs e)
        {
            if (PurchaseOrderId <= 0)
            {
                RadGrid rgPurchaseOrderDetails = (RadGrid)sender;
                rgPurchaseOrderDetails.Visible = false;
            }
        }

        protected void lblPurchaseOrderDetails_PreRender(object sender, EventArgs e)
        {
            if (PurchaseOrderId > 0)
            {
                Label lblPurchaseOrderDetails = (Label)sender;
                lblPurchaseOrderDetails.Visible = false;
            }
        }

        protected void rgPurchaseOrderDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editableItem = e.Item as GridEditableItem;

                var col = editableItem.FindControl("DropDownProductListColumn");
               
                // execute custom logic
            }
        }

        protected void rgPurchaseOrderDetails_CreateColumnEditor(object sender, GridCreateColumnEditorEventArgs e)
        {
            
        }

        protected void rgPurchaseOrderDetails_ItemCreated(object sender, GridItemEventArgs e)
        {             
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)  
            {
                GridEditableItem editedItem = (e.Item as GridEditableItem);
                RadComboBox dropDownProductListColumn = editedItem["DropDownProductListColumn"].Controls[0] as RadComboBox;  
  
                //attach SelectedIndexChanged event for the dropdown control  
                dropDownProductListColumn.AutoPostBack = true;
                dropDownProductListColumn.SelectedIndexChanged += dropDownProductListColumn_SelectedIndexChanged;

                dropDownProductListColumn.PreRender += dropDownProductListColumn_PreRender;

                if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                {
                    // insert item                   
                }
                else 
                {
                    RadComboBox dropDownUnitListColumn = editedItem["DropDownUnitListColumn"].Controls[0] as RadComboBox;
                    dropDownProductListColumn.Enabled = false;
                    dropDownUnitListColumn.Enabled = false;

                    // edit item
                }
            }  
        }

        void dropDownProductListColumn_PreRender(object sender, EventArgs e)
        {
            dropDownProductListColumn_SelectedIndexChanged(sender, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));
        }

       

        void dropDownProductListColumn_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //first reference the edited grid item through the NamingContainer                                                     attribute  
            GridEditableItem editedItem = (sender as RadComboBox).NamingContainer as GridEditableItem;

            RadNumericTextBox tbUnitPrice = editedItem["UnitPrice"].Controls[0] as RadNumericTextBox;
            RadComboBox dropDownProductListColumn = editedItem["DropDownProductListColumn"].Controls[0] as RadComboBox;
            RadComboBox dropDownUnitListColumn = editedItem["DropDownUnitListColumn"].Controls[0] as RadComboBox;
            string productId = dropDownProductListColumn.SelectedValue;
            tbUnitPrice.Text = "0";
            if (!string.IsNullOrEmpty(productId))
            {
                int intProductId;
                if (int.TryParse(productId, out intProductId))
                {
                    Product product = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == intProductId);
                    if (product != null)
                    {
                        //tbUnitPrice.Text = product.GetAveragePriceLastDays(14).ToString();
                        tbUnitPrice.Text = product.UnitPrice.GetValueOrDefault().ToString();

                        //dropDownUnitListColumn.DataSource = product.UnitMeasure.GetRelatedUnitMeasures();
                        PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);

                        if (purchaseOrder.Vendor != null)
                        {
                            ProductVendor productVendor = purchaseOrder.Vendor.ProductVendors.FirstOrDefault(pv => pv.ProductId == product.ProductId);
                            dropDownUnitListColumn.DataSource = new List<UnitMeasure>() { productVendor.UnitMeasure };
                            dropDownUnitListColumn.DataBind();
                        }

                    }
                }
            }
        }

        protected void OpenAccessLinqDataSourcePurchaseOrders_Updating(object sender, OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            PurchaseOrderHeader oldPurchaseOrderHeader = e.OriginalObject as PurchaseOrderHeader;
            PurchaseOrderHeader newPurchaseOrderHeader = e.NewObject as PurchaseOrderHeader;


            bool isValidStatusTransition = newPurchaseOrderHeader.UpdateProductsFromStatus(oldPurchaseOrderHeader.StatusId, newPurchaseOrderHeader.StatusId);
            if (!isValidStatusTransition)
            {
                e.Cancel = true;                    
                (Master as SiteMaster).MasterRadWindowManager.RadAlert(
                    "Invalid status transition! Valid status transitions are Pending -> Approved, Approved -> Rejected, Approved -> Completed and vice versa!", 600, 300, "Can not update!", "");
            }
        }

        protected void rgPurchaseOrders_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                {
                    // insert item

                    GridEditableItem editedItem = (e.Item as GridEditableItem);
                    RadComboBox dropDownStatusListColumn = editedItem["DropDownStatusListColumn"].Controls[0] as RadComboBox;
                    dropDownStatusListColumn.Enabled = false;
                }                
            }  
        }
                
        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_InsertedUpdatedDeleted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            // So SubTotal and Total value  will be updated
            //rgPurchaseOrders.Rebind();
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Inserted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {            
             // So SubTotal and Total value  will be updated
             rgPurchaseOrders.Rebind();
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Updated(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            // So SubTotal and Total value  will be updated
            rgPurchaseOrders.Rebind();
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Deleted(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            
            // So SubTotal and Total value  will be updated
            rgPurchaseOrders.Rebind();
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Inserting(object sender, OpenAccessLinqDataSourceInsertEventArgs e)
        {
            PurchaseOrderDetail newPurchaseOrderDetail = e.NewObject as PurchaseOrderDetail;
            if (newPurchaseOrderDetail != null)
            {
                newPurchaseOrderDetail.PurchaseOrderId = PurchaseOrderId;
            }
            PurchaseOrderDetail insertedPurchaseOrderDetail = e.NewObject as PurchaseOrderDetail;
            PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == insertedPurchaseOrderDetail.ProductId);
            UnitMeasure selectedUnitMeasure = ContextFactory.GetContextPerRequest().UnitMeasures.FirstOrDefault(um => um.UnitMeasureId == newPurchaseOrderDetail.UnitMeasureId);

            if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Approved)
            {
                selectedProduct.UnitsOnOrder += selectedProduct.GetBaseUnitMeasureQuantityForProduct(insertedPurchaseOrderDetail.OrderQuantity, selectedUnitMeasure);
            }
            else if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
            {
                selectedProduct.UnitsInStock += selectedProduct.GetBaseUnitMeasureQuantityForProduct(insertedPurchaseOrderDetail.StockedQuantity, selectedUnitMeasure);
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Updating(object sender, OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            PurchaseOrderDetail oldPurchaseOrderDetail = e.OriginalObject as PurchaseOrderDetail;
            PurchaseOrderDetail newPurchaseOrderDetail = e.NewObject as PurchaseOrderDetail;
            double differenceOrderQuantity = newPurchaseOrderDetail.OrderQuantity.GetValueOrDefault() - oldPurchaseOrderDetail.OrderQuantity.GetValueOrDefault();
            double differenceStockedQuantity = newPurchaseOrderDetail.StockedQuantity - oldPurchaseOrderDetail.StockedQuantity;

            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == newPurchaseOrderDetail.ProductId);
            UnitMeasure selectedUnitMeasure = ContextFactory.GetContextPerRequest().UnitMeasures.FirstOrDefault(um => um.UnitMeasureId == newPurchaseOrderDetail.UnitMeasureId);


            PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
            if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Approved)
            {
                selectedProduct.UnitsOnOrder += selectedProduct.GetBaseUnitMeasureQuantityForProduct(differenceOrderQuantity, selectedUnitMeasure);
            }
            else if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
            {
                selectedProduct.UnitsInStock += selectedProduct.GetBaseUnitMeasureQuantityForProduct(differenceStockedQuantity, selectedUnitMeasure);
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Deleting(object sender, OpenAccessLinqDataSourceDeleteEventArgs e)
        {
            PurchaseOrderDetail deletedPurchaseOrderDetail = e.OriginalObject as PurchaseOrderDetail;
            PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == PurchaseOrderId);
            Product selectedProduct = ContextFactory.GetContextPerRequest().Products.FirstOrDefault(p => p.ProductId == deletedPurchaseOrderDetail.ProductId);
            UnitMeasure selectedUnitMeasure = ContextFactory.GetContextPerRequest().UnitMeasures.FirstOrDefault(um => um.UnitMeasureId == deletedPurchaseOrderDetail.UnitMeasureId);

            if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Approved)
            {
                selectedProduct.UnitsOnOrder -= selectedProduct.GetBaseUnitMeasureQuantityForProduct(deletedPurchaseOrderDetail.OrderQuantity, selectedUnitMeasure);
            }
            else if (purchaseOrder.StatusId == (int)PurchaseOrderStatusEnum.Completed)
            {
                selectedProduct.UnitsInStock -= selectedProduct.GetBaseUnitMeasureQuantityForProduct(deletedPurchaseOrderDetail.StockedQuantity, selectedUnitMeasure);
            }
            ContextFactory.GetContextPerRequest().SaveChanges();
        }
  
    }
}