using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace RecipiesWebFormApp.Sales
{
    public partial class Orders : System.Web.UI.Page
    {
        public int SalesOrderId
        {
            get
            {
                int salesOrderId = (int)ViewState["SalesOrderId"];
                return salesOrderId;
            }
            set
            {
                ViewState["SalesOrderId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblSalesOrderDetails_PreRender(object sender, EventArgs e)
        {
            if (SalesOrderId > 0)
            {
                Label lblSalesOrderDetails = (Label)sender;
                lblSalesOrderDetails.Visible = false;
            }
        }

        protected void rgSalesOrderDetails_PreRender(object sender, EventArgs e)
        {
            if (SalesOrderId <= 0)
            {
                RadGrid rgSalesOrderDetails = (RadGrid)sender;
                rgSalesOrderDetails.Visible = false;
            }
        }       

        protected void OpenAccessLinqDataSourceOrderDetail_Inserting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceInsertEventArgs e)
        {
            SalesOrderDetail newSalesOrderDetail = e.NewObject as SalesOrderDetail;
            if (newSalesOrderDetail != null)
            {
                newSalesOrderDetail.SalesOrderHeaderId = SalesOrderId;
            }
        }

        protected void OpenAccessLinqDataSourceOrderDetail_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
        {
            if (e.WhereParameters.ContainsKey("SalesOrderId"))
            {
                e.WhereParameters["SalesOrderId"] = SalesOrderId;
            }
        }

        protected void rgSalesOrderHeaders_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                GridEditableItem editableItem = e.Item as GridEditableItem;
                if (editableItem != null)
                {
                    int salesOrderId = (int)editableItem.GetDataKeyValue(rgSalesOrderHeaders.MasterTableView.DataKeyNames[0]);
                    SalesOrderHeader salesOrderHeader = ContextFactory.GetContextPerRequest().SalesOrderHeaders.FirstOrDefault(s => s.SalesOrderHeaderId == salesOrderId);
                    SalesOrderId = salesOrderHeader.SalesOrderHeaderId;                  
                }
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                SalesOrderId = 0;
            }
        }

        protected void OpenAccessLinqDataSourceOrder_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            SalesOrderHeader oldPurchaseOrderHeader = e.OriginalObject as SalesOrderHeader;
            SalesOrderHeader newPurchaseOrderHeader = e.NewObject as SalesOrderHeader;

            newPurchaseOrderHeader.UpdateProductsFromStatus(oldPurchaseOrderHeader.StatusId, newPurchaseOrderHeader.StatusId);
        }
    }
}