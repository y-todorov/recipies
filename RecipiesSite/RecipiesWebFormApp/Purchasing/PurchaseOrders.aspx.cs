using DynamicApplicationModel;
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
                    SalesOrderDetails salesOrderDetailsReport = new RecipiesReports.SalesOrderDetails();
                    salesOrderDetailsReport.DataSource = purchaseOrder.PurchaseOrderDetails;
                    instanceReportSource.ReportDocument = salesOrderDetailsReport;
                   
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
                    int purchaseOrderId = (int)dataItem.GetDataKeyValue(rgPurchaseOrders.MasterTableView.DataKeyNames[0]);
                    PurchaseOrderHeader purchaseOrder = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);

                    ReportProcessor reportProcessor = new ReportProcessor();

                    var instanceReportSource = new Telerik.Reporting.InstanceReportSource();
                    SalesOrderDetails salesOrderDetailsReport = new RecipiesReports.SalesOrderDetails();
                    salesOrderDetailsReport.DataSource = purchaseOrder.PurchaseOrderDetails;
                    instanceReportSource.ReportDocument = salesOrderDetailsReport;

                    RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);
                    EmailHelper.SendComplexMessage(result.DocumentBytes);

                }


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
        }



        protected void OpenAccessLinqDataSourceProduct_Selected(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            List<Product> products = e.Result as List<Product>;
            products.Clear();
            List<Product> filteredProducts = ContextFactory.GetContextPerRequest().ProductVendors.
                Where(pv => pv.VendorId == VendorId).Select(pv => pv.Product).ToList();
            products.AddRange(filteredProducts);
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Selected(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {

        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Inserting(object sender, OpenAccessLinqDataSourceInsertEventArgs e)
        {
            PurchaseOrderDetail newPurchaseOrderDetail = e.NewObject as PurchaseOrderDetail;
            if (newPurchaseOrderDetail != null)
            {
                newPurchaseOrderDetail.PurchaseOrderId = PurchaseOrderId;
            }
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
            }  
        }

        void dropDownProductListColumn_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //first reference the edited grid item through the NamingContainer                                                     attribute  
            GridEditableItem editedItem = (sender as RadComboBox).NamingContainer as GridEditableItem;

            RadNumericTextBox tbUnitPrice = editedItem["UnitPrice"].Controls[0] as RadNumericTextBox;
            RadComboBox dropDownProductListColumn = editedItem["DropDownProductListColumn"].Controls[0] as RadComboBox;
            string productId = dropDownProductListColumn.SelectedValue;
            if (!string.IsNullOrEmpty(productId))
            {
                int intProductId;
                if (int.TryParse(productId, out intProductId))
                {
                    ProductVendor productVendor = ContextFactory.GetContextPerRequest().ProductVendors.FirstOrDefault(pv => pv.ProductId == intProductId && pv.VendorId == VendorId);

                    tbUnitPrice.Text = productVendor.StandardPrice.ToString();
                }
            }
            else
            {
                tbUnitPrice.Text = "0"; // default price if nothing is selected
            }
        }
  
    }
}