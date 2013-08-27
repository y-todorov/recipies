using DynamicApplicationModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace RecipiesWebFormApp.Purchasing
{
    public partial class Po2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //rgPurchaseOrders.DataSource = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders;
                //rgPurchaseOrders.DataBind();
            }
        }

        protected void rgPurchaseOrders_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //rgPurchaseOrders.DataSource = ContextFactory.GetContextPerRequest().PurchaseOrderHeaders;
        }

        protected void rgPurchaseOrders_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Dictionary<object, object> dic = new Dictionary<object,object>();
                if (editedItem != null)
                {
                    Hashtable newValues = new Hashtable();
                    //The GridTableView will fill the values from all editable columns in the hash
                    e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);
                }
            }
        }
    }
}