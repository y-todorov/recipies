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
                    instanceReportSource.ReportDocument = new RecipiesReports.Report2() { };
                   
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

    }
}