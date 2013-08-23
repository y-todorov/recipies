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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
      
        protected void rgPurchaseOrders_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            e.Item.Selected = true;
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
                    ViewState.Add("VendorId", purchaseOrder.VendorId);
                    PurchaseOrderId = purchaseOrderId;
                }
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                PurchaseOrderId = 0;
            }
        }

        protected void rgPurchaseOrderDetails_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                // set here default values to show 
                //PurchaseOrderDetail newPod = new PurchaseOrderDetail() { PurchaseOrderId = (int)ViewState["PurchaseOrderId"] };
                //e.Item.OwnerTableView.InsertItem(newPod);
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

        protected void OpenAccessLinqDataSourceProduct_Selected(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {
            List<Product> products = e.Result as List<Product>;
            products.Clear();
            List<Product> filteredProducts = ContextFactory.GetContextPerRequest().ProductVendors.
                Where(pv => pv.VendorId == (int)ViewState["VendorId"]).Select(pv => pv.Product).ToList();
            products.AddRange(filteredProducts);            
        }

        protected void OpenAccessLinqDataSourcePurchaseOrderDetails_Selected(object sender, OpenAccessLinqDataSourceStatusEventArgs e)
        {

        }
       
    }
}